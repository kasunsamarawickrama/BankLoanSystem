﻿using BankLoanSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Models
{
    public class LoanSetupStep1
    {
        //public IList<UnitType> _allUnitTypes = (new LoanSetupAccess()).getAllUnitTypes();

        [Required]
        [Display(Name = "NonRegistered Branch")]
        public int nonRegisteredBranchId { get; set; }

        [Required]
        [Display(Name = "Registered Branch")]
        public int RegisteredBranchId { get; set; }

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

        [Required]
        [Display(Name = "Loan Amount")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        //[DataType()]
        public decimal loanAmount { get; set; }

        [Required]
        [Display(Name = "Advance Percentage")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Count must be a natural number")]
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
    }

    public class UnitType
    {
        
        public int unitTypeId { get; set; }

        public string unitTypeName { get; set; }

        public bool isSelected { get; set; }
    }

}