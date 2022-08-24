using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BankOfSLib
{
    public class adminLogin
    {
        //employee number
        public int empNum { get; set; }
        // administrator username
        public string adminName { get; set; }
        // adminstrator password
        public string adminPass { get; set; }

        SqlConnection con = new SqlConnection("server=DESKTOP-VNJ9EM3;database=BankDB;integrated security = true");
        //Admin Login
        public bool checkAdminLogin(string aUserName, string aPassWord)
        {
            SqlCommand cmdCheckALog = new SqlCommand("select count(*) from adminLogin where adminName=@aUserName and adminPass = @aPassWord", con);
            cmdCheckALog.Parameters.AddWithValue("@aUserName", aUserName);
            cmdCheckALog.Parameters.AddWithValue("@aPassWord", aPassWord);

            con.Open();
            int checkQueryResult = Convert.ToInt32(cmdCheckALog.ExecuteScalar());
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
    }
}
