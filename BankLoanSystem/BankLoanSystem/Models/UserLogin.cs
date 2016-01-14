using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BankLoanSystem.Models
{
    public class UserLogin
    {
        public int userId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string userName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

    }
}