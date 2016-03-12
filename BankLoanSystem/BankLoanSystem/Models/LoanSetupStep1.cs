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

        [Required]
        [Display(Name = "NonRegistered Branch")]
        [Range(1, int.MaxValue, ErrorMessage = "Branch is Required")]
        public int nonRegisteredBranchId { get; set; }

        [Required]
        [Display(Name = "Registered Branch")]
        [Range(1, int.MaxValue, ErrorMessage = "Branch is Required")]
        public int RegisteredBranchId { get; set; }

        public string RegisteredBranchCode { get; set; }
        public string RegisteredCompanyCode { get; set; }

        [Required]
        [RegularExpression(@"^[^<>@#$'{}!*?~;`%""]*$", ErrorMessage = "Invalid Character")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Minimum 5 and Maximum 30 characters required")]
        //[Remote("IsLoanNumberExists", "SetupProcess", ErrorMessage = "Loan Number already in use")]
        [Display(Name = "Loan Number")]
        [Remote("IsLoanNumberExists", "SetupProcess",
        AdditionalFields = "RegisteredBranchId",
        HttpMethod = "POST",
        ErrorMessage = "Loan Number already in use")]
        public string loanNumber { get; set; }


        [Required(ErrorMessage = "The Loan Number field is required.")]
        public string loanNumberForDisplay { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime startDate { get; set; }

        [Required]
        [Display(Name = "Maturity Date")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}",  ApplyFormatInEditMode = true)]
        public DateTime maturityDate { get; set; }

        [Required(ErrorMessage = "The Start Date is required")]
        public string DisplayStartDate { get; set; }

        [Required(ErrorMessage = "The Maturity Date is required")]
        public string DisplayMaturityDate { get; set; }

        [Required]
        [Display(Name = "Pay Off Period")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Invalid Data")]
        [Range(1, int.MaxValue, ErrorMessage = "Pay Off Period must be greater than zero")]
        //[Remote("CheckTheRangeOfPayOffPeriod", "SetupProcess",
        //AdditionalFields = "startDate,maturityDate,payOffPeriodType",
        //HttpMethod = "POST",
        //ErrorMessage = "Invalid")]
        public int payOffPeriod { get; set; }

        [Required]
        [Display(Name = "Pay Off Period Type")]
        [Range(0, 1, ErrorMessage = "Pay Off Period Type is Required")]
        public int payOffPeriodType { get; set; }

        [Required]
        [Display(Name = "Loan Amount")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        //[DataType()]
        [Range(0.009, double.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public decimal loanAmount { get; set; }

        [Required(ErrorMessage = "Required/Invalid")]
        [Display(Name = "Advance Percentage")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Advance Percentage must be a natural number")]
        [Range(1, 100, ErrorMessage = "Percentage must be between 1 and 100")]
        public int advancePercentage { get; set; }

        [Required]
        [Display(Name = "Payment Method")]
        public string paymentMethod { get; set; }

        [Required]
        [Display(Name = "Unit Types")]
        public IList<UnitType> selectedUnitTypes { get; set; }

        [Required(ErrorMessage = "Default Unit type is required")]
        [Display(Name = "Make Default")]
        public int defaultUnitType { get; set; }

        [Required]
        [Display(Name = "Select Unit Types")]
        public IList<UnitType> allUnitTypes
        {
            get; set;

        }

        [Required]
        [EmailAddress]
        [Display(Name = "Auto Reminder Email")]
        public string autoReminderEmail
        {
            get; set;

        }

        [Required(ErrorMessage = "Required/Invalid")]
        [Display(Name = "How many days prior to the maturity date the renewal reminder should be sent")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Invalid")]
        [Range(1, int.MaxValue, ErrorMessage = "Auto Reminder Period must be greater than zero")]
        public int autoReminderPeriod { get; set; }

        [Required(ErrorMessage = "Required")]
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

        [Required(ErrorMessage = "Required")]
        public bool autoReminder
        {
            get; set;

        }

        public int loanId { get; set; }

        public bool LoanStatus { get; set; }

        public string loanCode;
    }

    public class UnitType
    {
        
        public int unitTypeId { get; set; }

        public string unitTypeName { get; set; }

        public bool isSelected { get; set; }
    }

}