using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BankLoanSystem.Code;
using BankLoanSystem.Models;

namespace BankLoanSystem.DAL
{
    public class UnitAccess
    {
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/23
        /// Get all not advanced unit details by loan id
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns>unitList</returns>
        public List<Unit> GetNotAdvancedUnitDetailsByLoanId(int loanId)
        {

            List<Unit> unitList = new List<Unit>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {

                    var command = new SqlCommand("spGetNotAdvancedUnitDetailsByLoanId", con) { CommandType = CommandType.StoredProcedure };
                    command.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;
                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Unit NotAdvanced = new Unit();

                            NotAdvanced.UnitId = reader["unit_id"].ToString();
                            NotAdvanced.CreatedDate = Convert.ToDateTime(reader["created_date"].ToString());
                            NotAdvanced.IdentificationNumber = reader["identification_number"].ToString();
                            NotAdvanced.Year = Convert.ToInt32(reader["year"].ToString());
                            NotAdvanced.Make = reader["make"].ToString();
                            NotAdvanced.Model = reader["model"].ToString();
                            NotAdvanced.Cost = Convert.ToDecimal(reader["cost"].ToString());
                            NotAdvanced.AdvanceAmount = Convert.ToDecimal(reader["advance_amount"].ToString());


                            unitList.Add(NotAdvanced);
                        }
                    }
                    return unitList;
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
        /// CreatedDate:2016/2/23
        /// Advance all selected items
        /// </summary>
        /// <param name="advanceDate"></param>
        /// <param name="loanId"></param>
        /// <param name="unitList"></param>
        /// <param name="userId"></param>
        /// <returns>countVal</returns>
        public int AdvanceItemList(List<Unit> unitList, int loanId, int userId, DateTime advanceDate)
        {
            int countVal = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                con.Open();

                try
                {
                    foreach (Unit unitObj in unitList)
                    {
                        using (SqlCommand cmd = new SqlCommand("spAdvanceAllSelectedItems", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;
                            cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;
                            cmd.Parameters.Add("@advance_date", SqlDbType.DateTime).Value = advanceDate;
                            cmd.Parameters.Add("@unit_id", SqlDbType.VarChar).Value = unitObj.UnitId;
                            cmd.Parameters.Add("@advance_amount", SqlDbType.Decimal).Value = unitObj.AdvanceAmount;



                            SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);


                            returnParameter.Direction = ParameterDirection.ReturnValue;
                            cmd.ExecuteNonQuery();

                            countVal = (int)returnParameter.Value;
                            cmd.Parameters.Clear();

                            //return countVal;
                        }
                        //countVal = countVal + 1;

                    }
                    return countVal;
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
        /// CreatedDate:2016/2/24
        /// Advance a selected item
        /// </summary>
        /// <param name="advanceDate"></param>
        /// <param name="loanId"></param>
        /// <param name="unitObj"></param>
        /// <param name="userId"></param>
        /// <returns>countVal</returns>
        public int AdvanceItem(Unit unitObj, int loanId, int userId, DateTime advanceDate)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spAdvanceAllSelectedItems", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;
                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;
                        cmd.Parameters.Add("@advance_date", SqlDbType.DateTime).Value = advanceDate;
                        cmd.Parameters.Add("@unit_id", SqlDbType.VarChar).Value = unitObj.UnitId;
                        cmd.Parameters.Add("@advance_amount", SqlDbType.Decimal).Value = unitObj.AdvanceAmount;

                        con.Open();

                        SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);


                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();

                        int countVal = (int)returnParameter.Value;


                        return countVal;
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
        /// CreatedDate:02/24/2016
        /// 
        /// Insert unit to database
        /// 
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool InsertUnit(Unit unit, int userId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spInsertUnitDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@loan_id", unit.LoanId);
                        cmd.Parameters.AddWithValue("@user_id", userId);
                        cmd.Parameters.AddWithValue("@unit_id", unit.UnitId);
                        cmd.Parameters.AddWithValue("@created_date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@unit_type_id", unit.UnitTypeId);
                        if (unit.UnitTypeId == 1)
                        {
                            cmd.Parameters.AddWithValue("@identification_number", unit.vehicle.IdentificationNumber);
                            cmd.Parameters.AddWithValue("@year", unit.vehicle.Year);
                            cmd.Parameters.AddWithValue("@make", unit.vehicle.Make);
                            cmd.Parameters.AddWithValue("@model", unit.vehicle.Model);
                            cmd.Parameters.AddWithValue("@color", unit.vehicle.Color);
                            cmd.Parameters.AddWithValue("@trim", unit.vehicle.Trim);
                            cmd.Parameters.AddWithValue("@miles", unit.vehicle.Miles);
                            cmd.Parameters.AddWithValue("@new_or_used", unit.vehicle.NewOrUsed);
                            cmd.Parameters.AddWithValue("@length", unit.Length);
                            cmd.Parameters.AddWithValue("@hitch_style", unit.HitchStyle);
                            cmd.Parameters.AddWithValue("@speed", unit.Speed);
                            cmd.Parameters.AddWithValue("@trailer_id", unit.TrailerId);
                            cmd.Parameters.AddWithValue("@engine_serial", unit.EngineSerial);
                        }
                        else if (unit.UnitTypeId == 2)
                        {
                            cmd.Parameters.AddWithValue("@identification_number", unit.rv.IdentificationNumber);
                            cmd.Parameters.AddWithValue("@year", unit.rv.Year);
                            cmd.Parameters.AddWithValue("@make", unit.rv.Make);
                            cmd.Parameters.AddWithValue("@model", unit.rv.Model);
                            cmd.Parameters.AddWithValue("@color", unit.Color);
                            cmd.Parameters.AddWithValue("@trim", unit.Trim);
                            cmd.Parameters.AddWithValue("@miles", unit.rv.Miles);
                            cmd.Parameters.AddWithValue("@new_or_used", unit.rv.NewOrUsed);
                            cmd.Parameters.AddWithValue("@length", unit.rv.Length);
                            cmd.Parameters.AddWithValue("@hitch_style", unit.HitchStyle);
                            cmd.Parameters.AddWithValue("@speed", unit.Speed);
                            cmd.Parameters.AddWithValue("@trailer_id", unit.TrailerId);
                            cmd.Parameters.AddWithValue("@engine_serial", unit.EngineSerial);
                        }
                        else if (unit.UnitTypeId == 3)
                        {
                            cmd.Parameters.AddWithValue("@identification_number", unit.camper.IdentificationNumber);
                            cmd.Parameters.AddWithValue("@year", unit.camper.Year);
                            cmd.Parameters.AddWithValue("@make", unit.camper.Make);
                            cmd.Parameters.AddWithValue("@model", unit.camper.Model);
                            cmd.Parameters.AddWithValue("@color", unit.Color);
                            cmd.Parameters.AddWithValue("@trim", unit.Trim);
                            cmd.Parameters.AddWithValue("@miles", unit.Miles);
                            cmd.Parameters.AddWithValue("@new_or_used", unit.camper.NewOrUsed);
                            cmd.Parameters.AddWithValue("@length", unit.camper.Length);
                            cmd.Parameters.AddWithValue("@hitch_style", unit.camper.HitchStyle);
                            cmd.Parameters.AddWithValue("@speed", unit.Speed);
                            cmd.Parameters.AddWithValue("@trailer_id", unit.TrailerId);
                            cmd.Parameters.AddWithValue("@engine_serial", unit.EngineSerial);
                        }
                        else if (unit.UnitTypeId == 4)
                        {
                            cmd.Parameters.AddWithValue("@identification_number", unit.atv.IdentificationNumber);
                            cmd.Parameters.AddWithValue("@year", unit.atv.Year);
                            cmd.Parameters.AddWithValue("@make", unit.atv.Make);
                            cmd.Parameters.AddWithValue("@model", unit.atv.Model);
                            cmd.Parameters.AddWithValue("@color", unit.Color);
                            cmd.Parameters.AddWithValue("@trim", unit.Trim);
                            cmd.Parameters.AddWithValue("@miles", unit.atv.Miles);
                            cmd.Parameters.AddWithValue("@new_or_used", unit.atv.NewOrUsed);
                            cmd.Parameters.AddWithValue("@length", unit.Length);
                            cmd.Parameters.AddWithValue("@hitch_style", unit.HitchStyle);
                            cmd.Parameters.AddWithValue("@speed", unit.Speed);
                            cmd.Parameters.AddWithValue("@trailer_id", unit.TrailerId);
                            cmd.Parameters.AddWithValue("@engine_serial", unit.EngineSerial);
                        }
                        else if (unit.UnitTypeId == 5)
                        {
                            cmd.Parameters.AddWithValue("@identification_number", unit.boat.IdentificationNumber);
                            cmd.Parameters.AddWithValue("@year", unit.boat.Year);
                            cmd.Parameters.AddWithValue("@make", unit.boat.Make);
                            cmd.Parameters.AddWithValue("@model", "");
                            cmd.Parameters.AddWithValue("@color", unit.Color);
                            cmd.Parameters.AddWithValue("@trim", unit.Trim);
                            cmd.Parameters.AddWithValue("@miles", unit.Miles);
                            cmd.Parameters.AddWithValue("@new_or_used", unit.boat.NewOrUsed);
                            cmd.Parameters.AddWithValue("@length", unit.Length);
                            cmd.Parameters.AddWithValue("@hitch_style", unit.HitchStyle);
                            cmd.Parameters.AddWithValue("@speed", unit.Speed);
                            cmd.Parameters.AddWithValue("@trailer_id", unit.boat.TrailerId);
                            cmd.Parameters.AddWithValue("@engine_serial", unit.boat.EngineSerial);
                        }
                        else if (unit.UnitTypeId == 6)
                        {
                            cmd.Parameters.AddWithValue("@identification_number", unit.motorcycle.IdentificationNumber);
                            cmd.Parameters.AddWithValue("@year", unit.motorcycle.Year);
                            cmd.Parameters.AddWithValue("@make", unit.motorcycle.Make);
                            cmd.Parameters.AddWithValue("@model", unit.motorcycle.Model);
                            cmd.Parameters.AddWithValue("@color", unit.motorcycle.Color);
                            cmd.Parameters.AddWithValue("@trim", unit.Trim);
                            cmd.Parameters.AddWithValue("@miles", unit.motorcycle.Miles);
                            cmd.Parameters.AddWithValue("@new_or_used", unit.motorcycle.NewOrUsed);
                            cmd.Parameters.AddWithValue("@length", unit.Length);
                            cmd.Parameters.AddWithValue("@hitch_style", unit.HitchStyle);
                            cmd.Parameters.AddWithValue("@speed", unit.Speed);
                            cmd.Parameters.AddWithValue("@trailer_id", unit.TrailerId);
                            cmd.Parameters.AddWithValue("@engine_serial", unit.EngineSerial);
                        }
                        else if (unit.UnitTypeId == 7)
                        {
                            cmd.Parameters.AddWithValue("@identification_number", unit.snowmobile.IdentificationNumber);
                            cmd.Parameters.AddWithValue("@year", unit.snowmobile.Year);
                            cmd.Parameters.AddWithValue("@make", unit.snowmobile.Make);
                            cmd.Parameters.AddWithValue("@model", unit.snowmobile.Model);
                            cmd.Parameters.AddWithValue("@color", unit.Color);
                            cmd.Parameters.AddWithValue("@trim", unit.Trim);
                            cmd.Parameters.AddWithValue("@miles", unit.Miles);
                            cmd.Parameters.AddWithValue("@new_or_used", unit.snowmobile.NewOrUsed);
                            cmd.Parameters.AddWithValue("@length", unit.Length);
                            cmd.Parameters.AddWithValue("@hitch_style", unit.HitchStyle);
                            cmd.Parameters.AddWithValue("@speed", unit.Speed);
                            cmd.Parameters.AddWithValue("@trailer_id", unit.TrailerId);
                            cmd.Parameters.AddWithValue("@engine_serial", unit.EngineSerial);
                        }
                        else if (unit.UnitTypeId == 8)
                        {
                            cmd.Parameters.AddWithValue("@identification_number", unit.heavyequipment.SerialNumber);
                            cmd.Parameters.AddWithValue("@year", unit.heavyequipment.Year);
                            cmd.Parameters.AddWithValue("@make", unit.heavyequipment.Make);
                            cmd.Parameters.AddWithValue("@model", "");
                            cmd.Parameters.AddWithValue("@color", unit.Color);
                            cmd.Parameters.AddWithValue("@trim", unit.Trim);
                            cmd.Parameters.AddWithValue("@miles", unit.Miles);
                            cmd.Parameters.AddWithValue("@new_or_used", unit.NewOrUsed);
                            cmd.Parameters.AddWithValue("@length", unit.Length);
                            cmd.Parameters.AddWithValue("@hitch_style", unit.HitchStyle);
                            cmd.Parameters.AddWithValue("@speed", unit.Speed);
                            cmd.Parameters.AddWithValue("@trailer_id", unit.TrailerId);
                            cmd.Parameters.AddWithValue("@engine_serial", unit.EngineSerial);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@identification_number", unit.IdentificationNumber);
                            cmd.Parameters.AddWithValue("@year", unit.Year);
                            cmd.Parameters.AddWithValue("@make", unit.Make);
                            cmd.Parameters.AddWithValue("@model", unit.Model);
                            cmd.Parameters.AddWithValue("@color", unit.Color);
                            cmd.Parameters.AddWithValue("@trim", unit.Trim);
                            cmd.Parameters.AddWithValue("@miles", unit.Miles);
                            cmd.Parameters.AddWithValue("@new_or_used", unit.NewOrUsed);
                            cmd.Parameters.AddWithValue("@length", unit.Length);
                            cmd.Parameters.AddWithValue("@hitch_style", unit.HitchStyle);
                            cmd.Parameters.AddWithValue("@speed", unit.Speed);
                            cmd.Parameters.AddWithValue("@trailer_id", unit.TrailerId);
                            cmd.Parameters.AddWithValue("@engine_serial", unit.EngineSerial);
                        }

                        cmd.Parameters.AddWithValue("@cost", unit.Cost);
                        cmd.Parameters.AddWithValue("@advance_amount", unit.AdvanceAmount);
                        cmd.Parameters.AddWithValue("@is_title_received", unit.IsTitleReceived);
                        cmd.Parameters.AddWithValue("@note", unit.Note);
                        cmd.Parameters.AddWithValue("@add_or_advance", unit.AddAndAdvance);
                        cmd.Parameters.AddWithValue("@is_advanced", unit.IsAdvanced);
                        if (unit.IsAdvanced == true)
                        {
                            cmd.Parameters.AddWithValue("@advance_date", unit.AdvanceDate);
                        }
                        else {
                            cmd.Parameters.AddWithValue("@advance_date", DateTime.Now);
                        }
                        cmd.Parameters.AddWithValue("@is_approved", unit.IsApproved);
                        cmd.Parameters.AddWithValue("@status", unit.Status);

                        con.Open();

                        SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);


                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();

                        if (Convert.ToInt32(returnParameter.Value) == 1)
                            return true;
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //return false;
                    throw ex;
                }
            }
        }

        /// <summary>
        /// CreatedBy:  Kanishka 
        /// CreatedDate:02/24/2016
        /// 
        /// Get latest unit id from database
        /// 
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public string GetLatestUnitId(int loanId)
        {
            string latestUnitId = "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("spGetLatestUnitId", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@loan_id", loanId);
                        con.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                latestUnitId = reader["unit_id"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return latestUnitId;
        }

        /// <summary>
        /// CreatedBy:  Kanishka 
        /// CreatedDate:02/26/2016
        /// 
        /// Insert title document detail
        /// 
        /// </summary>
        /// <returns></returns>
        public bool InsertTitleDocumentUploadInfo(string xmlDoc, string unitId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("spInsertTitleDocumentDetails", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Input", xmlDoc);
                        command.Parameters.AddWithValue("@unit_id", unitId);

                        con.Open();
                        command.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return false;
        }

        /// <summary>
        /// CreatedBy:  Kanishka 
        /// CreatedDate:02/24/2016
        /// 
        /// Get latest unit image name from database
        /// 
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        //public string GetLatestUnitImageName(int unitId)
        //{
        //    string latestUnitId = "";
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
        //    {
        //        try
        //        {
        //            using (SqlCommand command = new SqlCommand("spGetLatestUnitId", con))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;

        //                command.Parameters.AddWithValue("@loan_id", loanId);
        //                con.Open();
        //                using (var reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        latestUnitId = reader["unit_id"].ToString();
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    return latestUnitId;
        //}


        /// <summary>
        /// CreatedBy:Irfan
        /// CreatedDate:2016/2/24
        /// Get loan payment details by loan id
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns>LoanPaymentDetails</returns>
        public LoanPaymentDetails GetLoanPaymentDetailsByLoanId(int loanId)
        {

            LoanPaymentDetails loanPaymentDetails = new LoanPaymentDetails();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {

                    var command = new SqlCommand("spGetLoanPaymentDetailsByLoanId", con) { CommandType = CommandType.StoredProcedure };
                    command.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;
                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            loanPaymentDetails.Amount = (Decimal)reader["loan_amount"];
                            //loanPaymentDetails.BalanceAmount = (reader["balance_amount"]) != DBNull.Value ? (Decimal) reader["balance_amount"] :  (Decimal)0.00; 
                            loanPaymentDetails.PendingAmount = (reader["pending_amount"]) != DBNull.Value ? (Decimal)reader["pending_amount"] : (Decimal)0.00;
                            loanPaymentDetails.UsedAmount = (reader["used_amount"]) != DBNull.Value ? (Decimal)reader["used_amount"] : (Decimal)0.00;
                            //loanPaymentDetails.BalanceAfterPending = (reader["balance_amount_after_pending"]) != DBNull.Value ? (Decimal)reader["balance_amount_after_pending"] : (Decimal)0.00;
                            loanPaymentDetails.BalanceAmount = loanPaymentDetails.Amount - loanPaymentDetails.UsedAmount;
                        }
                    }
                    return loanPaymentDetails;
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
        /// CreatedBy:Irfan
        /// CreatedDate:2016/2/24
        /// Get just added units details by loan id and user id
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns>LoanPaymentDetails</returns>
        public List<JustAddedUnit> GetJustAddedUnitDetails(int userId, int loanId)
        {

            List<JustAddedUnit> justAddedUnitList = new List<JustAddedUnit>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {

                    var command = new SqlCommand("spGetJustAddedUnitDetailsByLoanId", con) { CommandType = CommandType.StoredProcedure };
                    command.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;
                    command.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;
                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            JustAddedUnit justAddedUnit = new JustAddedUnit();

                            justAddedUnit.model = reader["model"].ToString();
                            justAddedUnit.advanceAmount = (reader["advance_amount"]) != DBNull.Value ? (Decimal)reader["advance_amount"] : (Decimal)0.00;
                            justAddedUnit.isAdvance = Convert.ToBoolean(reader["is_advanced"]);
                            justAddedUnitList.Add(justAddedUnit);
                        }
                    }
                    return justAddedUnitList;
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
        /// CreatedDate:2016/2/25
        /// Get vehicle models by make
        /// </summary>
        /// <param name="make"></param>
        /// <returns>modelList</returns>
        public List<UnitYearMakeModel> GetVehicleModelsByMakeYear(string make, int year,int unitType)
        {
            List<UnitYearMakeModel> modelList = new List<UnitYearMakeModel>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {

                    var command = new SqlCommand("spGetVehicleModelByMakeYear", con) { CommandType = CommandType.StoredProcedure };
                    command.Parameters.Add("@unit_type", SqlDbType.Int).Value = unitType;
                    command.Parameters.Add("@make", SqlDbType.VarChar).Value = make;
                    command.Parameters.Add("@year", SqlDbType.Int).Value = year;
                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UnitYearMakeModel vym = new UnitYearMakeModel();
                            vym.VehicleModel = reader["model"].ToString();
                            modelList.Add(vym);


                        }
                    }
                    return modelList;
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
        /// CreatedBy:kasun
        /// CreatedDate:2016/2/25
        /// Get vehicle makes by year
        /// </summary>
        /// <param name="make"></param>
        /// <returns>modelList</returns>
        public List<UnitYearMakeModel> GetVehicleMakesByYear(int year,int unitType)
        {
            List<UnitYearMakeModel> modelList = new List<UnitYearMakeModel>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {

                    var command = new SqlCommand("spGetVehicleMakesByYear", con) { CommandType = CommandType.StoredProcedure };
                    command.Parameters.Add("@unit_type", SqlDbType.Int).Value = unitType;
                    command.Parameters.Add("@year", SqlDbType.Int).Value = year;
                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UnitYearMakeModel vmy = new UnitYearMakeModel();
                            vmy.VehicleMake = reader["make"].ToString();
                            modelList.Add(vmy);


                        }
                    }
                    return modelList;
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

        public int DecodeVINYear(string vin)
        {
            int year = 0;

            char[] charArray = vin.ToCharArray();

            char seventhLetter = charArray[6];
            char tenthLetter = charArray[9];

            if (Char.IsLetter(seventhLetter))
            {
                switch (seventhLetter)
                {
                    case 'A':
                        year = 1980;
                        break;
                    case 'B':
                        year = 1981;
                        break;
                    case 'C':
                        year = 1982;
                        break;
                    case 'D':
                        year = 1983;
                        break;
                    case 'E':
                        year = 1984;
                        break;
                    case 'F':
                        year = 1985;
                        break;
                    case 'G':
                        year = 1986;
                        break;
                    case 'H':
                        year = 1987;
                        break;
                    case 'J':
                        year = 1988;
                        break;
                    case 'K':
                        year = 1989;
                        break;
                    case 'L':
                        year = 1990;
                        break;
                    case 'M':
                        year = 1991;
                        break;
                    case 'N':
                        year = 1992;
                        break;
                    case 'P':
                        year = 1993;
                        break;
                    case 'R':
                        year = 1994;
                        break;
                    case 'S':
                        year = 1995;
                        break;
                    case 'T':
                        year = 1996;
                        break;
                    case 'V':
                        year = 1997;
                        break;
                    case 'W':
                        year = 1998;
                        break;
                    case 'X':
                        year = 1999;
                        break;
                    case '1':
                        year = 2001;
                        break;
                    case '2':
                        year = 2002;
                        break;
                    case '3':
                        year = 2003;
                        break;
                    case '4':
                        year = 2004;
                        break;
                    case '5':
                        year = 2005;
                        break;
                    case '6':
                        year = 2006;
                        break;
                    case '7':
                        year = 2007;
                        break;
                    case '8':
                        year = 2008;
                        break;
                    case '9':
                        year = 2009;
                        break;
                }
            }
            else if (Char.IsNumber(seventhLetter))
            {
                switch (seventhLetter)
                {
                    case 'A':
                        year = 2010;
                        break;
                    case 'B':
                        year = 2011;
                        break;
                    case 'C':
                        year = 2012;
                        break;
                    case 'D':
                        year = 2013;
                        break;
                    case 'E':
                        year = 2014;
                        break;
                    case 'F':
                        year = 2015;
                        break;
                    case 'G':
                        year = 2016;
                        break;
                    case 'H':
                        year = 2017;
                        break;
                    case 'J':
                        year = 2018;
                        break;
                    case 'K':
                        year = 2019;
                        break;
                    case 'L':
                        year = 2020;
                        break;
                    case 'M':
                        year = 2021;
                        break;
                    case 'N':
                        year = 2022;
                        break;
                    case 'P':
                        year = 2023;
                        break;
                    case 'R':
                        year = 2024;
                        break;
                    case 'S':
                        year = 2025;
                        break;
                    case 'T':
                        year = 2026;
                        break;
                    case 'V':
                        year = 2027;
                        break;
                    case 'W':
                        year = 2028;
                        break;
                    case 'X':
                        year = 2029;
                        break;
                    case 'Y':
                        year = 2030;
                        break;
                }
            }

            return year;
        }

        /// <summary>
        /// 
        /// CreatedBy:  Kanishka 
        /// CreatedDate:02/26/2016
        /// 
        /// Delete just added unit records
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="loanId"></param>
        public void DeleteJustAddedUnits(int userId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("spDeleteJustAddedUnit", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@user_id", userId);

                        con.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //        private bool ValidateAdvanceAmount()
        //        {
        //            var advancePt = model.AdvancePt;
        //            //var maxCost = model.LoanAmount * 100 / advancePt;
        //            var maxCost = model.Balance * 100 / advancePt;
        //            var maxAdvance = model.Balance;

        //            if (cost <= maxCost)
        //            {
        //                var advanceAmount = (advancePt * val) / 100;


        //else
        //  $("#tagscloud span").text("Cost must be less than balance");
        //            }

    }
}