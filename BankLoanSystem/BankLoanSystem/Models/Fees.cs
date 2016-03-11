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

        [Required(ErrorMessage = "Advance Amount is required.")]
        [Display(Name = "Amount")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]+.[0-9]{0,2}|^.[0-9]{0,2}$", ErrorMessage = "Should be numeric values and Maximum 2 decimal points.")]
        public decimal AdvanceAmount { get; set; }
        [Required(ErrorMessage = "Advance reciept need required.")]
        [Display(Name = "Recipt for Advance Paid")]
        public bool AdvanceNeedReceipt { get; set; }

        [Required(ErrorMessage = "Advance Fee calculate Type is required.")]
        [Display(Name = "Advance Fee calculate")]
        public string AdvanceFeeCalculateType { get; set; }
        [Required(ErrorMessage = "Advance fee complete Remind Email is required.")]
        public bool IsAdvanceFeeCompleteEmailReminder { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Advance Email is required.")]
        [Display(Name = "Dealer Email")]
        public string AdvanceFeeDealerEmail { get; set; }

        [Display(Name = "Email Remind Period")]
        [Required(ErrorMessage = "Advance Email Remind numeric value Period is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]{1,10000}$", ErrorMessage = "No decimal points allowed.")]
        public int AdvanceFeeDealerEmailRemindPeriod { get; set; }

        [Required(ErrorMessage = "Advance Due Type is required.")]
        [Display(Name = "Due Type")]
        public string AdvanceDue { get; set; }

        [Required(ErrorMessage = "Advance Due Date is required.")]
        [Display(Name = "Due Date")]
        public string AdvanceDueDate { get; set; }
        [Required(ErrorMessage = "Advance fee Due Remind Email is required.")]
        public bool IsAdvanceFeeDueEmailReminder { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Remind Email is required.")]
        [Display(Name = "Remind Email")]
        public string AdvanceDueEmail { get; set; }

        [Display(Name = "Email Remind Period")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]{1,10000}$", ErrorMessage = "No decimal points allowed.")]
        [Required(ErrorMessage = "Email Remind Period numeric value is required.")]
        public int AdvanceDueEmailRemindPeriod { get; set; }




        [Display(Name = "Monthly Loan")]
        public int MonthlyLoanId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]+.[0-9]{0,2}|^.[0-9]{0,2}$", ErrorMessage = "Should be numeric values and Maximum 2 decimal points.")]
        [Required(ErrorMessage = "Loan Amount is required.")]
        [Display(Name = "Amount")]
        public decimal MonthlyLoanAmount { get; set; }
        [Required(ErrorMessage = "loan reciept need required.")]
        [Display(Name = "Recipt for Loan Paid")]
        public bool MonthlyLoanNeedReceipt { get; set; }
        [Required(ErrorMessage = "loan fee complete Remind Email is required.")]
        public bool IsLoanFeeCompleteEmailReminder { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Dealer Email is required.")]
        [Display(Name = "Dealer Email")]
        public string MonthlyLoanFeeDealerEmail { get; set; }

        [Display(Name = "Email Remind Period")]
        [Required(ErrorMessage = "Email Remind Period numeric value is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]{1,10000}$", ErrorMessage = "No decimal points allowed.")]
        public int MonthlyLoanFeeDealerEmailRemindPeriod { get; set; }

        [Required(ErrorMessage = "Loan Due Type is required.")]
        [Display(Name = "Due Type")]
        public string MonthlyLoanDue { get; set; }

        [Required(ErrorMessage = "Loan Due Date is required.")]
        [Display(Name = "Due Date")]
        public string MonthlyLoanDueDate { get; set; }
        [Required(ErrorMessage = "loan fee Due Remind Email is required.")]
        public bool IsLoanFeeDueEmailReminder { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Remind Email is required.")]
        [Display(Name = "Remind Email")]
        public string MonthlyLoanDueEmail { get; set; }

        [Display(Name = "Email Remind Period")]
        [Required(ErrorMessage = "Email Remind Period numeric value is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]{1,10000}$", ErrorMessage = "No decimal points allowed.")]
        public int MonthlyLoanDueEmailRemindPeriod { get; set; }




        [Display(Name = "Lot Inspection")]
        public int LotInspectionId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]+.[0-9]{0,2}|^.[0-9]{0,2}$", ErrorMessage = "Should be numeric values and Maximum 2 decimal points.")]
        [Required(ErrorMessage = "Lot Amount is required.")]
        [Display(Name = "Amount")]
        public decimal LotInspectionAmount { get; set; }
        [Required(ErrorMessage = "lot reciept need required.")]
        [Display(Name = "Recipt for Lot Inspection Paid")]
        public bool LotInspectionNeedReceipt { get; set; }
        [Required(ErrorMessage = "lot fee complete Remind Email is required.")]
        public bool IsLotFeeCompleteEmailReminder { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Dealer Email is required.")]
        [Display(Name = "Dealer Email")]
        public string LotInspectionFeeDealerEmail { get; set; }

        [Display(Name = "Email Remind Period")]
        [Required(ErrorMessage = "Email Remind Period numeric value is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]{1,10000}$", ErrorMessage = "No decimal points allowed.")]
        public int LotInspectionFeeDealerEmailRemindPeriod { get; set; }

        [Required(ErrorMessage = "Lot Due Type is required.")]
        [Display(Name = "Due Type")]
        public string LotInspectionDue { get; set; }

        [Required(ErrorMessage = "Lot Due Date is required.")]
        [Display(Name = "Due Date")]
        public string LotInspectionDueDate { get; set; }
        [Required(ErrorMessage = "lot fee Due Remind Email is required.")]
        public bool IsLotFeeDueEmailReminder { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Remind Email is required.")]
        [Display(Name = "Remind Email")]
        public string LotInspectionDueEmail { get; set; }

        [Display(Name = "Email Remind Period")]
        [Required(ErrorMessage = "Email Remind Period numeric value is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]{1,10000}$", ErrorMessage = "No decimal points allowed.")]
        public int LotInspectionDueEmailRemindPeriod { get; set; }

        public bool isEdit { get; set; }



    }
}