﻿using System;
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

      
        public string Year { get; set; }

       
        public string Make { get; set; }

       
        public string Model { get; set; }

        [Display(Name = "Unit Type")]
        public int UnitTypeId { get; set; }

        public vehicle vehicle { get; set; }

        public List<TitleUpload> filePaths { get; set; }
    }   
}