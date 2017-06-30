using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Finisar.SQLite;
using System.IO;

namespace inventoryManagement1._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SQLiteConnection conn; // Declare the SQLiteConnection-Object
            conn = new SQLiteConnection(); // Create an instance of the object
            string dataSourcePath = Directory.GetCurrentDirectory() + "\\techChem.db";
            conn.ConnectionString = "Data Source= " + dataSourcePath + ";Version=3;"; // Set the ConnectionString
            conn.Open(); // Open the connection. Now you can fire SQL-Queries
            var queryCmd = conn.CreateCommand();

            queryCmd.CommandText = "SELECT * FROM currentInventory;";

            // Now the SQLiteCommand object can give us a DataReader-Object:
            var reader = queryCmd.ExecuteReader();

            reader.Read(); // Read() returns true if there is still a result line to read

            int F= Convert.ToInt32(reader["F"]); 
            int G = Convert.ToInt32(reader["G"]);
            int N = Convert.ToInt32(reader["N"]);
            int L = Convert.ToInt32(reader["L"]);
            reader.Close();


            dataGridMaterialView.Rows[0].Cells[0].Value = Convert.ToString(F);
            dataGridMaterialView.Rows[0].Cells[1].Value = Convert.ToString(G);
            dataGridMaterialView.Rows[0].Cells[2].Value = Convert.ToString(N);
            dataGridMaterialView.Rows[0].Cells[3].Value = Convert.ToString(L);




            conn.Close();
            productsCombo.Items.Add("A");
            productsCombo.Items.Add("B");
            productsCombo.Items.Add("C");
            productsCombo.Items.Add("D");

        }

        private void addMaterialBtn_Click(object sender, EventArgs e)
        {
            var time = DateTime.Now;
            int F = 0, G = 0, N = 0, L = 0;
            
            string dateTimeFormat = "{0}/{1}/{2}";
            string date = string.Format(dateTimeFormat, time.Year, time.Month, time.Day);

            if(dataGridMaterialInput.Rows[0].Cells[0].Value !=null) date = dataGridMaterialInput.Rows[0].Cells[0].Value.ToString();
            if (dataGridMaterialInput.Rows[0].Cells[1].Value != null) F = Convert.ToInt32(dataGridMaterialInput.Rows[0].Cells[1].Value.ToString());
            if (dataGridMaterialInput.Rows[0].Cells[2].Value != null) G = Convert.ToInt32(dataGridMaterialInput.Rows[0].Cells[2].Value.ToString());
            if (dataGridMaterialInput.Rows[0].Cells[3].Value != null) N = Convert.ToInt32(dataGridMaterialInput.Rows[0].Cells[3].Value.ToString());
            if (dataGridMaterialInput.Rows[0].Cells[4].Value != null)  L = Convert.ToInt32(dataGridMaterialInput.Rows[0].Cells[4].Value.ToString());


           


            SQLiteConnection conn; // Declare the SQLiteConnection-Object
            conn = new SQLiteConnection(); // Create an instance of the object
            string dataSourcePath = Directory.GetCurrentDirectory() + "\\techChem.db";
            conn.ConnectionString = "Data Source= " + dataSourcePath + ";Version=3;"; // Set the ConnectionString
            conn.Open(); // Open the connection. Now you can fire SQL-Queries
            var sql_cmd = conn.CreateCommand();

            /*sql_cmd.CommandText = "CREATE TABLE currentInventory ( id INTEGER PRIMARY KEY, F INTEGER, G INTEGER, N INTEGER, L INTEGER); CREATE TABLE inventory ( id INTEGER PRIMARY KEY ,date TEXT NOT NULL , F INTEGER NOT NULL, G INTEGER NOT NULL, N INTEGER NOT NULL, L INTEGER NOT NULL );CREATE TABLE sales ( date TEXT NOT NULL, batchNo INTEGER NOT NULL UNIQUE, product TEXT NOT NULL, quantity INTEGER NOT NULL, PRIMARY KEY(batchNo) )";
            sql_cmd.ExecuteNonQuery();*/ //create new empty database

            sql_cmd.CommandText = "insert into inventory (date,F,G,N,L) values ( '" + date + "'," + F + "," + G + "," + N + "," + L + ");";
            sql_cmd.ExecuteNonQuery();





            var queryCmd = conn.CreateCommand();
            
            queryCmd.CommandText = "SELECT * FROM currentInventory;";

            // Now the SQLiteCommand object can give us a DataReader-Object:
            var reader = queryCmd.ExecuteReader();

            reader.Read(); // Read() returns true if there is still a result line to read
            
                F += Convert.ToInt32(reader["F"]);
                G += Convert.ToInt32(reader["G"]);
                N += Convert.ToInt32(reader["N"]);
                L += Convert.ToInt32(reader["L"]);
                reader.Close();

            


            sql_cmd.CommandText = "UPDATE currentInventory SET F = "+Convert.ToString(F)+", G = " + Convert.ToString(G)+",N = "+Convert.ToString(N)+",L = "+Convert.ToString(L)+" WHERE id = 1;";
            sql_cmd.ExecuteNonQuery();

            dataGridMaterialView.Rows[0].Cells[0].Value = Convert.ToString(F);
            dataGridMaterialView.Rows[0].Cells[1].Value = Convert.ToString(G);
            dataGridMaterialView.Rows[0].Cells[2].Value = Convert.ToString(N);
            dataGridMaterialView.Rows[0].Cells[3].Value = Convert.ToString(L);


            conn.Close();

        }

        private void addProductBtn_Click(object sender, EventArgs e)
        {

            int productIdx = productsCombo.SelectedIndex;
            string product = productsCombo.Items[productIdx].ToString();
            string date = dateTxt.Text;
            int batchNo = Convert.ToInt32(batchTxt.Text);
            int quantity = Convert.ToInt32(quantityTxt.Text);

            


           if (product == "A") {
                float fp = .5F;
                float gp = .2F;
                float np = .1F;
                float lp = .05F;

                int f = Convert.ToInt32( quantity * fp);
                int g = Convert.ToInt32(quantity * gp);
                int n = Convert.ToInt32(quantity * np);
                int l = Convert.ToInt32(quantity * lp);

                SQLiteConnection conn; // Declare the SQLiteConnection-Object
                conn = new SQLiteConnection(); // Create an instance of the object
                string dataSourcePath = Directory.GetCurrentDirectory() + "\\techChem.db";
                conn.ConnectionString = "Data Source= " + dataSourcePath + ";Version=3;"; // Set the ConnectionString
                conn.Open(); // Open the connection. Now you can fire SQL-Queries
                var sql_cmd = conn.CreateCommand();

                /*sql_cmd.CommandText = "CREATE TABLE currentInventory ( id INTEGER PRIMARY KEY, F INTEGER, G INTEGER, N INTEGER, L INTEGER); CREATE TABLE inventory ( id INTEGER PRIMARY KEY ,date TEXT NOT NULL , F INTEGER NOT NULL, G INTEGER NOT NULL, N INTEGER NOT NULL, L INTEGER NOT NULL );CREATE TABLE sales ( date TEXT NOT NULL, batchNo INTEGER NOT NULL UNIQUE, product TEXT NOT NULL, quantity INTEGER NOT NULL, PRIMARY KEY(batchNo) )";
                sql_cmd.ExecuteNonQuery();*/ //create new empty database

                sql_cmd.CommandText = "insert into sales (date,batchNo,product,quantity) values ( '" + date + "'," + batchNo + ",'A'," + quantity +");";
                sql_cmd.ExecuteNonQuery();




                int F = 0, G = 0, N = 0, L = 0;
                var queryCmd = conn.CreateCommand();

                queryCmd.CommandText = "SELECT * FROM currentInventory;";

                // Now the SQLiteCommand object can give us a DataReader-Object:
                var reader = queryCmd.ExecuteReader();

                reader.Read(); // Read() returns true if there is still a result line to read

                F = Convert.ToInt32(reader["F"])-f;
                G = Convert.ToInt32(reader["G"])-g;
                N = Convert.ToInt32(reader["N"])-n;
                L = Convert.ToInt32(reader["L"])-l;
                reader.Close();




                sql_cmd.CommandText = "UPDATE currentInventory SET F = " + Convert.ToString(F) + ", G = " + Convert.ToString(G) + ",N = " + Convert.ToString(N) + ",L = " + Convert.ToString(L) + " WHERE id = 1;";
                sql_cmd.ExecuteNonQuery();

                dataGridMaterialView.Rows[0].Cells[0].Value = Convert.ToString(F);
                dataGridMaterialView.Rows[0].Cells[1].Value = Convert.ToString(G);
                dataGridMaterialView.Rows[0].Cells[2].Value = Convert.ToString(N);
                dataGridMaterialView.Rows[0].Cells[3].Value = Convert.ToString(L);


                conn.Close();


            }

        }
    }
}
