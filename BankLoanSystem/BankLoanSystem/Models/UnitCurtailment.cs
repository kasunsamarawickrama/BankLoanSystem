using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace BankLoanSystem.Models
{
    public class UnitCurtailment
    {
        public int LoanId { get; set; }
        public string UnitId { get; set; }
        public int CurtailmentNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public int CurtailmentStatus { get; set; }
        public string IdentificationNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }

    public class UnitPayOffModel
    {
        public int LoanId { get; set; }
        public string UnitId { get; set; }
        public DateTime DateEntered { get; set; }
        public DateTime DateAdvanced { get; set; }
        public string IdentificationNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal Balance { get; set; }
        public decimal PurchesePrice { get; set; }
        
        
    }

    public class UnitPayOffViewModel
    {
        public List<UnitPayOffModel> UnitPayOffList { get; set; }
        public List<UnitPayOffModel> SearchList { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PayDate { get; set; }

        [RegularExpression(@"^[-a-zA-Z0-9]*", ErrorMessage = "Invalid VIN/HIN/Serial No")]
        public string IdentificationNumber { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Year must be a positive value.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "year must contain only digits")]
        public string Year { get; set; }

        [RegularExpression(@"^[-a-zA-Z0-9]*", ErrorMessage = "Invalid Make")]
        public string Make { get; set; }

        [RegularExpression(@"^[-a-zA-Z0-9]*", ErrorMessage = "Invalid Model")]
        public string Model { get; set; }

        public DateTime CreatedDate { get; set; }

        [Display(Name = "Unit Type")]
        public int UnitTypeId { get; set; }

        public string TitleReturn { get; set; }

        public vehicle vehicle { get; set; }
    }
}