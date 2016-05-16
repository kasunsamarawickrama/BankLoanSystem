using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Models
{
    public class User
    {

        [Required(ErrorMessage = "Please indicate the username.")]
        [Display(Name = "Username")]
        public int UserId { get; set; }


        [Required(ErrorMessage = "Please indicate the username.")]
        [Display(Name = "Username")]
        [Remote("IsUserNameExists", "CreateUser", ErrorMessage = "Username already in use")]
        [StringLength(30, ErrorMessage = "Your Username needs to be between 3 and 30 characters in length.", MinimumLength = 3)]
        [RegularExpression(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$", ErrorMessage = "Please use only letters (a-z) and numbers.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please indicate a User Name")]
        [Display(Name = "Username")]
        public string UneditUserName { get; set; }

       

        [Required(ErrorMessage = "Please enter the user's first name.")]
        [Display(Name = "First Name")]
        [StringLength(30, ErrorMessage = "First Name cannot be more than 30 characters in length. Please use an abbreviation.", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter the user's last name.")]
        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "Last Name cannot be more than 30 characters in length. Please use an abbreviation.", MinimumLength = 3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter the user's email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email Address.")]
        [Display(Name = "Email")]
        [Remote("IsEmailExists", "CreateUser", ErrorMessage = "Email already in use")]
        public string NewEmail { get; set; }

        [Required(ErrorMessage = "Please enter the user's email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email Address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter the user's phone number.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid Phone Number including area code")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

       
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid Phone Number including area code")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber2 { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Please chose a Password at least 6 characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Please create the Password")]
        [StringLength(30, ErrorMessage = "Please chose a Password at least 6 characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please select")]
        public bool ChangePassword { get; set; }

        [Required(ErrorMessage = "Please confirm the Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password",ErrorMessage = "Your Passwords do not match, please check your inputs")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please select")]
        [Display(Name = "Status")]
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

        [Required(ErrorMessage = "Please select the branch")]
        [Display(Name = "Branch ")]
        public int BranchId { get; set; }

        [Required(ErrorMessage = "Please select the branch")]
        [Display(Name = "Branch ")]
        public int BranchIdUser { get; set; }

        [Required]
        [Display(Name = "Branch ")]
        public int LoanIdUser { get; set; }

        [Required(ErrorMessage = "Please select a role for the User")]
        [Display(Name = "User Role")]
        public int RoleId { get; set; }

        
        public int Company_Id { get; set; }

        [Required]
        public int LoanId { get; set; }

        public string LoanNumber { get; set; }

        public int CompanyType { get; set; }

        public string CompanyName { get; set; }

        public string CompanyCode { get; set; }

        public string BranchName { get; set; }

        public string BranchNameAddress { get; set; }

        public string RoleName { get; set; }

        public string UserRights { get; set; }

        public int step_status { get; set; }

        public List<Right> UserRightsList { get; set; }

        public string ActivationCode { get; set; }

        public List<Branch> BranchList { get; set; }
        public List<User> UserList { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserListViewModel
    {
        public User User { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string EditableUserName { get; set; }

        [Required]
        [EmailAddress]
        public string EditableEmail { get; set; }[Required]
        public int BranchId { get; set; }
        public int RoleId { get; set; }
        public List<User> Users { get; set; }

        
    }

    public class DealerUserModel : User
    {
        public int NonRegCompanyId { get; set; }
        public int NonRegBranchId { get; set; }
        public int LoanId { get; set; }
    }

   
}