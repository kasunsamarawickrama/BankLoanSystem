using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankLoanSystem.DAL
{
    public class LogAccess
    {
        /// <summary>
        /// CreatedBy:
        /// CreatedDate:4/24/2016
        ///insert log in every transaction
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int InsertLog(Log log)
        {
            try {
                if (log != null)
                {
                    DataHandler dataHandler = new DataHandler();
                    List<object[]> paramertList = new List<object[]>();

                    paramertList.Add(new object[] { "@date_time", log.DateTime });
                    paramertList.Add(new object[] { "@user_id", log.UserId });
                    paramertList.Add(new object[] { "@company_id", log.CompanyId });
                    paramertList.Add(new object[] { "@branch_id", log.BranchId });
                    paramertList.Add(new object[] { "@loan_id", log.LoanId });
                    paramertList.Add(new object[] { "@description", log.Description });
                    paramertList.Add(new object[] { "@page", log.Page });


                    try
                    {
                        return dataHandler.ExecuteSQLReturn("spInsertLog", paramertList);
                    }
                    catch
                    {
                        return 0;
                    }
                    // return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}