using Finisar.SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventoryManagement1._0
{
    public partial class productionHistoryForm : Form
    {
        public productionHistoryForm()
        {
            InitializeComponent();
        }

        private void productionHistoryForm_Load(object sender, EventArgs e)
        {

            DataSet DT = new DataSet();
            var con = new SQLiteConnection(); // Create an instance of the object
            string dataSourcePath = Directory.GetCurrentDirectory() + "\\techChem.db";
            con.ConnectionString = "Data Source= " + dataSourcePath + ";Version=3;"; // Set the ConnectionString
            con.Open(); // Open the connection. Now you can fire SQL-Queries
          //con.Open();
            var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM sales";
            var adapter = new SQLiteDataAdapter(cmd);
            adapter.AcceptChangesDuringFill = false;
            adapter.Fill(DT);
            con.Close();

            
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = DT.Tables[0];


            DataSet DT2 = new DataSet();
            cmd.CommandText = "SELECT * FROM inventory";
            var adapter1 = new SQLiteDataAdapter(cmd);
            adapter1.AcceptChangesDuringFill = false;
            adapter1.Fill(DT2);
            con.Close();


            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = DT2.Tables[0];



        }
    }
}
