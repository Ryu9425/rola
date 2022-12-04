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
        }

        static void WebDataThread()
        {
            wepDataIniting(null, null);
            timer = new System.Timers.Timer(300000);
            timer.Elapsed += wepDataIniting;
            timer.AutoReset = true; ;
            timer.Enabled = true;
        }

        public static async void wepDataIniting(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            DateTime currentTime = DateTime.Now;
            double minuts = -6;
            DateTime pre_Time = currentTime.AddMinutes(minuts);

            if (OpenConnection() == false) return;

            // string start_date_time = pre_Time.ToString("yyyy-MM-dd HH:mm:ss");  
            string start_date_time = GetPreDate() == "" ? pre_Time.ToString("yyyy-MM-dd HH:mm:ss")
                                                    : GetPreDate();
            //MessageBox.Show(start_date_time);

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    module = "uck9JBnekzPe",
                    // datetime = start_date_time
                    datetime = "2022-12-01 18:00:00"
                }),
                Encoding.UTF8,
                "application/json");

            string web_api = GetWebApi();

            // using HttpResponseMessage response = await client.PostAsync(web_api, jsonContent);
            using HttpResponseMessage response = await client.PostAsync("https://collapse.sakura.ne.jp/getstream.php", jsonContent);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
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
                }
            }
            catch
            {

            }
            return date_time;
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
                web_api = reader.GetString(1);
                Constant.web_api = web_api;
                Constant.connection_time = reader.GetInt32(2);
                Constant.connection_interval = reader.GetInt32(3);
                Constant.store_path = reader.GetString(4);
                Constant.display_count = reader.GetInt32(5);
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
            // MessageBox.Show(items.Length.ToString());
            List<Item> item_list = new List<Item>();

            foreach (Item item in items)
            {
                // if (item_list.Count != 0 && item_list[0].uuid == item.uuid && item_list[0].datetime == item.datetime)
                // {
                //     item_list.Add(item);
                // }
                // else if (item_list.Count == 0)
                // {
                //     item_list.Add(item);
                // }
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
            string check_sql = "SELECT COUNT('id') FROM display WHERE uuid='" + uuid + "' and sensor_time='" + sensor_date+" "+sensor_time 
                            + "' and voltage='" + voltage + "' and temperature='" + temperature + "' and humidity='" + humidity+ "'";
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