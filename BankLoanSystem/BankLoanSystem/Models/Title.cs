using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class Title
    {
        [EmailAddress(ErrorMessage = "Email is not valid")]
        [Display(Name = "Email address for a reminder to be sent if the title has not been received in the timeframe")]
        [Required(ErrorMessage = "Please specify an email address for the reminder to be sent to.")]
        public string RemindEmail { get; set; }

        [Required(ErrorMessage = "Please specify when to email the reminder if the title is not received.")]
        [Range(1, int.MaxValue, ErrorMessage = "Remind Period must be greater than 0")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid Remind Period")]
        public int RemindPeriod { get; set; }

        [Display(Name = "Physically required")]
        [Required(ErrorMessage = "Receipt required method is required.")]
        public bool NeedPyhsical { get; set; }

        [Display(Name = "Need a scan copy")]
        [Required(ErrorMessage = "Please select if you will accept a scanned copy of the title.")]
        public bool NeedScanCopy { get; set; }

        [Required(ErrorMessage = "Please select if the title will be tracked in the program.")]
        public bool IsTitleTrack { get; set; }

        [Required(ErrorMessage = "Please select how the title will be accepted.")]
        public string TitleAcceptMethod { get; set; }

        [Required(ErrorMessage = "Please specify how many days should pass before title reminder is sent.")]
        public string ReceivedTimeLimit { get; set; }

        [Required(ErrorMessage = "Please select if the receipt/invoice will be tracked in the program.")]
        public bool IsReceipRequired { get; set; }

        [Required(ErrorMessage = "Please select how the receipt/invoice will be accepted.")]
        public string ReceiptRequiredMethod { get; set; }

        public int LoanId { get; set; }
        [Display(Name = "Need to select at least one method ")]
        public string ErrorMessage { get; set; }
    }
}