﻿using System;
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

        [Required(ErrorMessage = "Please confirm the last 6 characters of the VIN/HIN/Serial No.")]
        public string IDNumber { get; set; }
    }

    public class LoanIdNumber
    {
        public int LoanId { get; set; }
        public string LoanNumberB { get; set; }
        public int BranchId { get; set; }
    }

    public class Account
    {
        public int LoanId { get; set; }
        public string LoanNumber { get; set; }
        public string LoanCode { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string PatBranchName { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal UsedAmount { get; set; }
        public int ActiveUnits { get; set; }
        public string PatBranchAddress1 { get; set; }
        public string PatBranchAddress2 { get; set; }
        public string PatCity { get; set; }
        public bool hasAdvanceFee { get; set; }
        public bool hasMonthFee { get; set; }
        public bool hasLotInsFee { get; set; }
        public bool hasTitleTrack { get; set; }
        public string userReportRights { get; set; }
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
        public string CreaterName { get; set; }
        public string CompanyName { get; set; }
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
        public string LoanStatus { get; set; }
        public string PayOffType { get; set; }
        public string AllowEdit { get; set; }
        public string ActiveDate { get; set; }
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

    public class RptCompanySummary
    {
        public string BranchName { get; set; }
        public int NoOfPartnerBranches { get; set; }
        public int NoOfActiveLoans { get; set; }
        public int TotalActiveUnits { get; set; }
        public decimal TotalLoanBalance { get; set; }
        public decimal TotalLoanAmount { get; set; }
    }

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

    public class RptLoanSummary
    {
        public decimal LoanAmount { get; set; }
        public int TotalUnitsAdded { get; set; }
        public int TotalUnitsAdvanced { get; set; }
        public decimal TotalAmountAdvanced { get; set; }
        public decimal TotalAdvanceFees { get; set; }
        public decimal TotalCurtailmentsRecieved { get; set; }
        public int TotalUnitsPaidOff { get; set; }
        public decimal TotalAmountPaidOff { get; set; } 
        public int TotalUnitsDeleted { get; set; }
    }

    public class RptFullCurtailmentSummary
    {
        public string IdentificationNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal AdvanceAmount { get; set; }
        public int LoanId { get; set; }
        public string UnitId { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public decimal TotalCurtPaidAmount { get; set; }
        public decimal TotalPartPaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public int TotalCurtPaid { get; set; }
        public int TotalPartPaid { get; set; }
        public int TotalPaid { get; set; }


    }
    public class RptIndividualCurtailmentSummary
    {
        public string IdentificationNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal AdvanceAmount { get; set; }
        public int LoanId { get; set; }
        public string UnitId { get; set; }
        public string PaidDate { get; set; }
        public string DueDate { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public int CurtNo { get; set; }
        public decimal TotalPaidAmount { get; set; }

    }
    #endregion

    public class RptDeletedUnit
    {
        public int LoanId { get; set; }
        public string UnitId { get; set; }
        public string IdentificationNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string AdvanceDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal TotalCurtPaid { get; set; }
        public decimal BalanceDue { get; set; }
        public string TitleStatus { get; set; }
        public string DeletedDate { get; set; }
        public string ReasonForDeletion { get; set; }
    }
}