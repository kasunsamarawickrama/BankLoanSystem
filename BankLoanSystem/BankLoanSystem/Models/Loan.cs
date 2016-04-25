using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public string LoanNumber { get; set; }
        
        [Display(Name = "Loan Amount")]
        public decimal LoanAmount { get; set; }
        public int NonRegBranchId { get; set; }
        public string PartnerName { get; set; }
        public int PartnerType { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int IsTitleTrack { get; set; }
        public int AdvanceFee { get; set; }
        public int LotInspectionFee { get; set; }
        public int MonthlyLoanFee { get; set; }
        public bool AdvanceFeePayAtPayoff { get; set; }
        public bool AddUnit { get; set; }
        public bool ViewReports { get; set; }
        public string Rights { get; set; }
        public string LoanCode { get; set; }
        [Display(Name = "Created Date")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Current Loan Status")]
        public bool CurrentLoanStatus { get; set; }

        public bool LoanStatus { get; set; }
    }
}