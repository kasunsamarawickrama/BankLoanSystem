using BankLoanSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Models
{
    public class LoanSetupStep1
    {
        //public IList<UnitType> _allUnitTypes = (new LoanSetupAccess()).getAllUnitTypes();

        [Required(ErrorMessage = "Please select the branch which the loan associated with.")]
        [Display(Name = "NonRegistered Branch")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select")]
        public int nonRegisteredBranchId { get; set; }

        [Required(ErrorMessage = "Please select the branch which the loan associated with.")]
        [Display(Name = "Registered Branch")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select")]
        //[Remote("IsLoanNumberExists", "SetupProcess",
        //AdditionalFields = "loanNumber",
        //HttpMethod = "POST",
        //ErrorMessage = "Loan Number already in use")]
        public int RegisteredBranchId { get; set; }

        public string RegisteredBranchCode { get; set; }
        public string RegisteredCompanyCode { get; set; }

        [Required(ErrorMessage = "Please enter a Loan Number")]
        [RegularExpression(@"^[^<>@#$'{}!*?~;`%""]*$", ErrorMessage = "Invalid Character")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "The Loan Number should be between 5 and 30 characters")]
        //[Remote("IsLoanNumberExists", "SetupProcess", ErrorMessage = "Loan Number already in use")]
        [Display(Name = "Loan Number")]
        //[Remote("IsLoanNumberExists", "SetupProcess",
        //AdditionalFields = "RegisteredBranchId",
        //HttpMethod = "POST",
        //ErrorMessage = "Loan Number already in use")]
        public string loanNumber { get; set; }


        //[Required(ErrorMessage = "Please enter a Loan Number")]
        //public string loanNumberForDisplay { get; set; }

        [Required(ErrorMessage = "Please select the Start Date of the Loan")]
        [Display(Name = "Start Date")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime startDate { get; set; }

        [Display(Name = "Created Date")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Please select the Maturity Date of the Loan")]
        [Display(Name = "Maturity Date")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}",  ApplyFormatInEditMode = true)]
        public DateTime maturityDate { get; set; }

        [Required(ErrorMessage = "Please select the Start Date of the Loan")]
        public string DisplayStartDate { get; set; }

        [Required(ErrorMessage = "Please select the Maturity Date of the Loan")]
        public string DisplayMaturityDate { get; set; }

        //[Required(ErrorMessage = "Please enter the PayOff Period")]
        //[Display(Name = "Pay Off Period")]
        //[RegularExpression("([0-9][0-9]*)", ErrorMessage = "Invalid Data")]
        //[Range(1, int.MaxValue, ErrorMessage = "Please enter Pay Off Period greater than 0")]
        //[Remote("CheckTheRangeOfPayOffPeriod", "SetupProcess",
        //AdditionalFields = "startDate,maturityDate,payOffPeriodType",
        //HttpMethod = "POST",
        //ErrorMessage = "Invalid")]
        //public int payOffPeriod { get; set; }

        [Required(ErrorMessage = "Please select the type of PayOff")]
        [Display(Name = "Pay Off Period Type")]
        [Range(0, 1, ErrorMessage = "Please select the type of PayOff")]
        public int payOffPeriodType { get; set; }

        [Required(ErrorMessage = "Please enter the Loan Amount")]
        [Display(Name = "Loan Amount")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        //[DataType()]
        [Range(0.009, double.MaxValue, ErrorMessage = "Please enter a Loan Amount greater than $1")]
        public decimal loanAmount { get; set; }

        [Required(ErrorMessage = "Please enter the initial percentage to be advanced")]
        [Display(Name = "Advance Percentage")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Advance Percentage must be a natural number")]
        [Range(1, 100, ErrorMessage = "Percentage must be between 1 and 100")]
        public int advancePercentage { get; set; }

        [Required(ErrorMessage = "Please select Payment Method for the loan")]
        [Display(Name = "Payment Method")]
        public string paymentMethod { get; set; }

        
        [Display(Name = "Current Loan Status")]
        public bool CurrentLoanStatus { get; set; }

        [Required]
        [Display(Name = "Unit Types")]
        public IList<UnitType> selectedUnitTypes { get; set; }

        [Required(ErrorMessage = "Please select which type of unit will be the most common for the loan")]
        [Display(Name = "Make Default")]
        public int defaultUnitType { get; set; }

        [Required(ErrorMessage = "Please select at least one unit type to be advanced on this loan")]
        [Display(Name = "Select Unit Types")]
        public IList<UnitType> allUnitTypes
        {
            get; set;

        }

        [Required(ErrorMessage = "Please enter the Email Address to send the loan renewal reminder to")]
        [EmailAddress]
        [Display(Name = "Auto Reminder Email")]
        public string autoReminderEmail
        {
            get; set;

        }

        [Required(ErrorMessage = "Please choose when to email the reminder for loan renewal")]
        [Display(Name = "How many days prior to the maturity date the renewal reminder should be sent")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Invalid")]
        [Range(1, int.MaxValue, ErrorMessage = "Auto Reminder Period must be greater than zero")]
        public int autoReminderPeriod { get; set; }

        [Required(ErrorMessage = "Please select if you will allow the advance amount to be changed on this loan.")]
        [Display(Name = "Is Edit Allowable for an authorized user to change the loan Amount or Advanced percentage")]
        public bool isEditAllowable
        {
            get; set;

        }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Do you want to track the interest")]
        public bool isInterestCalculate
        {
            get; set;

        }

        [Required(ErrorMessage = "Please select if you would like a reminder emailed for the loan renewal.")]
        public bool autoReminder
        {
            get; set;

        }

        public int loanId { get; set; }

        public bool LoanStatus { get; set; }

        public string loanCode;

        public bool titleTracked { get; set; }

        public string CurtailmentCalculationBase { get; set; }
        public string CurtailmentDueDate { get; set; }
        public string CurtailmentAutoRemindEmail { get; set; }
        public int CurtailmentEmailRemindPeriod { get; set; }

        public IList<Curtailment> curtailmetList
        {
            get; set;

        }

        public string rightId { get; set; }

        public int AdvanceFee { get; set; }
        public int LotInspectionFee { get; set; }
        public int MonthlyLoanFee { get; set; }
        public bool AdvanceFeePayAtPayoff { get; set; }
    }

    public class UnitType
    {
        
        public int unitTypeId { get; set; }

        public string unitTypeName { get; set; }

        public bool isSelected { get; set; }
    }

    
}