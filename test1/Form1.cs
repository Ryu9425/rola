using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Windows;

namespace test1
{
    public partial class Form1 : Form
    {
        string[] itemLists = { "傾斜", "気温", "湿度", "気圧", "電池電圧" };
        public System.Windows.Forms.Timer timer;

        List<KeyUUID> key_uuid_list = new List<KeyUUID>();

        //usb data process
        public UsbController usbController;
        public Thread usb_thread;
        public System.Timers.Timer usb_total_timer, usb_new_data_timer;
        public string started_time = string.Empty;
        public bool is_port_opened = false;

        Form3 sensor_info;

        public Form1()
        {
            InitializeComponent();

            AddDateTime();
            GetKeyUUID_Datas();
            SensorDatasView();


            timer = new System.Windows.Forms.Timer();
            timer.Interval = 300000;
            timer.Tick += new System.EventHandler(DisplayDataUpdate);
            timer.Enabled = true;
            timer.Start();

            sensor_info = new Form3();
        }

        public void DisplayDataUpdate(object sender, EventArgs e)
        {
            AddDateTime();
            GetKeyUUID_Datas();
            SensorDatasView();
        }

        public void GetKeyUUID_Datas()
        {
            try
            {
                var command = Program.m_dbConnection.CreateCommand();
                command.CommandText = "SELECT *FROM sensor_setting ORDER BY id";
                using (var reader = command.ExecuteReader())
                {
                    key_uuid_list.Clear();
                    while (reader.Read())
                    {
                        KeyUUID key_uuid = new KeyUUID();
                        key_uuid.display_name = reader.GetString(1);
                        key_uuid.uuid = reader.GetString(2);
                        key_uuid.standard = reader.GetValue(3).ToString();
                        key_uuid.is_gradient = reader.GetInt32(4) == 1 ? true : false;
                        key_uuid.is_humidity = reader.GetInt32(5) == 1 ? true : false;
                        key_uuid.is_temperature = reader.GetInt32(6) == 1 ? true : false;
                        key_uuid.is_pressure = reader.GetInt32(7) == 1 ? true : false;
                        key_uuid.is_voltage = reader.GetInt32(8) == 1 ? true : false;

                        key_uuid_list.Add(key_uuid);
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            Constant.key_uuid_list = key_uuid_list;
        }

        public void SensorDatasView()
        {
            AddDataTable(this.dataGridView_1, 0);
            AddDataTable(this.dataGridView_2, 1);
            AddDataTable(this.dataGridView_3, 2);
            AddDataTable(this.dataGridView_4, 3);
            AddDataTable(this.dataGridView_5, 4);
            AllClearSelection();
        }
        public void AddDataTable(DataGridView dataGridView, int _id)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ColA", typeof(string));
            dt.Columns.Add("ColB", typeof(string));

            string uuid = key_uuid_list[_id].uuid;
            int row_count = 0;

            dt.Rows.Add(new object[] { key_uuid_list[_id].display_name, key_uuid_list[_id].uuid });
            row_count++;

            try
            {
                var command = Program.m_dbConnection.CreateCommand();
                command.CommandText = "SELECT *FROM display WHERE uuid='" + uuid + "' ORDER BY datetime DESC";

                using (var reader = command.ExecuteReader())
                {
                    // MessageBox.Show("SELECT *FROM display WHERE uuid='" + uuid + "' ORDER BY datetime DESC");
                    if (reader.Read())
                    {
                        var temperature = reader.GetString(1) != "" ? reader.GetString(1) : "";
                        var humidity = reader.GetString(2) != "" ? reader.GetString(2) : "";
                        var voltage = reader.GetValue(3).ToString();
                        var pressure = reader.GetString(4) != "" ? reader.GetString(4) : "";
                        var gradient = reader.GetString(5) != "" ? reader.GetString(5) : "";

                        if (key_uuid_list[_id].is_gradient)
                        {
                            decimal diff_gradient = Math.Round(decimal.Parse(key_uuid_list[_id].standard), 1) - Math.Round(decimal.Parse(gradient), 1);
                            dt.Rows.Add(new object[] { itemLists[0], diff_gradient.ToString() + "°" });
                            row_count++;
                        }
                        if (key_uuid_list[_id].is_temperature)
                        {
                            dt.Rows.Add(new object[] { itemLists[1], temperature + "℃" });
                            row_count++;
                        }
                        if (key_uuid_list[_id].is_humidity)
                        {
                            dt.Rows.Add(new object[] { itemLists[2], humidity + "%" });
                            row_count++;
                        }
                        if (key_uuid_list[_id].is_pressure)
                        {
                            dt.Rows.Add(new object[] { itemLists[3], pressure + "hPa" });
                            row_count++;
                        }
                        if (key_uuid_list[_id].is_voltage)
                        {
                            dt.Rows.Add(new object[] { itemLists[4], voltage + "V" });
                            row_count++;
                        }
                    }
                    else
                    {
                        if (key_uuid_list[_id].is_gradient)
                        {
                            dt.Rows.Add(new object[] { itemLists[0], "" });
                            row_count++;
                        }
                        if (key_uuid_list[_id].is_temperature)
                        {
                            dt.Rows.Add(new object[] { itemLists[1], "" });
                            row_count++;
                        }
                        if (key_uuid_list[_id].is_humidity)
                        {
                            dt.Rows.Add(new object[] { itemLists[2], "" });
                            row_count++;
                        }
                        if (key_uuid_list[_id].is_pressure)
                        {
                            dt.Rows.Add(new object[] { itemLists[3], "" });
                            row_count++;
                        }
                        if (key_uuid_list[_id].is_voltage)
                        {
                            dt.Rows.Add(new object[] { itemLists[4], "" });
                            row_count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // dataGridView.DataSource = null;

            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;

                dataGridView.DataSource = dt;
                dataGridView.Height = 35 * row_count;

                DataGridViewStyiling(dataGridView);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        public void AddDateTime()
        {
            //  Control.CheckForIllegalCrossThreadCalls = false;
            try
            {
                var command = Program.m_dbConnection.CreateCommand();
                command.CommandText = "SELECT MAX(datetime) FROM display";

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var date_time = reader.GetString(0);

                        string day = date_time.Split(" ")[0];
                        string time = date_time.Split(" ")[1];
                        string dis = day.Split("-")[0] + "年" + day.Split("-")[1] + "月" + day.Split("-")[2] + "日";
                        if (!Constant.is_first)
                            ;
                        // MessageBox.Show("Updated display state at once by 5 min!", "Sensor");
                        // AutoClosingMessageBox.Show("Updated!", "Sensor", 2000);
                        else
                            Constant.is_first = false;

                        dateLabel.Text = dis;
                        timeLabel.Text = time;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("eeeeeeeeeeeeAddDateTime");
            }
        }

        private void DataGridViewStyiling(DataGridView dataGridView)
        {
            dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ScrollBars = ScrollBars.None;
            dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.RowTemplate.Height = 35;
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 15);
            dataGridView.Width = 230;
        }

        private void button_Click(object sender, EventArgs e)
        {
            Form form2 = new Form2();
            this.Hide();
            form2.ShowDialog();
            this.Show();
            AddDateTime();
            GetKeyUUID_Datas();
            this.SensorDatasView();
            dataGridView_1.ClearSelection();
            dataGridView_2.ClearSelection();
            dataGridView_3.ClearSelection();
            dataGridView_4.ClearSelection();
            dataGridView_5.ClearSelection();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView_1.ClearSelection();
            dataGridView_2.ClearSelection();
            dataGridView_3.ClearSelection();
            dataGridView_4.ClearSelection();
            dataGridView_5.ClearSelection();
        }

        private void DisplayDetailDatas(object sender, EventArgs e)
        {
            dataGridView_1.ClearSelection();
            Constant.selected_uuid_index = 1;
            Form detail = new Detail();
            this.Hide();
            detail.ShowDialog();
            this.Show();
            this.AddDateTime();
            this.GetKeyUUID_Datas();
            this.SensorDatasView();
            this.AllClearSelection();
        }

        public void AllClearSelection()
        {
            dataGridView_1.ClearSelection();
            dataGridView_2.ClearSelection();
            dataGridView_3.ClearSelection();
            dataGridView_4.ClearSelection();
            dataGridView_5.ClearSelection();
        }


        private void dataGridView_3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView_1.ClearSelection();
            Constant.selected_uuid_index = 3;
            Form detail = new Detail();
            this.Hide();
            detail.ShowDialog();
            this.Show();
            this.AddDateTime();
            this.GetKeyUUID_Datas();
            this.SensorDatasView();
            this.AllClearSelection();
        }

        private void dataGridView_2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView_1.ClearSelection();
            Constant.selected_uuid_index = 2;
            Form detail = new Detail();
            this.Hide();
            detail.ShowDialog();
            this.Show();
            this.AddDateTime();
            this.GetKeyUUID_Datas();
            this.SensorDatasView();
            this.AllClearSelection();
        }

        private void dataGridView_4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView_1.ClearSelection();
            Constant.selected_uuid_index = 4;
            Form detail = new Detail();
            this.Hide();
            detail.ShowDialog();
            this.Show();
            this.AddDateTime();
            this.GetKeyUUID_Datas();
            this.SensorDatasView();
            this.AllClearSelection();
        }

        private void dataGridView_5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView_1.ClearSelection();
            Constant.selected_uuid_index = 5;
            Form detail = new Detail();
            this.Hide();
            detail.ShowDialog();
            this.Show();
            this.AddDateTime();
            this.GetKeyUUID_Datas();
            this.SensorDatasView();
            this.AllClearSelection();
        }

        public class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                MessageBox.Show(text, caption);
            }

            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }

            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow(null, _caption);
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }

