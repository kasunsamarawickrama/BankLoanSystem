﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankLoanSystem.Models
{
    public class Unit
    {
        public string UnitId { get; set; }
        
        public vehicle vehicle { get; set; }

        public rv rv { get; set; }

        public camper camper { get; set; }

        public boat boat { get; set; }

        public atv atv { get; set; }

        public snowmobile snowmobile { get; set; }

        public heavyequipment heavyequipment { get; set; }

        public motorcycle motorcycle { get; set; }

        [Display(Name = "VIN")]
        [Required(ErrorMessage = "Enter 17 characters for VIN")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{13}[0-9]{4}$", ErrorMessage = "Invalid VIN Format.")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Display(Name = "SN")]
        [Required(ErrorMessage = "Vehicle Serial Number is required.")]
        public string SerialNumber { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Trim { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        public double Miles { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Color { get; set; }

        [Display(Name = "Condition")]
        public bool NewOrUsed { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        public double Length { get; set; }

        [Required]
        [Display(Name = "Hitch Style")]
        public string HitchStyle { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        public double Speed { get; set; }

        [Required]
        [Display(Name = "Trailer")]
        public string TrailerId { get; set; }

        [Required]
        [Display(Name = "Engine Serial")]
        public string EngineSerial { get; set; }

        [Required]
        //[Remote("CalculateAdvance", "Unit", ErrorMessage = "error")]
        [Range(0.009, double.MaxValue, ErrorMessage = "Out of range")]
        public decimal Cost { get; set; }

        [Required]
        [Display(Name = "Advance Amount")]
        [Range(0.009, double.MaxValue, ErrorMessage = "Out of range")]
        public decimal AdvanceAmount { get; set; }

        public bool IsTitleReceived { get; set; }

        [Required(ErrorMessage = "Please select")]
        [Display(Name = "Title(Document) Received")]
        public string TitleReceived { get; set; }

        public string Note { get; set; }

        public DateTime AdvanceDate { get; set; }

        [Required(ErrorMessage = "The Advance Date is required")]
        public string DisplayAdvanceDate { get; set; }

        public bool AddAndAdvance { get; set; }

        public bool IsAdvanced { get; set; }

        [Required(ErrorMessage = "Please select")]
        [Display(Name = "Do you also want to advance this unit now")]
        public string AdvanceNow { get; set; }

        public bool IsApproved { get; set; }

        [Display(Name = "Unit Type")]
        public int UnitTypeId { get; set; }

        public int LoanId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public int AdvancePt { get; set; }

        public string Status { get; set; }

        public decimal LoanAmount { get; set; }

        public decimal Balance { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //Image
        public string FileName { get; set; }

        
    }

    public class LoanPaymentDetails {

        public decimal Amount;
        public decimal UsedAmount;
        public decimal BalanceAmount;
        public decimal PendingAmount;
        public decimal BalanceAfterPending;

    }

    public class JustAddedUnit
    {

        public int userId;
        public string model;
        public decimal advanceAmount;
        public bool isAdvance;

    }

    public class TitleUpload
    {
        public int UploadId { get; set; }
        public string FilePath { get; set; }
        public string UnitId { get; set; }
        public string OriginalFileName { get; set; }
    }

    public class ListViewModel
    {
        public List<Unit> ItemList { get; set; }

    }

    public class vehicle
    {

        [Display(Name = "VIN")]
        [Required(ErrorMessage = "Enter 17 characters for VIN")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{13}[0-9]{4}$", ErrorMessage = "Invalid VIN Format.")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Trim { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        public double Miles { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Color { get; set; }

        [Display(Name = "Condition")]
        public bool NewOrUsed { get; set; }

    }

    public class rv
    {

        [Display(Name = "VIN")]
        [Required(ErrorMessage = "Enter 17 characters for VIN")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{13}[0-9]{4}$", ErrorMessage = "Invalid VIN Format.")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        public double Miles { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        public double Length { get; set; }

        [Display(Name = "Condition")]
        public bool NewOrUsed { get; set; }

    }

    public class camper
    {

        [Display(Name = "VIN")]
        [Required(ErrorMessage = "Enter 17 characters for VIN")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{13}[0-9]{4}$", ErrorMessage = "Invalid VIN Format.")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        public double Length { get; set; }

        [Required]
        [Display(Name = "Hitch Style")]
        public string HitchStyle { get; set; }

        [Display(Name = "Condition")]
        public bool NewOrUsed { get; set; }

    }
    public class atv
    {

        [Display(Name = "VIN")]
        [Required(ErrorMessage = "Enter 17 characters for VIN")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{13}[0-9]{4}$", ErrorMessage = "Invalid VIN Format.")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        public double Miles { get; set; }

        [Display(Name = "Condition")]
        public bool NewOrUsed { get; set; }

    }
    public class boat
    {

        [Display(Name = "VIN")]
        [Required(ErrorMessage = "Enter 17 characters for VIN")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{13}[0-9]{4}$", ErrorMessage = "Invalid VIN Format.")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        [Display(Name = "Trailer")]
        public string TrailerId { get; set; }

        [Required]
        [Display(Name = "Engine Serial")]
        public string EngineSerial { get; set; }

        [Display(Name = "Condition")]
        public bool NewOrUsed { get; set; }

    }
    
    public class motorcycle
    {

        [Display(Name = "VIN")]
        [Required(ErrorMessage = "Enter 17 characters for VIN")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{13}[0-9]{4}$", ErrorMessage = "Invalid VIN Format.")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        public double Miles { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Color { get; set; }

        [Display(Name = "Condition")]
        public bool NewOrUsed { get; set; }

    }
    public class snowmobile
    {
        [Display(Name = "VIN")]
        [Required(ErrorMessage = "Enter 17 characters for VIN")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{13}[0-9]{4}$", ErrorMessage = "Invalid VIN Format.")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Display(Name = "Condition")]
        public bool NewOrUsed { get; set; }

    }
    public class heavyequipment
    {
        [Display(Name = "SN")]
        [Required(ErrorMessage = "Vehicle Serial Number is required.")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Required]
        public string Make { get; set; }

    }
}