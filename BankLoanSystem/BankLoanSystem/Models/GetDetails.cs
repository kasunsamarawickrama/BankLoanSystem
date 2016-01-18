using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankLoanLocal.Models
{
    public class GetDetails
    {
        [Display(Name = "First Name")]
        public string first_name { get; set; }
        [Display(Name = "Last Name")]
        public string last_name { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Phone Number")]
        public string phone_no { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
        [Display(Name = "Created Date")]
        public string created_date { get; set; }
        [Display(Name = "Modified Date")]
        public DateTime modified_date { get; set; }
        [Display(Name = "Is Delete")]
        public bool is_delete { get; set; }
        [Display(Name = "Role")]
        public string role_name { get; set; }
        [Display(Name = "Branch")]
        public string branch_name { get; set; }
    }
}