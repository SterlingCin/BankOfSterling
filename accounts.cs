using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BankOfSLib
{
    public class accounts
    {
        public int phoneNum { get; set; }
        public int accountNum { get; set; }
        public float accBal { get; set; }
        public string accType { get; set; }

        // Connect to Data base
        SqlConnection con = new SqlConnection("server=DESKTOP-VNJ9EM3;database=BankDB;integrated security = true");

        //Add Account
        public string addAccount(int NphoneNum, float NaccBal, string NaccType)
        {
            SqlCommand cmdAddAccount = new SqlCommand("insert into accounts values(@NphoneNum, @NaccBal,@NaccType)",con);
            cmdAddAccount.Parameters.AddWithValue("@NphoneNum",NphoneNum);
            cmdAddAccount.Parameters.AddWithValue("@NaccBal",NaccBal);
            cmdAddAccount.Parameters.AddWithValue("@NaccType",NaccType);
            con.Open();
            int accountsAdded = cmdAddAccount.ExecuteNonQuery();
            con.Close();
                return "Account has been created, now create customer login";
         
        }
        
       //Delete Account
       public string deleteUser(int uPhoneNum)
        {
            SqlCommand cmdDeleteAccount = new SqlCommand("delete from accounts where phoneNum = @uphoneNum", con);
            cmdDeleteAccount.Parameters.AddWithValue("@uphoneNum", uPhoneNum);

            SqlCommand cmdDeleteUser = new SqlCommand("delete from userLogin where phoneNum = @phoneNum", con);
            cmdDeleteUser.Parameters.AddWithValue("@phoneNum", uPhoneNum);

            con.Open();
            cmdDeleteAccount.ExecuteNonQuery();
            cmdDeleteUser.ExecuteNonQuery();
            con.Close();
            return "This account has been deleted";

        }

        //Get account info in list using the user's login phone number
        public List<accounts> getAccounts()
        {
            //declared list that will be returned
            List<accounts> activeAccInfo = new List<accounts>();

            SqlCommand cmdGatherInfo = new SqlCommand("select * from accounts", con);
            
            //get accounts and their details
            con.Open();
            SqlDataReader readDetail = cmdGatherInfo.ExecuteReader();
            while (readDetail.Read())
            {
                accounts account = new accounts();
                account.phoneNum = (int)readDetail[1];
                account.accountNum = (int)readDetail[0];
                account.accBal = Convert.ToSingle(readDetail[2]);
                account.accType = (string)readDetail[3];
                activeAccInfo.Add(account);
                
            }
            readDetail.Close();
            con.Close();
            return activeAccInfo;
        }

        //Check Account Balance
        public float getYourBalance(int accNumber)
        {
            float balance = 0;
            SqlCommand cmdGatherInfo = new SqlCommand("select accBal from accounts where accountNum = @accNumber", con);
            cmdGatherInfo.Parameters.AddWithValue("@accNumber", accNumber);
            con.Open();
            SqlDataReader readDetail = cmdGatherInfo.ExecuteReader();
            while (readDetail.Read())
            {
                balance = Convert.ToSingle(readDetail[0]);
            }
            readDetail.Close();
            con.Close();
            return balance;
        }

        // Check Accounts
        public List<accounts> getYourAccounts(int custPhoneNum)
        {
            //declared list that will be returned
            List<accounts> custAccInfo = new List<accounts>();

            SqlCommand cmdGatherInfo = new SqlCommand("select * from accounts where phoneNum = @custPhoneNum", con);
            cmdGatherInfo.Parameters.AddWithValue("@custPhoneNum", custPhoneNum);
           
            con.Open();
            SqlDataReader readDetail = cmdGatherInfo.ExecuteReader();
            while (readDetail.Read())
            {
                accounts account = new accounts();
                account.phoneNum = (int)readDetail[1];
                account.accountNum = (int)readDetail[0];
                account.accBal = Convert.ToSingle(readDetail[2]);
                account.accType = (string)readDetail[3];
                custAccInfo.Add(account);

            }
            readDetail.Close();
            con.Close();
            return custAccInfo;
        }
    }
}
