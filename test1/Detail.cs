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

        List<DisplayItem> display_item_list = new List<DisplayItem>();
        
        public bool is_initing = false;
        public DataTable dt;
        public Detail()
        {
            InitializeComponent();
            Combos_Initing();
            AddDataTableIniting(); 
            AddDataTableContaining();           
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
            dataGridView.ScrollBars = ScrollBars.Both;         
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
            int row_count = display_item_list.Count;

            for (int i = 0; i < display_item_list.Count; i++)
            {
                dt.Rows.Add(create_row_obj(i));
            }
           // dataGridView.Height = dataGridView.ColumnHeadersHeight + dataGridView.RowTemplate.Height * (row_count-1);
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 13);  
        }

        public object[] create_row_obj(int row_number)
        {
            object[] objs = new object[6];
            objs[0]=display_item_list[row_number].datetime;
            objs[1]=display_item_list[row_number].temperature;
            objs[2]=display_item_list[row_number].humidity;
            objs[3]=display_item_list[row_number].pressure;
            objs[4]=display_item_list[row_number].gradient;
            objs[5]=display_item_list[row_number].voltage;
            
            return objs;
        }


        private void Combos_Initing()
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
        }
        public void Add_today_combo()
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
            //searching....
            AddDataTableContaining();
        }

        private void GettingFromDB(){

            string from_month =  fromMComboBox.Text.Length==1?"0"+fromMComboBox.Text:fromMComboBox.Text;
            string from_day =  fromDComboBox.Text.Length==1?"0"+fromDComboBox.Text:fromDComboBox.Text;
            string to_month =  toMComboBox.Text.Length==1?"0"+toMComboBox.Text:toMComboBox.Text;
            string to_day =  toDComboBox.Text.Length==1?"0"+toDComboBox.Text:toDComboBox.Text;
            
            string from_date = fromYComboBox.Text+"-"+from_month+"-"+from_day;
            string to_date = toYComboBox.Text+"-"+to_month+"-"+to_day;

            SQLiteConnection m_dbConnection;
            var connection_path = "Data Source=" + Path.Combine(Directory.GetCurrentDirectory(), "rola.db");
            m_dbConnection = new SQLiteConnection(connection_path);

            try
            {
                m_dbConnection.Open();
                
                var cmd = m_dbConnection.CreateCommand();                            
                string get_display_sql = "SELECT *FROM display WHERE datetime>'"+from_date
                +" 00:00:00' AND datetime<'"+to_date+" 24:00:00'";

                //get_display_sql = "SELECT *FROM display WHERE datetime>'2022-11-29 00:00:00' AND datetime<='2022-11-29 24:00:00'";
                
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
                    item.datetime = display_data_reader.GetString(7);   
                    display_item_list.Add(item);              
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
