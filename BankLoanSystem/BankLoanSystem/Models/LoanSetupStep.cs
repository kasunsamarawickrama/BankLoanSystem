namespace BankLoanSystem.Models
{
    public class LoanSetupStep
    {
        public int CompanyId { get; set; }

        public int BranchId { get; set; }

        public int nonRegisteredBranchId { get; set; }

        public int loanId { get; set; }

        public int stepId { get; set; }
    }
}