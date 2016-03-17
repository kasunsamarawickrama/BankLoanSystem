using System;
using System.Collections.Generic;
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
        public List<CurtailmentShedule> CurtailmentScheduleInfoModel { get; set; }
    }
}