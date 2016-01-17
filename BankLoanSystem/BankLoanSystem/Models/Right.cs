using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BankLoanSystem.Models
{
    public class Right
    {
        public int rightId { get; set; }

        [Display(Name = "Add")]
        public bool active { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        public int userId { get; set; }

        public string rightsPermissionString { get; set; }

        public int editorId { get; set; }

    }

}