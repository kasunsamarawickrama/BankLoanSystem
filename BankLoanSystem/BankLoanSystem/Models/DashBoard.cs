using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    /// <summary>
    /// CreatedBy : Kasun Smarawickrama
    /// CreatedDate: 2016/01/14
    /// 
    /// </summary>
    public class DashBoard
    {
        public int userId { get; set; }

        public int levelId { get; set; }

        public string userName { get; set; }

        public string roleName { get; set; }
    }
}