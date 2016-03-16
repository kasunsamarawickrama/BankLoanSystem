using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class TitleStatus
    {
        public string IdentificationNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string UnitModel { get; set; }
        public string CurrentStatus { get; set; }
    }
}