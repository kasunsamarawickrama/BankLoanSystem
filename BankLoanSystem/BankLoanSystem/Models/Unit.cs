using System;
using System.ComponentModel.DataAnnotations;

namespace BankLoanSystem.Models
{
    public class Unit
    {
    public int UnitId { get; set; }

        [Required(ErrorMessage = "Vehicle Identification Number is required.")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        public int Year { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Trim { get; set; }

        public double Miles { get; set; }

        public string Color { get; set; }

        public bool NewOrUsed { get; set; }

        public double Length { get; set; }

        public string HitchStyle { get; set; }

        public double Speed { get; set; }

        public string TrailerId { get; set; }

        public string EngineSerial { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        [Display(Name = "Advance Amount")]
        public decimal AdvanceAmount { get; set; }

        public bool IsTitleReceived { get; set; }

        [Required]
        [Display(Name = "Title(Document) Received")]
        public string TitleReceived { get; set; }

        public String Note { get; set; }

        public DateTime AdvanceDate { get; set; }

        public bool AddAndAdvance { get; set; }

        public bool IsAdvanced { get; set; }

        public bool IsApproved { get; set; }

        public int UnitTypeId { get; set; }

        public int LoanId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }


    }
}