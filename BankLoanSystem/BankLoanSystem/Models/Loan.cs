using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public bool IsTitleTrack { get; set; }
        public bool AddUnit { get; set; }
        public bool ViewReports { get; set; }
    }
}