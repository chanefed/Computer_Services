using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Computer_Services
{
    public partial class frmSearch : Form
    {
        public frmSearch()
        {
            InitializeComponent();
        }
        private void frmSearch_Load(object sender, EventArgs e)
        {
            string username = UserData.LoggedFirstName + " " + UserData.LoggedLastName;
            lblLogged.Text = username;

            if (UserData.LoggedSecurityLevel)
            {
                btnUser.Visible = true;
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

        private void btnLogout_Click(object sender, EventArgs e)
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

        private void btnComputer_Click(object sender, EventArgs e)
        {
            frmComputerSearch search = new frmComputerSearch();
            search.Show();
            this.Hide();
            this.Dispose();
        }

        private void btnLocation_Click(object sender, EventArgs e)
        {
            frmLocationSearch location = new frmLocationSearch();
            location.Show();
            this.Hide();
            this.Dispose();
        }

        private void btnSoftware_Click(object sender, EventArgs e)
        {
            frmSoftwareSearch software = new frmSoftwareSearch();
            software.Show();
            this.Hide();
            this.Dispose();
        }
        private void btnUser_Click(object sender, EventArgs e)
        {
            frmUserSearch users = new frmUserSearch();
            users.Show();
            this.Hide();
            this.Dispose();
        }
    }
}
