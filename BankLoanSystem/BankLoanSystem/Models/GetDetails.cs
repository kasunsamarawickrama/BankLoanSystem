using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankLoanLocal.Models
{
    public class GetDetails
    {
        public int userId { get; set; }
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Phone Number")]
        public string phoneNo { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
        [Display(Name = "Created Date")]
        public string createdDate { get; set; }
        [Display(Name = "Modified Date")]
        public DateTime modifiedDate { get; set; }
        [Display(Name = "Is Delete")]
        public bool isDelete { get; set; }
        [Display(Name = "Role")]
        public string roleName { get; set; }
        [Display(Name = "Branch")]
        public string branchName { get; set; }
    }
}