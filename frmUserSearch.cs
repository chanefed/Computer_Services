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
    public partial class frmUserSearch : Form
    {
        public frmUserSearch()
        {
            InitializeComponent();
        }

        //Set the logged on label to the current user
        private void frmUserSearch_Load(object sender, EventArgs e)
        {
            string username = UserData.LoggedFirstName + " " + UserData.LoggedLastName;
            lblLogged.Text = username;

            PopulateUserGrid();
        }

        //Sign out 
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

        //Exit button
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

        //delete button
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (userGrid.CurrentRow.Cells[2].Value.ToString() == UserData.LoggedUserName)
            {
                MessageBox.Show("Cannot delete users currently logged in. \n Please have another admin delete your information");
            }
            else
            {
            frmConfirmDelete delete = new frmConfirmDelete();

            DialogResult result = delete.ShowDialog();
                if (result == DialogResult.OK)
                {
                    try
                    {
                        //Connect to the Database
                        string connectionString;
                        SqlConnection connection;

                        connectionString = @"Data Source={SQL Server};SERVER=Nsccsqlinst16.nscc.edu;Database=CITC_CTEAM;UID=CITC_CTEAM;PWD=ITROCKS;";
                        connection = new SqlConnection(connectionString);
                        connection.Open();

                        //Build the DELETE string to reach out to the DB and delete the entered data
                        string sqlDelete = "DELETE FROM UserTable WHERE UserID = '" + UserData.OldUserName + "'";

                        SqlCommand command = new SqlCommand(sqlDelete, connection);
                        command.ExecuteNonQuery();

                        UserData.OldUserName = "";

                        connection.Close();
                        PopulateUserGrid();
                        MessageBox.Show("User sucessfully deleted.");

                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("An error has occured. User not deleted.\n" + ex.Message);
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (userGrid.CurrentRow.Cells[2].Value.ToString() == UserData.LoggedUserName)
            {
                MessageBox.Show("Cannot edit users currently logged in. \n Please have another admin edit your information");
            }
            else
            {
            frmEditUser edit = new frmEditUser();
            DialogResult result = edit.ShowDialog();

                //If everything checks out on the EditUser form, edit the user to the database.
                if (result == DialogResult.OK)
                {
                    try
                    {
                        //Connect to the Database
                        string connectionString;
                        SqlConnection connection;

                        connectionString = @"Data Source={SQL Server};SERVER=Nsccsqlinst16.nscc.edu;Database=CITC_CTEAM;UID=CITC_CTEAM;PWD=ITROCKS;";
                        connection = new SqlConnection(connectionString);
                        connection.Open();

                        string sqlSelect = "SELECT FName, LName FROM UserTable WHERE UserID = '" + UserData.OldUserName + "'";
                        SqlCommand command = new SqlCommand(sqlSelect, connection);

                        SqlDataReader sqlReader = command.ExecuteReader();

                        if (sqlReader.HasRows)
                        {
                            //Close the above reader first
                            sqlReader.Close();

                            //Build the UPDATE string to reach out to the DB and update the data.
                            string sqlInsert = "UPDATE UserTable " +
                            "SET FName = '" + UserData.FirstName + "', LName = '" + UserData.LastName + "', UserID = '" +
                            UserData.UserName + "', UserPassword = '" + UserData.PassWord + "', SecurityLevel = '" + UserData.SecurityLevel + "'" +
                            "WHERE UserID = '" + UserData.OldUserName + "'";

                            command = new SqlCommand(sqlInsert, connection);

                            //Check to see if the data was actually updated
                            int results = command.ExecuteNonQuery();
                            if (results < 0)
                            {
                                //If not, whoops!
                                MessageBox.Show("Error editing data into the database");
                            }
                            else
                            {
                                //If so, repopulate the grid
                                PopulateUserGrid();
                                MessageBox.Show("User sucessfully edited.");
                            }
                        }
                        connection.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Something went wrong. Please try again.\n" + ex.Message);
                    }
                }
            }
        }

        //back button
        private void btnBack_Click(object sender, EventArgs e)
        {
            frmSearch search = new frmSearch();
            search.Show();
            this.Hide();
            this.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //If everything checks out on the AddUser form, add the new user to the database.
            frmAddUser newUser = new frmAddUser();
            DialogResult result = newUser.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    //Connect to the Database
                    string connectionString;
                    SqlConnection connection;

                    connectionString = @"Data Source={SQL Server};SERVER=Nsccsqlinst16.nscc.edu;Database=CITC_CTEAM;UID=CITC_CTEAM;PWD=ITROCKS;";
                    connection = new SqlConnection(connectionString);
                    connection.Open();

                    //First, make sure that the username isn't a duplicate.
                    string sqlSelect = "SELECT FName, LName FROM UserTable WHERE UserID = '" + UserData.UserName + "'";
                    SqlCommand command = new SqlCommand(sqlSelect, connection);

                    SqlDataReader sqlReader = command.ExecuteReader();

                    if (sqlReader.HasRows)
                    {
                        MessageBox.Show("Username already exists. Please choose a unique username.");
                        connection.Close();
                        command.Dispose();
                    }
                    else
                    {
                        sqlReader.Close();

                        //Build the INSERT string to reach out to the DB and insert the data.
                        string sqlInsert = "INSERT INTO UserTable " +
                        "Values('" + UserData.FirstName + "', '" + UserData.LastName + "', '" + UserData.UserName + "', '" + UserData.PassWord + "', '" + UserData.SecurityLevel + "')";

                        command = new SqlCommand(sqlInsert, connection);

                        //Check to see if the data was actually inserted
                        int results = command.ExecuteNonQuery();
                        if (results < 0)
                        {
                            //If not, whoops!
                            MessageBox.Show("Error inserting data into the database");
                        }
                        else
                        {
                            //If so, repopulate the grid
                            PopulateUserGrid();
                            MessageBox.Show("User sucessfully added.");
                        }
                    }
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Something went wrong. Please try again.\n" + ex.Message);
                }
            }
        }

        public void PopulateUserGrid()
        {
            try
            {
                //Connect to the Database
                string connectionString;
                SqlConnection connection;

                connectionString = @"Data Source={SQL Server};SERVER=Nsccsqlinst16.nscc.edu;Database=CITC_CTEAM;UID=CITC_CTEAM;PWD=ITROCKS;";
                connection = new SqlConnection(connectionString);
                connection.Open();

                //Build the SELECT string to reach out to the DB and find the data
                string sqlSelect = "SELECT FName, LName, UserID, SecurityLevel FROM UserTable";
                SqlCommand command = new SqlCommand(sqlSelect, connection);

                //Bind the data to a DataSet and bind the set to the grid with code.
                command.CommandType = CommandType.Text;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "ss");

                userGrid.DataSource = dataSet.Tables["ss"];
                userGrid.ColumnHeadersVisible = false;

                connection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error. Please ensure that the connection is stable. \n\n" + ex.Message);
            }
        }

        private void userGrid_SelectionChanged(object sender, EventArgs e)
        {
            //Store the username of the selected User for either edit or delete operations.
            UserData.OldFirstName = userGrid.CurrentRow.Cells[0].Value.ToString();
            UserData.OldLastName = userGrid.CurrentRow.Cells[1].Value.ToString();
            UserData.OldUserName = userGrid.CurrentRow.Cells[2].Value.ToString();
        }
    }
}
