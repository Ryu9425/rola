using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text.Json;
using System.Net.Http;
using MySqlX.XDevAPI.Common;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Data.SQLite;
using Org.BouncyCastle.Utilities.Collections;

namespace test1
{
    public partial class Detail : Form
    {
        string[] itemLists = { "傾斜", "気温", "湿度", "雨量", "風速", "風向", "水位" };

        // KeyUUID repeater = new KeyUUID("中継器","b7fe3929");
        // KeyUUID al_1 = new KeyUUID("AL1","f3eac187");
        // KeyUUID al_2 = new KeyUUID("AL2","53057b55");
        // KeyUUID al_3 = new KeyUUID("AL3","e1103333");
        // KeyUUID al_4 =new KeyUUID("AL4","fb2cd3bd"); 
        List<DisplayItem> display_item_list = new List<DisplayItem>();

        public bool is_initing = false;
        public DataTable dt;
        int total_items_count = 0;
        int per_page_count = 15;
        int current_page_group = 1;
        int current_page_index = 1;
        string current_display_name = "";
        string current_uuid = "";
        string current_standard = "";

        public Detail()
        {
            InitializeComponent();
            this.ControlBox = false;
            BaseDataAdding();
            Combos_Initing();
            AddDataTableIniting();
            AddDataTableContaining();
        }

