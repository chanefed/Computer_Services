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
    public partial class frmAddUser : Form
    {
        public frmAddUser()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Check to see if there are any empty textboxes.
            foreach(Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    TextBox textbox = control as TextBox;
                    if (textbox.Text == string.Empty)
                    {
                        MessageBox.Show("Missing new user data. \n\n User data entry cannot have empty fields.");
                        DialogResult = DialogResult.None;

                        txtFirstName.Focus();
                        txtPassword.Clear();
                        txtRePassword.Clear();
                        picChecked.Visible = false;
                        break;
                    }
                    else
                    {
                        //If not, then set the UserData properties and prepare to INSERT into the database
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
                            MessageBox.Show("Passwords do not match. \n\n Please re-enter the passwords.");
                            DialogResult = DialogResult.None;

                            txtPassword.Clear();
                            txtRePassword.Clear();

                            txtPassword.Focus();
                            picChecked.Visible = false;
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
    }
}
