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
  /*  public class Item
    {
        public string sensorid { set; get; }
        public string uuid { set; get; }
        public string data_id { set; get; }
        public string data { set; get; }
        public string datetime { set; get; }
    }*/
    public partial class Detail : Form
    {
        string[] itemLists = { "傾斜", "気温", "湿度", "雨量", "風速", "風向", "水位" };
        public bool is_initing = false;
        public Detail()
        {
            InitializeComponent();
            Debug.Print("print...");
        //    wepDataIniting();
            combos_initing();
            addDataTable();
        }

        public async void wepDataIniting()
        {
            HttpClient client = new HttpClient();
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    module = "uck9JBnekzPe",
                    datetime = "2022-11-26 23:24:00"
                }),
                Encoding.UTF8,
                "application/json");
            using HttpResponseMessage response = await client.PostAsync("https://collapse.sakura.ne.jp/getstream.php", jsonContent);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            Item[] items = JsonSerializer.Deserialize<Item[]>(jsonResponse);

            if (items != null)
            {
                SQLiteConnection m_dbConnection;
                var connection_path = "Data Source=" + Path.Combine(Directory.GetCurrentDirectory(), "rola.db");
                m_dbConnection = new SQLiteConnection(connection_path);

                MessageBox.Show(connection_path);

                
                try
                {
                    m_dbConnection.Open();

                    foreach (var item in items){
                      /*  var cmd = m_dbConnection.CreateCommand();                            
                        string check_sql = "SELECT COUNT('sensorid') FROM sensors WHERE sensorid='" + item.sensorid + "'";
                        cmd.CommandText = check_sql;

                        var exist_status_reader = cmd.ExecuteReader();
                        while (exist_status_reader.Read())
                        {
                            int myreader = exist_status_reader.GetInt32(0);
                            if(myreader == 0)
                            {*/
                                var insert_cmd = m_dbConnection.CreateCommand();
                                string inser_sql = "INSERT INTO sensors('sensorid','uuid','data_id','data','datetime') VALUES('" + item.sensorid + "', '"
                                    + item.uuid + "', '" + item.data_id + "','" + item.data + "', '" + item.datetime + "')";
                                insert_cmd.CommandText = inser_sql;
                                insert_cmd.ExecuteNonQuery(); 
                       /*     }
                        } */
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }                   
                

                m_dbConnection.Close();
            }

            Debug.Print(jsonResponse);

            return;
        }

        public void addDataTable()
        {
            int row_count = 15;

            DataTable dt = new DataTable();
            dt.Columns.Add("登録日", typeof(string));
            dt.Columns.Add("気温", typeof(string));
            dt.Columns.Add("湿度", typeof(string));
            dt.Columns.Add("気圧", typeof(string));
            dt.Columns.Add("傾斜角度", typeof(string));


            for (int i = 0; i < row_count; i++)
            {
                dt.Rows.Add(create_row_obj(i));
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

            dataGridView.Height = dataGridView.ColumnHeadersHeight + dataGridView.RowTemplate.Height * row_count;
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 13);


            for (int i = 0; i < 5; i++)
            {
                dataGridView.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        public object[] create_row_obj(int row_number)
        {
            object[] objs = new object[5];

            for (int i = 0; i < 5; i++)
            {
                objs[i] = false;
            }
            return objs;
        }


        private void combos_initing()
        {
            for (int i = 2020; i < 2099; i++)
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

            add_fromday_combo();
            add_today_combo();
            is_initing = true;
        }

        public void add_fromday_combo()
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
        }
        public void add_today_combo()
        {
            toDComboBox.Items.Clear();
            int year = Convert.ToInt32(toYComboBox.Text);
            int month = Convert.ToInt32(toMComboBox.Text);

            int day_count = month % 2 == 0 ? 30 : 31;
            if (month == 2) day_count = 28;
            if (year % 4 == 0 && month == 2) day_count = 29;
            for (int i = 1; i <= day_count; i++)
            {
                toDComboBox.Items.Add(i.ToString());
            }
        }

        private void fromYComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (is_initing) add_fromday_combo();
        }

        private void fromMComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (is_initing) add_fromday_combo();
        }

        private void fromDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //filtering...
        }

        private void toYComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (is_initing) add_today_combo();
        }

        private void toMComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (is_initing) add_today_combo();
        }

        private void toDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //filtering...
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            //searching....
        }
    }
}
