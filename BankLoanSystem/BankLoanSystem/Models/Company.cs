﻿using System;
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
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Company Code")]
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

        [Required]
        [Phone]
        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Required]
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