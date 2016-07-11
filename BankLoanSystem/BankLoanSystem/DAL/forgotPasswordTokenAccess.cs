using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BankLoanSystem.Models;
using BankLoanSystem.Code;

namespace BankLoanSystem.DAL
{
    public class forgotPasswordTokenAccess
    {


        /*

 Frontend page: Account help page
 Title: update/Insert the user id and token
 Designed: Irfan Mam
 User story: 
 Developed: Irfan MAM
 Date created: 1/17/2016

*/

        public bool updateToken(int userId, string token)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", userId });
            paramertList.Add(new object[] { "@token", token });
            paramertList.Add(new object[] { "@expired_date", DateTime.Now.AddHours(24) });
           
            try
            {
                return dataHandler.ExecuteSQLWithReturnVal("spUpdateOrInsertToken", paramertList)>0 ? true : false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*

Frontend page: Reset Password page
Title:  Verifying the user account using usedId and token
Designed: Irfan Mam
User story: 
Developed: Irfan MAM
Date created: 1/17/2016

*/
        public bool verifyAccount(int userId, string token)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", userId });
            paramertList.Add(new object[] { "@token", token });
            paramertList.Add(new object[] { "@expired_date", DateTime.Now });

            try
            {
                return dataHandler.ExecuteSQLWithReturnVal("spverifyAccountBytoken", paramertList) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*

Frontend page: Reset Password page
Title:  Update Password
Designed: Irfan Mam
User story: 
Developed: Irfan MAM
Date created: 1/17/2016

*/
        public bool resetPassword(int userId, ResetPassword resetPasswordModel)
        {
            DataHandler dataHandler = new DataHandler();
            string newSalt = PasswordEncryption.RandomString();
            resetPasswordModel.Password = PasswordEncryption.encryptPassword(resetPasswordModel.Password, newSalt);

            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", userId });
            paramertList.Add(new object[] { "@password", resetPasswordModel.Password });
            

            try
            {
                return dataHandler.ExecuteSQLWithReturnVal("spUpdatePassword", paramertList) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}