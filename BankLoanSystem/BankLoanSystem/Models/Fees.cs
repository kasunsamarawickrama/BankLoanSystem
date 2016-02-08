using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class Fees
    {
        public int LoanId { get; set; }

        [Display(Name = "Advance")]
        public int AdvanceId { get; set; }
        public string AdvanceAmount { get; set; }
        public string AdvanceDue { get; set; }
        public string AdvanceDueDate { get; set; }
        public string AdvanceLenderEmail { get; set; }
        public string AdvanceDealerEmail { get; set; }

        [Display(Name = "Monthly Loan")]
        public int MonthlyLoanId { get; set; }
        public string MonthlyLoanAmount { get; set; }
        public string MonthlyLoanDue { get; set; }
        public string MonthlyLoanRadio { get; set; }
        public string MonthlyLoanDueDate { get; set; }
        public string MonthlyLoanLenderEmail { get; set; }
        public string MonthlyLoanDealerEmail { get; set; }

        [Display(Name = "Lot Inspection")]
        public int LotInspectionId { get; set; }
        public string LotInspectionAmount { get; set; }
        public string LotInspectionDue { get; set; }
        public string LotInspectionLenderEmail { get; set; }
        public string LotInspectionDealerEmail { get; set; }
    }
}