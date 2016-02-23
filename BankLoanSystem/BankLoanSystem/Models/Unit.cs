using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class Unit
    {
    public int UnitId { get; set; }

        [Required(ErrorMessage = "Vehicle Identification Number is required.")]
        public String IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        public int Year { get; set; }

        public String Make { get; set; }

        public String Model { get; set; }

        public String Trim { get; set; }

        public Double Miles { get; set; }

        public String Color { get; set; }

        public bool NewOrUsed { get; set; }

        public Double Length { get; set; }

        public String HitchStyle { get; set; }

        public Double Speed { get; set; }

        public String TrailerId { get; set; }

        public String EngineSerial { get; set; }

        public Double Cost { get; set; }

        public Double AdvanceAmount { get; set; }

        public bool IsTitleReceived { get; set; }

        public String Note { get; set; }

        public DateTime AdvanceDate { get; set; }

        public bool AddAndAdvance { get; set; }

        public bool IsAdvanced { get; set; }

        public int UnitTypeId { get; set; }


    }
}