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

    }
}