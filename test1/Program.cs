using System.Globalization;
using System.Xml;
using System.Reflection;
using System.Data.SQLite;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace test1
{

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        public static System.Timers.Timer timer;

        public static string connection_path = "Data Source=" + Path.Combine(Directory.GetCurrentDirectory(), "rola.db");
        public static SQLiteConnection m_dbConnection = new SQLiteConnection(connection_path);

        [STAThread]

        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Thread web_thread = new Thread(WebDataThread);
            web_thread.IsBackground = true;
            web_thread.Start();
            while (!OpenConnection())
            {
                OpenConnection();
            }
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
           //  Application.Run(new UsbTestForm());
        }

        static void WebDataThread()
        {
            while (OpenConnection() == false)
            {
                OpenConnection();
            }
            // wepDataIniting(null, null);
            timer = new System.Timers.Timer(600000);
            timer.Elapsed += wepDataInviting;
            timer.AutoReset = true; 
            timer.Enabled = true;
        }

        public static async void wepDataInviting(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            string current_module = GetModule();

            string start_date_time = GetPreDate() == "" ? "2022-12-01 12:00:00"
                                                    : GetPreDate();

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    //module = "uck9JBnekzPe",
                    module = current_module,
                    datetime = start_date_time
                }),
                Encoding.UTF8,
                "application/json");

            string web_api = GetWebApi();

            using HttpResponseMessage response = await client.PostAsync("https://collapse.sakura.ne.jp/getstream.php", jsonContent);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

           // WriteWebContent(start_date_time);

            try
            {
                Item[] items = JsonSerializer.Deserialize<Item[]>(jsonResponse);
                // SensorDataAdding(items);
                DiaplayDataAdding(items);
            }
            catch
            {

            }

            return;
        }

        public static bool OpenConnection()
        {
            try
            {
                m_dbConnection.Open();
                return true;
            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
            }

            return false;
        }

        public static bool CloseConnection()
        {
            try
            {
                m_dbConnection.Close();
                return true;
            }
            catch (Exception ex)
            {
                //   MessageBox.Show(ex.Message);
            }

            return false;
        }
        public static string GetPreDate()
        {
            var date_time = "";

            var cmd = m_dbConnection.CreateCommand();
            string check_sql = "SELECT MAX(datetime) FROM display";
            cmd.CommandText = check_sql;

            var exist_status_reader = cmd.ExecuteReader();
            try
            {
                if (exist_status_reader.Read())
                {
                    date_time = exist_status_reader.GetString(0);
                    // DateTime oDate = DateTime.ParseExact(date_time, "yyyy-MM-dd HH:mm:ss", null);
                    // DateTime start_date = oDate.AddSeconds(-20);
                    // date_time = start_date.ToString("yyyy-mm-dd HH:mm:ss");
                    // MessageBox.Show(start_date.ToString("yyyy-mm-dd HH:mm:ss"));
                }
            }
            catch (System.Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }

            return date_time;
        }
       
        public static string GetModule(){
            var current_module = "";

            var cmd = m_dbConnection.CreateCommand();
            string module_sql = "SELECT module FROM main_setting";
            cmd.CommandText = module_sql;

            var exist_status_reader = cmd.ExecuteReader();
            try
            {
                if (exist_status_reader.Read())
                {
                    current_module = exist_status_reader.GetString(0);
                }
            }
            catch (System.Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }

            return current_module;
        }
        public static string GetWebApi()
        {
            var web_api = "";

            var cmd = m_dbConnection.CreateCommand();
            cmd.CommandText = "SELECT *FROM main_setting";
            cmd.ExecuteNonQuery();
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                web_api = reader.GetString(2);
                Constant.web_api = web_api;
                Constant.connection_time = reader.GetInt32(3);
                Constant.connection_interval = reader.GetInt32(4);
                Constant.store_path = reader.GetString(5);
                Constant.display_count = reader.GetInt32(6);
            }
            return web_api;
        }

        public static void SensorDataAdding(Item[] items)
        {
            foreach (var item in items)
            {
                var cmd = m_dbConnection.CreateCommand();
                string check_sql = "SELECT COUNT('sensorid') FROM sensors WHERE sensorid='" + item.sensorid + "'";
                cmd.CommandText = check_sql;

                var exist_status_reader = cmd.ExecuteReader();
                while (exist_status_reader.Read())
                {
                    int myreader = exist_status_reader.GetInt32(0);
                    if (myreader == 0)
                    {
                        var insert_sensor_cmd = m_dbConnection.CreateCommand();
                        string inser_sensor_sql = "INSERT INTO sensors('sensorid','uuid','data_id','data','datetime') VALUES('" + item.sensorid + "', '"
                            + item.uuid + "', '" + item.data_id + "','" + item.data + "', '" + item.datetime + "')";
                        insert_sensor_cmd.CommandText = inser_sensor_sql;
                        insert_sensor_cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public static void DiaplayDataAdding(Item[] items)
        {
            //  WriteCharacters(items);

            List<Item> item_list = new List<Item>();

            foreach (Item item in items)
            {
                string item_datas = "sensor id:" + item.sensorid + ", uuid: " + item.uuid + ", data_id: "
                              + item.data_id + ", data: " + item.data + ",datetime:" + item.datetime;


                if (item.data_id == "15")
                {
                    item_list.Add(item);
                    DisplayDBAdding(item_list);
                    item_list.Clear();
                }
                else
                {
                    item_list.Add(item);
                }
            }
        }
        static async void WriteCharacters(Item[] items)
        {
            string file_path = Path.Combine(Directory.GetCurrentDirectory(), "rola.txt");

            string access_time = GetPreDate();

            //  MessageBox.Show(items.Length.ToString());
            StreamWriter file = File.AppendText(file_path);
            foreach (Item item in items)
            {
                string item_datas = " _module:uck9JBnekzPe: _datetime:" + access_time + ", sensor id:" + item.sensorid + ", uuid: " + item.uuid + ", data_id: "
                              + item.data_id + ", data: " + item.data + ",datetime:" + item.datetime;

                await file.WriteLineAsync(item_datas);
            }
         //   MessageBox.Show("rola.txt log file updated at once by 10 min!");

            file.Close();
        }

        static async void WriteWebContent(string content)
        {
            string file_path = Path.Combine(Directory.GetCurrentDirectory(), "rola.txt");

            string access_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


            StreamWriter file = File.AppendText(file_path);

            content = access_time + ", datetime: " + content + "\n";
            //  MessageBox.Show(content);

            await file.WriteAsync(content);
            file.Close();
        }

        public static void DisplayDBAdding(List<Item> item_list)
        {
            if (item_list.Count < 7)
            {
                return;
            }
            string gradient = "";
            string temperature = "";
            string humidity = "";
            string pressure = "";
            string voltage = "";
            string sensor_date = "";
            string sensor_time = "";
            string sensorid = item_list[0].sensorid;
            string uuid = item_list[0].uuid;
            string datetime = item_list[0].datetime;

            foreach (Item item in item_list)
            {
                if (item.data_id == "0")
                {
                    temperature = string.Format("{0:N2}", Int32.Parse(item.data) / 256.0f);
                }
                if (item.data_id == "1")
                {
                    humidity = string.Format("{0:N2}", Int32.Parse(item.data) / 256.0f);
                }
                if (item.data_id == "2")
                {
                    voltage = string.Format("{0:N3}", Int32.Parse(item.data) * 3.3f / 32767);
                }
                if (item.data_id == "3")
                {
                    pressure = string.Format("{0:N2}", Int32.Parse(item.data) / 16.0f);
                }
                if (item.data_id == "4")
                {
                    gradient = string.Format("{0:N1}", Int32.Parse(item.data) / 256.0f);
                }
                if (item.data_id == "14")
                {
                    sensor_date = Constant.Sensor_Receive_Date(item.data);
                    //  MessageBox.Show(sensor_date);
                }
                if (item.data_id == "15")
                {
                    sensor_time = Constant.Sensor_Receive_Time(item.data);
                    //  MessageBox.Show(sensor_time);
                }
            }
            var cmd = m_dbConnection.CreateCommand();
            string check_sql = "SELECT COUNT('id') FROM display WHERE uuid='" + uuid + "' and sensor_time='" + sensor_date + " " + sensor_time
                            + "' and voltage='" + voltage + "' and temperature='" + temperature + "' and humidity='" + humidity + "'";
            cmd.CommandText = check_sql;

            var exist_status_reader = cmd.ExecuteReader();
            while (exist_status_reader.Read())
            {
                int myreader = exist_status_reader.GetInt32(0);
                if (myreader == 0)
                {
                    var insert_display_cmd = m_dbConnection.CreateCommand();
                    string inser_sensor_sql = "INSERT INTO display('temperature','humidity','voltage','pressure','gradient','uuid','sensor_time','datetime') VALUES('"
                        + temperature + "', '" + humidity + "','" + voltage + "','" + pressure + "','" + gradient + "','" + uuid + "','" + sensor_date + " " + sensor_time + "', '" + datetime + "')";

                    insert_display_cmd.CommandText = inser_sensor_sql;
                    insert_display_cmd.ExecuteNonQuery();
                }
            }
        }
    }
}