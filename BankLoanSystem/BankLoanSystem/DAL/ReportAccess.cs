using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using BankLoanSystem.Models;

namespace BankLoanSystem.DAL
{
    public class ReportAccess: Controller
    {
        public List<LoanIdNumber> GetLoanNumbersWithBranch(int companyId)
        {
            List<LoanIdNumber> loanNumbers = new List<LoanIdNumber>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_id", companyId });
            //paramertList.Add(new object[] { "@user_id", companyId });

            DataSet dataSet = dataHandler.GetDataSet("spGetLoanNumbersWithBranch", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    LoanIdNumber loanNumber = new LoanIdNumber();
                    loanNumber.LoanId = Convert.ToInt32(dataRow["loan_id"]);
                    loanNumber.LoanNumberB = dataRow["LoanDisplay"].ToString();
                    loanNumber.BranchId = Convert.ToInt32(dataRow["branch_id"]);
                    loanNumbers.Add(loanNumber);
                    //unitModels.
                }

                //foreach (DataRow dataRow in dataSet.Tables[1].Rows)
                //{
                //    UserRights userRights = new UserRights();
                //    userRights.LoanId = Convert.ToInt32(dataRow["loan_id"]);
                //    userRights.PermissionList = dataRow["right_id"].ToString();
                //    userLoanRights.Add(userRights);
                //}
                //Session["UserRightListReport"] = userLoanRights;
            }

            //using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            //{
            //    connection.Open();
            //    SqlCommand cmd = new SqlCommand("spGetLoanNumbersWithBranch", connection);
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    cmd.Parameters.AddWithValue("@company_id", companyId);
            //    cmd.Parameters.AddWithValue("@user_id", companyId);

            //    using (SqlDataReader dr = cmd.ExecuteReader())
            //    {
            //        while (dr.Read())
            //        {
            //            LoanIdNumber loanNumber = new LoanIdNumber();

            //            loanNumber.LoanId = Convert.ToInt32(dr["loan_id"]);
            //            loanNumber.LoanNumberB = dr["LoanDisplay"].ToString();
            //            loanNumber.BranchId = Convert.ToInt32(dr["branch_id"]);
            //            loanNumbers.Add(loanNumber);
            //        }

            //        if (dr.NextResult())
            //        {
            //            while (dr.Read())
            //            {
            //                UserRights userRights = new UserRights();
            //                userRights.LoanId = Convert.ToInt32(dr["loan_id"]);
            //                userRights.PermissionList = dr["right_id"].ToString();
            //                userLoanRights.Add(userRights);
            //            }
            //        }
            //        Session["UserRightListReport"] = userLoanRights;
            //    }

            //    //SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            //    //dataAdapter.TableMappings.Add("0", "");
            //    //DataSet dataSet = new DataSet();
            //    //dataAdapter.Fill(dataSet);

            //    //DataTable ta = dataSet.Tables[0];

            //    //dataSet.Tables[1].TableName = "UserPermission";
            //    //DataTable tb = dataSet.Tables[1];

            //    //dataAdapter.Dispose();
            //}

            return loanNumbers;
        }

        public List<UserRights> GeUserRightsForReporting(int userId)
        {
            List<UserRights> userLoanRights = new List<UserRights>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", userId });

            DataSet dataSet = dataHandler.GetDataSet("spGeUserRightsForReporting", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    UserRights userRights = new UserRights();
                    userRights.LoanId = Convert.ToInt32(dataRow["loan_id"]);
                    userRights.PermissionList = dataRow["right_id"].ToString();
                    userLoanRights.Add(userRights);
                }
            }

