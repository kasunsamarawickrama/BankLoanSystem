using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BankLoanSystem.Models;

namespace BankLoanSystem.DAL
{
    public class LoanSetupAccess
    {
        /// <summary>
        /// CreatedBy:Irfan MAM
        /// CreatedDate:2016/2/9
        /// check the loan number is unique for a branch
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsUniqueLoanNumberForBranch(string loanNumber, int RegisteredBranchId, User user,int loanId)
        {
            try
            {
                DataHandler dataHandler = new DataHandler();
                List<object[]> paramertList = new List<object[]>();
                paramertList.Add(new object[] { "@loan_number", loanNumber });
                paramertList.Add(new object[] { "@branch_id", RegisteredBranchId });
                paramertList.Add(new object[] { "@loan_id", loanId });
                DataSet dataSet = dataHandler.GetDataSet("spIsUniqueLoanNumberForBranch", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return true;
            }

        }

        /// <summary>
        /// CreatedBy:Irfan MAM
        /// CreatedDate:2016/2/9
        /// getting all unit types
        /// </summary>
        /// <returns>IList<UnitType></returns>
        /// UpdatedBy:Asanka Senarathna
        /// 
        internal IList<UnitType> getAllUnitTypes()
        {
            List<UnitType> unitTypes = new List<UnitType>();
            DataHandler dataHandler = new DataHandler();

            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetAllUnitTypes", null);
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        UnitType unitType = new UnitType();
                        unitType.unitTypeName = dataRow["unit_type_name"].ToString();

                        unitType.unitTypeId = int.Parse(dataRow["unit_type_id"].ToString());
                        unitType.isSelected = false;
                        unitTypes.Add(unitType);
                    }

                    return unitTypes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/8
        /// get loanId by userId from step table
        /// </summary>
        /// <returns>loanId</returns>
      
        public int getLoanIdByUserId(int uId)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();

            paramertList.Add(new object[] { "@user_id", uId });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetLoanIdByUserId", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    return int.Parse(dataSet.Tables[0].Rows[0]["return"].ToString());

                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/12
        /// get loanId by branchId from step table
        /// EditedBy:Piyumi
        /// EditedDate:2016/3/8
        /// change input parameters
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="branchId"></param>
        /// <param name="nonRegBranchId"></param>
        /// <returns>loanId</returns>
        public int getLoanIdByBranchId(int branchId)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();

            paramertList.Add(new object[] { "@branch_id", branchId });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetLoanIdByBranchId", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    return int.Parse(dataSet.Tables[0].Rows[0]["return"].ToString());

                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Created By: kasun Smarawickrama
        /// Date : 10/2/2012
        /// 
        /// check the loan inserted values to advance,lot,monthly loan fee tables.
        /// </summary>
        /// <param name="loanId">loan id</param>
        /// <returns>fees Details</returns>
        /// 
        public Fees checkLoanIsInFeesTables(int loanId)
        {
            Fees fees = new Fees();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });

            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spCheckLoanIsInAdvanceFeeTable", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    DataRow dataRow = dataSet.Tables[0].Rows[0];

                    fees.AdvanceAmount = decimal.Parse(dataRow["advance_fee_amount"].ToString());
                    fees.AdvanceFeeCalculateType = dataRow["advance_fee_calculate_type"].ToString();
                    fees.AdvanceNeedReceipt = bool.Parse(dataRow["receipt"].ToString());
                    fees.AdvanceDue = dataRow["payment_due_method"].ToString();
                    fees.AdvanceDueDate = dataRow["payment_due_date"].ToString();
                    fees.AdvanceFeeDealerEmail = dataRow["auto_remind_dealer_email"].ToString();
                    if (dataRow["delaer_remind_period"] != System.DBNull.Value)
                    {
                        fees.AdvanceFeeDealerEmailRemindPeriod = int.Parse(dataRow["delaer_remind_period"].ToString());
                    }
                    fees.AdvanceDueEmail = dataRow["due_auto_remind_email"].ToString();
                    if (dataRow["due_auto_remind_period"] != System.DBNull.Value)
                    {
                        fees.AdvanceDueEmailRemindPeriod = int.Parse(dataRow["due_auto_remind_period"].ToString());
                    }
                }


