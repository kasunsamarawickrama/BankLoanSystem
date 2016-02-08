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
        public float AdvanceAmount { get; set; }
        public string AdvanceDue { get; set; }
        public string AdvanceDueDate { get; set; }
        public string AdvanceRadio { get; set; }
        public string AdvanceLenderEmail { get; set; }
        public string AdvanceLenderEmailRemindPeriod { get; set; }
        public string AdvanceDealerEmail { get; set; }
        public string AdvanceDealerEmailRemindPeriod { get; set; }

        [Display(Name = "Monthly Loan")]
        public int MonthlyLoanId { get; set; }
        public float MonthlyLoanAmount { get; set; }
        public string MonthlyLoanDue { get; set; }
        public string MonthlyLoanRadio { get; set; }
        public string MonthlyLoanDueDate { get; set; }
        public string MonthlyLoanLenderEmail { get; set; }
        public string MonthlyLoanDealerEmail { get; set; }
        public string MonthlyLoanLenderEmailRemindPeriod { get; set; }
        public string MonthlyLoanDealerEmailRemindPeriod { get; set; }

        [Display(Name = "Lot Inspection")]
        public int LotInspectionId { get; set; }
        public float LotInspectionAmount { get; set; }
        public string LotInspectionDue { get; set; }
        public string LotInspectionLenderEmail { get; set; }
        public string LotInspectionDealerEmail { get; set; }
        public string LotInspectionLenderEmailRemindPeriod { get; set; }
        public string LotInspectionDealerEmailRemindPeriod { get; set; }
    }
}