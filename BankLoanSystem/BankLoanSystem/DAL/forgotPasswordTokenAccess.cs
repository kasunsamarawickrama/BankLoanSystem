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
        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/17
        /// 
        /// update Token using user id and token
        /// if a user not exists it will insert user and token
        /// 
        /// argument : user_id (int), token : string
        /// 
        /// </summary>
        /// <returns>true : success, false : fail</returns>
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

        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/17
        /// 
        ///Verify the account using userId and token
        /// 
        /// argument : user_id (int), token : string
        /// 
        /// </summary>
        /// <returns>true : success, false : fail</returns>
        public bool verifyAccount(int userId, string token)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", userId });
            paramertList.Add(new object[] { "@token", token });
            paramertList.Add(new object[] { "@expired_date", DateTime.Now.AddHours(24) });

            try
            {
                return dataHandler.ExecuteSQLWithReturnVal("spverifyAccountBytoken", paramertList) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/17
        /// 
        ///update the Password
        /// 
        /// argument : user_id (int), resetPasswordModel : ResetPassword
        /// 
        /// </summary>
        /// <returns>true : success, false : fail</returns>
        public bool resetPassword(int userId, ResetPassword resetPasswordModel)
        {
            DataHandler dataHandler = new DataHandler();
            string newSalt = PasswordEncryption.RandomString();
            resetPasswordModel.Password = PasswordEncryption.encryptPassword(resetPasswordModel.Password, newSalt);

            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", userId });
            paramertList.Add(new object[] { "@password", resetPasswordModel.Password });
            paramertList.Add(new object[] { "@expired_date", DateTime.Now.AddHours(24) });

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