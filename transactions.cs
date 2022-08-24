using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BankOfSLib
{

    public class transactions
    {
        public int OriginAccNum { get; set; }
        public int TransAccNum { get; set; }   
        public float amountSent { get; set; }
        public int transIDNum { get; set; }
        public string dateOfTrans { get; set; }

        public int destinAccount { get; set; }
        SqlConnection con = new SqlConnection("server=DESKTOP-VNJ9EM3;database=BankDB;integrated security = true");

        // Admin transfer
        public string accTransfer(int originAccNum, int TransAccNum ,int amountSent )
        {
            SqlCommand cmdFrom = new SqlCommand("update accounts set accBal = accBal - @amount where accountNum = @fromAccount", con);
            cmdFrom.Parameters.AddWithValue("@amount", amountSent);
            cmdFrom.Parameters.AddWithValue("@fromAccount", originAccNum);

            SqlCommand cmdTo = new SqlCommand("update accounts set accBal = accBal + @amount where accountNum = @ToAccount", con);
            cmdTo.Parameters.AddWithValue("@amount", amountSent);
            cmdTo.Parameters.AddWithValue("@ToAccount", TransAccNum);

            SqlCommand cmdTransaction = new SqlCommand("insert into transactions values(@fromAccount,@amount,@ToAccount,GETDATE())", con);
            cmdTransaction.Parameters.AddWithValue("@fromAccount", originAccNum);
            cmdTransaction.Parameters.AddWithValue("@ToAccount", TransAccNum);
            cmdTransaction.Parameters.AddWithValue("@amount",amountSent);

            con.Open();
            cmdFrom.ExecuteNonQuery();
            cmdTo.ExecuteNonQuery();
            cmdTransaction.ExecuteNonQuery();
            con.Close();

            return "Transfer Complete";
        }

        //Customer Transfer
        public string accTransfer(int OriginAccNum, int TransAccNum, float amountSent)
        {
            SqlCommand cmdFrom = new SqlCommand("update accounts set accBal = accBal - @amount where accountNum = @fromAccount", con);
            cmdFrom.Parameters.AddWithValue("@amount", amountSent);
            cmdFrom.Parameters.AddWithValue("@fromAccount", OriginAccNum);

            SqlCommand cmdTo = new SqlCommand("update accounts set accBal = accBal + @amount where accountNum = @ToAccount", con);
            cmdTo.Parameters.AddWithValue("@amount", amountSent);
            cmdTo.Parameters.AddWithValue("@ToAccount", TransAccNum);

            SqlCommand cmdTransaction = new SqlCommand("insert into transactions values(@fromAccount,@amount,@ToAccount,GETDATE())", con);
            cmdTransaction.Parameters.AddWithValue("@fromAccount", OriginAccNum);
            cmdTransaction.Parameters.AddWithValue("@ToAccount", TransAccNum);
            cmdTransaction.Parameters.AddWithValue("@amount", amountSent);

            con.Open();
            cmdFrom.ExecuteNonQuery();
            cmdTo.ExecuteNonQuery();
            cmdTransaction.ExecuteNonQuery();
            con.Close();

            return "Transfer Complete";
        }

        
        public List<transactions>getTransactionHistory(int AccNum)
        {
            List<transactions> History = new List<transactions>();

            //Sending History 
            SqlCommand cmdGatherInfo = new SqlCommand("select * from transactions where OriginAccNum = @accountNum", con);
            cmdGatherInfo.Parameters.AddWithValue("@accountNum", AccNum);
    
            //get accounts and their details
            con.Open();
            SqlDataReader readDetail = cmdGatherInfo.ExecuteReader();
            while (readDetail.Read())
            {
                transactions Actions = new transactions();
                Actions.OriginAccNum = AccNum;
                Actions.transIDNum = (int)(readDetail[1]);
                Actions.TransAccNum = (int)(readDetail[3]);
                Actions.amountSent = Convert.ToSingle(readDetail[2]);
                Actions.dateOfTrans = (string)readDetail[4];
              
                History.Add(Actions);
            }

            readDetail.Close();
            con.Close();

            //Recieving History
            SqlCommand cmdGetInfo = new SqlCommand("select * from transactions where TransAccNum = @accountNum", con);
            cmdGatherInfo.Parameters.AddWithValue("@accountNum", AccNum);

            //get accounts and their details
            con.Open();
            SqlDataReader sqlDataReader = cmdGetInfo.ExecuteReader();
            while (sqlDataReader.Read())
            {
                transactions Actions = new transactions();
                Actions.OriginAccNum = (int)(sqlDataReader[0]);
                Actions.transIDNum = (int)(sqlDataReader[1]);
                Actions.TransAccNum = AccNum;
                Actions.amountSent = Convert.ToSingle(readDetail[2]);
                Actions.dateOfTrans = (string)readDetail[4];
                History.Add(Actions);
            }

            readDetail.Close();
            con.Close();
            return History;
        }

        //Customer and admin withdrawl
        public string preformWithdrawl(int OriginAccNum, float amountSent)
        {
            SqlCommand cmdFrom = new SqlCommand("update accounts set accBal = accBal - @amount where accountNum = @fromAccount", con);
            cmdFrom.Parameters.AddWithValue("@amount", amountSent);
            cmdFrom.Parameters.AddWithValue("@fromAccount", OriginAccNum);


            SqlCommand cmdTransaction = new SqlCommand("insert into transactions values(@fromAccount,@amount,@ToAccount,GETDATE())", con);
            cmdTransaction.Parameters.AddWithValue("@fromAccount", OriginAccNum);
            cmdTransaction.Parameters.AddWithValue("@ToAccount", 0);
            cmdTransaction.Parameters.AddWithValue("@amount", amountSent);

            con.Open();
            cmdFrom.ExecuteNonQuery();
            cmdTransaction.ExecuteNonQuery();
            con.Close();
            return "Withdrawl Successful";
        }

  
        //Customer Deposit
        public string preformDeposit(int TransAccNum, float amountSent)
        {
            SqlCommand cmdTo = new SqlCommand("update accounts set accBal = accBal + @amount where accountNum = @ToAccount", con);
            cmdTo.Parameters.AddWithValue("@amount",amountSent);
            cmdTo.Parameters.AddWithValue("@ToAccount", TransAccNum);

            SqlCommand cmdTransaction = new SqlCommand("insert into transactions values(@fromAccount,@amount,GETDATE(),@ToAccount)", con);
            cmdTransaction.Parameters.AddWithValue("@fromAccount", TransAccNum);
            cmdTransaction.Parameters.AddWithValue("@ToAccount", TransAccNum);
            cmdTransaction.Parameters.AddWithValue("@amount", amountSent);

            con.Open();
            cmdTo.ExecuteNonQuery();
            cmdTransaction.ExecuteNonQuery();
            con.Close();

            return "Deposit Complete";
        }

       
    }
}
