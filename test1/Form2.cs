using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        string[] itemLists = { "傾斜", "気温", "湿度", "雨量", "風速", "風向", "水位" };
        public Form2()
        {
            InitializeComponent();

            minuteBox.Text = "10";
            secondBox.Text = "10";
            countBox.Text = "7";
            addDataTable(7);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url_text = "";          
            DialogResult result = this.folderBrowserDialog.ShowDialog();
            if(result == DialogResult.OK)
            { 
                url_text = this.folderBrowserDialog.SelectedPath;
               // MessageBox.Show(url_text);
                this.urlBox.Text = url_text;
            }
        }

        private void countBox_TextChanged(object sender, EventArgs e)
        {
            string count = countBox.Text;
            int val = 0;            
            if (Int32.TryParse(count, out val)){
                int column_count = Convert.ToInt32(count);
                addDataTable(column_count);
            }
        }

        public void addDataTable(int column_count)
        {            
            int row_count = 7;

            DataTable dt = new DataTable();
            dt.Columns.Add("       ", typeof(string));
            dt.Columns.Add("e6f4cc1", typeof(bool));

            for (int i= 1; i <column_count; i++)
            {
                dt.Columns.Add("ID"+i.ToString(), typeof(bool));
            }
            
            for (int i=0; i <row_count; i++)
            {
                dt.Rows.Add(create_row_obj(i, column_count));
            }    

            dataGridView.DataSource = dt;
            dataGridView.RowHeadersVisible = false;
            dataGridView.Columns[0].ReadOnly =true;            
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ScrollBars = ScrollBars.None;            
            dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView.ColumnHeadersHeight = 50;

            dataGridView.Height = dataGridView.ColumnHeadersHeight + dataGridView.RowTemplate.Height * row_count;
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 13);
            

            for (int i = 0; i < column_count; i++)
            {
                dataGridView.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView.DefaultCellStyle.Alignment= DataGridViewContentAlignment.MiddleCenter;
            }
        }

        public object[] create_row_obj(int row_number, int column_count)
        {
            object[] objs = new object[column_count];
            objs[0] = itemLists[row_number];
            for(int i = 1; i < column_count ; i++)
            {
                objs[i]=false;
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

        }
    }
}
