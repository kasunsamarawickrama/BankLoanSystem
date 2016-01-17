using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class Branch
    {
       
        public int BranchId { get; set; }
        [Display(Name = "Name")]
        public string BranchName { get; set; }
        [Display(Name = "Code")]
        public string BranchCode { get; set; }
        [Display(Name = "Address")]
        public string BranchAddress { get; set; }
        [Display(Name = "State")]
        public string BranchState { get; set; }
        [Display(Name = "City")]
        public string BranchCity { get; set; }
        [Display(Name = "Zip Code")]
        public string BranchZip { get; set; }
        [Display(Name = "Email")]
        public string BranchEmail { get; set; }
        [Display(Name = "Phone Number")]
        public string BranchPhoneNum { get; set; }
        [Display(Name = "Fax Number")]
        public string BranchFax { get; set; }
        
        public int BranchCreatedBy { get; set; }
        public DateTime BranchCreatedDate { get; set; }
        public int BranchCompany { get; set; }
        public string BranchCompanyCode { get; set; }
    }
}