using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankLoanSystem.Models
{
    public class Unit
    {
        public string UnitId { get; set; }

        [Display(Name = "Identification Number")]
        [Required(ErrorMessage = "Vehicle Identification Number is required.")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{13}[0-9]{4}$", ErrorMessage = "Invalid Vehicle Identification Number Format.")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        public int Year { get; set; }

        [Display(Name = "Serial Number")]
        [Required(ErrorMessage = "Vehicle Serial Number is required.")]
        public string SerialNumber { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Trim { get; set; }

        [Range(0, 1000, ErrorMessage = "Please enter a value bigger than zero")]
        public double Miles { get; set; }

        public string Color { get; set; }

        [Display(Name = "New or Used")]
        public bool NewOrUsed { get; set; }

        [Range(0, 1000, ErrorMessage = "Please enter a value bigger than zero")]
        public double Length { get; set; }

        [Display(Name = "Hitch Style")]
        public string HitchStyle { get; set; }

        [Range(0, 1000, ErrorMessage = "Please enter a value bigger than zero")]
        public double Speed { get; set; }

        [Display(Name = "Trailer")]
        public string TrailerId { get; set; }

        [Display(Name = "Engine Serial")]
        public string EngineSerial { get; set; }

        [Required]
        //[Remote("CalculateAdvance", "Unit", ErrorMessage = "error")]
        public decimal Cost { get; set; }

        [Required]
        [Display(Name = "Advance Amount")]
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

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class LoanPaymentDetails {


        public decimal Amount;
        public decimal UsedAmount;
        public decimal BalanceAmount;
        public decimal PendingAmount;
        public decimal BalanceAfterPending;



    }
}