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

namespace test1
{
    public partial class UsbTestForm : Form
    {
        public int command_no = 0;
        const string conn_format_code = "ff";
        const string conn_data_length = "0014";
        const string child_sensor_count = "0004";
        const string conn_request_code = "05";
        const string conn_data_stx = "02";
        const string conn_data_etx = "030d0a";

        const string sensor_data_count = "18";
        const string sensor_total_data = "00ff";
        const string sensor_total_length = "0005";
        const string sensor_command = "50";


        public string physics_address;

        public UsbTestForm()
        {
            InitializeComponent();
            // ComPortOpen();
            // TransIotInit();
            //SensorDatas();
        }

        private const string _logPrefix = "IoTTrans";

        private CommBase _IoTIF = null;

        private string _ComPort_Iot = "COM6";
        private int _BaudRate_Iot = 9600;
        private int _DataBits_IoT = 8;
        private System.IO.Ports.Parity _Parity_IoT = System.IO.Ports.Parity.None;
        private System.IO.Ports.StopBits _StopBits_IoT = System.IO.Ports.StopBits.One;


        private void ComPortOpen()
        {
            MessageBox.Show("test");
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
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private void OpenBtn_Click(object sender, EventArgs e)
        {
            string date_time_hex = DateToHexConcvert();

            command_no++;
            string hexid = $"{command_no:X2}";

            physics_address = this.GetPhysicalAddresses()[0].ToString();
            physics_address = physics_address.Substring(0, 8);

            string crc_cal_part = physics_address + hexid + conn_format_code + conn_data_length +
                conn_request_code + child_sensor_count + date_time_hex;
            MessageBox.Show("crc_cal: " + crc_cal_part);
            //crc_cal_part = "44032c7a01ff00140500013230323231323138313431393237303030";

            byte[] crc_body = StringToByteArray(crc_cal_part);
            ushort crc_part = Crc16Ccitt(crc_body);

            string crc_part_hex = crc_part.ToString("X2");
            MessageBox.Show("crc_check: " + crc_part_hex);

            string base64PreStr = crc_cal_part + crc_part_hex;

            string stringToConvert = "44032c7a01ff001405000132303232313231383134313932373030304741";

            byte[] convertedByte = StringToByteArray(base64PreStr);
            string hex = System.Convert.ToBase64String(convertedByte);
            string cmdBuf = Base64ToHexadecimal(hex);

            string total_result = conn_data_stx + cmdBuf + conn_data_etx;

            MessageBox.Show(total_result);

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

            // string test = "";

            // foreach (var t in recvStr.Trim())
            // {
            //     test+=t+"=";

            // }
            // MessageBox.Show("test  "+test);
            recvStr = recvStr.Substring(0, recvStr.Length - 3);
            MessageBox.Show("connection status : " + recvStr);
        }

        private void SensorDatas()
        {
            string result = "";
            command_no++;
            // command_no = 2;
            string hexid = $"{command_no:X2}";

            int child_id_no = 2;

            string child_id = $"{child_id_no:X2}";

            // physics_address = "44032c7a";

            string crc_cal_part = physics_address + hexid + conn_format_code + sensor_total_length
                               + sensor_command + child_id + sensor_total_data + sensor_data_count;

            MessageBox.Show("crc_cal: " + crc_cal_part);
            //crc_cal_part = "44032c7a01ff00140500013230323231323138313431393237303030";

            byte[] crc_body = StringToByteArray(crc_cal_part);
            ushort crc_part = Crc16Ccitt(crc_body);

            string crc_part_hex = crc_part.ToString("X2");
            MessageBox.Show("crc_check: " + crc_part_hex);

            string base64PreStr = crc_cal_part + crc_part_hex;

            string stringToConvert = "44032c7a01ff001405000132303232313231383134313932373030304741";

            byte[] convertedByte = StringToByteArray(base64PreStr);
            string hex = System.Convert.ToBase64String(convertedByte);
            string cmdBuf = Base64ToHexadecimal(hex);

            string total_result = conn_data_stx + cmdBuf + conn_data_etx;

            MessageBox.Show(total_result);

            byte[] total_bites = StringToByteArray(total_result);

            _IoTIF.WriteBytes(total_bites);


            const int HT03_COM_TIMEOUT = 1500;

            var sw = new Stopwatch();
            sw.Restart();

            string recvStr = string.Empty;
            do
            {
                if (sw.ElapsedMilliseconds > HT03_COM_TIMEOUT)
                {
                    sw.Stop();
                    //  throw new ApplicationException("sensor: データ送信の応答待ちでタイムアウト");
                }
                System.Threading.Thread.Sleep(1);
                var recvBuf = _IoTIF.ReadBytes();
                recvStr += Encoding.ASCII.GetString(recvBuf);
                // } while (!recvStr.EndsWith("\n"));
            } while (sw.ElapsedMilliseconds < HT03_COM_TIMEOUT);

            string[] second_strs = recvStr.Split("\u0003", StringSplitOptions.RemoveEmptyEntries);

            string latest_recvStr = "";

            if (second_strs.Length > 2)
            {
                latest_recvStr = second_strs[1];
            }
            else
            {
                latest_recvStr = second_strs[0];
            }

            //  string latest_recvStr = recvStr.Substring(0, recvStr.Length - 3);

            MessageBox.Show("received sensor data" + latest_recvStr);
            string newest_id = this.GetNewestId(latest_recvStr);
            this.NewestSensorData(newest_id);
        }

        public string GetNewestId(string latest_recvStr)
        {
            latest_recvStr = "lcDv7BoCABuhuS7wAgAAAG4AiQAAAAAAAAAAD/9HDywhAABbtg==";
            byte[] bytes = Convert.FromBase64String(latest_recvStr);
            string hex = BitConverter.ToString(bytes);
            string newest_hex_str = bytes[10].ToString("x2") + bytes[11].ToString("x2");
            // int diff = int.Parse(newest_hex_str, System.Globalization.NumberStyles.HexNumber) - Convert.ToInt32((byte)0x0018);

            int diff = int.Parse("0089", System.Globalization.NumberStyles.HexNumber) - Convert.ToInt32((byte)0x0018);
            string diff_str = (diff + 1).ToString("x4");

            MessageBox.Show("latest: " + hex + " new: " + newest_hex_str + " test  " + diff_str);
            return diff_str;
        }

        public void NewestSensorData(string newest_id)
        {
            string result = "";
            command_no++;
            // command_no = 2;
            string hexid = $"{command_no:X2}";

            int child_id_no = 2;

            string child_id = $"{child_id_no:X2}";

            // physics_address = "44032c7a";

            string crc_cal_part = physics_address + hexid + conn_format_code + sensor_total_length
                               + sensor_command + child_id + newest_id + sensor_data_count;

            MessageBox.Show("crc_cal: " + crc_cal_part);

            byte[] crc_body = StringToByteArray(crc_cal_part);
            ushort crc_part = Crc16Ccitt(crc_body);

            string crc_part_hex = crc_part.ToString("X2");
            MessageBox.Show("crc_check: " + crc_part_hex);

            string base64PreStr = crc_cal_part + crc_part_hex;

            byte[] convertedByte = StringToByteArray(base64PreStr);
            string hex = System.Convert.ToBase64String(convertedByte);
            string cmdBuf = Base64ToHexadecimal(hex);

            string total_result = conn_data_stx + cmdBuf + conn_data_etx;

            MessageBox.Show(total_result);

            byte[] total_bites = StringToByteArray(total_result);

            _IoTIF.WriteBytes(total_bites);

            const int NEWEST_COM_TIMEOUT = 15000;

            var sw = new Stopwatch();
            sw.Restart();

            string recvStr = string.Empty;
            do
            {
                if (sw.ElapsedMilliseconds > NEWEST_COM_TIMEOUT)
                {
                    sw.Stop();
                    throw new ApplicationException("sensor: データ送信の応答待ちでタイムアウト");
                }
                System.Threading.Thread.Sleep(1);
                var recvBuf = _IoTIF.ReadBytes();
                recvStr += Encoding.ASCII.GetString(recvBuf);
            } while (sw.ElapsedMilliseconds < NEWEST_COM_TIMEOUT);

            string[] second_strs = recvStr.Split("\u0003", StringSplitOptions.RemoveEmptyEntries);
            string first_recvStr = second_strs[0];

            MessageBox.Show("sensor result data: " + recvStr + " :  first :" + first_recvStr);
        }

        public void GettingDetailData(string receive_sensor_data)
        {
            receive_sensor_data = "1cDv7AMCABuhuS7wBQECAAEAAQN6FPoexz+uNX1HGSwhAUIPHg==";
            /*1cDv7AMCABuhuS7wBQECAAEAAQN6FPoexz+uNX1HGSwhAUIPHg==*/

            byte[] bytes = Convert.FromBase64String(receive_sensor_data);

            int seq = Convert.ToInt32(bytes[4]);
            int str_index = Convert.ToInt32(bytes[5]);
            int num = Convert.ToInt32(bytes[6]);
            string _uuid = receive_sensor_data.Substring(0, 4);
            int data_id = int.Parse(bytes[7].ToString("x2") + bytes[8].ToString("x2"), System.Globalization.NumberStyles.HexNumber);
            int newest_id = int.Parse(bytes[9].ToString("x2") + bytes[10].ToString("x2"), System.Globalization.NumberStyles.HexNumber);
            int gradient_int = int.Parse(bytes[11].ToString("x2") + bytes[12].ToString("x2"), System.Globalization.NumberStyles.HexNumber);
            int temperature_int = int.Parse(bytes[13].ToString("x2") + bytes[14].ToString("x2"), System.Globalization.NumberStyles.HexNumber);
            int humidity_int = int.Parse(bytes[15].ToString("x2") + bytes[16].ToString("x2"), System.Globalization.NumberStyles.HexNumber);
            int pressure_int = int.Parse(bytes[17].ToString("x2") + bytes[18].ToString("x2"), System.Globalization.NumberStyles.HexNumber);
            int voltage_int = int.Parse(bytes[19].ToString("x2") + bytes[20].ToString("x2"), System.Globalization.NumberStyles.HexNumber);

            string csq = bytes[21].ToString("x2") + bytes[22].ToString("x2");
            string date_hex = bytes[21].ToString("x2") + bytes[22].ToString("x2");
            string time_hex = bytes[23].ToString("x2") + bytes[24].ToString("x2");


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
            MessageBox.Show(_uuid + ":" + data_id + ":" + temperature + ":" + humidity + ":" + voltage + ":"
                           + pressure + ":" + gradient + ":" + sensor_time + ":" + date_time);

            return;
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
                        string inser_sensor_sql = "INSERT INTO app_display('temperature','humidity','voltage','pressure','gradient','uuid','sensor_time','datetime') VALUES('"
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

        /// <summary>
        /// IoTデバイス初期化コマンド送信
        /// </summary>
        private void TransIotInit()
        {
            string cmdStr;
            _IoTIF.WriteStr("echo off\r\n\x1b[?01h\r\n");

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
                }
                System.Threading.Thread.Sleep(1);
                var recvBuf = _IoTIF.ReadBytes();
                recvStr += Encoding.ASCII.GetString(recvBuf);
            } while (!recvStr.EndsWith("\n"));

            MessageBox.Show("start init setting status : " + recvStr);
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static ushort Crc16Ccitt(byte[] bytes)
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

        private string DateToHexConcvert()
        {
            string datetimeconvert = "";
            string date_time = DateTime.Now.ToString("yyyyMMddHHmmss000");
            MessageBox.Show(date_time);

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

            MessageBox.Show(datetimeconvert);
            return datetimeconvert;
        }

        private string Base64ToHexadecimal(string _base64str)
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

            MessageBox.Show(result);
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

        private void TransIoT2byte(byte sqnsNo, byte ch, short data)
        {
            const int HT03_COM_TIMEOUT = 1000;

            // データ送信
            var cmd = new List<byte>();
            cmd.Add(PC_COMMAND_HT03_DATA);
            cmd.Add(sqnsNo);
            cmd.Add(ch);
            var dataAry = BitConverter.GetBytes(data);
            cmd.Add(dataAry[1]);
            cmd.Add(dataAry[0]);
            ushort crc = Crc16Ccitt(cmd.ToArray());
            cmd.Add(Convert.ToByte(crc >> 8));
            cmd.Add(Convert.ToByte(crc & 0xff));
            string base64Str = Convert.ToBase64String(cmd.ToArray());
            string cmdStr = string.Format("@{0}\r\n", base64Str);
            var cmdBuf = Encoding.ASCII.GetBytes(cmdStr);
            _IoTIF.WriteBytes(cmdBuf);

            // 応答確認
            var sw = new Stopwatch();
            sw.Restart();
            bool? ack = null;
            string errStr = string.Empty;
            bool isRecving = true;
            while (isRecving)
            {
                string recvStr = string.Empty;
                do
                {
                    if (sw.ElapsedMilliseconds > HT03_COM_TIMEOUT)
                    {
                        sw.Stop();
                        throw new ApplicationException("データ送信の応答待ちでタイムアウト");
                    }
                    System.Threading.Thread.Sleep(1);
                    var recvBuf = _IoTIF.ReadBytes();
                    recvStr += Encoding.ASCII.GetString(recvBuf);
                } while (!recvStr.EndsWith("\n"));

                if (string.IsNullOrWhiteSpace(recvStr))
                {
                    continue;
                }
                var splts = recvStr.Split("\n".ToArray()).Select(x => x.Trim());
                foreach (string s in splts)
                {
                    if (!s.StartsWith("@"))
                    {
                        continue;
                    }
                    if (CheckIotRes(s.Substring(1), sqnsNo, out ack, out errStr))
                    {
                        isRecving = false;
                    }
                    else
                    {

                    }
                }
            }
            sw.Stop();
            if (ack != null && ack.Value)
            {
                return;
            }
            else
            {
                throw new ApplicationException(errStr);
            }
        }

        /// <summary>
        /// 対象チャンネルの送信完了を待機
        /// </summary>
        /// <param name="ch"></param>
        private void WaitTransComplete(IEnumerable<byte> ch)
        {
            const int WAIT_TIMEOUT = 90000;

            var sw = new Stopwatch();
            sw.Restart();
            bool isRecving = true;
            var recvChs = new List<byte>(ch);

            while (isRecving)
            {
                string recvStr = string.Empty;
                do
                {
                    if (sw.ElapsedMilliseconds > WAIT_TIMEOUT)
                    {
                        sw.Stop();
                        throw new ApplicationException("データ送信完了の応答待ちでタイムアウト");
                    }
                    System.Threading.Thread.Sleep(1);
                    var recvBuf = _IoTIF.ReadBytes();
                    recvStr += Encoding.ASCII.GetString(recvBuf);
                } while (!recvStr.EndsWith("\n"));

                if (string.IsNullOrWhiteSpace(recvStr))
                {
                    continue;
                }
                var splts = recvStr.Split("\n".ToArray()).Select(x => x.Trim());
                foreach (string s in splts)
                {
                    // TraceLog.WriteLog2(10, "HD01 > " + s, _logPrefix);
                    if (s.StartsWith("gid:"))
                    {
                        for (int i = 0; i < recvChs.Count; i++)
                        {
                            int chTmp = 0x40 | recvChs[i];
                            if (s.Contains(String.Format("sensor-id: 0x{0:X2}", chTmp)))
                            {
                                recvChs.RemoveAt(i);
                                break;
                            }
                        }
                    }

                    if (!recvChs.Any())
                    {
                        // 送信完了
                        isRecving = false;
                    }
                }
            }
            sw.Stop();
        }


        /// <summary>
        /// IoTデバイスのリプライをチェック
        /// </summary>
        private bool CheckIotRes(string resBase64, byte sqnsNo, out bool? ack, out string errStr)
        {
            const byte PC_COMMAND_ACK = (byte)0xff;

            var recvBuf = Convert.FromBase64String(resBase64);
            if (recvBuf.Length < 5)
            {
                ack = null;
                errStr = "応答サイズ不正(" + string.Join(" ", recvBuf.Select(x => string.Format("{0,0:X2}", x))) + ")";
                return false;
            }
            ushort crc = Crc16Ccitt(recvBuf.Take(3).ToArray());
            if (recvBuf[recvBuf.Length - 2] != Convert.ToByte(crc >> 8) ||
                recvBuf[recvBuf.Length - 1] != Convert.ToByte(crc & 0xff))
            {
                ack = null;
                errStr = "応答データのCRC不正";
                return false;
            }
            if (recvBuf[1] != PC_COMMAND_HT03_DATA)
            {
                ack = null;
                errStr = "異なるコマンドの応答";
                return false;
            }
            if (recvBuf[2] != sqnsNo)
            {
                ack = null;
                errStr = "異なるシーケンスNo";
                return false;
            }
            if (recvBuf[0] == PC_COMMAND_ACK)
            {
                ack = true;
                errStr = String.Empty;
                return true;
            }
            else
            {
                ack = false;
                errStr = "NACK応答";
                return true;
            }
        }

        private void SensorBtn_Click(object sender, EventArgs e)
        {
            //this.GetNewestId("lcDv7BoCABuhuS7wAgAAAG4AiQAAAAAAAAAAD/9HDywhAABbtg==");
            GettingDetailData("");
        }
    }

}
