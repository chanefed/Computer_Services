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

//PROGRAMMING II Final Project (Computer Services)
//UI for this software developed by SCF

//Windows forms that collect data and interact with a database allowing for add, edit, or deletion of 
//items in the DB pending certain security levels.
namespace Computer_Services
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPassword.Clear();
            txtUserName.Clear();
            txtUserName.Focus();
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            //Clear the username and password properties on form load for hacking reasons
            UserData.UserName = "";
            UserData.PassWord = "";
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                //Connect to the Database
                string connectionString;
                SqlConnection connection;

                connectionString = @"Data Source={SQL Server};SERVER=Nsccsqlinst16.nscc.edu;Database=CITC_CTEAM;UID=CITC_CTEAM;PWD=ITROCKS;";
                connection = new SqlConnection(connectionString);
                connection.Open();

                //Set the properties to be checked. Never send over the textBox contents directly.
                UserData.LoggedUserName = txtUserName.Text;
                UserData.LoggedPassWord = txtPassword.Text;

                //Build the SELECT string to reach out to the DB and find the entered data
                string sqlSelect = "SELECT DISTINCT UserID, UserPassword, FName, LName, SecurityLevel " +
                     "FROM UserTable WHERE UserID = " + 
                     "'" + UserData.LoggedUserName + "'" + " AND " + "UserPassword = " + "'" + UserData.LoggedPassWord + "'";

                SqlCommand command = new SqlCommand(sqlSelect, connection);
                SqlDataReader sqlReader = command.ExecuteReader();

                //If correct data is entered, open the next form
                //Set the admin rights to access user button on next form
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {  
                        UserData.LoggedFirstName = sqlReader["FName"].ToString();
                        UserData.LoggedLastName = sqlReader["LName"].ToString();
                        UserData.LoggedSecurityLevel = (bool)sqlReader["SecurityLevel"];
                    }

                    frmSearch search = new frmSearch();
                    search.Show();
                    this.Hide();
                    this.Dispose();
                }
                else if (!sqlReader.HasRows)
                {
                    //Incorrect data entered
                    MessageBox.Show("Incorrect username or password entered. \n                     Please try again.");
                    txtPassword.Clear();
                    txtUserName.Clear();
                    txtUserName.Focus();
                }
          
                else if (UserData.UserName == "" && UserData.PassWord == "")
                {
                    //No data entered
                    MessageBox.Show("Please enter a username and password.");
                    txtPassword.Clear();
                    txtUserName.Clear();
                    txtUserName.Focus();
                }
                //Close the connection
                connection.Close();
                connection.Dispose();
            }
            catch (SqlException ex)
            {
                //Error catching to the DB. Might need to be altered to reflect actual error
                MessageBox.Show("Connection to the database could not be established \n\n" + ex.Message);
                txtPassword.Clear();
                txtUserName.Clear();
                txtUserName.Focus();
            }
        }
    }
}
