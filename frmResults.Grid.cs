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
    public partial class frmResultsGrid : Form
    {
        public frmResultsGrid()
        {
            InitializeComponent();
        }

        private void frmResultsGrid_Load(object sender, EventArgs e)
        {
            string username = UserData.FirstName + " " + UserData.LastName;
            lblLogged.Text = username;
        }
    }
}
