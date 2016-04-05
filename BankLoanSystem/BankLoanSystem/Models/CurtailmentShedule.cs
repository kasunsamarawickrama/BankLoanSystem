using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Helpers;

namespace BankLoanSystem.Models
{
    public class CurtailmentShedule
    {

        public int LoanId { get; set; }
        public string UnitId { get; set; }
        public int Year { get; set; }

        public string AdvanceDate { get; set; }
        public string DueDate { get; set; }
        public string PaidDate { get; set; }

        public int Status { get; set; }

        public decimal CurtAmount { get; set; }
        public decimal PaidCurtAmount { get; set; }

        public string IDNumber { get; set; }
        public int CurtNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        public DateTime PayDate { get; set; }

        public decimal PurchasePrice { get; set; }
        public decimal TotalAmountDue { get; set; }
        public decimal TotalAmountPaid { get; set; }

    }
    public class CurtailmentScheduleModel
    {
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PayDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        public List<CurtailmentShedule> CurtailmentScheduleInfoModel { get; set; }

        [RegularExpression(@"^[-a-zA-Z0-9]*", ErrorMessage = "Invalid VIN/HIN/Serial No")]
        public string IdentificationNumber { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Year must be a positive value.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "year must contain only digits")]
        public string Year { get; set; }

        [RegularExpression(@"^[-a-zA-Z0-9]*", ErrorMessage = "Invalid Make")]
        public string Make { get; set; }

        [RegularExpression(@"^[-a-zA-Z0-9]*", ErrorMessage = "Invalid Model")]
        public string Model { get; set; }
    }

    public class SelectedCurtailmentList
    {
        [Required]
        public List<CurtailmentShedule> SelectedCurtailmentSchedules{ get; set; }
    }
}