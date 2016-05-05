using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class PartnerCompany
    {
        private string _companyAddress1;
        public int CompanyId { get; set; }
        public int RegCompanyId { get; set; }


        public string CompanyType { get; set; }

        [Required(ErrorMessage = "Please enter the Company Name")]
        [Display(Name = "Company Name")]
        //[Remote("IsCompanyNameExists", "SetupCompany", ErrorMessage = "Company Name already in use")] //Check company name is alredy in database
        [StringLength(50, ErrorMessage = "Company Name should be between 3 and 50 characters", MinimumLength = 3)]
        //[RegularExpression(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$", ErrorMessage = "Company name can't be start with !*@#$%^&*()_+-= and any number.")]
        public string CompanyName { get; set; }

        public string CompanyCode { get; set; }

        [Required(ErrorMessage = "Please enter the Street Address")]
        [Display(Name = "Street Address1")]
        public string CompanyAddress1
        {
            get { return _companyAddress1; }
            set { _companyAddress1 = value; }
        }

        [Display(Name = "Street Address2")]
        public string CompanyAddress2 { get; set; }

        [Required(ErrorMessage = "Please select the State")]
        [Display(Name = "State")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "Please enter the City")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Zip Code")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter the Zip Code")]
        [Display(Name = "Zip")]
        [RegularExpression("^[0-9]{5}", ErrorMessage = "Please enter a valid 5 digit Zip Code")]
        public string ZipPre { get; set; }

        [Display(Name = "Extension")]
        [RegularExpression("^[0-9]{4}", ErrorMessage = "Please enter a valid 4 digit Extension")]
        public string Extension { get; set; }

        [Required(ErrorMessage = "Please enter a Phone Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid Phone Number including area code")]
        [Display(Name = "Phone Number 1")]
        public string PhoneNum1 { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid Phone Number including area code")]
        [Display(Name = "Phone Number 2")]
        public string PhoneNum2 { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid Phone Number including area code")]
        [Display(Name = "Phone Number 3")]
        public string PhoneNum3 { get; set; }

        [Phone]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid Fax Number including area code")]
        [Display(Name = "Fax Number")]
        public string Fax { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Website")]
        //[Url(ErrorMessage = "Please enter a valid URL inlcuding the suffix")]
        public string WebsiteUrl { get; set; }

        public Nullable<int> CreatedBy { get; set; }


        public Nullable<System.DateTime> CreatedDate { get; set; }

        [Required(ErrorMessage = "Please select the Company Type")]
        [Display(Name = "Company Type")]
        public int TypeId { get; set; }

        [Required]
        [Display(Name = "Company Status Active?")]
        public bool CompanyStatus { get; set; }

        public List<PartnerCompany> PartnerCompanyList { get; set; }
    }
}