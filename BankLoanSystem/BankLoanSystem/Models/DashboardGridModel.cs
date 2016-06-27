using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class DashboardGridModel
    {
        public int Id { get; set; }

        public int BranchId { get; set; }

        public string BranchName { get; set; }

        public int PartnerBranchId { get; set; }

        public string PartnerBranchName { get; set; }

        public int Loanid { get; set; }

        public string LoanNumber { get; set; }

        public decimal TotalAmount  { get; set; }

        public decimal UsedAmount { get; set; }

        public string status { get; set; }
    }
}