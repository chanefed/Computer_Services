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
    public partial class frmConfirmDelete : Form
    {
        public frmConfirmDelete()
        {
            InitializeComponent();
        }

        private void frmConfirmDelete_Load(object sender, EventArgs e)
        {
            lblUser.Text = UserData.OldUserName;
        }
    }
}
