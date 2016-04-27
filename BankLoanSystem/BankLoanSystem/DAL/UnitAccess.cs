using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BankLoanSystem.Code;
using BankLoanSystem.Models;
using System.Xml.Linq;

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

            DataHandler dataHandler = new DataHandler();
            List<object[]> parameterList = new List<object[]>();
            List<Unit> unitList = new List<Unit>();

            parameterList.Add(new object[] { "@loan_id", loanId });
            try
            {
                DataSet dataSet =  dataHandler.GetDataSet("spGetNotAdvancedUnitDetailsByLoanId", parameterList);

                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow reader in dataSet.Tables[0].Rows)
                    {
                        Unit unit = new Unit();

                        unit.UnitId = reader["unit_id"].ToString();
                        unit.CreatedDate = Convert.ToDateTime(reader["created_date"].ToString());
                        unit.IdentificationNumber = reader["identification_number"].ToString();
                        unit.Year = Convert.ToInt32(reader["year"].ToString());
                        unit.Make = reader["make"].ToString();
                        unit.Model = reader["model"].ToString();
                        unit.Cost = Convert.ToDecimal(reader["cost"].ToString());
                        unit.AdvanceAmount = Convert.ToDecimal(reader["advance_amount"].ToString());

                        unitList.Add(unit);
                    }
                    return unitList;
                }
                else {
                    return unitList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// CreatedBy:kasun
        /// CreatedDate:2016/4/22
        /// 
        /// Get all titles for a loan
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public List<TitleUpload> GetUploadTitlesByLoanId(string unitId)
        {

            DataHandler dataHandler = new DataHandler();
            List<object[]> parameterList = new List<object[]>();
            List<TitleUpload> titleList = new List<TitleUpload>();

            parameterList.Add(new object[] { "@unit_id", unitId });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetUploadTitlesByUnitId", parameterList);

                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow reader in dataSet.Tables[0].Rows)
                    {
                        TitleUpload title = new TitleUpload();

                        title.FilePath = reader["file_path"].ToString();
                        title.UnitId = reader["unit_id"].ToString();
                        title.OriginalFileName = reader["original_file_name"].ToString();

                        titleList.Add(title);
                    }
                    return titleList;
                }
                else {
                    return null;
                }
            }
            catch
            {
                return null;
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

            DataHandler dataHandler = new DataHandler();
            List<object[]> parameterList = new List<object[]>();

            try
            {
                foreach (Unit unitObj in unitList)
                {

                    parameterList.Add(new object[] { "@loan_id", loanId });
                    parameterList.Add(new object[] { "@user_id", userId });
                    parameterList.Add(new object[] { "@advance_date", advanceDate });
                    parameterList.Add(new object[] { "@unit_id", unitObj.UnitId });
                    parameterList.Add(new object[] { "@advance_amount", unitObj.AdvanceAmount});

                    this.GetLoanCurtailmentDetails(loanId, unitObj.UnitId, advanceDate, unitObj.AdvanceAmount, unitObj.Cost);

                    countVal = dataHandler.ExecuteSQLWithIntOutPutParam("spAdvanceAllSelectedItems", parameterList);
                    parameterList.Clear();

                }
                return countVal;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/24
        /// 
        /// Advance a selected item
        /// 
        /// 
        /// </summary>
        /// <param name="advanceDate"></param>
        /// <param name="loanId"></param>
        /// <param name="unitObj"></param>
        /// <param name="userId"></param>
        /// <returns>countVal</returns>
        public int AdvanceItem(Unit unitObj, int loanId, int userId, DateTime advanceDate)
        {
            int countVal = 0;

            DataHandler dataHandler = new DataHandler();
            List<object[]> parameterList = new List<object[]>();

            try
            {
                parameterList.Add(new object[] { "@loan_id", loanId });
                parameterList.Add(new object[] { "@user_id", userId });
                parameterList.Add(new object[] { "@advance_date", advanceDate });
                parameterList.Add(new object[] { "@unit_id", unitObj.UnitId });
                parameterList.Add(new object[] { "@advance_amount", unitObj.AdvanceAmount });

                this.GetLoanCurtailmentDetails(loanId, unitObj.UnitId, advanceDate, unitObj.AdvanceAmount, unitObj.Cost);

                countVal = dataHandler.ExecuteSQLWithIntOutPutParam("spAdvanceAllSelectedItems", parameterList);
                parameterList.Clear();

                return countVal;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// CreatedBy:  Kanishka 
        /// CreatedDate:02/24/2016
        /// 
        /// Insert unit to database
        /// EditedBy: Piyumi
        /// EditedDate: 03/16/2016
        /// add isActive field to parameter list
        /// 
        /// EditedBy: Irfan
        /// EditedDate: 4/25/2016
        /// adding output value IDNumber for logging purpose
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool InsertUnit(Unit unit, int userId ,out string IDNumber)
        {

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();

            paramertList.Add(new object[] { "@loan_id", unit.LoanId });
            paramertList.Add(new object[] { "@user_id", userId });
            paramertList.Add(new object[] { "@unit_id", unit.UnitId });
            paramertList.Add(new object[] { "@created_date", DateTime.Now });
            paramertList.Add(new object[] { "@unit_type_id", unit.UnitTypeId });
            if (unit.UnitTypeId == 1)
            {
                paramertList.Add(new object[] { "@identification_number", unit.vehicle.IdentificationNumber });
                paramertList.Add(new object[] { "@year", unit.vehicle.Year });
                paramertList.Add(new object[] { "@make", unit.vehicle.Make });
                paramertList.Add(new object[] { "@model", unit.vehicle.Model });
                paramertList.Add(new object[] { "@color", unit.vehicle.Color });
                paramertList.Add(new object[] { "@trim", unit.vehicle.Trim });
                paramertList.Add(new object[] { "@miles", unit.vehicle.Miles });
                paramertList.Add(new object[] { "@new_or_used", unit.vehicle.NewOrUsed });
                paramertList.Add(new object[] { "@length", unit.Length });
                paramertList.Add(new object[] { "@hitch_style", unit.HitchStyle });
                paramertList.Add(new object[] { "@speed", unit.Speed });
                paramertList.Add(new object[] { "@trailer_id", unit.TrailerId });
                paramertList.Add(new object[] { "@engine_serial", unit.EngineSerial });
                IDNumber = unit.vehicle.IdentificationNumber;
            }
            else if (unit.UnitTypeId == 2)
            {
                paramertList.Add(new object[] { "@identification_number", unit.rv.IdentificationNumber });
                paramertList.Add(new object[] { "@year", unit.rv.Year });
                paramertList.Add(new object[] { "@make", unit.rv.Make });
                paramertList.Add(new object[] { "@model", unit.rv.Model });
                paramertList.Add(new object[] { "@color", unit.Color });
                paramertList.Add(new object[] { "@trim", unit.Trim });
                paramertList.Add(new object[] { "@miles", unit.rv.Miles });
                paramertList.Add(new object[] { "@new_or_used", unit.rv.NewOrUsed });
                paramertList.Add(new object[] { "@length", unit.rv.Length });
                paramertList.Add(new object[] { "@hitch_style", unit.HitchStyle });
                paramertList.Add(new object[] { "@speed", unit.Speed });
                paramertList.Add(new object[] { "@trailer_id", unit.TrailerId });
                paramertList.Add(new object[] { "@engine_serial", unit.EngineSerial });
                IDNumber = unit.rv.IdentificationNumber;

            }
            else if (unit.UnitTypeId == 3)
            {
                paramertList.Add(new object[] { "@identification_number", unit.camper.IdentificationNumber });
                paramertList.Add(new object[] { "@year", unit.camper.Year });
                paramertList.Add(new object[] { "@make", unit.camper.Make });
                paramertList.Add(new object[] { "@model", unit.camper.Model });
                paramertList.Add(new object[] { "@color", unit.Color });
                paramertList.Add(new object[] { "@trim", unit.Trim });
                paramertList.Add(new object[] { "@miles", unit.Miles });
                paramertList.Add(new object[] { "@new_or_used", unit.camper.NewOrUsed });
                paramertList.Add(new object[] { "@length", unit.camper.Length });
                paramertList.Add(new object[] { "@hitch_style", unit.camper.HitchStyle });
                paramertList.Add(new object[] { "@speed", unit.Speed });
                paramertList.Add(new object[] { "@trailer_id", unit.TrailerId });
                paramertList.Add(new object[] { "@engine_serial", unit.EngineSerial });
                IDNumber = unit.camper.IdentificationNumber;
            }
            else if (unit.UnitTypeId == 4)
            {
                paramertList.Add(new object[] { "@identification_number", unit.atv.IdentificationNumber });
                paramertList.Add(new object[] { "@year", unit.atv.Year });
                paramertList.Add(new object[] { "@make", unit.atv.Make });
                paramertList.Add(new object[] { "@model", unit.atv.Model });
                paramertList.Add(new object[] { "@color", unit.Color });
                paramertList.Add(new object[] { "@trim", unit.Trim });
                paramertList.Add(new object[] { "@miles", unit.atv.Miles });
                paramertList.Add(new object[] { "@new_or_used", unit.atv.NewOrUsed });
                paramertList.Add(new object[] { "@length", unit.Length });
                paramertList.Add(new object[] { "@hitch_style", unit.HitchStyle });
                paramertList.Add(new object[] { "@speed", unit.Speed });
                paramertList.Add(new object[] { "@trailer_id", unit.TrailerId });
                paramertList.Add(new object[] { "@engine_serial", unit.EngineSerial });
                IDNumber = unit.atv.IdentificationNumber;
            }
            else if (unit.UnitTypeId == 5)
            {
                paramertList.Add(new object[] { "@identification_number", unit.boat.IdentificationNumber });
                paramertList.Add(new object[] { "@year", unit.boat.Year });
                paramertList.Add(new object[] { "@make", unit.boat.Make });
                paramertList.Add(new object[] { "@model", "" });
                paramertList.Add(new object[] { "@color", unit.Color });
                paramertList.Add(new object[] { "@trim", unit.Trim });
                paramertList.Add(new object[] { "@miles", unit.Miles });
                paramertList.Add(new object[] { "@new_or_used", unit.boat.NewOrUsed });
                paramertList.Add(new object[] { "@length", unit.Length });
                paramertList.Add(new object[] { "@hitch_style", unit.HitchStyle });
                paramertList.Add(new object[] { "@speed", unit.Speed });
                paramertList.Add(new object[] { "@trailer_id", unit.boat.TrailerId });
                paramertList.Add(new object[] { "@engine_serial", unit.boat.EngineSerial });
                IDNumber = unit.boat.IdentificationNumber;
            }
            else if (unit.UnitTypeId == 6)
            {
                paramertList.Add(new object[] { "@identification_number", unit.motorcycle.IdentificationNumber });
                paramertList.Add(new object[] { "@year", unit.motorcycle.Year });
                paramertList.Add(new object[] { "@make", unit.motorcycle.Make });
                paramertList.Add(new object[] { "@model", unit.motorcycle.Model });
                paramertList.Add(new object[] { "@color", unit.motorcycle.Color });
                paramertList.Add(new object[] { "@trim", unit.Trim });
                paramertList.Add(new object[] { "@miles", unit.motorcycle.Miles });
                paramertList.Add(new object[] { "@new_or_used", unit.motorcycle.NewOrUsed });
                paramertList.Add(new object[] { "@length", unit.Length });
                paramertList.Add(new object[] { "@hitch_style", unit.HitchStyle });
                paramertList.Add(new object[] { "@speed", unit.Speed });
                paramertList.Add(new object[] { "@trailer_id", unit.TrailerId });
                paramertList.Add(new object[] { "@engine_serial", unit.EngineSerial });
                IDNumber = unit.motorcycle.IdentificationNumber;
            }
            else if (unit.UnitTypeId == 7)
            {
                paramertList.Add(new object[] { "@identification_number", unit.snowmobile.IdentificationNumber });
                paramertList.Add(new object[] { "@year", unit.snowmobile.Year });
                paramertList.Add(new object[] { "@make", unit.snowmobile.Make });
                paramertList.Add(new object[] { "@model", unit.snowmobile.Model });
                paramertList.Add(new object[] { "@color", unit.Color });
                paramertList.Add(new object[] { "@trim", unit.Trim });
                paramertList.Add(new object[] { "@miles", unit.Miles });
                paramertList.Add(new object[] { "@new_or_used", unit.snowmobile.NewOrUsed });
                paramertList.Add(new object[] { "@length", unit.Length });
                paramertList.Add(new object[] { "@hitch_style", unit.HitchStyle });
                paramertList.Add(new object[] { "@speed", unit.Speed });
                paramertList.Add(new object[] { "@trailer_id", unit.TrailerId });
                paramertList.Add(new object[] { "@engine_serial", unit.EngineSerial });
                IDNumber = unit.snowmobile.IdentificationNumber;
            }
            else if (unit.UnitTypeId == 8)
            {
                paramertList.Add(new object[] { "@identification_number", unit.heavyequipment.SerialNumber });
                paramertList.Add(new object[] { "@year", unit.heavyequipment.Year });
                paramertList.Add(new object[] { "@make", unit.heavyequipment.Make });
                paramertList.Add(new object[] { "@model", "" });
                paramertList.Add(new object[] { "@color", unit.Color });
                paramertList.Add(new object[] { "@trim", unit.Trim });
                paramertList.Add(new object[] { "@miles", unit.Miles });
                paramertList.Add(new object[] { "@new_or_used", unit.NewOrUsed });
                paramertList.Add(new object[] { "@length", unit.Length });
                paramertList.Add(new object[] { "@hitch_style", unit.HitchStyle });
                paramertList.Add(new object[] { "@speed", unit.Speed });
                paramertList.Add(new object[] { "@trailer_id", unit.TrailerId });
                paramertList.Add(new object[] { "@engine_serial", unit.EngineSerial });
                IDNumber = unit.heavyequipment.SerialNumber;
            }
            else
            {
                paramertList.Add(new object[] { "@identification_number", unit.IdentificationNumber });
                paramertList.Add(new object[] { "@year", unit.Year });
                paramertList.Add(new object[] { "@make", unit.Make });
                paramertList.Add(new object[] { "@model", unit.Model });
                paramertList.Add(new object[] { "@color", unit.Color });
                paramertList.Add(new object[] { "@trim", unit.Trim });
                paramertList.Add(new object[] { "@miles", unit.Miles });
                paramertList.Add(new object[] { "@new_or_used", unit.NewOrUsed });
                paramertList.Add(new object[] { "@length", unit.Length });
                paramertList.Add(new object[] { "@hitch_style", unit.HitchStyle });
                paramertList.Add(new object[] { "@speed", unit.Speed });
                paramertList.Add(new object[] { "@trailer_id", unit.TrailerId });
                paramertList.Add(new object[] { "@engine_serial", unit.EngineSerial });
                IDNumber = unit.IdentificationNumber;
            }

            paramertList.Add(new object[] { "@cost", unit.Cost });
            paramertList.Add(new object[] { "@advance_amount", unit.AdvanceAmount });

            if (unit.TitleReceived == "Yes")
            {
                unit.TitleStatus = 1;
            }
            else
            {
                unit.TitleStatus = 0;
            }
            paramertList.Add(new object[] { "@title_status", unit.TitleStatus });
            paramertList.Add(new object[] { "@note", unit.Note });
            paramertList.Add(new object[] { "@add_or_advance", unit.AddAndAdvance });
            paramertList.Add(new object[] { "@is_advanced", unit.IsAdvanced });
            if (unit.IsAdvanced == true)
            {
                unit.UnitStatus = 1;
                paramertList.Add(new object[] { "@advance_date", unit.AdvanceDate });

            }
            else {
                unit.UnitStatus = 0;
                paramertList.Add(new object[] { "@advance_date", DateTime.Now });
            }
            paramertList.Add(new object[] { "@unit_status", unit.UnitStatus });
            paramertList.Add(new object[] { "@is_approved", unit.IsApproved });
            paramertList.Add(new object[] { "@status", unit.Status });

            try
            {
                bool val = dataHandler.ExecuteSQL("spInsertUnitDetails", paramertList) ? true : false ;


                if (val == true && unit.AddAndAdvance)
                {

                    return this.GetLoanCurtailmentDetails(unit.LoanId, unit.UnitId, unit.AdvanceDate, unit.AdvanceAmount, unit.Cost);
                }
                else if (val == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch
            {
                return false;
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
            DataHandler dataHandler = new DataHandler();
            List<object[]> parameterList = new List<object[]>();

            parameterList.Add(new object[] {"@loan_id" , loanId });

            DataSet dataSet = dataHandler.GetDataSet("spGetLatestUnitId", parameterList);

            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    latestUnitId = row["unit_id"].ToString();
                }
                return latestUnitId;
            }
            else {
                return latestUnitId;
            }
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

            DataHandler dataHandler = new DataHandler();
            List<object[]> parameterList = new List<object[]>();
            parameterList.Add(new object[] { "@Input", xmlDoc });
            parameterList.Add(new object[] { "@unit_id", unitId });

            try
            {
                return dataHandler.ExecuteSQL("spInsertTitleDocumentDetails", parameterList) ? true : false;
            }
            catch
            {
                return false;
            }
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

            DataHandler dataHandler = new DataHandler();
            List<object[]> parameterList = new List<object[]>();

            parameterList.Add(new object[] { "@loan_id", loanId });

            DataSet dataSet = dataHandler.GetDataSet("spGetLoanPaymentDetailsByLoanId", parameterList);

            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    loanPaymentDetails.Amount = (Decimal)row["loan_amount"];
                    //loanPaymentDetails.BalanceAmount = (reader["balance_amount"]) != DBNull.Value ? (Decimal) reader["balance_amount"] :  (Decimal)0.00; 
                    loanPaymentDetails.PendingAmount = (row["pending_amount"]) != DBNull.Value ? (Decimal)row["pending_amount"] : (Decimal)0.00;
                    loanPaymentDetails.UsedAmount = (row["used_amount"]) != DBNull.Value ? (Decimal)row["used_amount"] : (Decimal)0.00;
                    //loanPaymentDetails.BalanceAfterPending = (reader["balance_amount_after_pending"]) != DBNull.Value ? (Decimal)reader["balance_amount_after_pending"] : (Decimal)0.00;
                    loanPaymentDetails.BalanceAmount = loanPaymentDetails.Amount - loanPaymentDetails.UsedAmount;
                }
                return loanPaymentDetails;
            }
            else {
                return loanPaymentDetails;
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

            DataHandler dataHandler = new DataHandler();
            List<object[]> parameterList = new List<object[]>();

            parameterList.Add(new object[] { "@loan_id", loanId });
            parameterList.Add(new object[] { "@user_id", userId });

            DataSet dataSet = dataHandler.GetDataSet("spGetJustAddedUnitDetailsByLoanId", parameterList);

            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    JustAddedUnit justAddedUnit = new JustAddedUnit();

                    justAddedUnit.model = row["model"].ToString();
                    justAddedUnit.advanceAmount = (row["advance_amount"]) != DBNull.Value ? (Decimal)row["advance_amount"] : (Decimal)0.00;
                    justAddedUnit.isAdvance = Convert.ToBoolean(row["is_advanced"]);
                    justAddedUnitList.Add(justAddedUnit);
                }
                return justAddedUnitList;
            }
            else {
                return justAddedUnitList;
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/25
        /// Get vehicle models by make
        /// </summary>
        /// <param name="make"></param>
        /// <returns>modelList</returns>
        public List<UnitYearMakeModel> GetVehicleModelsByMakeYear(string make, int year, int unitType)
        {
            List<UnitYearMakeModel> modelList = new List<UnitYearMakeModel>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> parameterList = new List<object[]>();

            parameterList.Add(new object[] { "@unit_type", unitType });
            parameterList.Add(new object[] { "@make", make });
            parameterList.Add(new object[] { "@year", year });

            DataSet dataSet = dataHandler.GetDataSet("spGetVehicleModelByMakeYear", parameterList);

            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    UnitYearMakeModel vym = new UnitYearMakeModel();
                    vym.VehicleModel = row["model"].ToString();
                    modelList.Add(vym);
                }
                return modelList;
            }
            else {
                return modelList;
            }
        }

        /// <summary>
        /// CreatedBy:kasun
        /// CreatedDate:2016/2/25
        /// Get vehicle makes by year
        /// </summary>
        /// <param name="make"></param>
        /// <returns>modelList</returns>
        public List<UnitYearMakeModel> GetVehicleMakesByYear(int year, int unitType)
        {
            List<UnitYearMakeModel> modelList = new List<UnitYearMakeModel>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> parameterList = new List<object[]>();

            parameterList.Add(new object[] { "@unit_type", unitType });
            parameterList.Add(new object[] { "@year", year });

            DataSet dataSet = dataHandler.GetDataSet("spGetVehicleMakesByYear", parameterList);

            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    UnitYearMakeModel vmy = new UnitYearMakeModel();
                    vmy.VehicleMake = row["make"].ToString();
                    modelList.Add(vmy);
                }
                return modelList;
            }
            else {
                return modelList;
            }
        }
        /// <summary>
        /// CreatedBy:kasun
        /// CreatedDate:2016/4/09
        /// Get vehicle make by vin
        /// </summary>
        /// <param name="vin"></param>
        /// <returns>modelList</returns>
        public string DecodeVINMake(string vin) {

            string strReturn = "";
            string str3 = vin.Substring(0, 3);
            string str2 = vin.Substring(0, 2);

            DataHandler dataHandler = new DataHandler();
            List<object[]> parameterList = new List<object[]>();

            parameterList.Add(new object[] { "@str3", str3 });
            parameterList.Add(new object[] { "@str2", str2 });

            DataSet dataSet = dataHandler.GetDataSet("spGetVehicleMakeByVin", parameterList);

            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    strReturn = row["make"].ToString();
                }
                return strReturn;
            }
            else {
                return strReturn;
            }
        }

        public int DecodeVINYear(string vin)
        {
            int year = 0;

            char[] charArray = vin.ToCharArray();

            char seventhLetter = charArray[6];
            char tenthLetter = charArray[9];

            if (Char.IsNumber(seventhLetter))
            {
                switch (tenthLetter)
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
                    case 'Y':
                        year = 2000;
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
            else if (Char.IsLetter(seventhLetter))
            {
                switch (tenthLetter)
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
            DataHandler dataHandler = new DataHandler();
            List<object[]> parameterList = new List<object[]>();
            parameterList.Add(new object[] { "@user_id", userId });

            try
            {
                dataHandler.ExecuteSQL("spDeleteJustAddedUnit", parameterList);

            }
            catch
            {
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

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 2016/03/15
        /// 
        /// Get loan curtailment schedule by calling StepAccess class method, and creating curtailment date using that values
        /// 
        /// </summary>
        /// <param name="loanId"></param>
        public bool GetLoanCurtailmentDetails(int loanId, string unitId, DateTime advaceDate, decimal advanceAmount, decimal cost)
        {
            try
            {
                StepAccess stepAccess = new StepAccess();
            LoanSetupStep1 loan = stepAccess.GetLoanCurtailmentBreakdown(loanId);
            Int32 curtailmentNo = 1;
            //bool isEditAdvanceAmount = false;
            //isEditAdvanceAmount = ((cost * loan.advancePercentage) / 100) == advanceAmount ? false : true;
            foreach (Curtailment curtailment in loan.curtailmetList)
            {
                curtailment.CurtailmentId = curtailmentNo;
                //check pay off period as days or month               
                curtailment.CurtailmentDate = loan.payOffPeriodType == 0 ? (advaceDate.AddDays(Convert.ToDouble(curtailment.TimePeriod))) : advaceDate.AddMonths(curtailment.TimePeriod ?? 0);
                if (loan.CurtailmentCalculationBase == "f")
                { 
                    if (loan.curtailmetList.Count==curtailmentNo)
                    {
                        curtailment.Amount = Math.Round(advanceAmount - GetCurtailmentCurrentTotal(loan.curtailmetList),2);
                    }
                    else
                    {
                        curtailment.Amount = Math.Round(this.CalculateAdditionalPercentage(advanceAmount, curtailment.Percentage, loan.advancePercentage), 2);
                    }
                }
                else if (loan.CurtailmentCalculationBase == "a")
                {
                    if (loan.curtailmetList.Count == curtailmentNo)
                    {
                        curtailment.Amount = Math.Round(advanceAmount - GetCurtailmentCurrentTotal(loan.curtailmetList), 2);
                    }
                    else
                    {
                        curtailment.Amount = Math.Round((advanceAmount * curtailment.Percentage ?? 0) / 100, 2);
                    }                   
                }
                curtailmentNo++;
            }

           
                XElement xEle = new XElement("Curtailments",
                    from curtailment in loan.curtailmetList
                    select new XElement("Curtailment",
                        new XElement("LoanId", loanId),
                        new XElement("UnitId", unitId),
                        new XElement("CurtNo", curtailment.CurtailmentId),
                        new XElement("CurtAmount", curtailment.Amount),
                        new XElement("CurtDueDate", curtailment.CurtailmentDate),
                        new XElement("CurtStatus", 0)
                        ));
                string xmlDoc = xEle.ToString();
                CurtailmentAccess curtailmentAccess = new CurtailmentAccess();
                return curtailmentAccess.InsertCurtailmentScheduleInfo(xmlDoc, unitId, loanId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal CalculateAdditionalPercentage(decimal advanceAmount, int? percentage, decimal advancePercentage)
        {
            decimal actualPercentage = ((percentage ?? 0 * 100) / advancePercentage)*100;
            return (advanceAmount * actualPercentage) / 100;
        }

        public decimal GetCurtailmentCurrentTotal(IList<Curtailment> lstCurtailment)
        {
            return lstCurtailment.Sum(a => a.Amount);
        }

        /// <summary>
        /// CreatedBy : Kasun
        /// CreatedDate: 2016/03/30
        /// 
        /// get permissioned loans with branch , non reg branch and loan details
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public LoanSelection GetPermisssionGivenLoanwithBranchDeatils(int userId,int companyId,int? branchId,int roleId)
        {
            LoanSelection detailList = new LoanSelection();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();

            paramertList.Add(new object[] { "@userId", userId });
            paramertList.Add(new object[] { "@companyId", companyId });
            paramertList.Add(new object[] { "@branchId", branchId });
            paramertList.Add(new object[] { "@roleId", roleId });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spRetrivePermissionGivenLoans", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    List<Branch> RegBranches = new List<Branch>();
                    List<NonRegBranch> NonRegBranchList = new List<NonRegBranch>();
                    List<LoanSetupStep1> LoanList =  new List<LoanSetupStep1>();

                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        Branch branch = new Branch();
                        NonRegBranch nonRegBranch = new NonRegBranch();
                        LoanSetupStep1 loan = new LoanSetupStep1();

                        branch.BranchId = int.Parse(dataRow["branch_id"].ToString());
                        branch.BranchName = dataRow["regBranchName"].ToString();

                        nonRegBranch.NonRegBranchId = int.Parse(dataRow["non_reg_branch_id"].ToString());
                        nonRegBranch.BranchId = branch.BranchId;
                        nonRegBranch.CompanyNameBranchName = dataRow["nonRegBranchName"].ToString();

                        loan.loanId = int.Parse(dataRow["loan_id"].ToString());
                        loan.loanNumber = dataRow["loan_number"].ToString();
                        loan.loanCode = dataRow["loan_code"].ToString();
                        loan.rightId = dataRow["right_id"].ToString();
                        if (dataRow["is_title_tracked"].ToString() != null && dataRow["is_title_tracked"].ToString() != "") {
                            loan.titleTracked = bool.Parse(dataRow["is_title_tracked"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dataRow["has_lot_inspection_fee"].ToString()))
                        {
                            if (bool.Parse(dataRow["has_lot_inspection_fee"].ToString()))
                            {
                                loan.LotInspectionFee = 1;
                            }
                            else
                            {
                                loan.LotInspectionFee = 0;
                            }
                        }

                        else
                        {
                            loan.LotInspectionFee = 0;
                        }
                        if (!string.IsNullOrEmpty(dataRow["has_monthly_loan_fee"].ToString()))
                        {
                            if (bool.Parse(dataRow["has_monthly_loan_fee"].ToString()))
                            {
                                loan.MonthlyLoanFee = 1;
                            }
                            else
                            {
                                loan.MonthlyLoanFee = 0;
                            }
                        }

                        else
                        {
                            loan.MonthlyLoanFee = 0;
                        }
                        if (!string.IsNullOrEmpty(dataRow["has_advance_fee"].ToString()))
                        {
                            if (bool.Parse(dataRow["has_advance_fee"].ToString()))
                            {
                                loan.AdvanceFee = 1;
                            }
                            else
                            {
                                loan.AdvanceFee = 0;
                            }
                            if (!string.IsNullOrEmpty(dataRow["payment_due_method"].ToString()))
                            {
                                if (dataRow["payment_due_method"].ToString().Contains("Vehicle Payoff"))
                                {
                                    loan.AdvanceFeePayAtPayoff = true;
                                }
                                else
                                {
                                    loan.AdvanceFeePayAtPayoff = false;
                                }

                            }
                        }

                        else
                        {
                            loan.AdvanceFee = 0;
                        }
                        loan.nonRegisteredBranchId = nonRegBranch.NonRegBranchId;
                        bool checkBranch = false;
                        bool checkNonRegBranch = false;
                        bool checkLoan = false;
                        foreach (var br in RegBranches) {
                            if (br.BranchId == branch.BranchId) {
                                checkBranch = true;
                            }
                        }
                        if (checkBranch == false) {
                            RegBranches.Add(branch);
                        }
                        foreach (var nrbr in NonRegBranchList)
                        {
                            if (nrbr.NonRegBranchId == nonRegBranch.NonRegBranchId)
                            {
                                checkNonRegBranch = true;
                            }
                        }
                        if (checkNonRegBranch == false)
                        {
                            NonRegBranchList.Add(nonRegBranch);
                        }

                        foreach (var l in LoanList)
                        {
                            if (l.loanId == loan.loanId)
                            {
                                checkLoan = true;
                            }
                        }
                        if (checkLoan == false)
                        {
                            LoanList.Add(loan);
                        }
                        
                    }
                    detailList.RegBranches = RegBranches;
                    detailList.NonRegBranchList = NonRegBranchList;
                    detailList.LoanList = LoanList;

                    return detailList;
                }
                else
                {
                    return null;
                }
            }

            catch
            {
                return null;
            }
        }
        /// <summary>
        /// CreatedBy : Kasun
        /// CreatedDate: 2016/04/20
        /// 
        /// vin existing check 
        /// <param name="vin"></param>
        /// <param name="loanId"></param>
        /// <returns>
        /// 0 - exist in loan advanced
        /// 1 - exist in loan pending
        /// 2 - exist in loan but another loan
        /// 3 - not exist or payoff
        /// </returns>
        public int IsUniqueVinForaLoan(string vin, int loanId)
        {

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@vin", vin });
            paramertList.Add(new object[] { "@loan_id", loanId });

            return dataHandler.ExecuteSQLWithIntOutPutParam("spIsUniqueVinForaLoan", paramertList);
        }

        /// <summary>
        /// Created by :Asanka senarathna
        /// Created date : 2016/04/20
        /// insert fee 
        /// </summary>
        /// <param name="loan">loan id</param>
        /// <param name="unitid">unit id</param>
        /// <param name="type">fee type</param>
        /// <param name="dis">discription</param>
        /// <param name="amount">fee amount</param>
        /// <param name="duedate">fee due date</param>
        /// <param name="billdate">Bill due date</param>
        /// <returns></returns>
        public int insertFreeDetails(Unit unit)
        {
            try
            {
                string fee_type = "";
            string fee_due_method = "";
            decimal fee_amount = 0;
            int fee_due_date = 0;
            DateTime fee_billdate=unit.AdvanceDate;
                string v_vin="", v_year="", v_model="", v_make="";

            DataHandler dataHandler = new DataHandler();

            List<object[]> paramertList1 = new List<object[]>();
            paramertList1.Add(new object[] { "@loan_id", unit.LoanId });
            DataSet dataSet = dataHandler.GetDataSet("spGetAdvanceFeeData", paramertList1);

            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    fee_due_method = row["payment_due_method"].ToString();
                    fee_type = row["advance_fee_calculate_type"].ToString();
                    fee_amount = decimal.Parse(row["advance_fee_amount"].ToString());
                        if(fee_type!="")
                        {
                            if(row["payment_due_date"].ToString()== "EoM")
                            {
                                fee_due_date= DateTime.DaysInMonth(fee_billdate.Year, fee_billdate.Month+1);
                            }
                            else
                            {
                                fee_due_date = int.Parse(row["payment_due_date"].ToString());
                            } 
                        }
                }
            }
                if (unit.UnitTypeId == 1)
                {
                    v_vin=  unit.vehicle.IdentificationNumber;
                    v_make = unit.vehicle.Make;
                    v_model = unit.vehicle.Model;
                    v_year = unit.vehicle.Year.ToString();
                }
                else if (unit.UnitTypeId == 2)
                {
                    v_vin = unit.rv.IdentificationNumber;
                    v_make = unit.rv.Make;
                    v_model = unit.rv.Model;
                    v_year = unit.rv.Year.ToString();
                }
                else if (unit.UnitTypeId == 3)
                {
                    v_vin = unit.camper.IdentificationNumber;
                    v_make = unit.camper.Make;
                    v_model = unit.camper.Model;
                    v_year = unit.camper.Year.ToString();
                }
                else if (unit.UnitTypeId == 4)
                {
                    v_vin = unit.atv.IdentificationNumber;
                    v_make = unit.atv.Make;
                    v_model = unit.atv.Model;
                    v_year = unit.atv.Year.ToString();
                }
                else if (unit.UnitTypeId == 5)
                {
                    v_vin = unit.boat.IdentificationNumber;
                    v_make = unit.boat.Make;
                    v_model = "";
                    v_year = unit.boat.Year.ToString();
                }
                else if (unit.UnitTypeId == 6)
                {
                    v_vin = unit.motorcycle.IdentificationNumber;
                    v_make = unit.motorcycle.Make;
                    v_model = unit.motorcycle.Model;
                    v_year = unit.motorcycle.Year.ToString();
                }
                else if (unit.UnitTypeId == 7)
                {
                    v_vin = unit.snowmobile.IdentificationNumber;
                    v_make = unit.snowmobile.Make;
                    v_model = unit.snowmobile.Model;
                    v_year = unit.snowmobile.Year.ToString();
                }
                else if (unit.UnitTypeId == 8)
                {
                    v_vin = unit.heavyequipment.SerialNumber;
                    v_make = unit.heavyequipment.Make;
                    v_model ="";
                    v_year = unit.heavyequipment.Year.ToString();
                }
                string discription = fee_due_method + ","+v_vin + "," + v_year + "," + v_make + "," + v_model;

            if (fee_due_method== "Time of Advance")
            {
                List<object[]> paramertList = new List<object[]>();
                paramertList.Add(new object[] { "@loan_id", unit.LoanId });
                paramertList.Add(new object[] { "@unit_id", unit.UnitId });
                paramertList.Add(new object[] { "@type", "advanceFee" });
                paramertList.Add(new object[] { "@description", discription });
                paramertList.Add(new object[] { "@amount", fee_amount });
                paramertList.Add(new object[] { "@due_date", unit.AdvanceDate });
                paramertList.Add(new object[] { "@bill_due_date", unit.AdvanceDate });

                 dataHandler.ExecuteSQL("spInsertAdvanceFee", paramertList);
            }
            else if (fee_due_method == "Once a Month")
            {
                if (fee_type== "Month")
                {
                        fee_billdate = fee_billdate.AddMonths(1);
                        fee_billdate = new DateTime(fee_billdate.Year, fee_billdate.Month, fee_due_date);
                 }
                else if (fee_type == "PayPeriod")
                {
                        if(fee_billdate.Date.Day> fee_due_date)
                        {
                            fee_billdate = fee_billdate.AddMonths(1);
                            fee_billdate=new DateTime(fee_billdate.Year, fee_billdate.Month, fee_due_date);
                        }
                        else
                        {
                            fee_billdate = new DateTime(fee_billdate.Year, fee_billdate.Month, fee_due_date);
                        }
                 }

                    List<object[]> paramertList = new List<object[]>();
                paramertList.Add(new object[] { "@loan_id", unit.LoanId });
                paramertList.Add(new object[] { "@unit_id", unit.UnitId });
                paramertList.Add(new object[] { "@type", "advanceFee" });
                paramertList.Add(new object[] { "@description", discription });
                paramertList.Add(new object[] { "@amount", fee_amount });
                paramertList.Add(new object[] { "@due_date", unit.AdvanceDate });
                paramertList.Add(new object[] { "@bill_due_date", fee_billdate });

                 dataHandler.ExecuteSQL("spInsertAdvanceFee", paramertList);
            }



                return 0;
            }
            catch
            {
                return 0;
            }
                
        }

        public int insertFreeDetailsForAdvance(Unit unit,int loanID)
        {
            try
            {
                string fee_type = "";
                string fee_due_method = "";
                decimal fee_amount = 0;
                int fee_due_date = 0;
                DateTime fee_billdate = unit.AdvanceDate;
                string v_vin = "", v_year = "", v_model = "", v_make = "";

                DataHandler dataHandler = new DataHandler();

                List<object[]> paramertList1 = new List<object[]>();
                paramertList1.Add(new object[] { "@loan_id", loanID });
                DataSet dataSet = dataHandler.GetDataSet("spGetAdvanceFeeData", paramertList1);

                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        fee_due_method = row["payment_due_method"].ToString();
                        fee_type = row["advance_fee_calculate_type"].ToString();
                        fee_amount = decimal.Parse(row["advance_fee_amount"].ToString());
                        if (fee_type != "")
                        {
                            if (row["payment_due_date"].ToString() == "EoM")
                            {
                                fee_due_date = DateTime.DaysInMonth(fee_billdate.Year, fee_billdate.Month + 1);
                            }
                            else
                            {
                                fee_due_date = int.Parse(row["payment_due_date"].ToString());
                            }
                        }
                    }
                }
                v_vin = unit.IdentificationNumber;
                v_make = unit.Make;
                v_model = unit.Model;
                v_year = unit.Year.ToString();

                string discription = fee_due_method + "," + v_vin + "," + v_year + "," + v_make + "," + v_model;

                if (fee_due_method == "Time of Advance")
                {
                    List<object[]> paramertList = new List<object[]>();
                    paramertList.Add(new object[] { "@loan_id", loanID });
                    paramertList.Add(new object[] { "@unit_id", unit.UnitId });
                    paramertList.Add(new object[] { "@type", "advanceFee" });
                    paramertList.Add(new object[] { "@description", discription });
                    paramertList.Add(new object[] { "@amount", fee_amount });
                    paramertList.Add(new object[] { "@due_date", unit.AdvanceDate });
                    paramertList.Add(new object[] { "@bill_due_date", unit.AdvanceDate });

                    dataHandler.ExecuteSQL("spInsertAdvanceFee", paramertList);
                }
                else if (fee_due_method == "Once a Month")
                {
                    if (fee_type == "Month")
                    {
                        fee_billdate = fee_billdate.AddMonths(1);
                        fee_billdate = new DateTime(fee_billdate.Year, fee_billdate.Month, fee_due_date);
                    }
                    else if (fee_type == "PayPeriod")
                    {
                        if (fee_billdate.Date.Day > fee_due_date)
                        {
                            fee_billdate = fee_billdate.AddMonths(1);
                            fee_billdate = new DateTime(fee_billdate.Year, fee_billdate.Month, fee_due_date);
                        }
                        else
                        {
                            fee_billdate = new DateTime(fee_billdate.Year, fee_billdate.Month, fee_due_date);
                        }
                    }

                    List<object[]> paramertList = new List<object[]>();
                    paramertList.Add(new object[] { "@loan_id", loanID });
                    paramertList.Add(new object[] { "@unit_id", unit.UnitId });
                    paramertList.Add(new object[] { "@type", "advanceFee" });
                    paramertList.Add(new object[] { "@description", discription });
                    paramertList.Add(new object[] { "@amount", fee_amount });
                    paramertList.Add(new object[] { "@due_date", unit.AdvanceDate });
                    paramertList.Add(new object[] { "@bill_due_date", fee_billdate });

                    dataHandler.ExecuteSQL("spInsertAdvanceFee", paramertList);
                }



                return 0;
            }
            catch
            {
                return 0;
            }

        }

        public int insertFreeDetailsForPayOff(UnitPayOffModel unit, DateTime payday)
        {
            try
            {
                string fee_type = "";
                string fee_due_method = "";
                decimal fee_amount = 0;
                DateTime fee_billdate = payday;
                string v_vin = "", v_year = "", v_model = "", v_make = "";

                DataHandler dataHandler = new DataHandler();

                List<object[]> paramertList1 = new List<object[]>();
                paramertList1.Add(new object[] { "@loan_id", unit.LoanId });
                DataSet dataSet = dataHandler.GetDataSet("spGetAdvanceFeeData", paramertList1);

                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        fee_due_method = row["payment_due_method"].ToString();
                        fee_type = row["advance_fee_calculate_type"].ToString();
                        fee_amount = decimal.Parse(row["advance_fee_amount"].ToString());
                    }
                }
                v_vin = unit.IdentificationNumber;
                v_make = unit.Make;
                v_model = unit.Model;
                v_year = unit.Year.ToString();

                string discription = fee_due_method + "," + v_vin + "," + v_year + "," + v_make + "," + v_model;

                
                    List<object[]> paramertList = new List<object[]>();
                    paramertList.Add(new object[] { "@loan_id", unit.LoanId });
                    paramertList.Add(new object[] { "@unit_id", unit.UnitId });
                    paramertList.Add(new object[] { "@type", "advanceFee" });
                    paramertList.Add(new object[] { "@description", discription });
                    paramertList.Add(new object[] { "@amount", fee_amount });
                    paramertList.Add(new object[] { "@due_date", fee_billdate });
                    paramertList.Add(new object[] { "@bill_due_date", fee_billdate });

                    dataHandler.ExecuteSQL("spInsertAdvanceFee", paramertList);
                
                return 0;
            }
            catch
            {
                return 0;
            }

        }


        public List<UnitDeleteModel> GetAllUnitsByLoanId(int loanId)
        {
            List<UnitDeleteModel> units = new List<UnitDeleteModel>();

            DataHandler dataHandler = new DataHandler();
            //List<object[]> paramertList = new List<object[]> {new object[] {"@loanId", loanId}};

            List<object[]> paramertList = new List<object[]>();

            paramertList.Add(new object[] { "@loan_id", loanId });

            DataSet dataSet = dataHandler.GetDataSet("spGetAllUnits", paramertList);
            try
            {
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        UnitDeleteModel unit = new UnitDeleteModel();

                        unit.LoanId = loanId;
                        unit.UnitId = dataRow["unit_id"].ToString();
                        unit.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"]).ToString("MM/dd/yyyy");
                        unit.IdentificationNumber = dataRow["identification_number"].ToString();
                        unit.Year = dataRow["year"].ToString();
                        unit.Make = dataRow["make"].ToString();
                        unit.Model = dataRow["model"].ToString();
                        unit.PurchasePrice = Convert.ToDecimal(dataRow["cost"]);
                        unit.AdvanceAmount = Convert.ToDecimal(dataRow["advance_amount"]);
                        unit.UnitStaus = Convert.ToInt32(dataRow["unit_status"]);
                        units.Add(unit);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return units;
        }

        public List<UnitFeeType> GetUnitFeeType(string unitId)
        {
            List<UnitFeeType> unitFeeTypes = new List<UnitFeeType>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]> {new object[] {"@unit_id", unitId}};

            DataSet dataSet = dataHandler.GetDataSet("spGetUnitPaymentDetailsByUnitId", paramertList);

            try
            {
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        UnitFeeType unitFeeType = new UnitFeeType();

                        unitFeeType.LoanId = Convert.ToInt32(dataRow["loan_id"]);
                        unitFeeType.UnitId = unitId;
                        unitFeeType.CurtNumber = Convert.ToInt32(dataRow["curt_number"]);
                        unitFeeType.TblName = dataRow["TableName"].ToString();
                        unitFeeType.FeeType = dataRow["FeeType"].ToString();
                        unitFeeType.PaidAmount = Convert.ToDecimal(dataRow["curt_amount"]);
                        unitFeeType.PaidDate = Convert.ToDateTime(dataRow["paid_date"].ToString()).ToString("MM/dd/yyyy");
                        unitFeeTypes.Add(unitFeeType);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return unitFeeTypes;
        }

        public int DeleteUnit(int loanId, string unitId, decimal paidAmount)
        {
            DataHandler dataHandler = new DataHandler();

            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@unit_id", unitId });
            paramertList.Add(new object[] { "@paid_amount", paidAmount });

            try
            {
                return dataHandler.ExecuteSQLReturn("spManageUnitDelete", paramertList);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:4/20/2016
        /// Get InActive Loans 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="company_Id"></param>
        /// <param name="branchId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public LoanSelection GetInActiveLoans(int userId, int companyId, int branchId, int roleId)
        {
            LoanSelection detailList = new LoanSelection();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();

            paramertList.Add(new object[] { "@userId", userId });
            paramertList.Add(new object[] { "@companyId", companyId });
            paramertList.Add(new object[] { "@branchId", branchId });
            paramertList.Add(new object[] { "@roleId", roleId });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetInActiveLoans", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    List<Branch> RegBranches = new List<Branch>();
                    List<NonRegBranch> NonRegBranchList = new List<NonRegBranch>();
                    List<LoanSetupStep1> LoanList = new List<LoanSetupStep1>();

                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        Branch branch = new Branch();
                        NonRegBranch nonRegBranch = new NonRegBranch();
                        LoanSetupStep1 loan = new LoanSetupStep1();

                        branch.BranchId = int.Parse(dataRow["branch_id"].ToString());
                        branch.BranchName = dataRow["regBranchName"].ToString();

                        nonRegBranch.NonRegBranchId = int.Parse(dataRow["non_reg_branch_id"].ToString());
                        nonRegBranch.BranchId = branch.BranchId;
                        nonRegBranch.CompanyNameBranchName = dataRow["nonRegBranchName"].ToString();

                        loan.loanId = int.Parse(dataRow["loan_id"].ToString());
                        loan.loanNumber = dataRow["loan_number"].ToString();
                        loan.loanCode = dataRow["loan_code"].ToString();
                        loan.loanAmount = decimal.Parse(dataRow["loan_amount"].ToString());
                        loan.CreatedDate = DateTime.Parse(dataRow["created_date"].ToString());
                        loan.CurrentLoanStatus = bool.Parse(dataRow["loan_status"].ToString());

                        loan.nonRegisteredBranchId = nonRegBranch.NonRegBranchId;
                        bool checkBranch = false;
                        bool checkNonRegBranch = false;
                        bool checkLoan = false;
                        foreach (var br in RegBranches)
                        {
                            if (br.BranchId == branch.BranchId)
                            {
                                checkBranch = true;
                            }
                        }
                        if (checkBranch == false)
                        {
                            RegBranches.Add(branch);
                        }
                        foreach (var nrbr in NonRegBranchList)
                        {
                            if (nrbr.NonRegBranchId == nonRegBranch.NonRegBranchId)
                            {
                                checkNonRegBranch = true;
                            }
                        }
                        if (checkNonRegBranch == false)
                        {
                            NonRegBranchList.Add(nonRegBranch);
                        }

                        foreach (var l in LoanList)
                        {
                            if (l.loanId == loan.loanId)
                            {
                                checkLoan = true;
                            }
                        }
                        if (checkLoan == false)
                        {
                            LoanList.Add(loan);
                        }

                    }
                    detailList.RegBranches = RegBranches;
                    detailList.NonRegBranchList = NonRegBranchList;
                    detailList.LoanList = LoanList;

                    return detailList;
                }
                else
                {
                    return null;
                }
            }

            catch
            {
                return null;
            }
        }
    }
}