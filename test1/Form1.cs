using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;

namespace test1
{
    public partial class Form1 : Form
    {   
        SQLiteConnection m_dbConnection;
        string connection_path = "Data Source=" + Path.Combine(Directory.GetCurrentDirectory(), "rola.db");
        string[] itemLists = { "傾斜", "気温", "湿度", "気圧", "電池電圧" };

        List<KeyUUID> key_uuid_list = new List<KeyUUID>();
        public Form1()
        {
            InitializeComponent();
            AddDateTime();
            GetKeyUUID_Datas();
            SensorDatasView();            
        }

        public void GetKeyUUID_Datas(){
            m_dbConnection = new SQLiteConnection(connection_path);
                       
            try{
                m_dbConnection.Open();                
                var command = m_dbConnection.CreateCommand();
                command.CommandText ="SELECT *FROM sensor_setting ORDER BY id";
                using (var reader = command.ExecuteReader())
                {
                    key_uuid_list.Clear();
                    while (reader.Read())
                    {    
                        KeyUUID key_uuid = new KeyUUID();   
                        key_uuid.display_name = reader.GetString(1);
                        key_uuid.uuid = reader.GetString(2);
                        key_uuid.standard = reader.GetValue(3).ToString();
                        key_uuid.is_gradient=reader.GetInt32(4)==1?true:false;
                        key_uuid.is_humidity=reader.GetInt32(5)==1?true:false;
                        key_uuid.is_temperature=reader.GetInt32(6)==1?true:false;
                        key_uuid.is_pressure=reader.GetInt32(7)==1?true:false;
                        key_uuid.is_voltage=reader.GetInt32(8)==1?true:false;

                        key_uuid_list.Add(key_uuid);
                    }
                }
            }catch{
            }
            m_dbConnection.Close();  
        }

        public void SensorDatasView()
        {
            AddDataTable(dataGridView_1,0);
            AddDataTable(dataGridView_2,1);
            AddDataTable(dataGridView_3,2);
            AddDataTable(dataGridView_4,3);
            AddDataTable(dataGridView_5,4);
        }
        public void AddDataTable(DataGridView dataGridView, int _id)
        {            
            DataTable dt = new DataTable();
            dt.Columns.Add("ColA", typeof(string));
            dt.Columns.Add("ColB", typeof(string));   

            m_dbConnection = new SQLiteConnection(connection_path);
            string uuid=key_uuid_list[_id].uuid; 
            int row_count=0;
          //  MessageBox.Show(uuid);
            
            try{
                m_dbConnection.Open();                
                var command = m_dbConnection.CreateCommand();
                command.CommandText ="SELECT *FROM display WHERE uuid='"+uuid+"' ORDER BY datetime DESC";
               
                using (var reader = command.ExecuteReader())
                {
                   // MessageBox.Show("SELECT *FROM display WHERE uuid='" + uuid + "' ORDER BY datetime DESC");
                    if (reader.Read())
                    {                       
                        var temperature= reader.GetString(1)!=""?reader.GetString(1):"";
                        var humidity=reader.GetString(2) != "" ? reader.GetString(2) : "";
                        var voltage = reader.GetValue(3).ToString();
                        var pressure=reader.GetString(4) != "" ? reader.GetString(4) : "";
                        var gradient=reader.GetString(5) != "" ? reader.GetString(5) : "";  
                         
                        if(key_uuid_list[_id].is_gradient) {
                            dt.Rows.Add(new object[]{itemLists[0],gradient+"°"});
                            row_count++;
                        }
                        if(key_uuid_list[_id].is_temperature) {
                            dt.Rows.Add(new object[]{itemLists[1],temperature+"℃"});
                            row_count++;
                        }
                        if(key_uuid_list[_id].is_humidity){
                            dt.Rows.Add(new object[]{itemLists[2],humidity+"%"});
                            row_count++;
                        } 
                        if(key_uuid_list[_id].is_pressure){
                            dt.Rows.Add(new object[]{itemLists[3],pressure+"hPa"});
                            row_count++;
                        } 
                        if(key_uuid_list[_id].is_voltage) {
                            dt.Rows.Add(new object[]{itemLists[4],voltage+"V"});
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
                            dt.Rows.Add(new object[] { itemLists[1],"" });
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
            }catch{

            }
            m_dbConnection.Close();                 

            dataGridView.DataSource = dt; 
            dataGridView.Height = 35*row_count;
            DataGridViewStyiling(dataGridView);                
        }

        public void AddDateTime()
        {       
            m_dbConnection = new SQLiteConnection(connection_path);           
            
            try{
                m_dbConnection.Open();                
                var command = m_dbConnection.CreateCommand();
                command.CommandText ="SELECT MAX(datetime) FROM display";
               
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {                       
                        var date_time= reader.GetString(0);  
                        
                        string day = date_time.Split(" ")[0];  
                        string time = date_time.Split(" ")[1];                         
                        string dis = day.Split("-")[0]+"年"+day.Split("-")[1]+"年"+day.Split("-")[2]+"日";
                        dateLabel.Text = dis;
                        timeLabel.Text= time;                                             
                    }
                }
            }catch{

            }
            m_dbConnection.Close();  
        }

        private void DataGridViewStyiling(DataGridView dataGridView){
            dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ScrollBars = ScrollBars.None;
            dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.RowTemplate.Height=35;
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 15);
            dataGridView.Width = 230; 
        }

        private void button_Click(object sender, EventArgs e)
        {
            Form form2 = new Form2();
            this.Hide();
            form2.ShowDialog();
            this.Show();
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
        }
       
    }
}