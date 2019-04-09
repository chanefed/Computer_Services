using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;


namespace Computer_Services
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        private void SplashTimer_Tick(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            SplashTimer.Stop();
            this.Hide();
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            try
            {
                //Attempt to open a connection to verify the presence of the database
                lblConnection.Text = "Checking connection...";

                string connectionString;
                SqlConnection connection;

                connectionString = @"Data Source={SQL Server};SERVER=Nsccsqlinst16.nscc.edu;Database=CITC_CTEAM;UID=CITC_CTEAM;PWD=ITROCKS;";
                connection = new SqlConnection(connectionString);
                connection.Open();

                //Check to see if a connection was established.
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    lblConnection.Text = "Connection established!";
                }
                connection.Close();
            }
            catch (SqlException ex)
            {
                //If no connection, stop the timer and throw an error.
                SplashTimer.Stop();
                MessageBox.Show("Connection to the database could not be established. \nPlease try again later.\n\n"
                    + ex.Message);
                this.Close();
            }
        }
    }
}
