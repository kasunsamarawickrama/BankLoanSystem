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
        [Required(ErrorMessage = "Please select if there is an Advance Fee for each unit advanced")]
        [Display(Name = "Per Item Advance Fee")]
        public string AdvanceId { get; set; }

        [Required(ErrorMessage = "Please enter the Advance fee for each unit")]
        [Display(Name = "Per Unit Advanced Fee Amount")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter an Advance Amount greater than $1")]
        [RegularExpression(@"^[+-]?[0-9]+.[0-9]{0,2}|^.[0-9]{0,2}$", ErrorMessage = "Should be numeric values and Maximum 2 decimal points.")]
        public decimal AdvanceAmount { get; set; }
        [Required(ErrorMessage = "Advance reciept need required.")]
        [Display(Name = "Recipt for Advance Paid")]
        public bool AdvanceNeedReceipt { get; set; }

        [Required(ErrorMessage = "Please chose when the advance fee is due")]
        [Display(Name = "Advance Fee calculate")]
        public string AdvanceFeeCalculateType { get; set; }
        [Required(ErrorMessage = "Advance fee complete Remind email is required.")]
        public bool IsAdvanceFeeCompleteEmailReminder { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Advance email is required.")]
        [Display(Name = "Dealer email")]
        public string AdvanceFeeDealerEmail { get; set; }

        [Display(Name = "email Remind Period")]
        [Required(ErrorMessage = "Advance email Remind numeric value Period is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]{1,10000}$", ErrorMessage = "No decimal points allowed.")]
        public int AdvanceFeeDealerEmailRemindPeriod { get; set; }

        [Required(ErrorMessage = "Advance Due Type is required.")]
        [Display(Name = "Due Type")]
        public string AdvanceDue { get; set; }

        [Required(ErrorMessage = "Advance Due Date is required.")]
        [Display(Name = "Due Date")]
        public string AdvanceDueDate { get; set; }
        [Required(ErrorMessage = "Advance fee Due Remind email is required.")]
        public bool IsAdvanceFeeDueEmailReminder { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Remind email is required.")]
        [Display(Name = "Remind email")]
        public string AdvanceDueEmail { get; set; }

        [Display(Name = "email Remind Period")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]{1,10000}$", ErrorMessage = "No decimal points allowed.")]
        [Required(ErrorMessage = "email Remind Period numeric value is required.")]
        public int AdvanceDueEmailRemindPeriod { get; set; }




        [Display(Name = "Monthly Load Administration Fee")]
        [Required(ErrorMessage = "Please select if there is a separate Monthly Loan Fee")]
        public string MonthlyLoanId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]+.[0-9]{0,2}|^.[0-9]{0,2}$", ErrorMessage = "Should be numeric values and Maximum 2 decimal points.")]
        [Required(ErrorMessage = "Please enter the Monthly Loan fee")]
        [Display(Name = "Amount")]
        public decimal MonthlyLoanAmount { get; set; }
        [Required(ErrorMessage = "loan reciept need required.")]
        [Display(Name = "Recipt for Loan Paid")]
        public bool MonthlyLoanNeedReceipt { get; set; }
        [Required(ErrorMessage = "loan fee complete Remind email is required.")]
        public bool IsLoanFeeCompleteEmailReminder { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Dealer email is required.")]
        [Display(Name = "Dealer email")]
        public string MonthlyLoanFeeDealerEmail { get; set; }

        [Display(Name = "email Remind Period")]
        [Required(ErrorMessage = "email Remind Period numeric value is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]{1,10000}$", ErrorMessage = "No decimal points allowed.")]
        public int MonthlyLoanFeeDealerEmailRemindPeriod { get; set; }

        [Required(ErrorMessage = "Loan Due Type is required.")]
        [Display(Name = "Due Type")]
        public string MonthlyLoanDue { get; set; }

        [Required(ErrorMessage = "Loan Due Date is required.")]
        [Display(Name = "Due Date")]
        public string MonthlyLoanDueDate { get; set; }
        [Required(ErrorMessage = "loan fee Due Remind email is required.")]
        public bool IsLoanFeeDueEmailReminder { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Remind email is required.")]
        [Display(Name = "Remind email")]
        public string MonthlyLoanDueEmail { get; set; }

        [Display(Name = "email Remind Period")]
        [Required(ErrorMessage = "email Remind Period numeric value is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]{1,10000}$", ErrorMessage = "No decimal points allowed.")]
        public int MonthlyLoanDueEmailRemindPeriod { get; set; }




        [Display(Name = "Lot Inventory Inspection Fee")]
        [Required(ErrorMessage = "Please select if there is a Lot / Inventory Inspection Fee")]
        public string LotInspectionId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]+.[0-9]{0,2}|^.[0-9]{0,2}$", ErrorMessage = "Should be numeric values and Maximum 2 decimal points.")]
        [Required(ErrorMessage = "Lot Amount is required.")]
        [Display(Name = "Amount")]
        public decimal LotInspectionAmount { get; set; }
        [Required(ErrorMessage = "lot reciept need required.")]
        [Display(Name = "Recipt for Lot Inspection Paid")]
        public bool LotInspectionNeedReceipt { get; set; }
        [Required(ErrorMessage = "lot fee complete Remind email is required.")]
        public bool IsLotFeeCompleteEmailReminder { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Dealer email is required.")]
        [Display(Name = "Dealer email")]
        public string LotInspectionFeeDealerEmail { get; set; }

        [Display(Name = "email Remind Period")]
        [Required(ErrorMessage = "email Remind Period numeric value is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]{1,10000}$", ErrorMessage = "No decimal points allowed.")]
        public int LotInspectionFeeDealerEmailRemindPeriod { get; set; }

        [Required(ErrorMessage = "Lot Due Type is required.")]
        [Display(Name = "Due Type")]
        public string LotInspectionDue { get; set; }

        [Required(ErrorMessage = "Lot Due Date is required.")]
        [Display(Name = "Due Date")]
        public string LotInspectionDueDate { get; set; }
        [Required(ErrorMessage = "lot fee Due Remind email is required.")]
        public bool IsLotFeeDueEmailReminder { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Remind email is required.")]
        [Display(Name = "Remind email")]
        public string LotInspectionDueEmail { get; set; }

        [Display(Name = "email Remind Period")]
        [Required(ErrorMessage = "email Remind Period numeric value is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]{1,10000}$", ErrorMessage = "No decimal points allowed.")]
        public int LotInspectionDueEmailRemindPeriod { get; set; }

        public bool isEdit { get; set; }

        public int FeeId { get; set; }
        public string UnitId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime BillDueDate { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime AdvanceDate { get; set; }
        public bool isPaid { get; set; }
        public int PaidUserId { get; set; }
        public string IdentificationNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string DueDate { get; set; }
        public DateTime FeeDueDate { get; set; }

    }

    public class FeesModel
    {
        //public string DueDate { get; set; }
        public DateTime FeePayDate { get; set; }
        public List<Fees> FeeModelList { get; set; }

        public string IdentificationNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime BillDueDate { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime AdvanceDate { get; set; }
        public DateTime FeeDueDate { get; set; }
        public string Description { get; set; }
    }

}