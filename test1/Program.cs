using System.Reflection;
using System.Data.SQLite;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace test1
{
    public class Item
    {
        public string sensorid { set; get; }
        public string uuid { set; get; }
        public string data_id { set; get; }
        public string data { set; get; }
        public string datetime { set; get; }
    }
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        public static System.Timers.Timer timer;
        [STAThread]

        static void Main()
        {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
            Thread web_thread = new Thread(WebDataThread);
            web_thread.IsBackground = true;
            web_thread.Start();
            ApplicationConfiguration.Initialize();
            Application.Run(new Detail());
        }

        static void WebDataThread()
        {
            timer = new System.Timers.Timer(300000);
            timer.Elapsed += wepDataIniting;
            timer.AutoReset = true; ;
            timer.Enabled = true;
            //  wepDataIniting();   
        }

        public static async void wepDataIniting(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            DateTime currentTime = DateTime.Now;
            double minuts = -6;
            DateTime pre_Time = currentTime.AddMinutes(minuts);
        
            string start_date_time = pre_Time.ToString("yyyy-MM-dd HH:mm:ss");          

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    module = "uck9JBnekzPe",
                    datetime = start_date_time
                    //datetime = "2022-11-26 04:45:42"
                }),
                Encoding.UTF8,
                "application/json");
            Console.WriteLine(jsonContent);
            using HttpResponseMessage response = await client.PostAsync("https://collapse.sakura.ne.jp/getstream.php", jsonContent);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            try
            {
                Item[] items = JsonSerializer.Deserialize<Item[]>(jsonResponse);
                SensorDataAdding(items);
                DiaplayDataAdding(items);
            }
            catch
            {

            }  
       
            return;
        }

        public static void SensorDataAdding(Item[] items)
        {
            SQLiteConnection m_dbConnection;
            var connection_path = "Data Source=" + Path.Combine(Directory.GetCurrentDirectory(), "rola.db");
            m_dbConnection = new SQLiteConnection(connection_path);

            try
            {
                m_dbConnection.Open();

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            m_dbConnection.Close();
        }
        public static void DiaplayDataAdding(Item[] items)
        {          

            List<Item> item_list = new List<Item>();

            foreach (Item item in items)
            {
                if (item_list.Count!=0 && item_list[0].uuid==item.uuid && item_list[0].datetime == item.datetime)
                {
                    item_list.Add(item);
                }else if (item_list.Count == 0)
                {
                    item_list.Add(item);
                }
                else
                {
                    DisplayDBAdding(item_list);
                    item_list.Clear();
                    item_list.Add(item);
                }                
            }
        }

        public static void DisplayDBAdding(List<Item> item_list)
        {
            string gradient = "";
            string temperature = "";
            string humidity = "";
            string pressure = "";
            string voltage = "";
            string sensorid = item_list[0].sensorid;
            string uuid = item_list[0].uuid;
            string datetime = item_list[0].datetime;

            SQLiteConnection m_dbConnection;
            var connection_path = "Data Source=" + Path.Combine(Directory.GetCurrentDirectory(), "rola.db");
            m_dbConnection = new SQLiteConnection(connection_path);

            try
            {
                m_dbConnection.Open();

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
                }
                var cmd = m_dbConnection.CreateCommand();
                string check_sql = "SELECT COUNT('id') FROM display WHERE uuid='" + uuid + "' and datetime='"+datetime+"'";
                cmd.CommandText = check_sql;

                var exist_status_reader = cmd.ExecuteReader();
                while (exist_status_reader.Read())
                {
                    int myreader = exist_status_reader.GetInt32(0);
                    if (myreader == 0)
                    {
                        var insert_display_cmd = m_dbConnection.CreateCommand();
                        string inser_sensor_sql = "INSERT INTO display('temperature','humidity','voltage','pressure','gradient','uuid','datetime') VALUES('"
                            + temperature + "', '" + humidity + "','" + voltage + "','" + pressure + "','" + gradient + "','" + uuid + "', '" + datetime + "')";
                        insert_display_cmd.CommandText = inser_sensor_sql;
                        insert_display_cmd.ExecuteNonQuery();
                    }
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            m_dbConnection.Close();            
        }
    }
}