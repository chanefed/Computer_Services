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
    public partial class frmEditUser : Form
    {
        public frmEditUser()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //Check to see if there are any empty textboxes.
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    //Check to see if there are empty fields
                    TextBox textbox = control as TextBox;
                    if (textbox.Text == string.Empty)
                    {
                        MessageBox.Show("Missing user data. \n\n User data cannot have empty fields.");
                        DialogResult = DialogResult.None;

                        txtFirstName.Focus();
                        break;
                    }
                    else
                    {
                        //If not, then set the UserData properties and prepare to EDIT into the database
                        bool selectedSecurity;

                        UserData.FirstName = txtFirstName.Text;
                        UserData.LastName = txtLastName.Text;
                        UserData.UserName = txtUserName.Text;

                        //Make sure that the passwords match before setting the properties
                        if (picChecked.Visible == true)
                        {
                            UserData.PassWord = txtRePassword.Text;
                        }
                        else
                        {
                            MessageBox.Show("Passwords do not match. \n\n Resetting to current password.");
                            DialogResult = DialogResult.None;

                            picChecked.Visible = false;

                            txtPassword.Text = UserData.OldPassWord;
                            txtRePassword.Text = UserData.OldPassWord;

                            txtPassword.Focus();
                        }

                        //Make sure that the security level is selected. Default is Tech if nothing is selected.
                        if (cboSecurityLevel.SelectedIndex == 1)
                        {
                            selectedSecurity = true;
                            UserData.SecurityLevel = selectedSecurity;
                        }
                        else
                        {
                            selectedSecurity = false;
                            UserData.SecurityLevel = selectedSecurity;
                        }
                    }
                }
            }
        }
        //No numeric or symbol values entered for names
        private void txtFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            CharacterCheck(sender, e);
        }

        private void txtLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            CharacterCheck(sender, e);
        }
        private void CharacterCheck(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        private void txtRePassword_TextChanged(object sender, EventArgs e)
        {
            //make sure that the passwords match
            if (txtPassword.Text == txtRePassword.Text)
            {
                if (txtPassword.Text == String.Empty && txtRePassword.Text == String.Empty)
                {
                    picChecked.Visible = false;
                }
                else
                    picChecked.Visible = true;
            }
            else
                this.picChecked.Visible = false;
        }

        private void frmEditUser_Load(object sender, EventArgs e)
        {
            txtFirstName.Text = UserData.OldFirstName;
            txtLastName.Text = UserData.OldLastName;
            txtUserName.Text = UserData.OldUserName;

            //Connect to the Database to populate the password fields
            string connectionString;
            SqlConnection connection;

            connectionString = @"Data Source={SQL Server};SERVER=Nsccsqlinst16.nscc.edu;Database=CITC_CTEAM;UID=CITC_CTEAM;PWD=ITROCKS;";
            connection = new SqlConnection(connectionString);
            connection.Open();

            //Build the SELECT string to reach out to the DB and find the entered data
            string sqlSelect = "SELECT DISTINCT UserID, UserPassword, FName, LName, SecurityLevel " +
                 "FROM UserTable WHERE UserID = " +
                 "'" + UserData.OldUserName + "'";

            SqlCommand command = new SqlCommand(sqlSelect, connection);
            SqlDataReader sqlReader = command.ExecuteReader();

            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    UserData.OldPassWord = sqlReader["UserPassword"].ToString();
                    UserData.SecurityLevel = (bool)sqlReader["Securitylevel"];
                }
                txtPassword.Text = UserData.OldPassWord;
                txtRePassword.Text = UserData.OldPassWord;
                if (UserData.SecurityLevel)
                {
                    cboSecurityLevel.SelectedIndex = 1;
                }
                else
                    cboSecurityLevel.SelectedIndex = 0;
            }
            connection.Close();
        }
    }
}
