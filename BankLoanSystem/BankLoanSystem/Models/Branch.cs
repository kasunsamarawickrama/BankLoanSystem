using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class Branch
    {
        /// <summary>
        /// CreatedBy: Piyumi
        /// CreatedDate:2016/1/17
        /// 
        /// </summary>
        public int BranchId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Branch Name Required!")]
        public string BranchName { get; set; }

        [Display(Name = "Code")]
        public string BranchCode { get; set; }

        [Display(Name = "Street Address 1")]
        [Required(ErrorMessage = "Branch Address Required!")]
        public string BranchAddress1 { get; set; }

        [Display(Name = "Street Address 2")]
        public string BranchAddress2 { get; set; }


        [Display(Name = "State")]
        [Required(ErrorMessage = "Branch State Required!")]
        public string BranchState { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Branch City Required!")]
        public string BranchCity { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Branch Zip Code Required!")]
        public string BranchZip { get; set; }


        [EmailAddress]
        [Display(Name = "Email")]
        public string BranchEmail { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number 1")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string BranchPhoneNum1 { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number 2")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string BranchPhoneNum2 { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number 3")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string BranchPhoneNum3 { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Fax Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered Fax format is not valid.")]
        public string BranchFax { get; set; }

        public int BranchCreatedBy { get; set; }
        public DateTime BranchCreatedDate { get; set; }
        public int BranchCompany { get; set; }
        public string BranchCompanyCode { get; set; }
    }
}