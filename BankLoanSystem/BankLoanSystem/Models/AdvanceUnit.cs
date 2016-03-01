using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class AdvanceUnit
    {
        public List<Unit> NotAdvanced { get; set; }
        public List<Unit> Search { get; set; }

        [Required]
        [Display(Name = "Advance Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AdvanceDate  { get; set; }

        [RegularExpression(@"^[-a-zA-Z0-9]*", ErrorMessage = "Invalid VIN/HIN/Serial No")]
        public string IdentificationNumber { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Year must be a positive value.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "year must contain only digits")]
        public string Year { get; set; }

        [RegularExpression(@"^[-a-zA-Z0-9]*", ErrorMessage = "Invalid Make")]
        public string Make { get; set; }

        [RegularExpression(@"^[-a-zA-Z0-9]*", ErrorMessage = "Invalid Model")]
        public string Model { get; set; }
    }
}