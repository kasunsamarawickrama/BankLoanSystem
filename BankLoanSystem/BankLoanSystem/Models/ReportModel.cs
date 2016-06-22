using System;
using System.Collections.Generic;
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

    public class UserRights
    {
        public int UserId { get; set; }
        public int LoanId { get; set; }
        public string PermissionList { get; set; }
    }

    //public class LoanIdNumberUserRights
    //{
    //    public List<LoanIdNumber> LoanNumbers;
    //    public List<UserRights> UserLoanRights;
    //}

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
        public string AddDate { get; set; }
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

    public class ReportPayOff
    {
       
        public string IdentificationNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal PayOffAmount { get; set; }
        public string AdvanceDate { get; set; }
        public string PayOffDate { get; set; }
        public string TitleStatus { get; set; }
        
    }

    public class ReportTransactionHistory
    {
        public string IdentificationNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public decimal TransactionAdvanced { get; set; }
        public decimal TransactionDeducted { get; set; }
    }

    public class RptLoanTerms
    {
        public string CompanyBranch { get; set; }
        public string PartnerBranch { get; set; }
        public int LoanId { get; set; }
        public string LoanNumber { get; set; }
        public string StartDate { get; set; }
        public string MaturityDate { get; set; }
        public decimal LoanAmount { get; set; }
        public int AdvancePercentage { get; set; }
        public string TitleRequired { get; set; }
        public string DocumentAcceptance { get; set; }
    }

    public class RptFeeLoanTerm
    {
        public string FeeType { get; set; }
        public decimal FeeAmount { get; set; }
        public string DueDate { get; set; }
    }

    public class RptEmailReminder
    {
        public string ReminderName { get; set; }
        public int TimeFrame { get; set; }
        public string Email { get; set; }
    }

    #region Summary Report

    public class RptLoanSummary
    {
        public string BranchName { get; set; }
        public int NoOfPartnerBranches { get; set; }
        public int NoOfActiveLoans { get; set; }
        public int TotalActiveUnits { get; set; }
        public decimal TotalLoanBalance { get; set; }
        public decimal TotalLoanAmount { get; set; }
    }

    #endregion

    public class RptBranchSummary
    {
        public string PartnerBranch { get; set; }
        public int LoanId { get; set; }
        public string LoanNumber { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal LoanBalance { get; set; }
        public decimal TotalLoanAmounts { get; set; }
        public decimal TotalLoanBalances { get; set; }
        public decimal PendingBalance { get; set; }
        public int ActiveUnits { get; set; }
        public int InActiveUnits { get; set; }
       
    }
}