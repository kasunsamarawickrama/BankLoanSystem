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

        public Unit TitleDetails { get; set; }
        public List<Unit> TitleList { get; set; }

    }
   
   
}