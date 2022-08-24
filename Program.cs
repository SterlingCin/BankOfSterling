using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOfSLib;

namespace BankOfSterling
{
    public class Program
    {
        static void Main(string[] args)
        {

            //empty accounts
            accounts adminAccount = new accounts();
            accounts trialAccount = new accounts();
            userLogin trialUser = new userLogin();
            List<userLogin> emptyList = new List<userLogin>();
            List<accounts> emptyAccList = new List<accounts>();
            transactions emptyTrans = new transactions();
            List<transactions> tHistory = new List<transactions>();

            //welcome and divide users
            Console.WriteLine("~~~Welcome to the Bank of Sterling~~~");
            Console.WriteLine(" ");
            Console.WriteLine("For Sterling quality banking services enter the number 1");
            Console.WriteLine("For Stering Administration enter the number 2");

            
            int exit = 0;
            //initialize and declare selection
            int select = Convert.ToInt32(Console.ReadLine());          
            int custAttempt = 0;
            switch (select)
            {
                // Customer login (Main Switch)
                case 1:
                    //STEP: determine if login is true or not (DECLARE AND INITALIZE LOGIN BOOLEAN)
                    do
                    {

                        Console.WriteLine("Please Enter your username");
                        string custUsername = Console.ReadLine();
                        Console.WriteLine("Please enter your password");
                        string custPassword = Console.ReadLine();
                    
                        userLogin userInfo = new userLogin();
                    
                        bool login = userInfo.checkUserLogin(custUsername, custPassword);
                        bool blockedStat = userInfo.blockedStat(custUsername);
                        custAttempt++;
                    
                        if (login == true && custAttempt != 3 && blockedStat == false)
                        {

                            Console.Clear();

                            Console.WriteLine("~~~~~~ Welcome Valued customer ~~~~~");
                            Console.WriteLine("1. Check Balances");
                            Console.WriteLine("2. Withdraw");
                            Console.WriteLine("3. Deposit");
                            Console.WriteLine("4. Transfer");
                            Console.WriteLine("5. View last 10 transactions");
                            Console.WriteLine("6. Change Password");
                            Console.WriteLine("7. Exit");
                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                            Console.WriteLine("Please Enter the number that matches the desired option");
                            int choice = Convert.ToInt32(Console.ReadLine());

                            //Customer Menu Switch


                            switch (choice)
                            {
                                //get the correct user's balance 
                                case 1:
                                    Console.WriteLine("To check the balance of your open accounts enter your phone number below");
                                    int number = Convert.ToInt32(Console.ReadLine());

                                    List<accounts> yourAccounts = trialAccount.getYourAccounts(number);

                                    int custOpenAccounts = 0;
                                    foreach (var acc in yourAccounts)
                                    {
                                        Console.WriteLine("Account phone number " + acc.phoneNum);
                                        Console.WriteLine("Account number " + acc.accNum);
                                        Console.WriteLine("Account balance " + acc.accBal);
                                        Console.WriteLine("Account type " + acc.accType);
                                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                                        custOpenAccounts++;
                                    }
                                    Console.WriteLine("Your have " + custOpenAccounts + " open accounts");
                                    choice = Convert.ToInt32(Console.ReadLine());
                                    break;

                                // Withdraw method and log amount on to transaction history
                                case 2:
                                    Console.WriteLine("Enter Withdraw Amount");
                                    float wAmount = float.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter account number to withdraw");
                                    int wAccount = Convert.ToInt32(Console.ReadLine());

                                    if(wAmount > trialAccount.getYourBalance(wAccount))
                                    {
                                        Console.WriteLine("Your balance is to low for this withdraw");
                                    }
                                    else
                                    {
                                        emptyTrans.preformWithdrawl(wAccount, wAmount);
                                    }
                                 
                                    choice = Convert.ToInt32(Console.ReadLine());
                                    break;
                                case 3:
                                    Console.WriteLine("Enter Deposit Amount");
                                    float dAmount = float.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter account number to deposit");
                                    int dAccount = Convert.ToInt32(Console.ReadLine());

                                    emptyTrans.preformDeposit(dAccount, dAmount);

                                    choice = Convert.ToInt32(Console.ReadLine());

                                    break;
                                case 4:
                                    Console.WriteLine("Please enter the account number of the origin account: ");
                                    int originAccNum = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Please enter the account number for the destination account: ");
                                    int accountNumTrans = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Please enter a 6 digit number to trace this transaction, this number will be your transaction number");
                                    int transactionNum = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Please enter the amount you would like to send");
                                    float sendAmount = float.Parse(Console.ReadLine());

                                    string transferTrans = emptyTrans.accTransfer(originAccNum, accountNumTrans, transactionNum, sendAmount);
                                    Console.WriteLine(transferTrans);

                                    choice = Convert.ToInt32(Console.ReadLine());
                                    break;
                                case 5:
                                    Console.WriteLine("To see an account's last 10 transactions enter account number below");
                                    int tempAccNum = Convert.ToInt32(Console.ReadLine());

                                    tHistory = emptyTrans.getTransactionHistory(tempAccNum);
                                    int historyCount = 0;
                                    Console.WriteLine("Transaction History");
                                    while (historyCount < 10)
                                    {
                                        foreach (var history in tHistory)
                                        {
                                            Console.WriteLine("Account Number: " + history.accNum);
                                            Console.WriteLine("Transaction Number: " + history.transNum);
                                            Console.WriteLine("Transaction Type: " + history.tranType);
                                            Console.WriteLine("Transaction Amount: " + history.tranAmount); Console.WriteLine("Date of Transacrion: " + history.dateOfTrans);
                                            if (history.tranType == "transfer")
                                            {
                                                Console.WriteLine("Money transfered to: " + history.destinAccount);
                                            }
                                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                                            // Enter current Balance
                                            Console.WriteLine("Account's Current Amount: ");
                                            historyCount++;
                                        }
                                    }

                                    break;
                                case 6:
                                    Console.WriteLine("To change your password please enter your username");
                                    string tempUserName = Console.ReadLine();
                                    Console.WriteLine("Enter your new password");
                                    string newPass = Console.ReadLine();
                              
                                    userInfo.modifyPassword(tempUserName, newPass);
                                    choice = Convert.ToInt32(Console.ReadLine());
                                    break;
                                case 7:
                                    Console.WriteLine("Thank you for Banking with the Bank of Sterling.");
                                    Console.WriteLine("We Appreciate your membership");
                                    exit = 4;
                                    break;
                                default:
                                    Console.WriteLine("Sorry you chose the wrong option");
                                    Console.WriteLine("Select 1 to see your new balance");
                                    choice = Convert.ToInt32(Console.ReadLine());
                                    break;
                            }
                        }
                        if (custAttempt <3 && login == false && blockedStat ==true )
                        {
                            Console.WriteLine("Invalid Credentials, please try again.");
                            Console.WriteLine("Contact Bank of Sterling Administration to change login your information");

                            Console.WriteLine("Please Enter your username");
                            custUsername = Console.ReadLine();
                            Console.WriteLine("Please enter your password");
                            custPassword = Console.ReadLine();
                            custAttempt++;
                        }
                        
                        else if (custAttempt==3 && login == false && blockedStat == true) {
                            Console.WriteLine("Invalid Credentials, this account has been blocked.");
                            Console.WriteLine("Contact Bank of Sterling Administration to get your account unblocked.");
                            trialUser.modifyBlockStatus(custUsername,1);
                            exit = 4;
                            custAttempt++;
                            
                        }
                    } while (exit != 4);
                    break;



                //Major Switch statement

                //Admin Login
#admin region
                case 2:
                    do
                    {
                        Console.WriteLine("Please Enter your username");
                        string adminUsername = Console.ReadLine();
                        Console.WriteLine("Please enter your password");
                        string adminPassword = Console.ReadLine();

                        adminLogin adminInfo = new adminLogin();
                        bool adLogin = adminInfo.checkAdminLogin(adminUsername, adminPassword);


                        //Test Login info
                        if (adLogin == true)
                        {
                            int option;
                            do
                            {

                                // Menu
                                Console.Clear();
                                Console.WriteLine("~~~Welcome Administrator of the Bank Of Sterling~~~");
                                Console.WriteLine("1. Create a new acount.");
                                Console.WriteLine("2. View all account details in list");
                                Console.WriteLine("3. Preform Withdraw");
                                Console.WriteLine("4. Preform Deposit");                            
                                Console.WriteLine("5. Transer funds");
                                Console.WriteLine("6. Disable and account");
                                //setBlocked(false)
                                Console.WriteLine("7. Activate a Blocked account");
                                Console.WriteLine("8. Exit");
                                option = Convert.ToInt32(Console.ReadLine());


                                switch (option)
                                {
                                    case 1:
                                        Console.WriteLine("To Create a new account start by creating the new account");
                                        Console.WriteLine("");
                                        Console.WriteLine("Enter Customer's Phone Number");
                                        int newNumber = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Enter New Account Number");
                                        int newAccNum = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Enter Starting Balance");
                                        float newBalance = float.Parse(Console.ReadLine());
                                        Console.WriteLine("Enter Account Type");
                                        string newAccountType = Console.ReadLine();

                                        trialAccount.addAccount(newNumber, newAccNum, newBalance, newAccountType);

                                        Console.WriteLine("Is the Customer New to the Bank of Sterling? If so type a.");
                                        string newCust = Console.ReadLine();
                                        if (newCust == "a")
                                        {
                                            Console.WriteLine("Enter the New Customer's Phone Number");
                                            int newCustNum = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Enter the New Customer's username");
                                            string newCustUserName = Console.ReadLine();
                                            Console.WriteLine("Enter the New Customer's password");
                                            string newCustPassword = Console.ReadLine();
                                            Console.WriteLine("Enter the New Customer's First Name");
                                            string newCustFName = Console.ReadLine();
                                            Console.WriteLine("Enter the New Customer's Last Name");
                                            string newCustLName = Console.ReadLine();
                        
                                            trialUser.addNewUser(newCustNum, newCustUserName, newCustPassword, newCustFName, newCustLName);
                                        }
                                        Console.WriteLine("");

                                        Console.WriteLine("Enter 8 to exit");
                                        option = Convert.ToInt32(Console.ReadLine());
                                        break;
                                    case 2:

                                        List<accounts> allAccounts = adminAccount.getAccounts();
                                        //counter for list
                                        int numOfAccounts = 0;
                                        foreach (var acc in allAccounts)
                                        {
                                            Console.WriteLine("Account phone number " + acc.phoneNum);
                                            Console.WriteLine("Account number " + acc.accNum);
                                            Console.WriteLine("Account balance " + acc.accBal);
                                            Console.WriteLine("Account type " + acc.accType);
                                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                                            numOfAccounts++;
                                        }
                                        
                                        Console.WriteLine("The Bank of Sterling has "+ numOfAccounts+ " Open accounts");
                                        option = Convert.ToInt32(Console.ReadLine());
                                        break;

                                    case 3:
                                        Console.WriteLine("Enter Withdraw Amount");
                                        float wAmount = float.Parse(Console.ReadLine());
                                        Console.WriteLine("Enter account number to withdraw");
                                        int wAccount = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Enter a transaction number to track this transaction");
                                        int tNumber = Convert.ToInt32(Console.ReadLine());

                                        emptyTrans.preformWithdrawl(wAccount, wAmount,tNumber);

                                        option = Convert.ToInt32(Console.ReadLine());
                                        break;

                                    case 4:
                                        Console.WriteLine("Enter Deposit Amount");
                                        float dAmount = float.Parse(Console.ReadLine());
                                        Console.WriteLine("Enter account number to deposit");
                                        int dAccount = Convert.ToInt32(Console.ReadLine());
                                        int tNum= Convert.ToInt32(Console.ReadLine());

                                        emptyTrans.preformDeposit(dAccount, dAmount,tNum);

                                        option = Convert.ToInt32(Console.ReadLine());
                                        break;
                                    case 5:
                                        Console.WriteLine("To conduct a transfer follow the instructions below");
                                        Console.WriteLine("Enter the origin account number");
                                        int originAcconunt = Convert.ToInt32(Console.ReadLine());

                                        Console.WriteLine("Enter the destination account number");
                                        int destinationAccount = Convert.ToInt32(Console.ReadLine());

                                        Console.WriteLine("Enter transaction tracking number");
                                        int transactionNum = Convert.ToInt32(Console.ReadLine());

                                        Console.WriteLine("Enter transfer amount");
                                        float amountToTransfer = Convert.ToSingle(Console.ReadLine());

                                        emptyTrans.accTransfer(originAcconunt, destinationAccount, transactionNum, amountToTransfer);

                                        option = Convert.ToInt32(Console.ReadLine());
                                        break;
                                    case 6:
                                        Console.WriteLine("Before ending a customer's membership remember to transfer any remaining funds in there connected accounts.");
                                        Console.WriteLine("Please enter customer's phone number below to delete their userlogin and close any open accounts");
                                        int delPhoneNum = Convert.ToInt32(Console.ReadLine());
                                        trialAccount.deleteAccount(delPhoneNum);

                                        option = Convert.ToInt32(Console.ReadLine());
                                        break;
                                    case 7:
                                        Console.WriteLine("To change a customer's blocked status please enter their username below");
                                        string custUserName = Console.ReadLine();

                                        Console.WriteLine("To block user set blocked status to 1 and login attempts to 3. To unblock user set blocked status to 0 and login attempts to 0");
                                        Console.WriteLine("Enter Blocked Status 1 or 0");
                                        int newStatBlock = Convert.ToInt32(Console.ReadLine());
                                        
                                        trialUser.modifyBlockStatus(custUserName, newStatBlock);

                                        option = Convert.ToInt32(Console.ReadLine());
                                        break;
                                    case 8:
                                        Console.WriteLine("Thank you for working at the Bank of Sterling");
                                        exit = 4;
                                        break;
                                    default:
                                        Console.WriteLine("Wrong Option, please try again");
                                        option = Convert.ToInt32(Console.ReadLine());
                                        break;
                                }
                            } while (exit != 4);

                        }
                        if (adLogin == false)
                        {
                            Console.WriteLine("Invalid Credentials, please try again");
                            Console.WriteLine("");
                        }
                    } while (exit != 4);
                    break;

                default:
                    Console.WriteLine("Incorrect option please try again.");
                    Console.WriteLine("Customer - 1");
                    Console.WriteLine("Employee - 2");
                    break;




            }
        }
#endregion
    }
}
  
