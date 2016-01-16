using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

    }
}