using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class Log
    {

        

        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int LoanId { get; set; }
        public string Description { get; set; }
        public string Page { get; set; }

        public Log(int UserId, int CompanyId, int BranchId , int LoanId , string Page, string Description, DateTime DateTime)
        {
            this.UserId = UserId;
            this.CompanyId = CompanyId;
            this.BranchId = BranchId;
            this.LoanId = LoanId;
            this.Page = Page;
            this.Description = Description;
            this.DateTime = DateTime;
        }
    }
}