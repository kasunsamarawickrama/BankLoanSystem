using System;

namespace BankLoanSystem.Models
{
    public class ReportModel
    {

    }

    public class ReportUnitModels
    {
        public string LoanNumber { get; set; }
        public int LoanId { get; set; }
        public string UnitId { get; set; }
        public string AdvanceDate { get; set; }
        public string IdentificationNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
    }

    public class LoanIdNumberViewModel
    {
        public int LoanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class LoanIdNumber
    {
        public int LoanId { get; set; }
        public string LoanNumberB { get; set; }
        public int BranchId { get; set; }
    }

    public class LoanDetailsRpt
    {
        public string LenderBrnchName { get; set; }
        public string DealerBrnchName { get; set; }
        public string LoanNumber { get; set; }
        public int LoanId { get; set; }
        public string StartRange { get; set; }
        public string EndRange { get; set; }
        public string ReportDate { get; set; }
    }
}