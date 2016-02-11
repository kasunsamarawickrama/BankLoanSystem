using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Models
{

    public class CurtailmentModel
    {
        public List<Curtailment> InfoModel { get; set; }
        public float RemainingPercentage { get; set; }
        public int RemainingTime { get; set; }

        [Display(Name = "Base of calculation")]
        [Required]
        //[Remote("KeepCalculationBaseInfo", "LoanSetUpStep5", AdditionalFields = "TimeBase")]
        public string CalculationBase { get; set; }

        [Display(Name = "Time Period")]
        [Required]
        //[Remote("KeepTimeBaseType", "LoanSetUpStep5")]
        public string TimeBase { get; set; }
    }

    public class Curtailment
    {
        
        [Display(Name = "Curtailment Id")]
        public int CurtailmentId { get; set; }

        [Required]
        //[Remote("CalculateTimePeriod", "LoanSetUpStep5")]
        [RegularExpression("^[1-9][0-9]*$")]
        public int TimePeriod { get; set; }

        [Display(Name = "Percentage")]
        [Required]
        [Range(typeof(float), "0.001", "100")]
        //[Remote("CheckPrecentage", "LoanSetUpStep5", AdditionalFields = "cal", ErrorMessage = "Percentage can not be greater than actual one.")]
        public float Percentage { get; set; }       

        public int LoanId { get; set; }
    }


}