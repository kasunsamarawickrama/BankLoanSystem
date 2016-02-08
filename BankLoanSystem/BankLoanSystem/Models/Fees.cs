using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class Fees
    {
        [Display(Name = "Advance")]
        public int AdvanceId { get; set; }
        public string AdvanceAmount { get; set; }
        public DateTime AdvanceDue { get; set; }
        public string AdvanceReminder { get; set; }

        [Display(Name = "Monthly Loan")]
        public int MonthlyLoanId { get; set; }
        public string MonthlyLoanAmount { get; set; }
        public DateTime MonthlyLoanDue { get; set; }
        public string MonthlyLoanReminder { get; set; }

        [Display(Name = "Lot Inspection")]
        public int LotInspectionId { get; set; }
        public string LotInspectionAmount { get; set; }
        public DateTime LotInspectionDue { get; set; }
        public string LotInspectionReminder { get; set; }
    }
}