            return userLoanRights;
        }

        public List<LoanDetailsRpt> GetLoanDetailsRpt(int loanId)
        {
            List<LoanDetailsRpt> loanDetails = new List<LoanDetailsRpt>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });

            DataSet dataSet = dataHandler.GetDataSet("spGetLoanDetailsByLoanIdRpts", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    LoanDetailsRpt details = new LoanDetailsRpt();

                    details.LenderBrnchName = dataRow["branch_name"].ToString();
                    details.DealerBrnchName = dataRow["nr_branch_name"].ToString();
                    details.LoanNumber = dataRow["loan_number"].ToString();

                    loanDetails.Add(details);

                    break;
                }
            }

            return loanDetails;
        }

        public List<CurtailmentShedule> GetCurtailmentScheduleByDateRange(int loanId, DateTime dueDateStart, DateTime dueDateEnd)
        {
            List<CurtailmentShedule> lstCurtailmentShedule = new List<CurtailmentShedule>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@due_date_start", dueDateStart });
            paramertList.Add(new object[] { "@due_date_end", dueDateEnd });

            decimal totalDue = 0.00M;

            DataSet dataSet = dataHandler.GetDataSet("spGetCurtailmentSheduleByDateRange", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    CurtailmentShedule curtailment = new CurtailmentShedule();
                    curtailment.UnitId = dataRow["unit_id"].ToString();
                    curtailment.LoanId = Convert.ToInt32(dataRow["loan_id"]);
                    curtailment.Year = Convert.ToInt32(dataRow["year"]);
                    curtailment.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"].ToString()).ToString("MM/dd/yyyy");
                    curtailment.DueDate = Convert.ToDateTime(dataRow["curt_due_date"].ToString()).ToString("MM/dd/yyyy");
                    curtailment.Status = Convert.ToInt32(dataRow["curt_status"]);
                    curtailment.CurtAmount = Convert.ToDecimal(dataRow["curt_amount"]);
                    curtailment.IDNumber = dataRow["identification_number"].ToString();
                    curtailment.CurtNumber = Convert.ToInt32(dataRow["curt_number"]);
                    curtailment.Make = dataRow["make"].ToString();
                    curtailment.Model = dataRow["model"].ToString();
                    curtailment.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"].ToString()).ToString("MM/dd/yyyy");
                    curtailment.PurchasePrice = Convert.ToDecimal(dataRow["cost"]);
                    totalDue = totalDue + Convert.ToDecimal(dataRow["curt_amount"]);
                    lstCurtailmentShedule.Add(curtailment);
                }
                if (lstCurtailmentShedule.Count > 0)
                    lstCurtailmentShedule[0].TotalAmountDue = totalDue;

                return lstCurtailmentShedule;
            }
            else
            {
                return null;
            }
        }

        public List<CurtailmentShedule> GetCurtailmentPaidDetailsByDateRange(int loanId, DateTime paidDateStart, DateTime paidDateEnd)
        {
            List<CurtailmentShedule> lstCurtailmentShedule = new List<CurtailmentShedule>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@paid_date_start", paidDateStart });
            paramertList.Add(new object[] { "@paid_date_end", paidDateEnd });

            decimal totalpaid = 0.00M;

            DataSet dataSet = dataHandler.GetDataSet("spGetCurtailmentPaidDetailsByDateRange", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    CurtailmentShedule curtailment = new CurtailmentShedule();
                    curtailment.LoanId = Convert.ToInt32(dataRow["loan_id"]);
                    curtailment.UnitId = dataRow["UnitId"].ToString();
                    curtailment.IDNumber = dataRow["identification_number"].ToString();
                    curtailment.Year = Convert.ToInt32(dataRow["year"]);
                    curtailment.Make = dataRow["make"].ToString();
                    curtailment.Model = dataRow["model"].ToString();
                    curtailment.PurchasePrice = Convert.ToDecimal(dataRow["cost"]); 
                    curtailment.CurtNumber = Convert.ToInt32(dataRow["curt_number"]);

                    curtailment.PaidDate = Convert.ToDateTime(dataRow["paid_date"].ToString()).ToString("MM/dd/yyyy");
                    //curtailment.DueDate = Convert.ToDateTime(dataRow["curt_due_date"].ToString()).ToString("MM/dd/yyyy");
                    //curtailment.Status = Convert.ToInt32(dataRow["curt_status"]);
                    curtailment.CurtAmount = Convert.ToDecimal(dataRow["curt_amount"]);
                    curtailment.PaidCurtAmount = Convert.ToDecimal(dataRow["CurtPaidAmount"]);



                    curtailment.AdvanceDate = Convert.ToDateTime(dataRow["paid_date"].ToString()).ToString("MM/dd/yyyy");

                    totalpaid = totalpaid + Convert.ToDecimal(dataRow["CurtPaidAmount"]);
                    lstCurtailmentShedule.Add(curtailment);
                }
                //if (lstCurtailmentShedule.Count > 0)
                //    lstCurtailmentShedule[0].TotalAmountPaid = totalpaid;

                for(int i = 0; i < lstCurtailmentShedule.Count; i++ )
                    lstCurtailmentShedule[i].TotalAmountPaid = totalpaid;

                
            }
            return lstCurtailmentShedule;
        }

        public List<ReportUnitModels> GetAllActiveUnitDetailsRpt(int loanId)
        {
            List<ReportUnitModels> units = new List<ReportUnitModels>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });

            DataSet dataSet = dataHandler.GetDataSet("spGetUnitLotInspection", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    ReportUnitModels unit = new ReportUnitModels();
                    unit.LoanNumber = dataRow["loan_number"].ToString();
                    unit.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"]).ToString("MM/dd/yyyy");
                    unit.IdentificationNumber = dataRow["identification_number"].ToString();
                    unit.Year = Convert.ToInt32(dataRow["year"]);
                    unit.Make = dataRow["make"].ToString();
                    unit.Model = dataRow["model"].ToString();
                    unit.Color = dataRow["color"].ToString();

                    units.Add(unit);

                    //unitModels.
                }
            }

            return units;
        }


        /// <summary>
        /// CreatedBy:Irfan
        /// CreatedDate:2016/2/24
        /// Get just added units by loan id and user id
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns>list of units</returns>
        public List<RptAddUnit> GetJustAddedUnitDetails(int userId, int loanId)
        {

            List<RptAddUnit> justAddedUnitList = new List<RptAddUnit>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@user_id", userId });

            DataSet dataSet = dataHandler.GetDataSet("spGetJustAddedUnitsByLoanId", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    //Unit justAddedUnit = new Unit();

                    //justAddedUnit.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"].ToString());
                    //justAddedUnit.IdentificationNumber = dataRow["identification_number"].ToString();
                    //justAddedUnit.Year = Convert.ToInt32(dataRow["year"]);
                    //justAddedUnit.Make = dataRow["make"].ToString();
                    //justAddedUnit.Model = dataRow["model"].ToString();
                    //justAddedUnit.AdvanceAmount = (dataRow["advance_amount"]) != DBNull.Value ? (Decimal)dataRow["advance_amount"] : (Decimal)0.00M;
                    //justAddedUnit.IsAdvanced = Convert.ToBoolean(dataRow["is_advanced"]);
                    //justAddedUnit.TitleStatus = Convert.ToInt32(dataRow["title_status"]);

                    //justAddedUnit.CreatedDate = Convert.ToDateTime(dataRow["created_date"].ToString());

                    RptAddUnit justAddedUnit = new RptAddUnit();

                    justAddedUnit.AdvanceDate = !dataRow.IsNull("advance_date") ? Convert.ToDateTime(dataRow["advance_date"]).ToString("MM/dd/yyy") : "";

                    justAddedUnit.IdentificationNumber = dataRow["identification_number"].ToString();
                    justAddedUnit.Year = Convert.ToInt32(dataRow["year"]);
                    justAddedUnit.Make = dataRow["make"].ToString();
                    justAddedUnit.Model = dataRow["model"].ToString();
                    justAddedUnit.PurchasePrice = (dataRow["cost"]) != DBNull.Value ? (Decimal)dataRow["cost"] : (Decimal)0.00M;
                    justAddedUnit.AdvanceAmount = (dataRow["advance_amount"]) != DBNull.Value ? (Decimal)dataRow["advance_amount"] : (Decimal)0.00M;
                    justAddedUnit.UnitStaus = Convert.ToBoolean(dataRow["is_advanced"]) ? "Advanced" : "Not Advanced";
                    int status = Convert.ToInt32(dataRow["title_status"]);

                    if (status == 0)
                    {
                        justAddedUnit.TitleStatus = "Not Received";
                    }
                    else if (status == 1)
                    {
                        justAddedUnit.TitleStatus = "Received";
                    }
                    else if (status == 2)
                    {
                        justAddedUnit.TitleStatus = "Returned to Dealer";
                    }

                    //justAddedUnit.CreatedDate = Convert.ToDateTime(dataRow["created_date"].ToString());
                    justAddedUnitList.Add(justAddedUnit);
                    
                }
            }

            return justAddedUnitList;

        }


        public List<Unit> GeUnitDetailsByTitleStatus(int loanId, int titleStatus)
        {

            List<Unit> units = new List<Unit>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@title_status", titleStatus });

            DataSet dataSet = dataHandler.GetDataSet("spGetUnitsByLoanIdTitleStatus", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    Unit unit = new Unit();

                    unit.IdentificationNumber = dataRow["identification_number"].ToString();
                    unit.Year = Convert.ToInt32(dataRow["year"]);
                    unit.Make = dataRow["make"].ToString();
                    unit.Model = dataRow["model"].ToString();
                    unit.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"].ToString());
                    unit.AdvanceDateStr = unit.AdvanceDate.ToString("MM/dd/yyyy")
                    ;
                    unit.TitleStatus = Convert.ToInt32(dataRow["title_status"]);

                    if (unit.TitleStatus == 0)
                    {
                        unit.TitleStatusText = "Not Received";
                    }
                    else if (unit.TitleStatus == 1)
                    {
                        unit.TitleStatusText = "Received";
                    }
                    else if(unit.TitleStatus == 2)
                    {
                        unit.TitleStatusText = "Returned to Dealer";
                    }

                    unit.AdvanceAmount = (dataRow["advance_amount"]) != DBNull.Value ? (Decimal)dataRow["advance_amount"] : (Decimal)0.00;
                    unit.IsAdvanced = Convert.ToBoolean(dataRow["is_advanced"]);
                    unit.CreatedDate = Convert.ToDateTime(dataRow["created_date"].ToString());
                    
                    units.Add(unit);

                }
            }

            return units;

        }

        public List<ReportFullInventoryUnit> GetFullInventoryByLoanId(int loanId)
        {
            List<ReportFullInventoryUnit> units = new List<ReportFullInventoryUnit>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });

            DataSet dataSet = dataHandler.GetDataSet("spGetFullInventoryByLoanId", paramertList);

            decimal loanBalance = 0.00M;
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    ReportFullInventoryUnit unit = new ReportFullInventoryUnit();

                    unit.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"]).ToString("MM/dd/yyyy");
                    unit.IdentificationNumber = dataRow["identification_number"].ToString();
                    unit.Year = Convert.ToInt32(dataRow["year"]);
                    unit.Make = dataRow["make"].ToString();
                    unit.Model = dataRow["model"].ToString();
                    unit.PurchasePrice = Convert.ToDecimal(dataRow["cost"]);
                    unit.AdvanceAmount = Convert.ToDecimal(dataRow["advance_amount"]);
                    unit.TotalCurtPaid = Convert.ToDecimal(dataRow["TotalPaid"]);
                    unit.BalanceDue = Convert.ToDecimal(dataRow["Balance"]);

                    int status = Convert.ToInt32(dataRow["title_status"]);

                    if (status == 0)
                    {
                        unit.TitleStatus = "Not Received";
                    }
                    else if (status == 1)
                    {
                        unit.TitleStatus = "Received";
                    }
                    else if (status == 2)
                    {
                        unit.TitleStatus = "Returned to Dealer";
                    }
                    loanBalance += unit.BalanceDue;
                    units.Add(unit);

                }

                if (units.Count > 0)
                    units[0].LoanBalance = loanBalance;
            }

            return units;
        }


        public List<RptAddUnit> AdvanceUnitsDuringSession(string unitIdList)
        {
            List<RptAddUnit> AdvanceUnits = new List<RptAddUnit>();

            DataHandler dataHandler = new DataHandler();

            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@Input", unitIdList });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetAdvanceUnitsDuringSession", paramertList);

                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        //Unit justAddedUnit = new Unit();

                        //justAddedUnit.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"].ToString());
                        //justAddedUnit.IdentificationNumber = dataRow["identification_number"].ToString();
                        //justAddedUnit.Year = Convert.ToInt32(dataRow["year"]);
                        //justAddedUnit.Make = dataRow["make"].ToString();
                        //justAddedUnit.Model = dataRow["model"].ToString();
                        //justAddedUnit.AdvanceAmount = (dataRow["advance_amount"]) != DBNull.Value ? (Decimal)dataRow["advance_amount"] : (Decimal)0.00M;
                        //justAddedUnit.IsAdvanced = Convert.ToBoolean(dataRow["is_advanced"]);
                        //justAddedUnit.TitleStatus = Convert.ToInt32(dataRow["title_status"]);

                        //justAddedUnit.CreatedDate = Convert.ToDateTime(dataRow["created_date"].ToString());

                        RptAddUnit unit = new RptAddUnit();
                        unit.LoanId = Convert.ToInt32(dataRow["loan_id"]);
                        unit.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"]).ToString("MM/dd/yyy");
                        unit.IdentificationNumber = dataRow["identification_number"].ToString();
                        unit.Year = Convert.ToInt32(dataRow["year"]);
                        unit.Make = dataRow["make"].ToString();
                        unit.Model = dataRow["model"].ToString();
                        unit.AdvanceAmount = (dataRow["advance_amount"]) != DBNull.Value ? (Decimal)dataRow["advance_amount"] : (Decimal)0.00M;
                        int status = Convert.ToInt32(dataRow["title_status"]);

                        if (status == 0)
                        {
                            unit.TitleStatus = "Not Received";
                        }
                        else if (status == 1)
                        {
                            unit.TitleStatus = "Received";
                        }
                        else if (status == 2)
                        {
                            unit.TitleStatus = "Returned to Dealer";
                        }

                        //unit.CreatedDate = Convert.ToDateTime(dataRow["created_date"].ToString());
                        AdvanceUnits.Add(unit);

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return AdvanceUnits;
        }

        public List<RptFee> GetFeeInvoiceByDateRange(int loanId, string type, DateTime dueDateStart, DateTime dueDateEnd)
        {
            List<RptFee> feeInvoiceData = new List<RptFee>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@type", type });
            paramertList.Add(new object[] { "@due_date_start", dueDateStart });
            paramertList.Add(new object[] { "@due_date_end", dueDateEnd });

            decimal totalDue = 0.00M;

            DataSet dataSet = dataHandler.GetDataSet("spGetFeeInvoiceDetailsByDateRange", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    RptFee feeInvoice = new RptFee();
                    feeInvoice.IdentificationNumber = dataRow["identification_number"].ToString();
                    feeInvoice.Year = (dataRow["year"]) != DBNull.Value ? (Int32)dataRow["year"] : 0000;//Convert.ToInt32(dataRow["year"]);
                    feeInvoice.Make = dataRow["make"].ToString();
                    feeInvoice.Model = dataRow["model"].ToString();
                    feeInvoice.DueDate = Convert.ToDateTime(dataRow["due_date"].ToString()).ToString("MM/dd/yyyy");
                    feeInvoice.PurchasePrice = (dataRow["cost"]) != DBNull.Value ? (Decimal)dataRow["advance_amount"] : (Decimal)0.00M;//Convert.ToDecimal(dataRow["cost"]);
                    feeInvoice.AdvanceAmount = Convert.ToDecimal(dataRow["amount"]);
                    totalDue = totalDue + Convert.ToDecimal(dataRow["amount"]);
                    feeInvoiceData.Add(feeInvoice);
                }
                if (feeInvoiceData.Count > 0)
                    feeInvoiceData[0].TotalAdvanceAmount = totalDue;

                return feeInvoiceData; 
            }
            else
            {
                return null;
            }
        }

        public List<ReportFullInventoryUnit> GetAdvanceUnitByLoanId(int loanId, DateTime advanceDateStart, DateTime advanceDateEnd)
        {
            List<ReportFullInventoryUnit> units = new List<ReportFullInventoryUnit>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@advance_date_start", advanceDateStart });
            paramertList.Add(new object[] { "@advance_date_end", advanceDateEnd });

            DataSet dataSet = dataHandler.GetDataSet("spRptAdvanceUnitByDateRange", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    ReportFullInventoryUnit unit = new ReportFullInventoryUnit();

                    unit.IdentificationNumber = dataRow["identification_number"].ToString();
                    unit.Year = Convert.ToInt32(dataRow["year"]);
                    unit.Make = dataRow["make"].ToString();
                    unit.Model = dataRow["model"].ToString();
                    unit.PurchasePrice = Convert.ToDecimal(dataRow["cost"]);
                    unit.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"]).ToString("MM/dd/yyyy");
                    unit.AdvanceAmount = Convert.ToDecimal(dataRow["advance_amount"]);

                    int status = Convert.ToInt32(dataRow["title_status"]);

                    if (status == 0)
                    {
                        unit.TitleStatus = "Not Received";
                    }
                    else if (status == 1)
                    {
                        unit.TitleStatus = "Received";
                    }
                    else if (status == 2)
                    {
                        unit.TitleStatus = "Returned to Dealer";
                    }
                    units.Add(unit);

                }
            }

            return units;
        }

        /// <summary>
        /// Created By: IRFAN MAM
        /// 
        /// 
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="advanceDateStart"></param>
        /// <param name="advanceDateEnd"></param>
        /// <returns></returns>
        public List<ReportPayOff> GetPayOffDetailsByLoanId(int loanId, DateTime advanceDateStart, DateTime advanceDateEnd)
        {
            List<ReportPayOff> units = new List<ReportPayOff>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@date_start", advanceDateStart });
            paramertList.Add(new object[] { "@date_end", advanceDateEnd });

            DataSet dataSet = dataHandler.GetDataSet("spRptPayOffDetailsByDateRange", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    ReportPayOff unit = new ReportPayOff();

                    unit.IdentificationNumber = dataRow["identification_number"].ToString();
                    unit.Year = Convert.ToInt32(dataRow["year"]);
                    unit.Make = dataRow["make"].ToString();
                    unit.Model = dataRow["model"].ToString();
                    unit.PurchasePrice = Convert.ToDecimal(dataRow["cost"]);
                    
                    unit.AdvanceAmount = Convert.ToDecimal(dataRow["advance_amount"]);
                    unit.PayOffAmount = Convert.ToDecimal(dataRow["payoff_amount"]);
                    int status = Convert.ToInt32(dataRow["title_status"]);

                    if (status == 0)
                    {
                        unit.TitleStatus = "Not Received";
                    }
                    else if (status == 1)
                    {
                        unit.TitleStatus = "Received";
                    }
                    else if (status == 2)
                    {
                        unit.TitleStatus = "Returned to Dealer";
                    }
                    unit.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"]).ToString("MM/dd/yyyy");
                    unit.PayOffDate = Convert.ToDateTime(dataRow["pay_date"]).ToString("MM/dd/yyyy");
                    units.Add(unit);

                }
            }

            return units;
        }


        public List<RptFee> GetFeeReceiptByDateRange(int loanId, string type, DateTime dueDateStart, DateTime dueDateEnd)
        {
            List<RptFee> feeInvoiceData = new List<RptFee>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@type", type });
            paramertList.Add(new object[] { "@paid_date_start", dueDateStart });
            paramertList.Add(new object[] { "@paid_date_end", dueDateEnd });

            decimal totalDue = 0.00M;

            DataSet dataSet = dataHandler.GetDataSet("spGetFeeReceiptDetailsByDateRange", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    RptFee feeInvoice = new RptFee();
                    feeInvoice.IdentificationNumber = dataRow["identification_number"].ToString();
                    feeInvoice.Year = (dataRow["year"]) != DBNull.Value ? (Int32)dataRow["year"] : 0000;//Convert.ToInt32(dataRow["year"]);
                    feeInvoice.Make = dataRow["make"].ToString();
                    feeInvoice.Model = dataRow["model"].ToString();
                    feeInvoice.DueDate = Convert.ToDateTime(dataRow["paid_date"].ToString()).ToString("MM/dd/yyyy");
                    feeInvoice.PurchasePrice = (dataRow["cost"]) != DBNull.Value ? (Decimal)dataRow["advance_amount"] : (Decimal)0.00M;//Convert.ToDecimal(dataRow["cost"]);
                    feeInvoice.AdvanceAmount = Convert.ToDecimal(dataRow["amount"]);
                    totalDue = totalDue + Convert.ToDecimal(dataRow["amount"]);
                    feeInvoiceData.Add(feeInvoice);
                }
                if (feeInvoiceData.Count > 0)
                    feeInvoiceData[0].TotalAdvanceAmount = totalDue;

                return feeInvoiceData;
            }
            else
            {
                return null;
            }
        }


        public List<CurtailmentShedule> GetCurtailmentPaidDetailsDuringSession()
        {
            List<CurtailmentShedule> lstCurtailmentShedule = new List<CurtailmentShedule>();
            //DataHandler dataHandler = new DataHandler();
            //List<object[]> paramertList = new List<object[]>();
            //paramertList.Add(new object[] { "@loan_id", loanId });
            //paramertList.Add(new object[] { "@paid_date_start", paidDateStart });
            //paramertList.Add(new object[] { "@paid_date_end", paidDateEnd });

            decimal totalpaid = 0.00M;

            //DataSet dataSet = dataHandler.GetDataSet("spGetCurtailmentPaidDetailsByDateRange", paramertList);
            //if (dataSet != null && dataSet.Tables.Count != 0)
            //{
            //    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            //    {
            //        CurtailmentShedule curtailment = new CurtailmentShedule();
            //        curtailment.LoanId = Convert.ToInt32(dataRow["loan_id"]);
            //        curtailment.UnitId = dataRow["UnitId"].ToString();
            //        curtailment.IDNumber = dataRow["identification_number"].ToString();
            //        curtailment.Year = Convert.ToInt32(dataRow["year"]);
            //        curtailment.Make = dataRow["make"].ToString();
            //        curtailment.Model = dataRow["model"].ToString();
            //        curtailment.PurchasePrice = Convert.ToDecimal(dataRow["cost"]);
            //        curtailment.CurtNumber = Convert.ToInt32(dataRow["curt_number"]);

            //        curtailment.PaidDate = Convert.ToDateTime(dataRow["paid_date"].ToString()).ToString("MM/dd/yyyy");
            //        //curtailment.DueDate = Convert.ToDateTime(dataRow["curt_due_date"].ToString()).ToString("MM/dd/yyyy");
            //        //curtailment.Status = Convert.ToInt32(dataRow["curt_status"]);
            //        curtailment.CurtAmount = Convert.ToDecimal(dataRow["curt_amount"]);
            //        curtailment.PaidCurtAmount = Convert.ToDecimal(dataRow["CurtPaidAmount"]);



            //        curtailment.AdvanceDate = Convert.ToDateTime(dataRow["paid_date"].ToString()).ToString("MM/dd/yyyy");

            //        totalpaid = totalpaid + Convert.ToDecimal(dataRow["CurtPaidAmount"]);
            //        lstCurtailmentShedule.Add(curtailment);
            //    }
                //if (lstCurtailmentShedule.Count > 0)
                //    lstCurtailmentShedule[0].TotalAmountPaid = totalpaid;

                for (int i = 0; i < lstCurtailmentShedule.Count; i++)
                    lstCurtailmentShedule[i].TotalAmountPaid = totalpaid;


            //}
            return lstCurtailmentShedule;
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:5/9/2016
        /// Get all transaction details by date range
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="dueDateStart"></param>
        /// <param name="dueDateEnd"></param>
        /// <returns></returns>
        public List<ReportLoanSummary> GetLoanSummaryByDateRange(int loanId,DateTime dueDateStart, DateTime dueDateEnd)
        {
            List<ReportLoanSummary> loanData = new List<ReportLoanSummary>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            //paramertList.Add(new object[] { "@type", type });
            paramertList.Add(new object[] { "@due_date_start", dueDateStart });
            paramertList.Add(new object[] { "@due_date_end", dueDateEnd });

            //decimal totalDue = 0.00M;

            DataSet dataSet = dataHandler.GetDataSet("spGetLoanSummary", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    ReportLoanSummary loanSummary = new ReportLoanSummary();
                    loanSummary.IdentificationNumber = dataRow["identification_number"].ToString();
                    loanSummary.Year = (dataRow["year"]) != DBNull.Value ? (Int32)dataRow["year"] : 0000;//Convert.ToInt32(dataRow["year"]);
                    loanSummary.Make = dataRow["make"].ToString();
                    loanSummary.Model = dataRow["model"].ToString();
                    loanSummary.TransactionDate = Convert.ToDateTime(dataRow["transaction_date"].ToString()).ToString("MM/dd/yyyy");
                    loanSummary.TransactionAmount = (dataRow["transaction_amount"]) != DBNull.Value ? (Decimal)dataRow["transaction_amount"] : (Decimal)0.00M;//Convert.ToDecimal(dataRow["cost"]);
                    loanSummary.TransactionType = dataRow["type"].ToString();
                    //totalDue = totalDue + Convert.ToDecimal(dataRow["amount"]);
                    loanData.Add(loanSummary);
                }
                //if (loanData.Count > 0)
                //    feeInvoiceData[0].TotalAdvanceAmount = totalDue;

                return loanData;
            }
            else
            {
                return null;
            }
        }

    }
}