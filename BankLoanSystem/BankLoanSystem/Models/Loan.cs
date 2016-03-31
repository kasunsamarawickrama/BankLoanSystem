using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public string LoanNumber { get; set; }
        public string PartnerName { get; set; }
        public int PartnerType { get; set; }
        public string BranchName { get; set; }
        public int IsTitleTrack { get; set; }
        public bool AddUnit { get; set; }
        public bool ViewReports { get; set; }
    }
}