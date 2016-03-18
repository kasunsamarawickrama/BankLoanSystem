using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankLoanSystem.Models
{
    public class CurtailmentShedule
    {

        public int LoanId { get; set; }
        public string UnitId { get; set; }
        public int Year { get; set; }

        public DateTime AdvanceDate { get; set; }
        public DateTime DueDate { get; set; }

        public int Status { get; set; }

        public decimal CurtAmount { get; set; }

        public string IDNumber { get; set; }
        public int CurtNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }


    }
    public class CurtailmentScheduleModel
    {
        public DateTime PayDate { get; set; }
        public DateTime DueDate { get; set; }
        public List<CurtailmentShedule> CurtailmentScheduleInfoModel { get; set; }




        [RegularExpression(@"^[-a-zA-Z0-9]*", ErrorMessage = "Invalid VIN/HIN/Serial No")]
        public string IdentificationNumber { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Year must be a positive value.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "year must contain only digits")]
        public string Year { get; set; }

        [RegularExpression(@"^[-a-zA-Z0-9]*", ErrorMessage = "Invalid Make")]
        public string Make { get; set; }

        [RegularExpression(@"^[-a-zA-Z0-9]*", ErrorMessage = "Invalid Model")]
        public string Model { get; set; }
    }
}