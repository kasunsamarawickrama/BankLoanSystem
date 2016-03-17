using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankLoanSystem.Models
{

    public class CurtailmentModel
    {
        public List<Curtailment> InfoModel { get; set; }
        public int? RemainingPercentage { get; set; }
        public int? RemainingTime { get; set; }
        public int? AdvancePt { get; set; }

        [Display(Name = "Base of calculation")]
        [Required]
        public string CalculationBase { get; set; }

        [Display(Name = "Curtailment Calculated By")]
        public string TimeBase { get; set; }

        [Display(Name = "Do you want Activate Loan?")]
        public string LoanStatus { get; set; }

        [Required(ErrorMessage = "Due date is required.")]
        [Display(Name = "Due Date")]
        public string DueDate { get; set; }

        [EmailAddress]
        [Display(Name = "email")]
        [Required(ErrorMessage = "Email Address is required.")]
        public string AutoRemindEmail { get; set; }

        [Display(Name = "Email Remind Period")]
        [Required(ErrorMessage = "Email Remind Period numeric value is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
        [RegularExpression(@"^[+-]?[0-9]{1,10000}$", ErrorMessage = "No decimal points allowed.")]
        public int EmailRemindPeriod { get; set; }
    }

    public class Curtailment
    {
        [Display(Name = "Curtailment Id")]
        public int CurtailmentId { get; set; }

        [Display(Name = "Time period")]
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Invalid")]
        public int? TimePeriod { get; set; }

        [Display(Name = "Percentage")]
        [Range(typeof(int), "1", "100", ErrorMessage = "Invalid")]
        public int? Percentage { get; set; }

        public decimal PaymentPercentage { get; set; }

        public int LoanId { get; set; }
        //public string LoanStatus { get; set; }
        public DateTime CurtailmentDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public int UnitId { get; set; }
    }


}