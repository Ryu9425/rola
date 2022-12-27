using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using System.Diagnostics;
using ActuatorInspectionCommon;
using ComInterface;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace test1
{
    public partial class UsbController
    {
        public int command_no = 0;
        const string conn_format_code = "ff";
        const string conn_data_length = "0014";
        const string child_sensor_count = "0004";
        const string conn_request_code = "05";
        const string conn_data_stx = "02";
        const string conn_data_etx = "030d0a";

        const string sensor_data_count = "18";
        const string sensor_latest_count = "01";
        const string sensor_total_data = "ffff";
        const string sensor_total_length = "0005";
        const string sensor_command = "50";


        public string physics_address;

        public UsbController()
        {

            // ComPortOpen();
            //  TransIotInit();
            // NopCommand();
            //SensorDatas();

        }

        private const string _logPrefix = "IoTTrans";

        private CommBase _IoTIF = null;

        private string _ComPort_Iot = "COM6";
        private int _BaudRate_Iot = 9600;
        private int _DataBits_IoT = 8;
        private System.IO.Ports.Parity _Parity_IoT = System.IO.Ports.Parity.None;
        private System.IO.Ports.StopBits _StopBits_IoT = System.IO.Ports.StopBits.One;


        public bool ComPortOpen()
        {
            _IoTIF = new CommCdcDirect();
            ((CommCdcDirect)_IoTIF).PortName = "IoTデバイス";
            ((CommCdcDirect)_IoTIF).ComPort = _ComPort_Iot;
            ((CommCdcDirect)_IoTIF).BaudRate = _BaudRate_Iot;
            ((CommCdcDirect)_IoTIF).DataBits = _DataBits_IoT;
            ((CommCdcDirect)_IoTIF).ParityType = _Parity_IoT;
            ((CommCdcDirect)_IoTIF).StopBitType = _StopBits_IoT;

            try
            {
                // IoTデバイスオープン    device open
                _IoTIF.Open();
                return true;
            }
            catch (System.Exception)
            {
                throw;
                return false;
            }
        }

        public string SensorConnection()
        {
            string date_time_hex = DateToHexConcvert();

            command_no++;
            string hexid = $"{command_no:X2}";

            physics_address = this.GetPhysicalAddresses()[0].ToString();
            physics_address = physics_address.Substring(0, 8);

            string crc_cal_part = physics_address + hexid + conn_format_code + conn_data_length +
                  conn_request_code + child_sensor_count + date_time_hex;

            byte[] crc_body = StringToByteArray(crc_cal_part);
            ushort crc_part = Crc16Ccitt(crc_body);

            string crc_part_hex = crc_part.ToString("X4");

            string base64PreStr = crc_cal_part + crc_part_hex;

            string stringToConvert = "44032c7a01ff001405000132303232313231383134313932373030304741";

            byte[] convertedByte = StringToByteArray(base64PreStr);
            string hex = System.Convert.ToBase64String(convertedByte);
            string cmdBuf = Base64ToHexadecimal(hex);

            string total_result = conn_data_stx + cmdBuf + conn_data_etx;

            byte[] total_bites = StringToByteArray(total_result);

            _IoTIF.WriteBytes(total_bites);

            const int HT03_COM_TIMEOUT = 1000;

            var sw = new Stopwatch();
            sw.Restart();

            string recvStr = string.Empty;
            do
            {
                if (sw.ElapsedMilliseconds > HT03_COM_TIMEOUT)
                {
                    sw.Stop();
                    throw new ApplicationException("connect: データ送信の応答待ちでタイムアウト");
                }
                System.Threading.Thread.Sleep(1);
                var recvBuf = _IoTIF.ReadBytes();
                recvStr += Encoding.ASCII.GetString(recvBuf);
            } while (!recvStr.EndsWith("\n"));


            recvStr = recvStr.Replace("\u0002", "")
                .Replace("\u0003", "")
                .Replace("\u000D", "");
            //  MessageBox.Show("output data: "+ recvStr);


            string[] second_strs = recvStr.Split("\u000A", StringSplitOptions.RemoveEmptyEntries);
            string first_recvStr = second_strs[0];

            //   MessageBox.Show("trim connect data: " + recvStr + " :  first :" + first_recvStr);

            string test = "";

            foreach (var t in first_recvStr.Trim())
            {
                test += t + ">";

            }
            //      MessageBox.Show("first_recvStr  " + test);
            //      MessageBox.Show("connection status : " + first_recvStr);
            return this.GetStartEndDate(first_recvStr);
        }

        public bool GetChildSensorData(int _childId)
        {
            string result = "";
            command_no++;
            // command_no = 2;
            string hexid = $"{command_no:X2}";

            int child_id_no = _childId;

            string child_id = $"{child_id_no:X2}";

            // physics_address = "44032c7a";
            physics_address = this.GetPhysicalAddresses()[0].ToString();
            physics_address = physics_address.Substring(0, 8);

            string crc_cal_part = physics_address + hexid + conn_format_code + sensor_total_length
                               + sensor_command + child_id + sensor_total_data + sensor_latest_count;

            // MessageBox.Show("crc_cal: " + crc_cal_part);
            //crc_cal_part = "44032c7a01ff00140500013230323231323138313431393237303030";

            byte[] crc_body = StringToByteArray(crc_cal_part);
            ushort crc_part = Crc16Ccitt(crc_body);

            string crc_part_hex = crc_part.ToString("X4");
            // MessageBox.Show("crc_check: " + crc_part_hex);

            string base64PreStr = crc_cal_part + crc_part_hex;

            //  string stringToConvert = "44032c7a01ff001405000132303232313231383134313932373030304741";
            // MessageBox.Show("base64PreStr: " + base64PreStr);

            byte[] convertedByte = StringToByteArray(base64PreStr);
            string hex = System.Convert.ToBase64String(convertedByte);
            string cmdBuf = Base64ToHexadecimal(hex);
            // MessageBox.Show("cmdBuf"+ cmdBuf);

            string total_result = conn_data_stx + cmdBuf + conn_data_etx;

            // MessageBox.Show(total_result);

            byte[] total_bites = StringToByteArray(total_result);

            _IoTIF.WriteBytes(total_bites);


            const int HT03_COM_TIMEOUT = 6000;

            var sw = new Stopwatch();
            //sw.Restart();

            string recvStr = string.Empty;

            do
            {
                byte read_byte = _IoTIF.ReadByte(out bool _isRecv);
                if (_isRecv == false)
                {
                    if (!sw.IsRunning) sw.Restart();
                    // continue;
                }
                else
                {
                    sw.Reset();
                    System.Threading.Thread.Sleep(1);
                    var recvBuf = _IoTIF.ReadBytes();
                    recvStr += Encoding.ASCII.GetString(recvBuf);
                }
            } while (sw.ElapsedMilliseconds < HT03_COM_TIMEOUT);

            recvStr = recvStr.Replace("\u0002", "")
              .Replace("\u000A", "")
              .Replace("\u000D", "");

            //  MessageBox.Show("received sensor data no " + _childId.ToString());

            string[] second_strs = recvStr.Split("\u0003", StringSplitOptions.RemoveEmptyEntries);
            //  MessageBox.Show("received sensor data count  : " + second_strs.Length.ToString() );
            string second_str = "";
            if (second_strs.Length > 1)
            {
                second_str = second_strs[1];
                //  MessageBox.Show("received sensor data  : " + second_strs.Length.ToString() + " second: " + second_str);
            }
            else
            {
                second_str = second_strs[0];
                //  MessageBox.Show("Please try it after a minute: "+ recvStr);
                return false;
            }


            string newest_id = this.GetNewestId(second_str, _childId);
            //  MessageBox.Show("new_id: " + newest_id);
            if (newest_id != "error")
                this.NewestSensorData(newest_id, _childId);
            else
                return false;

            return true;
        }

        public string GetNewestId(string latest_recvStr, int _childId)
        {
            //  MessageBox.Show("GetNewestId: " + latest_recvStr);
            string test = "";

            //   latest_recvStr = latest_recvStr.Substring(1, latest_recvStr.Length - 1);


            foreach (var t in latest_recvStr.Trim())
            {
                test += t + ">";

            }

            //   MessageBox.Show("newstid test: " + test);

            // latest_recvStr = "lcDv7BoCABuhuS7wAgAAAG4AiQAAAAAAAAAAD/9HDywhAABbtg==";
            byte[] bytes = Convert.FromBase64String(latest_recvStr);
            string hex = BitConverter.ToString(bytes);
            string newest_hex_str = bytes[17].ToString("x2") + bytes[18].ToString("x2");
            int diff = int.Parse(newest_hex_str, System.Globalization.NumberStyles.HexNumber) - Convert.ToInt32((byte)0x0018);

            //int diff = int.Parse("0089", System.Globalization.NumberStyles.HexNumber) - Convert.ToInt32((byte)0x0018);
            string diff_str = diff < 0 ? "0000" : (diff + 1).ToString("x4");

            //   MessageBox.Show("latest: " + hex + " new: " + newest_hex_str + " test  " + diff_str);
            return diff_str;
        }
        public bool NopCommand()
        {
            command_no++;
            string hexid = $"{command_no:X2}";

            physics_address = this.GetPhysicalAddresses()[0].ToString();
            physics_address = physics_address.Substring(0, 8);
            const string nop_data_length = "0001";
            const string nop_code = "00";


            string crc_cal_part = physics_address + hexid + conn_format_code + nop_data_length +
                nop_code;
            //MessageBox.Show("crc_cal: " + crc_cal_part);
            //crc_cal_part = "44032c7a01ff00140500013230323231323138313431393237303030";

            byte[] crc_body = StringToByteArray(crc_cal_part);
            ushort crc_part = Crc16Ccitt(crc_body);

            string crc_part_hex = crc_part.ToString("X4");
            // MessageBox.Show("crc_check: " + crc_part_hex);

            string base64PreStr = crc_cal_part + crc_part_hex;

            byte[] convertedByte = StringToByteArray(base64PreStr);
            string hex = System.Convert.ToBase64String(convertedByte);
            string cmdBuf = Base64ToHexadecimal(hex);

            string total_result = conn_data_stx + cmdBuf + conn_data_etx;

            //  MessageBox.Show(total_result);

            byte[] total_bites = StringToByteArray(total_result);

            _IoTIF.WriteBytes(total_bites);

            const int HT03_COM_TIMEOUT = 1000;

            var sw = new Stopwatch();
            sw.Restart();

            string recvStr = string.Empty;
            do
            {
                if (sw.ElapsedMilliseconds > HT03_COM_TIMEOUT)
                {
                    sw.Stop();
                    throw new ApplicationException("connect: データ送信の応答待ちでタイムアウト");
                    return false;
                }
                System.Threading.Thread.Sleep(1);
                var recvBuf = _IoTIF.ReadBytes();
                recvStr += Encoding.ASCII.GetString(recvBuf);
            } while (!recvStr.EndsWith("\n"));

            // string test = "";

            // foreach (var t in recvStr.Trim())
            // {
            //     test+=t+"=";

            // }
            // MessageBox.Show("test  "+test);
            recvStr = recvStr.Substring(0, recvStr.Length - 3);
            //   MessageBox.Show("nop status : " + recvStr);
            return true;
        }
        public string GetStartEndDate(string latest_recvStr)
        {
            //latest_recvStr = "lcDv7AL/AAoGBS2Ve+ctlXyPe5E=";            
            try
            {
                byte[] bytes = Convert.FromBase64String(latest_recvStr);
                string hex = BitConverter.ToString(bytes);
                string start_date_hex_str = bytes[10].ToString("x2") + bytes[11].ToString("x2");
                string start_time_hex_str = bytes[12].ToString("x2") + bytes[13].ToString("x2");

                string end_date_hex_str = bytes[14].ToString("x2") + bytes[15].ToString("x2");
                string end_time_hex_str = bytes[16].ToString("x2") + bytes[17].ToString("x2");

                string start_date = this.GetDate(start_date_hex_str) + " " + this.GetTime(start_time_hex_str);
                string end_date = this.GetDate(end_date_hex_str) + " " + this.GetTime(end_time_hex_str);

                //    MessageBox.Show(" Sensor Data Receive time:: start: " + start_date + " end: " + end_date);
                return start_date + ";" + end_date;
            }
            catch (Exception ex)
            {
                //    MessageBox.Show("Please reset and try it!");
                return "date_error";
            }
        }


        public void NewestSensorData(string newest_id, int child_id_no)
        {
            string result_infos = "";
            command_no++;
            // command_no = 2;
            string hexid = $"{command_no:X2}";

            //int child_id_no = 2;

            string child_id = $"{child_id_no:X2}";

            // physics_address = "44032c7a";

            string crc_cal_part = physics_address + hexid + conn_format_code + sensor_total_length
                               + sensor_command + child_id + newest_id + sensor_data_count;

            // MessageBox.Show("newest crc_cal: " + crc_cal_part);

            byte[] crc_body = StringToByteArray(crc_cal_part);
            ushort crc_part = Crc16Ccitt(crc_body);

            string crc_part_hex = crc_part.ToString("X4");
            //  MessageBox.Show("newsest crc_check: " + crc_part_hex);

            string base64PreStr = crc_cal_part + crc_part_hex;

            byte[] convertedByte = StringToByteArray(base64PreStr);
            string hex = System.Convert.ToBase64String(convertedByte);
            string cmdBuf = Base64ToHexadecimal(hex);

            string total_result = conn_data_stx + cmdBuf + conn_data_etx;

            //   MessageBox.Show("total_result ---->  " +  total_result);

            byte[] total_bites = StringToByteArray(total_result);

            _IoTIF.WriteBytes(total_bites);

            const int HT03_COM_TIMEOUT = 32000;

            var sw = new Stopwatch();
            sw.Restart();

            //   MessageBox.Show("sensor result data start");

            string recvStr = string.Empty;
            do
            {
                // if (sw.ElapsedMilliseconds > HT03_COM_TIMEOUT)
                // {
                //     sw.Stop();
                //     throw new ApplicationException("sensor: データ送信の応答待ちでタイムアウト");
                // }
                // MessageBox.Show("doing");
                System.Threading.Thread.Sleep(1);
                var recvBuf = _IoTIF.ReadBytes();
                recvStr += Encoding.ASCII.GetString(recvBuf);
            } while (sw.ElapsedMilliseconds < 32000);

            //     MessageBox.Show("sensor result data: 1 " + recvStr);

            recvStr = recvStr.Replace("\u0002", "")
              .Replace("\u000A", "")
              .Replace("\u000D", "");

            string[] second_strs = recvStr.Split("\u0003", StringSplitOptions.RemoveEmptyEntries);
            //     MessageBox.Show("sensor result data: " + recvStr);

            for (int i = 1; i < second_strs.Length; i++)
            {
                string data_str = second_strs[i];

                //        MessageBox.Show("data: i=> " + i.ToString() + " :  data :" + data_str);
                if (data_str.Length > 39)
                {

                    result_infos += "data: child_id=> " + child_id + " :  data :" + data_str + "\r\n";
                    this.GettingDetailData(data_str);
                };
            }
            
            MessageBox.Show(result_infos);
        }

        public void GettingDetailData(string receive_sensor_data)
        {
            // receive_sensor_data = "1cDv7AMCABuhuS7wBQECAAEAAQN6FPoexz+uNX1HGSwhAUIPHg==";
            /*1cDv7AMCABuhuS7wBQECAAEAAQN6FPoexz+uNX1HGSwhAUIPHg==*/

            byte[] bytes = Convert.FromBase64String(receive_sensor_data);

            int seq = Convert.ToInt32(bytes[12]);
            int str_index = Convert.ToInt32(bytes[13]);
            int num = Convert.ToInt32(bytes[14]);
            string _uuid = bytes[8].ToString("x2") + bytes[9].ToString("x2") + bytes[10].ToString("x2") + bytes[11].ToString("x2");
            int data_id = int.Parse(bytes[15].ToString("x2") + bytes[16].ToString("x2"), System.Globalization.NumberStyles.HexNumber);
            int newest_id = int.Parse(bytes[17].ToString("x2") + bytes[18].ToString("x2"), System.Globalization.NumberStyles.HexNumber);
            int gradient_int = int.Parse(bytes[19].ToString("x2") + bytes[20].ToString("x2"), System.Globalization.NumberStyles.HexNumber);
            int temperature_int = int.Parse(bytes[21].ToString("x2") + bytes[22].ToString("x2"), System.Globalization.NumberStyles.HexNumber);
            int humidity_int = int.Parse(bytes[23].ToString("x2") + bytes[24].ToString("x2"), System.Globalization.NumberStyles.HexNumber);
            int pressure_int = int.Parse(bytes[25].ToString("x2") + bytes[26].ToString("x2"), System.Globalization.NumberStyles.HexNumber);
            int voltage_int = int.Parse(bytes[27].ToString("x2") + bytes[28].ToString("x2"), System.Globalization.NumberStyles.HexNumber);

            string csq = bytes[29].ToString("x2") + bytes[30].ToString("x2");
            string date_hex = bytes[31].ToString("x2") + bytes[32].ToString("x2");
            string time_hex = bytes[33].ToString("x2") + bytes[34].ToString("x2");


            string temperature = string.Format("{0:N2}", temperature_int / 256.0f);
            string humidity = string.Format("{0:N2}", humidity_int / 256.0f);
            string voltage = string.Format("{0:N3}", voltage_int * 3.3f / 32767);
            string pressure = string.Format("{0:N2}", pressure_int / 16.0f);
            string gradient = string.Format("{0:N1}", gradient_int / 256.0f);
            string sensor_time = this.GetDate(date_hex) + " " + this.GetTime(time_hex);
            string date_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            this.InsertSensorData(_uuid, data_id.ToString(), temperature, humidity, voltage, pressure, gradient, sensor_time, date_time);
        }

        public void InsertSensorData(string _uuid, string data_id, string temperature, string humidity, string voltage,
                string pressure, string gradient, string sensor_time, string date_time)
        {
            // MessageBox.Show(_uuid + ":" + data_id + ":" + temperature + ":" + humidity + ":" + voltage + ":"
            //                + pressure + ":" + gradient + ":" + sensor_time + ":" + date_time);


            try
            {
                var cmd = Program.m_dbConnection.CreateCommand();
                string check_sql = "SELECT COUNT('id') FROM display WHERE uuid='" + _uuid + "' and sensor_time='" + sensor_time
                                + "' and voltage='" + voltage + "' and temperature='" + temperature + "' and humidity='" + humidity + "'";
                cmd.CommandText = check_sql;

                var exist_status_reader = cmd.ExecuteReader();
                while (exist_status_reader.Read())
                {
                    int myreader = exist_status_reader.GetInt32(0);
                    if (myreader == 0)
                    {
                        var insert_display_cmd = Program.m_dbConnection.CreateCommand();
                        string inser_sensor_sql = "INSERT INTO display('temperature','humidity','voltage','pressure','gradient','uuid','sensor_time','datetime') VALUES('"
                            + temperature + "', '" + humidity + "','" + voltage + "','" + pressure + "','" + gradient + "','" + _uuid + "','" + sensor_time + "','" + date_time + "')";

                        insert_display_cmd.CommandText = inser_sensor_sql;
                        insert_display_cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {

            }

        }


        /// <summary>
        /// IoTデバイス初期化コマンド送信
        /// </summary>
        public bool TransIotInit()
        {
            string cmdStr;
            _IoTIF.WriteStr("echo off\r\n\x1b[?01h\r\n");
            // _IoTIF.WriteStr("\x1b[?00H\r\n");

            // 受信バッファをクリア
            const int HT03_COM_TIMEOUT = 1000;

            var sw = new Stopwatch();
            sw.Restart();

            string recvStr = string.Empty;
            do
            {
                if (sw.ElapsedMilliseconds > HT03_COM_TIMEOUT)
                {
                    sw.Stop();
                    throw new ApplicationException("データ送信の応答待ちでタイムアウト");
                    return false;
                }
                System.Threading.Thread.Sleep(1);
                var recvBuf = _IoTIF.ReadBytes();
                recvStr += Encoding.ASCII.GetString(recvBuf);
            } while (!recvStr.EndsWith("\n"));

            //   MessageBox.Show("start init setting status : " + recvStr);
            // _IoTIF.WriteStr("\x1b[?00H\r\n");
            return true;
        }

        public byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public ushort Crc16Ccitt(byte[] bytes)
        {
            const ushort poly = 4129;
            ushort[] table = new ushort[256];
            ushort initialValue = 0xffff;
            ushort temp, a;
            ushort crc = initialValue;
            for (int i = 0; i < table.Length; ++i)
            {
                temp = 0;
                a = (ushort)(i << 8);
                for (int j = 0; j < 8; ++j)
                {
                    if (((temp ^ a) & 0x8000) != 0)
                        temp = (ushort)((temp << 1) ^ poly);
                    else
                        temp <<= 1;
                    a <<= 1;
                }
                table[i] = temp;
            }
            for (int i = 0; i < bytes.Length; ++i)
            {
                crc = (ushort)((crc << 8) ^ table[((crc >> 8) ^ (0xff & bytes[i]))]);
            }
            return crc;
        }

        public string DateToHexConcvert()
        {
            string datetimeconvert = "";
            string date_time = DateTime.Now.ToString("yyyyMMddHHmmss000");
            //  MessageBox.Show(date_time);

            char[] values = date_time.ToCharArray();
            //  List<string> hexDateTimeOutputList = new List<string>();
            foreach (char letter in values)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(letter);
                // Convert the decimal value to a hexadecimal value in string form.
                string hexOutput = String.Format("{0:X}", value);
                //hexDateTimeOutputList.Add(hexOutput);
                datetimeconvert += hexOutput;
            }

            // MessageBox.Show(datetimeconvert);
            return datetimeconvert;
        }

        public string Base64ToHexadecimal(string _base64str)
        {

            string result = "";


            char[] values = _base64str.ToCharArray();
            foreach (char letter in values)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(letter);
                // Convert the decimal value to a hexadecimal value in string form.
                string hexOutput = String.Format("{0:X}", value);
                //hexDateTimeOutputList.Add(hexOutput);
                result += hexOutput;
            }

            //    MessageBox.Show(result);
            return result;
        }


        List<PhysicalAddress> GetPhysicalAddresses()
        {
            var macAddresses = new List<PhysicalAddress>();
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var adapter in interfaces)
            {
                if (adapter.OperationalStatus == OperationalStatus.Up)
                {
                    //Console.WriteLine(adapter.Description);
                    //Console.WriteLine(adapter.GetPhysicalAddress().ToString());                   
                    macAddresses.Add(adapter.GetPhysicalAddress());
                }
            }
            return macAddresses;
        }



        private const byte PC_COMMAND_HT03_DATA = (byte)0x01;


        public string GetDate(string _hex)
        {
            string result = "";
            int _decimal = int.Parse(_hex, System.Globalization.NumberStyles.HexNumber);
            var value = Convert.ToString(_decimal, 2).PadLeft(15, '0');
            var year_binary = value.Substring(0, 6);
            int _year = Convert.ToInt32(year_binary, 2);
            string year_str = _year > 9 ? "20" + _year.ToString() : "200" + _year.ToString();
            var month_binary = value.Substring(6, 4);
            int _month = Convert.ToInt32(month_binary, 2);
            string month_str = _month > 9 ? _month.ToString() : "0" + _month.ToString();
            var day_binary = value.Substring(10, 5);
            int _day = Convert.ToInt32(day_binary, 2);
            string day_str = _day > 9 ? _day.ToString() : "0" + _day.ToString();

            result = year_str + "-" + month_str + "-" + day_str;

            return result;
        }

        public string GetTime(string _hex)
        {
            string result = "";
            int _decimal = int.Parse(_hex, System.Globalization.NumberStyles.HexNumber);
            var value = Convert.ToString(_decimal, 2).PadLeft(16, '0');
            var hour_binary = value.Substring(0, 5);
            int _hour = Convert.ToInt32(hour_binary, 2);
            string hour_str = _hour > 9 ? _hour.ToString() : "0" + _hour.ToString();
            var minute_binary = value.Substring(5, 6);
            int _minute = Convert.ToInt32(minute_binary, 2);
            string minute_str = _minute > 9 ? _minute.ToString() : "0" + _minute.ToString();
            var second_binary = value.Substring(11, 5);
            int _second = 2 * Convert.ToInt32(second_binary, 2);
            string _second_str = _second > 9 ? _second.ToString() : "0" + _second.ToString();

            result = hour_str + ":" + minute_str + ":" + _second_str;

            return result;
        }

        public bool AllSenorData()
        {
            //     MessageBox.Show("AllSenorData");
            bool is_non_data = false;
            for (int i = 0; i < 4; i++)
            {
                is_non_data = GetChildSensorData(i + 1);
            }

            return is_non_data;
        }
    }
}
