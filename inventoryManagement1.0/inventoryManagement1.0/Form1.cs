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

        }

        private void addMaterialBtn_Click(object sender, EventArgs e)
        {
            string datasource = Directory.GetCurrentDirectory()+ "techChem";
            SQLiteConnection sqlite_conn; // Declare the SQLiteConnection-Object

            sqlite_conn = new SQLiteConnection(); // Create an instance of the object
            sqlite_conn.ConnectionString = "Data Source=c:\\test-database.db;Version=3;New=True;Compress=True;"; // Set the ConnectionString
            sqlite_conn.Open(); // Open the connection. Now you can fire SQL-Queries
        }
    }
}
