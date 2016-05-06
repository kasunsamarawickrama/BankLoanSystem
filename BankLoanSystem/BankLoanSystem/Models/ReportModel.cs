using System;
using System.ComponentModel.DataAnnotations;

namespace BankLoanSystem.Models
{
    public class ReportModel
    {

    }

    public class ReportUnitModels
    {
        public bool View { get; set; }
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

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
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

    public class ReportFullInventoryUnit
    {
        public string LoanNumber { get; set; }
        public int LoanId { get; set; }
        public string UnitId { get; set; }
        public string AdvanceDate { get; set; }
        public string IdentificationNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal TotalCurtPaid { get; set; }
        public decimal BalanceDue { get; set; }
        public string TitleStatus { get; set; }
        public decimal LoanBalance { get; set; }
    }

    public class RptAddUnit
    {
        public int LoanId { get; set; }
        public string AdvanceDate { get; set; }
        public string IdentificationNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal AdvanceAmount { get; set; }
        public string UnitStaus { get; set; }
        public string TitleStatus { get; set; }
    }

    public class RptFee
    {
        public int LoanId { get; set; }
        public string IdentificationNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string DueDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal TotalAdvanceAmount { get; set; }
    }
}