        private void UsbBtn_Click(object sender, EventArgs e)
        {
            usb_thread = new Thread(UsbDataProcessing);
            usb_thread.IsBackground = true;
            usb_thread.Start();

            usb_total_timer = new System.Timers.Timer(900000);
            usb_total_timer.Elapsed += usbDataStopping;
            usb_total_timer.AutoReset = false;
            usb_total_timer.Enabled = true;

            usb_new_data_timer = new System.Timers.Timer(630000);
            usb_new_data_timer.AutoReset = false;
            usb_new_data_timer.Elapsed += usbNewDataReceive;
            usb_new_data_timer.Enabled = true;
        }

        public void UsbDataProcessing()
        {
            usb_total_timer.Start();
            this.UsbBtn.Enabled = false;

            string serial_com_port = GetSerialPort();

            usbController = new UsbController();
            if (!is_port_opened)
            {
                if (!usbController.ComPortOpen(serial_com_port) || !usbController.TransIotInit() || !usbController.NopCommand())
                {
                    MessageBox.Show("Port Init is failed!");
                    this.UsbBtn.Enabled = true;
                    return;
                }
                else
                {
                    is_port_opened = true;
                }
            }
            System.Threading.Thread.Sleep(200);

            if (started_time == string.Empty)
            {
                string sensor_receive_start = usbController.SensorConnection();
                if (sensor_receive_start == "date_error")
                {
                    this.UsbBtn.Enabled = true;
                    return;
                }
                else
                {
                    started_time = sensor_receive_start.Split(";")[0];
                    string end_time = sensor_receive_start.Split(";")[1];

                    sensor_info.SettingDates(started_time, end_time);
                    sensor_info.Show();
                    sensor_info.Location = new Point(this.Location.X + this.Size.Width / 2 - sensor_info.Size.Width / 2,
                                              this.Location.Y + this.Size.Height / 2 - sensor_info.Size.Height / 2);

                    usb_new_data_timer.Start();
                }

            }
        }

        public void usbDataStopping(object sender, EventArgs e)
        {
            MessageBox.Show("usbDataStopping");
            this.UsbBtn.Enabled = true;
            started_time = string.Empty;
            usb_total_timer.Enabled = false;
            usb_new_data_timer.Enabled = false;
            usb_new_data_timer = null;
            usb_total_timer = null;
        }
        public void usbNewDataReceive(object sender, EventArgs e)
        {
            sensor_info.Hide();
            MessageBox.Show("usbNewDataReceive");
            usbController.AllSenorData();
        }

        public string GetSerialPort()
        {
            var _port = "COM6";

            var cmd = Program.m_dbConnection.CreateCommand();
            string port_sql = "SELECT port FROM main_setting";
            cmd.CommandText = port_sql;

            var exist_status_reader = cmd.ExecuteReader();
            try
            {
                if (exist_status_reader.Read())
                {
                    _port = exist_status_reader.GetString(0);
                }
            }
            catch (System.Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }

            return _port;
        }
    }
}