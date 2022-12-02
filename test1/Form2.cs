using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace test1
{
    public partial class Form2 : Form
    {
        string[] itemLists = { "傾斜", "気温", "湿度", "気圧", "電池電圧" };

        public List<KeyUUID> key_uuid_list = new List<KeyUUID>();

        public Form2()
        {
            InitializeComponent();

            this.ControlBox = false;
            key_uuid_list = Constant.key_uuid_list;
            DisplayMainSetting();
            addDataTable(6);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url_text = "";
            DialogResult result = this.folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                url_text = this.folderBrowserDialog.SelectedPath;
                // MessageBox.Show(url_text);
                this.urlBox.Text = url_text;
            }
        }

        private void countBox_TextChanged(object sender, EventArgs e)
        {
            // string count = countBox.Text;
            // int val = 0;
            // if (Int32.TryParse(count, out val))
            // {
            //     int column_count = Convert.ToInt32(count);
            //     addDataTable(column_count);
            // }
        }

        public void addDataTable(int column_count)
        {
            int row_count = 5;

            DataTable dt = new DataTable();
            dt.Columns.Add("       ", typeof(string));
            dt.Columns.Add("e6f4cc1", typeof(bool));

            for (int i = 2; i < column_count; i++)
            {
                dt.Columns.Add("ID" + i.ToString(), typeof(bool));
            }

            for (int i = 0; i < row_count; i++)
            {
                dt.Rows.Add(create_row_obj(i, column_count));
            }

            dataGridView.DataSource = dt;
            dataGridView.RowHeadersVisible = false;
            dataGridView.Columns[0].ReadOnly = true;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ScrollBars = ScrollBars.None;
            dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView.ColumnHeadersHeight = 50;

            dataGridView.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Transparent;

            dataGridView.Height = dataGridView.ColumnHeadersHeight + dataGridView.RowTemplate.Height * row_count;
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 15);
            dataGridView.RowTemplate.Height = 35;
            dataGridView.Height = 35 + 35 * 5;

            for (int i = 0; i < column_count; i++)
            {
                dataGridView.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        public object[] create_row_obj(int row_number, int column_count)
        {
            object[] objs = new object[column_count];
            objs[0] = itemLists[row_number];
            if (row_number == 0)
            {
                objs[1] = key_uuid_list[0].is_gradient;
                objs[2] = key_uuid_list[1].is_gradient;
                objs[3] = key_uuid_list[2].is_gradient;
                objs[4] = key_uuid_list[3].is_gradient;
                objs[5] = key_uuid_list[4].is_gradient;
            }
            else if (row_number == 1)
            {
                objs[1] = key_uuid_list[0].is_temperature;
                objs[2] = key_uuid_list[1].is_temperature;
                objs[3] = key_uuid_list[2].is_temperature;
                objs[4] = key_uuid_list[3].is_temperature;
                objs[5] = key_uuid_list[4].is_temperature;
            }
            else if (row_number == 2)
            {
                objs[1] = key_uuid_list[0].is_humidity;
                objs[2] = key_uuid_list[1].is_humidity;
                objs[3] = key_uuid_list[2].is_humidity;
                objs[4] = key_uuid_list[3].is_humidity;
                objs[5] = key_uuid_list[4].is_humidity;
            }
            else if (row_number == 3)
            {
                objs[1] = key_uuid_list[0].is_pressure;
                objs[2] = key_uuid_list[1].is_pressure;
                objs[3] = key_uuid_list[2].is_pressure;
                objs[4] = key_uuid_list[3].is_pressure;
                objs[5] = key_uuid_list[4].is_pressure;
            }
            else if (row_number == 4)
            {
                objs[1] = key_uuid_list[0].is_voltage;
                objs[2] = key_uuid_list[1].is_voltage;
                objs[3] = key_uuid_list[2].is_voltage;
                objs[4] = key_uuid_list[3].is_voltage;
                objs[5] = key_uuid_list[4].is_voltage;
            }

            return objs;
        }

        private void minuteBox_TextChanged(object sender, EventArgs e)
        {
            string count = minuteBox.Text;
            int val = 0;
            if (Int32.TryParse(count, out val))
            {
            }
            else
            {
                minuteBox.Text = "";
            }
        }

        private void secondBox_TextChanged(object sender, EventArgs e)
        {
            string count = secondBox.Text;
            int val = 0;
            if (Int32.TryParse(count, out val))
            {
            }
            else
            {
                secondBox.Text = "";
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView.ClearSelection();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void GettingStatusFromTable()
        {
            try
            {
                var command = Program.m_dbConnection.CreateCommand();

                for (int i = 1; i < 6; i++)
                {
                    var is_gradient = dataGridView.Rows[0].Cells[i].Value.ToString() == "True" ? "1" : "0";
                    var is_temperature = dataGridView.Rows[1].Cells[i].Value.ToString() == "True" ? "1" : "0";
                    var is_humidity = dataGridView.Rows[2].Cells[i].Value.ToString() == "True" ? "1" : "0";
                    var is_pressure = dataGridView.Rows[3].Cells[i].Value.ToString() == "True" ? "1" : "0";
                    var is_voltage = dataGridView.Rows[4].Cells[i].Value.ToString() == "True" ? "1" : "0";

                    command.CommandText = "UPDATE sensor_setting SET gradient = '" + is_gradient
                        + "', humidity= '" + is_humidity + "',pressure='" + is_pressure + "',temperature='"
                        + is_temperature + "', voltage='" + is_voltage + "' WHERE id = " + i.ToString();
                    command.ExecuteNonQuery();

                }
            }
            catch
            {

            }
        }

        public bool CheckParseStrToInt(string _str)
        {
            int t;
            if (Int32.TryParse(_str, out t))
            {
                return true;
            }
            return false;
        }

        private void DisplayMainSetting()
        {
            try
            {
                var command = Program.m_dbConnection.CreateCommand();
                command.CommandText = "SELECT *FROM main_setting";
                command.ExecuteNonQuery();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    apiBox.Text = reader.GetString(1);
                    minuteBox.Text = reader.GetInt32(2).ToString();
                    secondBox.Text = reader.GetInt32(3).ToString();
                    urlBox.Text = reader.GetString(4).ToString();
                    countBox.Text = reader.GetInt32(5).ToString();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                ControlPaint.DrawCheckBox(e.Graphics, e.CellBounds.X + 1, e.CellBounds.Y + 1,
                    e.CellBounds.Width - 2, e.CellBounds.Height - 2,
                    (bool)e.FormattedValue ? ButtonState.Checked : ButtonState.Normal);
                e.Handled = true;
            }
        }

        private void StoreMainSetting()
        {
            string web_api = apiBox.Text;
            string diff_minute = minuteBox.Text;
            string diff_second = secondBox.Text;
            string uuid_count = countBox.Text;
            string store_path = urlBox.Text;

            if (CheckParseStrToInt(diff_minute) == false || CheckParseStrToInt(diff_second) == false
                || CheckParseStrToInt(uuid_count) == false)
            {
                MessageBox.Show("Please Input number in minute, second, count fields");
                return;
            }

            if (web_api == "" || CheckParseStrToInt(diff_second) == false
                || CheckParseStrToInt(uuid_count) == false)
            {
                MessageBox.Show("Please Input number in minute, second, count fields");
                return;
            }

            try
            {
                var command = Program.m_dbConnection.CreateCommand();
                string update_sql = "UPDATE main_setting SET  api_url='" + web_api + "',connection_time = " + diff_minute
                      + ", connection_interval= " + diff_second + ",store_url='" + store_path + "',display_count=" + uuid_count;

                command.CommandText = update_sql;
                command.ExecuteNonQuery();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            StoreMainSetting();
            GettingStatusFromTable();

            this.Close();
        }
    }
}
