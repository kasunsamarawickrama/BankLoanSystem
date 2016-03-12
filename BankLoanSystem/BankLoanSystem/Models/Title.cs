using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class Title
    {
        [EmailAddress]
        [Display(Name = "Email address for a reminder to be sent if the title has not been received in the timeframe")]
        [Required(ErrorMessage = "Email is required.")]
        public string RemindEmail { get; set; }

        [Display(Name = "Physically required")]
        [Required(ErrorMessage = "Receipt required method is required.")]
        public bool NeedPyhsical { get; set; }

        [Display(Name = "Need a scan copy")]
        [Required(ErrorMessage = "Receipt required method is required.")]
        public bool NeedScanCopy { get; set; }

        [Required(ErrorMessage = "Please Select")]
        public bool IsTitleTrack { get; set; }

        [Required(ErrorMessage = "Accept Method is required.")]
        public string TitleAcceptMethod { get; set; }

        [Required(ErrorMessage = "Received Time  Limit is required.")]
        public string ReceivedTimeLimit { get; set; }

        [Required(ErrorMessage = "Please Select")]
        public bool IsReceipRequired { get; set; }

        [Required(ErrorMessage = "Required Method is required.")]
        public string ReceiptRequiredMethod { get; set; }

        public int LoanId { get; set; }
        [Display(Name = "Need to select at least one method ")]
        public string ErrorMessage { get; set; }
    }
}