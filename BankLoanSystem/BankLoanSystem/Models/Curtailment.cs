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
        public int RemainingPercentage { get; set; }
        public int RemainingTime { get; set; }
        public int AdvancePt { get; set; }

        [Display(Name = "Base of calculation")]
        [Required]
        public string CalculationBase { get; set; }

        [Display(Name = "Curtailment Calculated By")]
        public string TimeBase { get; set; }

        [Display(Name = "Activate Loan")]
        public string Activete { get; set; }
    }

    public class Curtailment
    {
        [Display(Name = "Curtailment Id")]
        public int CurtailmentId { get; set; }

        [Display(Name = "Time period")]
        //[Remote("CalculateTimePeriod", "SetupProcess")]
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Invalid")]
        public int TimePeriod { get; set; }

        [Display(Name = "Percentage")]
        [Required]
        [Range(typeof(decimal), "0.001", "100", ErrorMessage = "Invalid")]
        //[Remote("CheckPrecentage", "SetupProcess", AdditionalFields = "cal", ErrorMessage = "in")]
        public int Percentage { get; set; }

        public int LoanId { get; set; }
    }


}