using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BankLoanSystem.Models
{
    /// <summary>
    ///  CreatedBy : Kasun Smarawickrama
    /// CreatedDate: 2016/01/16
    /// 
    /// </summary>
    public class Right
    {
        public string rightId { get; set; }

        [Required(ErrorMessage = "Select Rights")]
        [Display(Name = "Add")]
        public bool active { get; set; }

        public bool DealerView { get; set; }

        public bool UserView { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        public string name { get; set; }

        public int userId { get; set; }

        public string rightsPermissionString { get; set; }

        public string ImagePath { get; set; }

        public int editorId { get; set; }

        public string reportRightsPermissionString { get; set; }
    }

    public class UserLoanRightsModel
    {
        public int UserId { get; set; }
        public int LoanId { get; set; }
        public List<Right> Rights { get; set; }
    }

}