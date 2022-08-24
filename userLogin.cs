using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BankOfSLib
{
    public class userLogin
    {
        // phone number
        public int phoneNum { get; set; }
        // username 
        public string userName { get; set; }
        //password
        public string userPass { get; set; }
        //first name 
        public string firstName { get; set; }
        // last name
        public string lastName { get; set; }
        //blocked (1 is active 0 is blocked)
        public int blocked { get; set; }

        SqlConnection con = new SqlConnection("server=DESKTOP-VNJ9EM3;database=BankDB;integrated security = true");
        // check Login 
        public bool checkUserLogin(string custName, string custWord)
        {
            SqlCommand cmdCheckLog = new SqlCommand("select count(*) from userLogin where userName=@uName and userPass=@uWord ", con);
            cmdCheckLog.Parameters.AddWithValue("@uName", custName);
            cmdCheckLog.Parameters.AddWithValue("@uWord", custWord);

            con.Open();
            int checkQueryResult = Convert.ToInt32(cmdCheckLog.ExecuteScalar());
            con.Close();
            if (checkQueryResult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
          
        }
        //Check BlockedStatus
        public bool blockedStat(string custName)
        {
            SqlCommand cmdReadBlockStat = new SqlCommand("select blocked from userLogin where userName = @uName", con);
            cmdReadBlockStat.Parameters.AddWithValue("@uName", custName);

            con.Open();
            SqlDataReader reader = cmdReadBlockStat.ExecuteReader();
            if (reader.Read())
            {
                int checkBlock = (int)reader[0];
                con.Close();
                if (checkBlock == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }

        // Create new user Login
        public string addNewUser(int N_phoneNum, string N_userName, string N_userPass, string N_firstName, string N_lastName, string email)
        {
            SqlCommand cmdCreateUser = new SqlCommand("insert into userLogin values(@N_phoneNum, @N_userName,@N_userPass,@N_firstName, @N_lastName, @emailAdd, @N_blocked)", con);
            cmdCreateUser.Parameters.AddWithValue("@N_phoneNum", N_phoneNum);
            cmdCreateUser.Parameters.AddWithValue("@N_userName", N_userName);
            cmdCreateUser.Parameters.AddWithValue("@N_userPass", N_userPass);
            cmdCreateUser.Parameters.AddWithValue("@N_firstName", N_firstName);
            cmdCreateUser.Parameters.AddWithValue("@N_lastName", N_lastName);
            cmdCreateUser.Parameters.AddWithValue("@emailAdd", email);
            cmdCreateUser.Parameters.AddWithValue("@N_blocked", 0);
         
            con.Open();
            int userCreated = Convert.ToInt32(cmdCreateUser.ExecuteNonQuery());
            con.Close();
            if (userCreated == 1)
            {
                return "The New Customer Account is Complete, Welcome to the Bank of Sterling";
            }
            else {
                return "Customer Login not complete";
            }

        }
        // Search for userLogin
        public userLogin findLoginInfo(string userNAME)
        {
            SqlCommand cmdSearch = new SqlCommand("select * from userLogin where userName = @userNAME", con);
            cmdSearch.Parameters.AddWithValue("@userNAME", userNAME);

            userLogin emptyLogin = new userLogin();
            con.Open();
            SqlDataReader search = cmdSearch.ExecuteReader();
            if (search.Read())
            {
                emptyLogin.phoneNum = (int)search[0];
                emptyLogin.userName = (string)search[1];
                emptyLogin.userPass = (string)search[2];
                emptyLogin.firstName = (string)search[3];
                emptyLogin.lastName = (string)search[4];
                emptyLogin.blocked = (int)search[5];
            }
            else
            {
                search.Close();
                con.Close();
                throw new Exception("No user login attached to this username");
            }
            search.Close();
            con.Close();
            return emptyLogin;

        }
        //Change Password
        public string modifyPassword(string userName, string newPass)
        {
            SqlCommand cmdChangePassword = new SqlCommand("update userLogin set userPass = @newPass where userName = @uName", con);
            cmdChangePassword.Parameters.AddWithValue("@newPass", newPass);
            cmdChangePassword.Parameters.AddWithValue("@uName", userName);

            con.Open();
            int recordAffected = cmdChangePassword.ExecuteNonQuery();
            con.Close();
            if (recordAffected > 0)
            {
                return "Your password has been successfully modified";
            }
            else
            {
                return "This Customer is not found";
            }
        }
    
        //Admin option 7 activate blocked account
        public string modifyBlockStatus(string userNam, int newBlockStat)
        {

            SqlCommand cmdModBlock = new SqlCommand("update userLogin set blocked = @newBlockStat where userName = @uUserNam", con);
            cmdModBlock.Parameters.AddWithValue("@uUserNam", userNam);
            cmdModBlock.Parameters.AddWithValue("@newBlockStat", newBlockStat);

            con.Open();
            int ModBlockComplete = cmdModBlock.ExecuteNonQuery();
            con.Close();
            return "Blocked Status has been updated";
        }


    }
}