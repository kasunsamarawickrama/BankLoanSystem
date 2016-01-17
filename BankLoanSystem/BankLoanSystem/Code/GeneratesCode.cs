using System;
using BankLoanSystem.DAL;

namespace BankLoanSystem.Code
{
    public class GeneratesCode
    {
        public void GenerateCompanyCode(string companyName)
        {
            string companyCode = "";
            string prefix = companyName.Substring(0, 3).ToUpper();

            CompanyAccess ca = new CompanyAccess();
            string latestCode = ca.GetLatestCompanyCode(prefix);
            
            if (latestCode != null)
            {
                string sufix = (Convert.ToInt32(latestCode.Substring(3, latestCode.Length - 3)) + 1).ToString("00");
                //int sufixId = Convert.ToInt32(latestCode.Substring(3, latestCode.Length - 3)) + 1;

            }
            else
            {
                companyCode = prefix + "01";
            }
        }
    }
}