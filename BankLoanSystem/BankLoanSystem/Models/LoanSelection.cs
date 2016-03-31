using System.Collections.Generic;

namespace BankLoanSystem.Models
{
    public class LoanSelection
    {
        public List<Branch> RegBranches;
        public List<Company> NonRegCompanies;
        public List<NonRegBranch> NonRegBranchList;
        public List<LoanSetupStep1> LoanList;
    }

}
