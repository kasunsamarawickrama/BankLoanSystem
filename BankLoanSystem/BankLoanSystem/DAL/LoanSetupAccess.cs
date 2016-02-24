using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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
        public bool IsUniqueLoanNumberForBranch(string loanNumber, int RegisteredBranchId, User user)
        {

            using (
                var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {

                
                LoanSetupAccess loanSetupAccess = new LoanSetupAccess();

                LoanSetupAccess la = new LoanSetupAccess();
                int loanId=0;


                if (user.RoleId == 2)
                {
                    loanId = la.getLoanIdByBranchId(user.BranchId);
                }
                else if (user.RoleId == 1)
                {
                    loanId = la.getLoanIdByUserId(user.UserId);
                }
                //loanId = la.getLoanIdByBranchId(user.BranchId);
                
               
                
                var command = new SqlCommand("spIsUniqueLoanNumberForBranch", con) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add("@loan_number", SqlDbType.NVarChar).Value = loanNumber;
                command.Parameters.Add("@branch_id", SqlDbType.Int).Value = RegisteredBranchId;
                command.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;
                con.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return false;
                    }
                }
            }

            return true;

        }

        /// <summary>
        /// CreatedBy:Irfan MAM
        /// CreatedDate:2016/2/9
        /// getting all unit types
        /// </summary>
        /// <returns>IList<UnitType></returns>

        internal IList<UnitType> getAllUnitTypes()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetAllUnitTypes", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        List<UnitType> unitTypes = new List<UnitType>();


                        while (reader.Read())
                        {
                            UnitType unitType = new UnitType();
                           

                            unitType.unitTypeName = reader["unit_type_name"].ToString();

                            unitType.unitTypeId = int.Parse(reader["unit_type_id"].ToString());
                            unitType.isSelected = false;
                            unitTypes.Add(unitType);

                        }
                        return unitTypes;

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
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/8
        /// get loanId by userId from step table
        /// </summary>
        /// <returns>loanId</returns>
        public int getLoanIdByUserId(int uId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetLoanIdByUserId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value =uId;

                        con.Open();

                        SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);


                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();

                        int loanId = (int)returnParameter.Value;

                        return loanId;
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
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/12
        /// get loanId by branchId from step table
        /// </summary>
        /// <returns>loanId</returns>
        public int getLoanIdByBranchId(int branchId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetLoanIdByBranchId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@branch_id", SqlDbType.Int).Value = branchId;

                        con.Open();

                        SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);


                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();

                        int loanId = (int)returnParameter.Value;

                        return loanId;
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

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spCheckLoanIsInAdvanceFeeTable", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read()) {                          
                            fees.AdvanceAmount = double.Parse(reader["advance_fee_amount"].ToString());
                            fees.AdvanceFeeCalculateType = reader["advance_fee_calculate_type"].ToString();
                            fees.AdvanceNeedReceipt = bool.Parse(reader["receipt"].ToString());
                            fees.AdvanceDue = reader["payment_due_method"].ToString();
                            fees.AdvanceDueDate = reader["payment_due_date"].ToString();
                            fees.AdvanceFeeDealerEmail = reader["auto_remind_dealer_email"].ToString();
                            if (reader["delaer_remind_period"] != System.DBNull.Value)
                            {
                                fees.AdvanceFeeDealerEmailRemindPeriod = int.Parse(reader["delaer_remind_period"].ToString());
                            }
                            fees.AdvanceDueEmail = reader["due_auto_remind_email"].ToString();
                            if (reader["due_auto_remind_period"] != System.DBNull.Value)
                            {
                                fees.AdvanceDueEmailRemindPeriod = int.Parse(reader["due_auto_remind_period"].ToString());
                            }
                            
                        }
                        reader.Close();
                    }
                    using (SqlCommand cmd = new SqlCommand("spCheckLoanIsInMonthlyLoanFeeTable", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            fees.MonthlyLoanAmount = double.Parse(reader["monthly_loan_fee_amount"].ToString());
                            fees.MonthlyLoanNeedReceipt = bool.Parse(reader["receipt"].ToString());
                            fees.MonthlyLoanDue = reader["payment_due_method"].ToString();
                            fees.MonthlyLoanDueDate = reader["payment_due_date"].ToString();
                            fees.MonthlyLoanFeeDealerEmail = reader["auto_remind_dealer_email"].ToString();
                            if (reader["delaer_remind_period"] != System.DBNull.Value)
                            {
                                fees.MonthlyLoanFeeDealerEmailRemindPeriod = int.Parse(reader["delaer_remind_period"].ToString());
                            }
                            fees.MonthlyLoanDueEmail = reader["due_auto_remind_email"].ToString();
                            if (reader["due_auto_remind_period"] != System.DBNull.Value)
                            {
                                fees.MonthlyLoanDueEmailRemindPeriod = int.Parse(reader["due_auto_remind_period"].ToString());
                            }

                        }
                        reader.Close();
                    }
                    using (SqlCommand cmd = new SqlCommand("spCheckLoanIsInLotInspectionFeeTable", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            fees.LotInspectionAmount = double.Parse(reader["lot_inspection_amount"].ToString());
                            fees.LotInspectionNeedReceipt = bool.Parse(reader["receipt"].ToString());
                            fees.LotInspectionDue = reader["payment_due_method"].ToString();
                            fees.LotInspectionDueDate = reader["payment_due_date"].ToString();
                            fees.LotInspectionFeeDealerEmail = reader["auto_remind_dealer_email"].ToString();
                            if (reader["delaer_remind_period"] != System.DBNull.Value)
                            {
                                fees.LotInspectionFeeDealerEmailRemindPeriod = int.Parse(reader["delaer_remind_period"].ToString());
                            }
                            fees.LotInspectionDueEmail = reader["due_auto_remind_email"].ToString();
                            if (reader["due_auto_remind_period"] != System.DBNull.Value)
                            {
                                fees.LotInspectionDueEmailRemindPeriod = int.Parse(reader["due_auto_remind_period"].ToString());
                        }
                        }
                        reader.Close();
                    }

                    return fees;
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
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/8
        /// get auto remind email from loan table
        /// </summary>
        /// <returns>autoRemindEmail</returns>
        public string getAutoRemindEmailByLoanId(int loanId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetAutoRemindEmailByLoanId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();


                        string autoRemindEmail = null;

                        while (reader.Read())
                        {


                            autoRemindEmail = reader["auto_remind_email"].ToString();
                                

                        }
                        return autoRemindEmail;

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

        internal int insertLoanStepOne(LoanSetupStep1 loanSetupStep1, int loanId)
        {
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

                        command.Parameters.Add("@loan_number", SqlDbType.NVarChar).Value = loanSetupStep1.loanNumber;
                        command.Parameters.Add("@maturity_date", SqlDbType.DateTime).Value = loanSetupStep1.maturityDate;
                        command.Parameters.Add("@non_reg_branch_id", SqlDbType.Int).Value = loanSetupStep1.nonRegisteredBranchId;
                        command.Parameters.Add("@payment_method", SqlDbType.NVarChar).Value = loanSetupStep1.paymentMethod;
                        command.Parameters.Add("@pay_off_period", SqlDbType.Int).Value = loanSetupStep1.payOffPeriod;
                        command.Parameters.Add("@pay_off_type", SqlDbType.Char).Value = ((loanSetupStep1.payOffPeriodType == 0) ? 'd':'m');
                        command.Parameters.Add("@loan_code", SqlDbType.NVarChar).Value = (new BranchAccess()).getBranchByBranchId(loanSetupStep1.RegisteredBranchId).BranchCode + "-" + loanSetupStep1.loanNumber;
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


        internal int updateLoanActivation(bool activation, int loanId)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("spUpdateLoanActivation", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;
                        command.Parameters.Add("@loan_status", SqlDbType.Bit).Value = activation;                        
                       
                        SqlParameter returnParameter = command.Parameters.Add("@return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        con.Open();
                        command.ExecuteNonQuery();
                        loanId = (int)returnParameter.Value;

                        return loanId;

                    }

                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }
    }
}
