using System;
using BankLoanSystem.DAL;

namespace BankLoanSystem.Code
{
    public class GeneratesCode
    {
        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/18
        /// 
        /// generate company code
        /// 
        /// argument: companyName(string)
        /// 
        /// </summary>
        /// <returns>companyCode(string)</returns>
        public string GenerateCompanyCode(string companyName)
        {
            var prefix = companyName.Substring(0, 3).ToUpper();

            var ca = new CompanyAccess();
            var latestCode = ca.GetLatestCompanyCode(prefix);
            
            if (latestCode != "")
            {
                var sufix = (Convert.ToInt32(latestCode.Substring(3, latestCode.Length - 3)) + 1).ToString("00");
                return prefix + sufix;
            }
            return prefix + "01";
        }
    }
}