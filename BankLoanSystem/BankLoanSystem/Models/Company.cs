using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankLoanSystem.Models
{
    public class Company
    {
        public int CompanyId { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        [Remote("IsCompanyNameExists", "SetupCompany", ErrorMessage = "Company Name already in use")]
        [StringLength(30, ErrorMessage = "Company name can't be longer than 30 characters and less than 3 charactors.", MinimumLength = 3)]
        //[RegularExpression(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$", ErrorMessage = "Company name can't be start with !*@#$%^&*()_+-= and any number.")]
        public string CompanyName { get; set; }

        public string CompanyCode { get; set; }

        [Required]
        [Display(Name = "Company Address")]
        public string CompanyAddress { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Zip")]
        public string Zip { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNum { get; set; }

        [Phone]
        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "Website Url")]
        [Url]
        public string WebsiteUrl { get; set; }

        public Nullable<int> CreatedBy { get; set; }


        public Nullable<System.DateTime> CreatedDate { get; set; }

        [Required]
        [Display(Name = "Company Type")]
        public int TypeId { get; set; }

        public int FirstSuperAdminId { get; set; }
        public int FirstSuperAdminName { get; set; }
    }
}