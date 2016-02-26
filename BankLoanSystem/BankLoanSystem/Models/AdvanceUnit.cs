using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class AdvanceUnit
    {
        public List<Unit> NotAdvanced { get; set; }
        public List<Unit> Search { get; set; }
    }
}