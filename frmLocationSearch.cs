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
    public partial class frmLocationSearch : Form
    {
        public frmLocationSearch()
        {
            InitializeComponent();
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            frmConfirmLogout logout = new frmConfirmLogout();

            DialogResult result = logout.ShowDialog();
            if (result == DialogResult.OK)
            {
                frmLogin newLogin = new frmLogin();
                newLogin.Show();
                this.Hide();
                logout.Dispose();
                this.Dispose();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            frmConfirmExit exit = new frmConfirmExit();

            DialogResult result = exit.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.Close();
                exit.Dispose();
                this.Dispose();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCampus.Clear();
            txtRoom.Clear();
            txtBuilding.Clear();

            txtCampus.Focus();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmSearch search = new frmSearch();
            search.Show();
            this.Hide();
            this.Dispose();
        }
        private void frmLocationSearch_Load(object sender, EventArgs e)
        {
            string username = UserData.LoggedFirstName + " " + UserData.LoggedLastName;
            lblLogged.Text = username;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //Logic here that will populate the grid view
            //Connect to the Database
            string connectionString;
            SqlConnection connection;

            //connectionString = @"NEED THE CONNECTION HERE";
            //connection = new SqlConnection(connectionString);
            //connection.Open();

            frmResultsGrid grid = new frmResultsGrid();
            grid.Show();
            this.Hide();
            this.Dispose();

            //Close the connection
            //connection.Close();
        }
    }
}