                DataSet dataSet1 = dataHandler.GetDataSet("spCheckLoanIsInMonthlyLoanFeeTable", paramertList);
                if (dataSet1 != null && dataSet1.Tables.Count != 0 && dataSet1.Tables[0].Rows.Count != 0)
                {
                    DataRow dataRow1 = dataSet1.Tables[0].Rows[0];

                    fees.MonthlyLoanAmount = decimal.Parse(dataRow1["monthly_loan_fee_amount"].ToString());
                    fees.MonthlyLoanNeedReceipt = bool.Parse(dataRow1["receipt"].ToString());
                    fees.MonthlyLoanDue = dataRow1["payment_due_method"].ToString();
                    fees.MonthlyLoanDueDate = dataRow1["payment_due_date"].ToString();
                    fees.MonthlyLoanFeeDealerEmail = dataRow1["auto_remind_dealer_email"].ToString();
                    if (dataRow1["delaer_remind_period"] != System.DBNull.Value)
                    {
                        fees.MonthlyLoanFeeDealerEmailRemindPeriod = int.Parse(dataRow1["delaer_remind_period"].ToString());
                    }
                    fees.MonthlyLoanDueEmail = dataRow1["due_auto_remind_email"].ToString();
                    if (dataRow1["due_auto_remind_period"] != System.DBNull.Value)
                    {
                        fees.MonthlyLoanDueEmailRemindPeriod = int.Parse(dataRow1["due_auto_remind_period"].ToString());
                    }
                }

                DataSet dataSet2 = dataHandler.GetDataSet("spCheckLoanIsInLotInspectionFeeTable", paramertList);
                if (dataSet2 != null && dataSet2.Tables.Count != 0 && dataSet2.Tables[0].Rows.Count != 0)
                {
                    DataRow dataRow2 = dataSet2.Tables[0].Rows[0];
                    fees.LotInspectionAmount = decimal.Parse(dataRow2["lot_inspection_amount"].ToString());
                    fees.LotInspectionNeedReceipt = bool.Parse(dataRow2["receipt"].ToString());
                    fees.LotInspectionDue = dataRow2["payment_due_method"].ToString();
                    fees.LotInspectionDueDate = dataRow2["payment_due_date"].ToString();
                    fees.LotInspectionFeeDealerEmail = dataRow2["auto_remind_dealer_email"].ToString();
                    if (dataRow2["delaer_remind_period"] != System.DBNull.Value)
                    {
                        fees.LotInspectionFeeDealerEmailRemindPeriod = int.Parse(dataRow2["delaer_remind_period"].ToString());
                    }
                    fees.LotInspectionDueEmail = dataRow2["due_auto_remind_email"].ToString();
                    if (dataRow2["due_auto_remind_period"] != System.DBNull.Value)
                    {
                        fees.LotInspectionDueEmailRemindPeriod = int.Parse(dataRow2["due_auto_remind_period"].ToString());
                    }

                }

