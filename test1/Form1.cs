using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using static System.Net.Mime.MediaTypeNames;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace test1
{
    public partial class Form1 : Form
    {

        private MySqlConnection connection;
        private string server;
        private string database;
        private string username;
        private string password;

       

        public Form1()
        {
            InitializeComponent();
            mysql_db_connection();
            addDataTable();
            addDataTable();
            addDataTable();
            addDataTable();
        }

        public void addDataTable()
        {
            string[] itemLists =  { "�X��", "�C��", "���x", "�J��", "����", "����", "����" };
            int row_count = 7;

            DataTable dt = new DataTable();
            dt.Columns.Add("ColA", typeof(string));
            dt.Columns.Add("ColB", typeof(string));   

            dt.Rows.Add(new object[] { itemLists[0], "a" });
            dt.Rows.Add(new object[] { itemLists[1], "b" });
            dt.Rows.Add(new object[] { itemLists[2], "c" });
            dt.Rows.Add(new object[] { itemLists[3], "d" });
            dt.Rows.Add(new object[] { itemLists[4], "e" });
            dt.Rows.Add(new object[] { itemLists[5], "f" });
            dt.Rows.Add(new object[] { itemLists[6], "g" });

            DataGridView dataGridView = new DataGridView();
            this.flowLayoutPanel1.Controls.Add(dataGridView);            

            dataGridView.DataSource = dt;
            dataGridView.RowHeadersVisible = false;
            dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ScrollBars = ScrollBars.None;
            dataGridView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Height = dataGridView.ColumnHeadersHeight+dataGridView.RowTemplate.Height*row_count;
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 13);
            dataGridView.Width = 150;
        }

        private void db_connection()
        {
            using (var connection = new SQLiteConnection("Data Source=hamster.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT *FROM status";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(1);
                        

                        Console.WriteLine($"Hello, {name}!");
                        //MessageBox.Show("name:" + name);
                    }
                }
            }
        }

        private void mysql_db_connection()
        {
            server = "localhost";
            database = "c_shap";
            username = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        private void button_Click(object sender, EventArgs e)
        {
            Form form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        //Insert statement
        public void Insert()
        {
            string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update()
        {
            string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete()
        {
            string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
    }
}