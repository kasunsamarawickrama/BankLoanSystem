using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class Branch
    {
       
        public int BranchId { get; set; }

        [Display(Name = "Branch Name")]
        [Required(ErrorMessage = "Please enter the Branch Name")]
        public string BranchName { get; set; }

        [Display(Name = "Code")]
        public string BranchCode { get; set; }

        [Display(Name = "Street Address 1")]
        [Required(ErrorMessage = "Please enter the Street Address")]
        public string BranchAddress1 { get; set; }

        [Display(Name = "Street Address 2")]
        public string BranchAddress2 { get; set; }


        [Required(ErrorMessage = "Please select the State")]
        [Display(Name = "State")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "Please select the State")]
        [Display(Name = "State")]
        public int StateId2 { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Please enter the City")]
        public string BranchCity { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Please enter the Zip Code")]
        public string BranchZip { get; set; }

        [Required(ErrorMessage = "Please enter the Zip Code")]
        [Display(Name = "Zip")]
        [RegularExpression("^[0-9]{5}", ErrorMessage = "Please enter a valid 5 digit Zip Code")]
        public string ZipPre { get; set; }

        [Display(Name = "Extention")]
        [RegularExpression("^[0-9]{4}", ErrorMessage = "Please enter a valid 4 digit Extension")]
        public string Extention { get; set; }


        [EmailAddress(ErrorMessage = "Please enter a valid Email Address")]
        [Display(Name = "Email")]
        public string BranchEmail { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number 1")]
        [Required(ErrorMessage = "Please enter a Phone Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid Phone Number including area code")]
        public string BranchPhoneNum1 { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number 2")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid Phone Number including area code")]
        public string BranchPhoneNum2 { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number 3")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid Phone Number including area code")]
        public string BranchPhoneNum3 { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Fax")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid Fax Number including area code")]
        public string BranchFax { get; set; }

        public int BranchCreatedBy { get; set; }
        public DateTime BranchCreatedDate { get; set; }
        public int BranchCompany { get; set; }
        public string BranchCompanyCode { get; set; }

        public int NonRegBranchId { get; set; }

        public int LoanId { get; set; }

        public string LoanNumber { get; set; }

        public string BranchNameAddress { get; set; }

        public bool IsTitleTrack { get; set; }

        public bool HasFee { get; set; }
    }

    public class NonRegBranch : Branch
    {
        public int NonRegBranchId { get; set; }
        public string RegBranchName { get; set; }
        public string CompanyNameBranchName { get; set; }
        public int NonRegCompanyId { get; set; }
        public string NonRegCompanyName { get; set; }
    }

    public class EditPartnerBranceModel
    {
        public CompanyBranchModel CompanyBranch { get; set; }

        [Required(ErrorMessage = "Please select the State")]
        [Display(Name = "State")]
        public int StateId { get; set; }

        public List<NonRegBranch> NonRegBranches { get; set; }

        [Required(ErrorMessage = "Please select the Branch")]
        [Display(Name = "Branch Name")]
        public int RegBranchId { get; set; }

        [Required(ErrorMessage = "Please select the Company")]
        [Display(Name = "Company Name")]
        public int NonRegCompanyId { get; set; }

        public Company NonRegCompany { get; set; }

        public string RegBranchName { get; set; }

        public string NonRegCompanyName { get; set; }
    }
}