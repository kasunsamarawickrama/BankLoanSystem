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
        public double AdvanceAmount { get; set; }
        public bool AdvanceNeedReceipt { get; set; }
        public string AdvanceDue { get; set; }
        public string AdvanceDueDate { get; set; }
        public string AdvanceRadio { get; set; }
        public string AdvanceLenderEmail { get; set; }
        public int AdvanceLenderEmailRemindPeriod { get; set; }
        public string AdvanceDealerEmail { get; set; }
        public int AdvanceDealerEmailRemindPeriod { get; set; }

        [Display(Name = "Monthly Loan")]
        public int MonthlyLoanId { get; set; }
        public double MonthlyLoanAmount { get; set; }
        public bool MonthlyLoanNeedReceipt { get; set; }
        public string MonthlyLoanDue { get; set; }
        public string MonthlyLoanRadio { get; set; }
        public string MonthlyLoanDueDate { get; set; }
        public string MonthlyLoanLenderEmail { get; set; }
        public string MonthlyLoanDealerEmail { get; set; }
        public int MonthlyLoanLenderEmailRemindPeriod { get; set; }
        public int MonthlyLoanDealerEmailRemindPeriod { get; set; }

        [Display(Name = "Lot Inspection")]
        public int LotInspectionId { get; set; }
        public double LotInspectionAmount { get; set; }
        public bool LotInspectionNeedReceipt { get; set; }
        public string LotInspectionDue { get; set; }
        public string LotInspectionDueDate { get; set; }
        public string LotInspectionLenderEmail { get; set; }
        public string LotInspectionDealerEmail { get; set; }
        public int LotInspectionLenderEmailRemindPeriod { get; set; }
        public int LotInspectionDealerEmailRemindPeriod { get; set; }
    }
}