                return fees;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/8
        /// get auto remind email from loan table
        /// </summary>
        /// <returns>autoRemindEmail</returns>
        public string getAutoRemindEmailByLoanId(int loanId)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            string autoRemindEmail = null;

            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetAutoRemindEmailByLoanId", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    autoRemindEmail= dataSet.Tables[0].Rows[0]["auto_remind_email"].ToString();

                }
                return autoRemindEmail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal int insertLoanStepOne(LoanSetupStep1 loanSetupStep1, int loanId)
        {
            //DataHandler dataHandler = new DataHandler();
            //List<object[]> paramertList = new List<object[]>();
            //paramertList.Add(new object[] { "@loan_id", loanId });


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("spInsertLoanStepOne", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;
                        command.Parameters.Add("@advance", SqlDbType.Decimal).Value = loanSetupStep1.advancePercentage;
                        // command.Parameters.Add("@advance_receipt", SqlDbType.Bit).Value = loanSetupStep1.allUnitTypes;
                        command.Parameters.Add("@auto_remind_email", SqlDbType.NVarChar).Value = loanSetupStep1.autoReminderEmail;
                        command.Parameters.Add("@auto_remind_period", SqlDbType.Int).Value = loanSetupStep1.autoReminderPeriod;
                        command.Parameters.Add("@default_unit_type", SqlDbType.Int).Value = loanSetupStep1.defaultUnitType;
                        command.Parameters.Add("@is_edit_allowable", SqlDbType.Bit).Value = loanSetupStep1.isEditAllowable;
                        command.Parameters.Add("@is_interest_calculate", SqlDbType.Bit).Value = loanSetupStep1.isInterestCalculate;
                        command.Parameters.Add("@loan_amount", SqlDbType.Decimal).Value = loanSetupStep1.loanAmount;

                        command.Parameters.Add("@loan_number", SqlDbType.VarChar).Value = loanSetupStep1.loanNumber;
                        command.Parameters.Add("@maturity_date", SqlDbType.DateTime).Value = loanSetupStep1.maturityDate;
                        command.Parameters.Add("@non_reg_branch_id", SqlDbType.Int).Value = loanSetupStep1.nonRegisteredBranchId;
                        command.Parameters.Add("@payment_method", SqlDbType.VarChar).Value = loanSetupStep1.paymentMethod;
                        command.Parameters.Add("@pay_off_period", SqlDbType.Int).Value = loanSetupStep1.payOffPeriod;
                        command.Parameters.Add("@pay_off_type", SqlDbType.Char).Value = ((loanSetupStep1.payOffPeriodType == 0) ? 'd':'m');
                        command.Parameters.Add("@loan_code", SqlDbType.VarChar).Value = (new BranchAccess()).getBranchByBranchId(loanSetupStep1.RegisteredBranchId).BranchCode + "-" + loanSetupStep1.loanNumber;
                        //command.Parameters.Add("@monthly_loan_lender_remind_period", SqlDbType.NVarChar).Value = loanSetupStep1.selectedUnitTypes;

                        command.Parameters.Add("@start_date", SqlDbType.DateTime).Value = loanSetupStep1.startDate;
                        command.Parameters.Add("@created_date", SqlDbType.DateTime).Value = DateTime.Now;

                        command.Parameters.Add("@loan_status", SqlDbType.Bit).Value = false;
                        command.Parameters.Add("@is_delete", SqlDbType.Bit).Value = false;
                        SqlParameter returnParameter = command.Parameters.Add("@return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        con.Open();
                        command.ExecuteNonQuery();
                        loanId = (int)returnParameter.Value;

                        if(loanId== 0)
                        {
                            return (int)returnParameter.Value;
                        }
                        else
                        {
                            foreach(UnitType UnitType in loanSetupStep1.allUnitTypes)
                            {
                                if(UnitType.isSelected == true) { 
                                using (SqlCommand cmd = new SqlCommand("spInsertLoanUniType", con))
                                {
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;
                                        cmd.Parameters.Add("@unit_type_id", SqlDbType.Int).Value = UnitType.unitTypeId;
                                        cmd.ExecuteNonQuery();

                                    }
                                }


                            }

                            return (int)returnParameter.Value;
                        }
                        

                    }

                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }

        internal LoanSetupStep1 GetLoanStepOne(int loanId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetLoanStepOneByLoanId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        LoanSetupStep1 loanSetupStep1 = new LoanSetupStep1();


                        while (reader.Read())
                        {

                            loanSetupStep1.advancePercentage = int.Parse(reader["advance"].ToString());
                            //loanSetupStep1.allUnitTypes
                            loanSetupStep1.autoReminderEmail = reader["auto_remind_email"].ToString();
                            loanSetupStep1.autoReminderPeriod = int.Parse(reader["auto_remind_period"].ToString());
                            loanSetupStep1.defaultUnitType = int.Parse(reader["default_unit_type"].ToString());
                            loanSetupStep1.isEditAllowable = Convert.ToBoolean(reader["is_edit_allowable"].ToString());
                            loanSetupStep1.isInterestCalculate = Convert.ToBoolean(reader["is_interest_calculate"].ToString());
                            loanSetupStep1.loanAmount = Convert.ToDecimal(reader["loan_amount"].ToString());
                            loanSetupStep1.loanNumber = reader["loan_number"].ToString();
                            loanSetupStep1.loanNumberForDisplay = loanSetupStep1.loanNumber;
                            loanSetupStep1.maturityDate = Convert.ToDateTime(reader["maturity_date"].ToString());
                            loanSetupStep1.nonRegisteredBranchId = int.Parse(reader["non_reg_branch_id"].ToString());
                            loanSetupStep1.paymentMethod = reader["payment_method"].ToString();
                            loanSetupStep1.payOffPeriod = int.Parse(reader["pay_off_period"].ToString());
                            loanSetupStep1.payOffPeriodType = (Convert.ToChar(reader["pay_off_type"].ToString()) == 'd') ? 0 : 1;
                            //loanSetupStep1.selectedUnitTypes
                            loanSetupStep1.startDate = Convert.ToDateTime(reader["start_date"].ToString());
                            loanSetupStep1.loanCode = reader["loan_code"].ToString();

                        }

                        reader.Close();
                        IList<UnitType> unittypes = new List<UnitType>();
                        using (SqlCommand cmd2 = new SqlCommand("spGetLoanUnitTypesByLoanId", con))
                        {
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;
                            //con.Open();
                            SqlDataReader reader2 = cmd2.ExecuteReader();

                            while (reader2.Read())
                            {
                                UnitType unitType = new UnitType();
                                unitType.unitTypeId = int.Parse(reader2["unit_type_id"].ToString());
                                unitType.unitTypeName = reader2["unit_type_name"].ToString();
                                unittypes.Add(unitType);

                            }
                            loanSetupStep1.selectedUnitTypes = unittypes;

                            return loanSetupStep1;
                        }
                    }
                }


                catch (Exception ex)
                {
                    throw ex;

                }
                finally
                {
                    con.Close();
                }
            }
        }

        /// <summary>
        /// CreatedBy:  Kanishka
        /// CreatedDate:02/29/2016
        /// get loan details by loan code
        /// </summary>
        /// <param name="loanCode"></param>
        /// <returns></returns>
        internal LoanSetupStep1 GetLoanDetailsByLoanCode(string loanCode)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetLoanDetailsByLoanCode", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@loan_code", SqlDbType.VarChar).Value = loanCode;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        LoanSetupStep1 loanSetupStep1 = new LoanSetupStep1();


                        while (reader.Read())
                        {
                            loanSetupStep1.loanId = Convert.ToInt32(reader["loan_id"]);
                            loanSetupStep1.advancePercentage = int.Parse(reader["advance"].ToString());
                            //loanSetupStep1.allUnitTypes
                            loanSetupStep1.autoReminderEmail = reader["auto_remind_email"].ToString();
                            loanSetupStep1.autoReminderPeriod = int.Parse(reader["auto_remind_period"].ToString());
                            loanSetupStep1.defaultUnitType = int.Parse(reader["default_unit_type"].ToString());
                            loanSetupStep1.isEditAllowable = Convert.ToBoolean(reader["is_edit_allowable"].ToString());
                            loanSetupStep1.isInterestCalculate = Convert.ToBoolean(reader["is_interest_calculate"].ToString());
                            loanSetupStep1.loanAmount = Convert.ToDecimal(reader["loan_amount"].ToString());
                            loanSetupStep1.loanNumber = reader["loan_number"].ToString();
                            loanSetupStep1.loanNumberForDisplay = loanSetupStep1.loanNumber;
                            loanSetupStep1.maturityDate = Convert.ToDateTime(reader["maturity_date"].ToString());
                            loanSetupStep1.nonRegisteredBranchId = int.Parse(reader["non_reg_branch_id"].ToString());
                            loanSetupStep1.paymentMethod = reader["payment_method"].ToString();
                            loanSetupStep1.payOffPeriod = int.Parse(reader["pay_off_period"].ToString());
                            loanSetupStep1.payOffPeriodType = (Convert.ToChar(reader["pay_off_type"].ToString()) == 'd') ? 0 : 1;
                            //loanSetupStep1.selectedUnitTypes
                            loanSetupStep1.startDate = Convert.ToDateTime(reader["start_date"].ToString());

                            
                            loanSetupStep1.LoanStatus = Convert.ToBoolean(reader["loan_status"]);
                            //loanSetupStep1.RegisteredBranchId = reader["branch_id"] != null ? Convert.ToInt32(reader["branch_id"].ToString()) : 0;
                            loanSetupStep1.RegisteredBranchCode = reader["r_branch_code"].ToString();
                            loanSetupStep1.RegisteredCompanyCode = reader["company_code"].ToString();
                            if(reader["curtailment_due_date"].ToString()== "EoM") {
                                loanSetupStep1.CurtailmentDueDate = "End of Month";
                            }
                            else {
                                string dueDate = reader["curtailment_due_date"].ToString();
                                if (dueDate.Length > 0)
                                {
                                    if (dueDate.Length == 1)
                                    {
                                        if (dueDate.Contains("1"))
                                        {
                                            loanSetupStep1.CurtailmentDueDate = dueDate+ "st of each month";
                                        }
                                        else if (dueDate.Contains("2"))
                                        {
                                            loanSetupStep1.CurtailmentDueDate = dueDate + "nd of each month";
                                        }
                                        else if (dueDate.Contains("3"))
                                        {
                                            loanSetupStep1.CurtailmentDueDate = dueDate + "rd of each month";
                                        }
                                        else
                                        {
                                            loanSetupStep1.CurtailmentDueDate = dueDate + "th of each month";
                                        }
                                    }
                                    else if(dueDate.Length > 1)
                                    {
                                        if (!dueDate.ElementAt(dueDate.Length - 2).ToString().Contains("1"))
                                        {
                                            if (dueDate.ElementAt(dueDate.Length - 1).ToString().Contains("1"))
                                            {
                                                loanSetupStep1.CurtailmentDueDate = dueDate + "st of each month";
                                            }
                                            else if (dueDate.ElementAt(dueDate.Length - 1).ToString().Contains("2"))
                                            {
                                                loanSetupStep1.CurtailmentDueDate = dueDate + "nd of each month";
                                            }
                                            else if (dueDate.ElementAt(dueDate.Length - 1).ToString().Contains("3"))
                                            {
                                                loanSetupStep1.CurtailmentDueDate = dueDate + "rd of each month";
                                            }
                                            else
                                            {
                                                loanSetupStep1.CurtailmentDueDate = dueDate + "th of each month";
                                            }
                                        }
                                        else
                                        {
                                            loanSetupStep1.CurtailmentDueDate = dueDate + "th of each month";
                                        }
                                    }
                                }
                                //loanSetupStep1.CurtailmentDueDate = reader["curtailment_due_date"].ToString()+"th of each month";
                            }
                            //loanSetupStep1.CurtailmentDueDate = reader["curtailment_due_date"].ToString();
                        }

                        reader.Close();

                        IList<UnitType> unittypes = new List<UnitType>();
                        using (SqlCommand cmd2 = new SqlCommand("spGetLoanUnitTypesByLoanId", con))
                        {
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanSetupStep1.loanId;
                            //con.Open();
                            SqlDataReader reader2 = cmd2.ExecuteReader();

                            while (reader2.Read())
                            {
                                UnitType unitType = new UnitType();
                                unitType.unitTypeId = int.Parse(reader2["unit_type_id"].ToString());
                                unitType.unitTypeName = reader2["unit_type_name"].ToString();
                                unittypes.Add(unitType);

                            }
                            loanSetupStep1.selectedUnitTypes = unittypes;

                            return loanSetupStep1;

                        }
                    }
                }


                catch (Exception ex)
                {
                    throw ex;

                }
                finally
                {
                    con.Close();
                }
            }
        }


        internal void getSelectedUnitTypes(int loanId, LoanSetupStep1 loanSetupStep1)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetLoanUnitTypesByLoanId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        
                        

                        while (reader.Read())
                        {
                            UnitType unitType = new UnitType();

                            unitType.unitTypeId = int.Parse(reader["unit_type_id"].ToString());
                            //loanSetupStep1.selectedUnitTypes.Add(unitType);







                        }

                        reader.Close();




                       

                    }
                }
                catch (Exception ex)
                {
                    throw ex;

                }
                finally
                {
                    con.Close();
                }

            }

        }

        internal int UpdateLoanCurtailmentd(CurtailmentModel curtailmentModel, int loanId)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();

            bool loanStatus = curtailmentModel.LoanStatus == "Yes" ? true : false;
            char calMode = curtailmentModel.CalculationBase == "Full payment" ? 'f' : 'a';

            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@loan_status", loanStatus });
            paramertList.Add(new object[] { "@curtailment_due_date", curtailmentModel.DueDate });
            paramertList.Add(new object[] { "@curtailment_auto_remind_email", curtailmentModel.AutoRemindEmail});
            paramertList.Add(new object[] { "@curtailment_remind_period", curtailmentModel.EmailRemindPeriod });
            paramertList.Add(new object[] { "@curtailment_calculation_type", calMode });

            try
            {
                return dataHandler.ExecuteSQLReturn("spUpdateLoanCurtailmentd", paramertList);
            }
            catch
            {
                return 0;
            }
        }



        /// <summary>
        /// CreatedBy : iRFAN
        /// CreatedDate: 2016/02/25
        /// 
        /// GET LOAN DETAILS BY NON REG BRANCH ID
        /// 
        /// 
        /// 
        /// </summary>
        /// <returns>1</returns>

        public List<LoanSetupStep1> GetLoanDetailsByNonRegBranchId(int nonRegBranchId)
        {

            List<LoanSetupStep1> loanList = new List<LoanSetupStep1>();
            using (
                SqlConnection con =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {

                try
                {
                    var command = new SqlCommand("spGetLoanDetailsByNonRegBranchId", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@non_reg_branch_id", nonRegBranchId);

                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LoanSetupStep1 loan = new LoanSetupStep1();
                            loan.loanNumber = reader["loan_number"].ToString();
                            loan.startDate = Convert.ToDateTime(reader["start_date"]);
                            loan.maturityDate = Convert.ToDateTime(reader["maturity_date"]);
                            loan.loanAmount = Convert.ToDecimal(reader["loan_amount"]);
                            loan.advancePercentage = Convert.ToInt32(reader["advance"]);

                            //0 - day, 1 - month
                            if (reader["pay_off_type"].ToString() == "d")
                                loan.payOffPeriodType = 0;
                            loan.payOffPeriodType = 1;
                            loan.payOffPeriod = Convert.ToInt32(reader["pay_off_period"]);
                            loan.LoanStatus = Convert.ToBoolean(reader["loan_status"]);
                            loan.loanCode = reader["loan_code"].ToString();
                            loan.isEditAllowable = Convert.ToBoolean(reader["is_edit_allowable"]);

                            loanList.Add(loan);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return loanList;
        }
    }



}
