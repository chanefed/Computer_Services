using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Services
{
    public class UserData
    {
        //Current user data
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string UserName { get; set; }
        public static string PassWord { get; set; }
        public static bool SecurityLevel { get; set; }

        //Old user data
        public static string OldFirstName { get; set; }
        public static string OldLastName { get; set; }
        public static string OldUserName { get; set; }
        public static string OldPassWord { get; set; }

        //Logged in user
        public static string LoggedFirstName { get; set; }
        public static string LoggedLastName { get; set; }
        public static string LoggedUserName { get; set; }
        public static string LoggedPassWord { get; set; }
        public static bool LoggedSecurityLevel { get; set; }






        //Constructor ... never used
        public UserData(string firstName, string lastName, string userName, bool securityLevel)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            SecurityLevel = securityLevel;
        }
    }
}
