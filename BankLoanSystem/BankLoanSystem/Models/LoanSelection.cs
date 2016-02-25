using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class LoanSelection
    {
        public List<Company> NonRegCompanies;
        public List<NonRegBranch> NonRegBranchList;
        public List<LoanSetupStep1> LoanList;
    }
}