using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankLoanSystem.Models
{
    public class Company
    {
        private string _companyAddress1;
        public int CompanyId { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        //[Remote("IsCompanyNameExists", "SetupCompany", ErrorMessage = "Company Name already in use")] //Check company name is alredy in database
        [StringLength(30, ErrorMessage = "Company name can't be longer than 30 characters and less than 3 charactors.", MinimumLength = 3)]
        //[RegularExpression(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$", ErrorMessage = "Company name can't be start with !*@#$%^&*()_+-= and any number.")]
        public string CompanyName { get; set; }

        public string CompanyCode { get; set; }

        [Required]
        [Display(Name = "Street Address1")]
        public string CompanyAddress1
        {
            get { return _companyAddress1; }
            set { _companyAddress1 = value; }
        }

        [Display(Name = "Street Address2")]
        public string CompanyAddress2 { get; set; }

        [Required]
        [Display(Name = "State")]
        public int StateId { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Zip")]
        public string Zip { get; set; }

        [Required]
        [Display(Name = "Zip")]
        [RegularExpression("^[0-9]{5}", ErrorMessage = "Zip code must have 5 digits.")]
        public string ZipPre { get; set; }

        [Display(Name = "Extension")]
        [RegularExpression("^[0-9]{4}", ErrorMessage = "Extension needs 4 digits.")]
        public string Extension { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered format is not valid.")]
        [Display(Name = "Phone Number 1")]
        public string PhoneNum1 { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered format is not valid.")]
        [Display(Name = "Phone Number 2")]
        public string PhoneNum2 { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered format is not valid.")]
        [Display(Name = "Phone Number 3")]
        public string PhoneNum3 { get; set; }

        [Phone]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered format is not valid.")]
        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Website Url")]
        [Url]
        public string WebsiteUrl { get; set; }

        public Nullable<int> CreatedBy { get; set; }


        public Nullable<System.DateTime> CreatedDate { get; set; }

        [Required]
        [Display(Name = "Company Type")]
        public int TypeId { get; set; }

        [Required]
        [Display(Name = "Company Status Active?")]
        public bool CompanyStatus { get; set; }

        //[Required]
        //[Display(Name = "IsActive")]
        //public bool Status { get; set; }

        public int FirstSuperAdminId { get; set; }
        public int FirstSuperAdminName { get; set; }
        public int CreatedByCompany { get; set; }
    }


    public class CompanyViewModel
    {
        public Company Company { get; set; }

        [Required]
        [Display(Name = "State")]
        public int StateId { get; set; }
        public List<Company> Companies { get; set; }
    }

}