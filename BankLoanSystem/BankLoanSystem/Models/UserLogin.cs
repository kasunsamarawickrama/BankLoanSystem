using System.ComponentModel.DataAnnotations;

namespace BankLoanSystem.Models
{
    /// <summary>
    /// CreatedBy : Kasun Smarawickrama
    /// CreatedDate: 2016/01/14
    /// 
    /// </summary>
    public class UserLogin
    {
        public int userId { get; set; }

        public string lbl { get; set; }

        [Required]
        [Display(Name = "User Name")]
        //[StringLength(30, ErrorMessage = "Needs to be 3 to 30 characters.", MinimumLength = 3)]
        [RegularExpression(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$", ErrorMessage = "Invalid Characters Found")]
        public string userName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Display(Name = "Created By")]
        public int createdBy { get; set; }
        public int createdByRole { get; set; }

        public int roleId { get; set; }
        public bool isEdit { get; set; }

        [Display(Name = "Created Person")]
        public string createdName { get; set; }

        public int loginId { get; set; }
    }
}