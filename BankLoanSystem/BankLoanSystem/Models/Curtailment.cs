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
        public string CalculationBase { get; set; }

        [Display(Name = "Time Period")]
        [Required]
        public string TimeBase { get; set; }
    }

    public class Curtailment
    {
        
        [Display(Name = "Curtailment Id")]
        public int CurtailmentId { get; set; }

        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Invalid")]
        public int TimePeriod { get; set; }

        [Display(Name = "Percentage")]
        [Required]
        [Range(typeof(float), "0.001", "100", ErrorMessage = "Invalid")]
        public float Percentage { get; set; }       

        public int LoanId { get; set; }
    }


}