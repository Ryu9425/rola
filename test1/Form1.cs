using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;

namespace test1
{
    public partial class Form1 : Form
    {       
        string[] itemLists = { "傾斜", "気温", "湿度", "気圧", "電池電圧" };

        List<KeyUUID> key_uuid_list = new List<KeyUUID>();
        public Form1()
        {
            InitializeComponent();
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
            catch
            {
            }
            Constant.key_uuid_list = key_uuid_list;
        }

        public void SensorDatasView()
        {
            AddDataTable(dataGridView_1, 0);
            AddDataTable(dataGridView_2, 1);
            AddDataTable(dataGridView_3, 2);
            AddDataTable(dataGridView_4, 3);
            AddDataTable(dataGridView_5, 4);
        }
        public void AddDataTable(DataGridView dataGridView, int _id)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ColA", typeof(string));
            dt.Columns.Add("ColB", typeof(string));
            string uuid = key_uuid_list[_id].uuid;
            int row_count = 0;
            //  MessageBox.Show(uuid);
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
            catch
            {

            }

            dataGridView.DataSource = dt;
            dataGridView.Height = 35 * row_count;
            DataGridViewStyiling(dataGridView);
        }

        public void AddDateTime()
        {
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
                        string dis = day.Split("-")[0] + "年" + day.Split("-")[1] + "年" + day.Split("-")[2] + "日";
                        dateLabel.Text = dis;
                        timeLabel.Text = time;
                    }
                }
            }
            catch
            {
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
    }
}