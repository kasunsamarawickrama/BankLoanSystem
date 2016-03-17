using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class TitleStatus
    {
       //[RegularExpression(@"^[A-Z]{6}$", ErrorMessage = "Please type last 6 characters of VIN/HIN/Serial Number ")]
        public string IdentificationNumberSearch { get; set; }

        public string IdentificationNumber { get; set; }

        public int Year { get; set; }

        public string Make { get; set; }

        public string UnitModel { get; set; }

        public string CurrentStatus { get; set; }

        public List<TitleStatus> TitleList { get; set; }
    }
}