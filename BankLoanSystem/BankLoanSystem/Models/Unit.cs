using System;
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
        [Required(ErrorMessage = "Please enter all 17 Characters of the vehicle VIN")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{13}[0-9]{4}$", ErrorMessage = "Invalid VIN Format.")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Please select the vehicle year")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Vehicle Serial Number is required.")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "Please enter the vehicle Make")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Please enter the vehicle Model")]
        public string Model { get; set; }

        [Required]
        public string Trim { get; set; }


        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        public double? Miles { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Color { get; set; }

        [Display(Name = "Condition")]
        public bool NewOrUsed { get; set; }


        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        public double? Length { get; set; }


        [Display(Name = "Hitch Style")]
        public string HitchStyle { get; set; }


        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        public double Speed { get; set; }


        [Display(Name = "Trailer")]
        public string TrailerId { get; set; }


        [Display(Name = "Engine Serial")]
        public string EngineSerial { get; set; }

        [Required(ErrorMessage = "Please enter the cost")]
        //[Remote("CalculateAdvance", "Unit", ErrorMessage = "error")]
        [Range(0.009, double.MaxValue, ErrorMessage = "Out of range")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Please include an advance amount - to auto calculate reenter the Cost field")]
        [Display(Name = "Advance Amount")]
        [Range(0.009, double.MaxValue, ErrorMessage = "Out of range")]
        public decimal AdvanceAmount { get; set; }

        public bool IsTitleReceived { get; set; }

        [Required(ErrorMessage = "Please confirm if the title has been received")]
        [Display(Name = "Has the Title been received?")]
        public string TitleReceived { get; set; }

        public string Note { get; set; }


        [Required]
        [Display(Name = "Advance Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AdvanceDate { get; set; }

        public string AdvanceDateStr { get; set; }

        [Required(ErrorMessage = "Please select the Advance Date")]
        public string DisplayAdvanceDate { get; set; }

        public bool AddAndAdvance { get; set; }

        public bool IsAdvanced { get; set; }

        [Required(ErrorMessage = "Please chose if the vehicle will be advanced at this time")]
        [Display(Name = "Will you advance the unit today?")]
        public string AdvanceNow { get; set; }

        public bool IsApproved { get; set; }

        public int UnitStatus { get; set; }

       
        public int TitleStatus { get; set; }
        public string TitleStatusText { get; set; }

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

        public string CurrentTitleStatus { get; set; }

        public string CurrentUnitStatus { get; set; }


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
        [Required(ErrorMessage = "Please enter all 17 Characters of the vehicle VIN")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{13}[0-9]{4}$", ErrorMessage = "Invalid VIN Format.")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Please select the vehicle year")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please enter the vehicle Make")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Please enter the vehicle Model")]
        public string Model { get; set; }

        public string Trim { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        public double? Miles { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Please select the vehicle condition")]
        [Display(Name = "Condition")]
        public bool NewOrUsed { get; set; }

    }

    public class rv
    {

        [Display(Name = "VIN")]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "Please enter all 17 Characters of the rv VIN")]
        [Required(ErrorMessage = "Please enter all 17 Characters of the rv VIN")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Please select the rv year")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please enter the rv Make")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Please enter the rv Model")]
        public string Model { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        public double? Miles { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]+.[0-9]{0,2}|^.[0-9]{0,2}$", ErrorMessage = "Should be numeric values and Maximum 2 decimal points.")]
        public double? Length { get; set; }

        [Required(ErrorMessage = "Please select the rv condition")]
        [Display(Name = "Condition")]
        public bool NewOrUsed { get; set; }

    }

    public class camper
    {

        [Display(Name = "VIN")]
        [Required(ErrorMessage = "Please enter all 17 Characters of the camper VIN")]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "Please enter all 17 Characters of the camper VIN")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Please select the camper year")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please enter the camper Make")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Please enter the camper Model")]
        public string Model { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]+.[0-9]{0,2}|^.[0-9]{0,2}$", ErrorMessage = "Should be numeric values and Maximum 2 decimal points.")]
        public double? Length { get; set; }

        [Display(Name = "Hitch Style")]
        public string HitchStyle { get; set; }

        [Required(ErrorMessage = "Please select the camper condition")]
        [Display(Name = "Condition")]
        public bool NewOrUsed { get; set; }

    }
    public class atv
    {

        [Display(Name = "VIN")]
        [Required(ErrorMessage = "Please enter all 17 Characters of the atv VIN")]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "Please enter all 17 Characters of the atv VIN")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Please select the atv year")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please enter the atv Make")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Please enter the atv Model")]
        public string Model { get; set; }

        [Display(Name = "Miles/Hours")] 
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        public double? Miles { get; set; }

        [Required(ErrorMessage = "Please select the atv condition")]
        [Display(Name = "Condition")]
        public bool NewOrUsed { get; set; }

    }
    public class boat
    {

        [Display(Name = "HIN")]
        [Required(ErrorMessage = "Enter between 12-17 charactors for HIN")]
        [StringLength(17, MinimumLength = 12, ErrorMessage = "Enter between 12-17 charactors for HIN")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Please select the boat year")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please enter the boat Make")]
        public string Make { get; set; }

        [Display(Name = "Trailer")]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "Enter 17 charactors")]
        public string TrailerId { get; set; }

        //[StringLength(1, MinimumLength = 17, ErrorMessage = "Maximum 17 charactors")]
        [Display(Name = "Engine Serial")]
        public string EngineSerial { get; set; }

        [Required(ErrorMessage = "Please select the boat condition")]
        [Display(Name = "Condition")]
        public bool NewOrUsed { get; set; }

    }
    
    public class motorcycle
    {

        [Display(Name = "VIN")]
        [Required(ErrorMessage = "Please enter all 17 Characters of the motorcycle VIN")]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "Please enter all 17 Characters of the motorcycle VIN")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Please select the motorcycle year")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please enter the motorcycle Make")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Please enter the motorcycle Model")]
        public string Model { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        public double? Miles { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Please select the motorcycle condition")]
        [Display(Name = "Condition")]
        public bool NewOrUsed { get; set; }

    }
    public class snowmobile
    {
        [Display(Name = "VIN")]
        [Required(ErrorMessage = "Please enter all 17 Characters of the snowmobile VIN")]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "Please enter all 17 Characters of the snowmobile VIN")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Please select the snowmobile year")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please enter the snowmobile Make")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Please enter the snowmobile Model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Please select the snowmobile condition")]
        [Display(Name = "Condition")]
        public bool NewOrUsed { get; set; }

    }
    public class heavyequipment
    {
        [Display(Name = "SN")]
        [Required(ErrorMessage = "heavyequipment Serial Number is required.")]
        [StringLength(17, MinimumLength = 1, ErrorMessage = "Maximum 17 charactors")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "Please select the heavyequipment year")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid year, enter 4 numeric digits")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please enter the heavyequipment Make")]
        public string Make { get; set; }

    }
}