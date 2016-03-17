using System;
using System.Collections.Generic;

namespace BankLoanSystem.Models
{
    public class UnitCurtailment
    {
        public int LoanId { get; set; }
        public string UnitId { get; set; }
        public int CurtailmentNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public int CurtailmentStatus { get; set; }
        public string IdentificationNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }

    public class UnitPayOffModel
    {
        public int LoanId { get; set; }
        public string UnitId { get; set; }
        public DateTime DateEntered { get; set; }
        public decimal Balance { get; set; }
        public decimal PurchesePrice { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string IdentificationNumber { get; set; }
    }

    public class UnitPayOffViewModel
    {
        public List<UnitPayOffModel> UnitPayOffList { get; set; }
        public DateTime PayDate { get; set; }
    }
}