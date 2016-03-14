using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Models
{
    /// <summary>
    /// CreatedBy:Piyumi
    /// CreatedDate:2016/2/5
    /// properties related to interest page
    /// </summary>
    public class Interest
    {


        [Display(Name = "Interest Rate")]
        [Required(ErrorMessage = "Interest Rate is required.")]
        [Range(0.001, 100, ErrorMessage = "Interest must be between 0 and {2}.")]
        //[DisplayFormat(DataFormatString = "{0:#,##0.000#}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"^[0-9]+.?[0-9]{0,3}$", ErrorMessage = "Invalid; Maximum 3 Decimal Points.")]
        public double InterestRate { get; set; }

        [Display(Name = "Paid Date")]
        [Required(ErrorMessage = "Paid Date is required.")]
        public String PaidDate { get; set; }

        [Display(Name = "Payment Period")]
        [Required(ErrorMessage = "Payment Period is required.")]
        public String PaymentPeriod { get; set; }

        [Display(Name = "Payment Method")]
        [Required(ErrorMessage = "Payment Method is required.")]
        public String option { get; set; }

        [Required(ErrorMessage = "Auto reminder field is required")]
        public bool NeedReminder { get; set; }

        [EmailAddress]
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is required.")]
        public String AutoRemindEmail { get; set; }

        [Required(ErrorMessage = "Remind Period is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Remind Period must be greater than 0")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid Remind Period")]
        public int RemindPeriod { get; set; }

        [Display(Name = "Accrual Method")]
        [Required(ErrorMessage = "Accrual Method is required.")]
        public int AccrualMethodId { get; set; }

        public int LoanId { get; set; }



        //public SelectList AccrualMethodList { get; set; }
    }
}