using System;

namespace BankLoanSystem.Code
{
    public class Common
    {
        public string ConvertDateString(string sDate)
        {
            if (sDate.Length == 10 || sDate.Length == 9 || sDate.Length == 8)
            {
                var dateToken = sDate.Split('/');

                int month = Convert.ToInt32(dateToken[0]);
                int day = Convert.ToInt32(dateToken[1]);
                int year = Convert.ToInt32(dateToken[2]);

                sDate = month.ToString("00") + "/" + day.ToString("00") + "/" + year.ToString("0000");
            }

            return sDate;
        }
    }
}