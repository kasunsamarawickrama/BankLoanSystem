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
        [Display(Name = "Email Address")]
        public string RemindEmail { get; set; }

        [Display(Name = "Physically required")]
        public bool NeedPyhsical { get; set; }

        [Display(Name = "Need a scan copy")]
        public bool NeedScanCopy { get; set; }


        public bool IsTitleTrack { get; set; }

        public string TitleAcceptMethod { get; set; }

        public string ReceivedTimeLimit { get; set; }

        public bool IsReceipRequired { get; set; }

        public string ReceiptRequiredMethod { get; set; }

        public int LoanId { get; set; }
    }
}