        public void BaseDataAdding()
        {
            int sensor_setting_id = Constant.selected_uuid_index;

            try
            {
                var command = Program.m_dbConnection.CreateCommand();
                command.CommandText = "SELECT *FROM sensor_setting WHERE id = " + sensor_setting_id.ToString();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        current_display_name = reader.GetString(1) != "" ? reader.GetString(1) : "";
                        current_uuid = reader.GetString(2) != "" ? reader.GetString(2) : "";
                        current_standard = reader.GetValue(3).ToString();
                        displayBox.Text = current_display_name;
                        uuidBox.Text = current_uuid;
                        standardBox.Text = current_standard;
                    }
                }
            }
            catch
            {

            }
        }

        public void AddDataTableIniting()
        {
            dt = new DataTable();
            dt.Columns.Add("登録日", typeof(string));
            dt.Columns.Add("気温", typeof(string));
            dt.Columns.Add("湿度", typeof(string));
            dt.Columns.Add("気圧", typeof(string));
            dt.Columns.Add("傾斜角度", typeof(string));
            dt.Columns.Add("電池電圧", typeof(string));

            dataGridView.DataSource = dt;
            dataGridView.RowHeadersVisible = false;
            //  dataGridView.Columns[0].ReadOnly = true;
            //   dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ScrollBars = ScrollBars.None;
            //  dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            //  dataGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable; 
            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            for (int i = 0; i < 6; i++)
            {
                dataGridView.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        public void AddDataTableContaining()
        {
            dt.Clear();
            GettingFromDB();

            total_items_count = display_item_list.Count;
            current_page_group = 1;
            current_page_index = 1;

            ChangePaginating(current_page_index);
            Paginator_Buttons_Control();

            dataGridView.Height = dataGridView.ColumnHeadersHeight + dataGridView.RowTemplate.Height * 15 + 22;
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 14);
            dataGridView.ClearSelection();
        }

        public void ChangePaginating(int page_index)
        {
            dt.Clear();
            int end_index = total_items_count > per_page_count * page_index ? per_page_count * page_index : total_items_count;
            for (int i = per_page_count * (page_index - 1); i < end_index; i++)
            {
                dt.Rows.Add(create_row_obj(i));
            }
        }

        public void Paginator_Buttons_Control()
        {
            if (total_items_count < 5 * current_page_group * per_page_count)
            {
                lastBtn.Enabled = false;
            }
            else
            {
                lastBtn.Enabled = true;
            }
            if (current_page_group == 1)
            {
                firstBtn.Enabled = false;
            }
            else
            {
                firstBtn.Enabled = true;
            }
            btn_1.Text = (5 * (current_page_group - 1) + 1).ToString();
            btn_2.Text = (5 * (current_page_group - 1) + 2).ToString();
            btn_3.Text = (5 * (current_page_group - 1) + 3).ToString();
            btn_4.Text = (5 * (current_page_group - 1) + 4).ToString();
            btn_5.Text = (5 * (current_page_group - 1) + 5).ToString();

            if ((5 * (current_page_group - 1) + 1) * per_page_count < total_items_count)
                btn_2.Show();
            else
                btn_2.Hide();
            if ((5 * (current_page_group - 1) + 2) * per_page_count < total_items_count)
                btn_3.Show();
            else
                btn_3.Hide();
            if ((5 * (current_page_group - 1) + 3) * per_page_count < total_items_count)
                btn_4.Show();
            else
                btn_4.Hide();
            if ((5 * (current_page_group - 1) + 4) * per_page_count < total_items_count)
                btn_5.Show();
            else
                btn_5.Hide();

            if (current_page_index == 1)
            {
                preBtn.Enabled = false;
            }
            else
            {
                preBtn.Enabled = true;
            }
            if (current_page_index == 5 * current_page_group || per_page_count * current_page_index > total_items_count)
            {
                nextBtn.Enabled = false;
            }
            else if (per_page_count * 5 * current_page_group > total_items_count)
            {
                nextBtn.Enabled = false;
            }
            else
            {
                nextBtn.Enabled = true;
            }
        }

        public object[] create_row_obj(int row_number)
        {
            object[] objs = new object[6];
            objs[0] = display_item_list[row_number].sensor_time;
            objs[1] = display_item_list[row_number].temperature;
            objs[2] = display_item_list[row_number].humidity;
            objs[3] = display_item_list[row_number].pressure;
            decimal diff_gradient = Math.Round(decimal.Parse(standardBox.Text), 1) - Math.Round(decimal.Parse(display_item_list[row_number].gradient), 1);
            objs[4] = diff_gradient;
            objs[5] = display_item_list[row_number].voltage;

            return objs;
        }


        private void Combos_Initing()
        {
            for (int i = 2020; i < 2029; i++)
            {
                fromYComboBox.Items.Add(i.ToString());
                toYComboBox.Items.Add(i.ToString());
            }
            for (int i = 1; i < 13; i++)
            {
                fromMComboBox.Items.Add(i.ToString());
                toMComboBox.Items.Add(i.ToString());
            }

            DateTime current_date = DateTime.Now;
            var date = current_date.Date;

            fromYComboBox.Text = date.Year.ToString();
            toYComboBox.Text = date.Year.ToString();
            fromMComboBox.Text = date.Month.ToString();
            toMComboBox.Text = date.Month.ToString();
            fromDComboBox.Text = date.Day.ToString();
            toDComboBox.Text = date.Day.ToString();

            Add_fromday_combo();
            Add_today_combo();
            is_initing = true;
        }

        public void Add_fromday_combo()
        {

            fromDComboBox.Items.Clear();

            int year = Convert.ToInt32(fromYComboBox.Text);
            int month = Convert.ToInt32(fromMComboBox.Text);

            int day_count = 31;

            if (month % 2 == 0 && month < 8)
            {
                day_count = 30;
            }
            else if (month % 2 != 0 && month > 8)
            {
                day_count = 30;
            }

            if (month == 2) day_count = 28;
            if (year % 4 == 0 && month == 2) day_count = 29;
            for (int i = 1; i <= day_count; i++)
            {
                fromDComboBox.Items.Add(i.ToString());
            }
            fromDComboBox.Text = "1";
        }
        public void Add_today_combo()
        {
            toDComboBox.Items.Clear();
            int year = Convert.ToInt32(toYComboBox.Text);
            int month = Convert.ToInt32(toMComboBox.Text);

            int day_count = 31;

            if (month % 2 == 0 && month < 8)
            {
                day_count = 30;
            }
            else if (month % 2 != 0 && month > 8)
            {
                day_count = 30;
            }

            if (month == 2) day_count = 28;
            if (year % 4 == 0 && month == 2) day_count = 29;
            for (int i = 1; i <= day_count; i++)
            {
                toDComboBox.Items.Add(i.ToString());
            }
        }

        private void FromYComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (is_initing) Add_fromday_combo();
        }

        private void FromMComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (is_initing) Add_fromday_combo();
        }

        private void FromDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //filtering...
        }

        private void ToYComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (is_initing) Add_today_combo();
        }

        private void ToMComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (is_initing) Add_today_combo();
        }

        private void ToDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //filtering...
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string new_uuid = uuidBox.Text;
            string new_display_name = displayBox.Text;
            string new_standard = standardBox.Text;

            if (new_uuid == "" || new_display_name == "" || new_standard == "")
            {
                MessageBox.Show("Please input sensor datas!");
                return;
            }
            float f;
            if (float.TryParse(new_standard, out f))
            {
                float roundedTemp = (float)Math.Round(decimal.Parse(new_standard), 1);
            }
            else
            {
                MessageBox.Show("Please input as number into standard degree part!");
                return;
            }
            try
            {
                var cmd = Program.m_dbConnection.CreateCommand();
                string get_sensor_setting_sql = "UPDATE sensor_setting SET display_name = '" + new_display_name + "', uuid= '" + new_uuid + "',standard_value='" + new_standard + "' WHERE id =" + Constant.selected_uuid_index;

                cmd.CommandText = get_sensor_setting_sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

            //searching....
            AddDataTableContaining();
        }

        private void GettingFromDB()
        {

            string from_month = fromMComboBox.Text.Length == 1 ? "0" + fromMComboBox.Text : fromMComboBox.Text;
            string from_day = fromDComboBox.Text.Length == 1 ? "0" + fromDComboBox.Text : fromDComboBox.Text;
            string to_month = toMComboBox.Text.Length == 1 ? "0" + toMComboBox.Text : toMComboBox.Text;
            string to_day = toDComboBox.Text.Length == 1 ? "0" + toDComboBox.Text : toDComboBox.Text;

            string from_date = fromYComboBox.Text + "-" + from_month + "-" + from_day;
            string to_date = toYComboBox.Text + "-" + to_month + "-" + to_day;

            string selected_uuid = uuidBox.Text;

            try
            {
                var cmd = Program.m_dbConnection.CreateCommand();
                string get_display_sql = "SELECT *FROM display WHERE datetime>'" + from_date
                + " 00:00:00' AND datetime<'" + to_date + " 24:00:00' AND uuid='" + selected_uuid + "' ORDER BY datetime";

                cmd.CommandText = get_display_sql;

                var display_data_reader = cmd.ExecuteReader();

                display_item_list.Clear();

                while (display_data_reader.Read())
                {
                    DisplayItem item = new DisplayItem();
                    item.temperature = display_data_reader.GetString(1);
                    item.humidity = display_data_reader.GetString(2);
                    item.voltage = display_data_reader.GetString(3);
                    item.pressure = display_data_reader.GetString(4);
                    item.gradient = display_data_reader.GetString(5);
                    item.uuid = display_data_reader.GetString(6);
                    item.sensor_time = display_data_reader.GetString(7);
                    item.datetime = display_data_reader.GetString(8);
                    display_item_list.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void firstBtn_Click(object sender, EventArgs e)
        {
            current_page_index = 1;
            current_page_group = 1;
            Paginator_Buttons_Control();
            ChangePaginating(current_page_index);
        }

        private void lastBtn_Click(object sender, EventArgs e)
        {
            current_page_index = (total_items_count % per_page_count) == 0 ? (total_items_count / per_page_count) : (total_items_count / per_page_count) + 1;
            current_page_group = (current_page_index % 5) == 0 ? (current_page_index / 5) : (current_page_index / 5) + 1;
            Paginator_Buttons_Control();
            ChangePaginating(current_page_index);
        }

        private void preBtn_Click(object sender, EventArgs e)
        {
            current_page_group = current_page_group - 1;
            current_page_index = 5 * (current_page_group - 1) + 1;
            Paginator_Buttons_Control();
            ChangePaginating(current_page_index);
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            current_page_group = current_page_group + 1;
            current_page_index = 5 * (current_page_group - 1) + 1;
            Paginator_Buttons_Control();
            ChangePaginating(current_page_index);
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            current_page_index = 5 * (current_page_group - 1) + 1;
            ChangePaginating(current_page_index);
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            current_page_index = 5 * (current_page_group - 1) + 2;
            ChangePaginating(current_page_index);
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            current_page_index = 5 * (current_page_group - 1) + 3;
            ChangePaginating(current_page_index);
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            current_page_index = 5 * (current_page_group - 1) + 4;
            ChangePaginating(current_page_index);
        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            current_page_index = 5 * (current_page_group - 1) + 5;
            ChangePaginating(current_page_index);
        }

        private void displayBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void uuidBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void standardBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            var headers = dataGridView.Columns.Cast<DataGridViewColumn>();
            sb.AppendLine(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"").ToArray()));

            // foreach (DataGridViewRow row in dataGridView.Rows)
            // {
            //     var cells = row.Cells.Cast<DataGridViewCell>();
            //     sb.AppendLine(string.Join(",", cells.Select(cell => "\"" + cell.Value + "\"").ToArray()));
            // }

            foreach (DisplayItem item in display_item_list)
            {
                decimal diff_gradient = Math.Round(decimal.Parse(standardBox.Text), 1) - Math.Round(decimal.Parse(item.gradient), 1);

                string row = item.sensor_time + "," + item.temperature + "," + item.humidity + "," + item.pressure + "," + diff_gradient.ToString() + ","
                       + item.voltage;

                sb.AppendLine(row);
            }

            string content = sb.ToString();

            if (Constant.store_path.Length < 4)
            {
                MessageBox.Show("Please directory!");
                return;
            }

            string export_path = Constant.store_path + "/" + uuidBox.Text + "_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".csv";

            System.IO.File.WriteAllText(export_path, content, Encoding.UTF8);
            MessageBox.Show(export_path + "  exported!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string new_uuid = uuidBox.Text;
            string new_display_name = displayBox.Text;
            string new_standard = standardBox.Text;

            if (new_uuid == "" || new_display_name == "" || new_standard == "")
            {
                MessageBox.Show("Please input sensor datas!");
                return;
            }
            float f;
            if (float.TryParse(new_standard, out f))
            {
                float roundedTemp = (float)Math.Round(decimal.Parse(new_standard), 1);
            }
            else
            {
                MessageBox.Show("Please input as number into standard degree part!");
                return;
            }

            try
            {
                var cmd = Program.m_dbConnection.CreateCommand();
                string get_sensor_setting_sql = "UPDATE sensor_setting SET display_name = '" + new_display_name + "', uuid= '" + new_uuid + "',standard_value='" + new_standard + "' WHERE id =" + Constant.selected_uuid_index;

                cmd.CommandText = get_sensor_setting_sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }


            this.Close();
        }
    }
}
