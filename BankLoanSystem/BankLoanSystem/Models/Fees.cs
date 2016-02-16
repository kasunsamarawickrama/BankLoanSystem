﻿using System;
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
        [RegularExpression(@"^[0-9]+.?[0-9]{0,2}$", ErrorMessage = "Invalid; Maximum 2 Decimal Points.")]
        [Required(ErrorMessage = "Advance Amount is required.")]
        [Display(Name = "Amount")]
        public double AdvanceAmount { get; set; }
        [Display(Name = "Recipt for Advance Paid")]
        public bool AdvanceNeedReceipt { get; set; }
        [Required(ErrorMessage = "Advance Due Type is required.")]
        [Display(Name = "Due Type")]
        public string AdvanceDue { get; set; }
        [Required(ErrorMessage = "Advance Due Date is required.")]
        [Display(Name = "Due Date")]
        public string AdvanceDueDate { get; set; }
        public bool AdvanceRadio { get; set; }
        public bool IsAdvanceEmailReminder { get; set; }
        [EmailAddress]
        [Display(Name = "Lender Email")]
        public string AdvanceLenderEmail { get; set; }
        [Display(Name = "Email Remind Period")]
        public int AdvanceLenderEmailRemindPeriod { get; set; }
        [EmailAddress]
        [Display(Name = "Dealer Email")]
        public string AdvanceDealerEmail { get; set; }
        [Display(Name = "Email Remind Period")]
        public int AdvanceDealerEmailRemindPeriod { get; set; }

        [Display(Name = "Monthly Loan")]
        public int MonthlyLoanId { get; set; }
        [RegularExpression(@"^[0-9]+.?[0-9]{0,2}$", ErrorMessage = "Invalid; Maximum 2 Decimal Points.")]
        [Required(ErrorMessage = "Loan Amount is required.")]
        [Display(Name = "Amount")]
        public double MonthlyLoanAmount { get; set; }
        [Display(Name = "Recipt for Loan Paid")]
        public bool MonthlyLoanNeedReceipt { get; set; }
        [Required(ErrorMessage = "Loan Due Type is required.")]
        [Display(Name = "Due Type")]
        public string MonthlyLoanDue { get; set; }
        public bool MonthlyLoanRadio { get; set; }
        [Required(ErrorMessage = "Loan Due Date is required.")]
        [Display(Name = "Due Date")]
        public string MonthlyLoanDueDate { get; set; }
        public bool IsLoanEmailReminder { get; set; }
        [EmailAddress]
        [Display(Name = "Lender Email")]
        public string MonthlyLoanLenderEmail { get; set; }
        [EmailAddress]
        [Display(Name = "Dealer Email")]
        public string MonthlyLoanDealerEmail { get; set; }
        [Display(Name = "Email Remind Period")]
        public int MonthlyLoanLenderEmailRemindPeriod { get; set; }
        [Display(Name = "Email Remind Period")]
        public int MonthlyLoanDealerEmailRemindPeriod { get; set; }


        [Display(Name = "Lot Inspection")]
        public int LotInspectionId { get; set; }
        [RegularExpression(@"^[0-9]+.?[0-9]{0,2}$", ErrorMessage = "Invalid; Maximum 2 Decimal Points.")]
        [Required(ErrorMessage = "Lot Amount is required.")]
        [Display(Name = "Amount")]
        public double LotInspectionAmount { get; set; }
        [Display(Name = "Recipt for Lot Inspection Paid")]
        public bool LotInspectionNeedReceipt { get; set; }
        [Required(ErrorMessage = "Lot Due Type is required.")]
        [Display(Name = "Due Type")]
        public string LotInspectionDue { get; set; }
        public bool LotInspectionRadio { get; set; }
        [Required(ErrorMessage = "Lot Due Date is required.")]
        [Display(Name = "Due Date")]
        public string LotInspectionDueDate { get; set; }
        public bool IsLotEmailReminder { get; set; }
        [EmailAddress]
        [Display(Name = "Lender Email")]
        public string LotInspectionLenderEmail { get; set; }
        [EmailAddress]
        [Display(Name = "Dealer Email")]
        public string LotInspectionDealerEmail { get; set; }
        [Display(Name = "Email Remind Period")]
        public int LotInspectionLenderEmailRemindPeriod { get; set; }
        [Display(Name = "Email Remind Period")]
        public int LotInspectionDealerEmailRemindPeriod { get; set; }

        public bool isEdit { get; set; }

    }
}