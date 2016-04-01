﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Models
{
    public class User
    {

        [Required]
        [Display(Name = "User Id")]
        public int UserId { get; set; }


        [Required]
        [Display(Name = "User Name")]
        [Remote("IsUserNameExists", "CreateUser", ErrorMessage = "User Name already in use")]
        [StringLength(30, ErrorMessage = "User name can't be longer than 30 characters and less than 3 charactors.", MinimumLength = 3)]
        [RegularExpression(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$", ErrorMessage = "Please use only letters (a-z) and numbers.")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UneditUserName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(30, ErrorMessage = "First name can't be longer than 30 characters and less than 3 charactors.", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "Last name can't be longer than 30 characters and less than 3 charactors.", MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [Remote("IsEmailExists", "CreateUser", ErrorMessage = "Email already in use")]
        public string NewEmail { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered Phone number is not valid.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

       
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered Phone number is not valid.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber2 { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "IsActive")]
        public bool Status { get; set; }

        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [Display(Name = "Delete")]
        public bool IsDelete { get; set; }

        [Required]
        [Display(Name = "Created By ")]
        public int CreatedBy { get; set; }

        [Required]
        [Display(Name = "Branch ")]
        public int BranchId { get; set; }

        [Required]
        [Display(Name = "Role")]
        public int RoleId { get; set; }

        public int Company_Id { get; set; }

        public int LoanId { get; set; }

        public int CompanyType { get; set; }

        public string CompanyName { get; set; }

        public string BranchName { get; set; }

        public string UserRights { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserListViewModel
    {
        public User User { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string EditableUserName { get; set; }

        [Required]
        [EmailAddress]
        public string EditableEmail { get; set; }[Required]
        public int BranchId { get; set; }
        public int RoleId { get; set; }
        public List<User> Users { get; set; }
    }
}