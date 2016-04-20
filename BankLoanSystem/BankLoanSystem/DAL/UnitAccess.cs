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

                    DataSet dataSet = dataHandler.GetDataSet("spAdvanceAllSelectedItems", parameterList);
                    parameterList.Clear();

                    if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                    {
                        DataRow dataRow = dataSet.Tables[0].Rows[0];
                        countVal = int.Parse(dataRow["@return"].ToString());  
                    }
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
        /// Advance a selected item
        /// </summary>
        /// <param name="advanceDate"></param>
        /// <param name="loanId"></param>
        /// <param name="unitObj"></param>
        /// <param name="userId"></param>
        /// <returns>countVal</returns>
        public int AdvanceItem(Unit unitObj, int loanId, int userId, DateTime advanceDate)
        {

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

                DataSet dataSet = dataHandler.GetDataSet("spAdvanceAllSelectedItems", parameterList);
                parameterList.Clear();

                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    DataRow dataRow = dataSet.Tables[0].Rows[0];
                    int countVal = int.Parse(dataRow["@return"].ToString());
                    return countVal;
                }
                else {
                    return 0;
                }
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
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool InsertUnit(Unit unit, int userId)
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
                DataSet dataSet = dataHandler.GetDataSet("spInsertUnitDetails", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    int returnParameter = int.Parse(dataSet.Tables[0].Rows[0]["@return"].ToString());

                    if (returnParameter == 1 && unit.AddAndAdvance)
                    {
                        return this.GetLoanCurtailmentDetails(unit.LoanId, unit.UnitId, unit.AdvanceDate, unit.AdvanceAmount, unit.Cost);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
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
                dataHandler.ExecuteSQL("spInsertTitleDocumentDetails", parameterList);

                return true;
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

            try
            {
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
                return false;
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
                        if (!string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["has_lot_inspection_fee"].ToString()))
                        {
                            if (bool.Parse(dataSet.Tables[0].Rows[0]["has_lot_inspection_fee"].ToString()))
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
                        if (!string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["has_monthly_loan_fee"].ToString()))
                        {
                            if (bool.Parse(dataSet.Tables[0].Rows[0]["has_monthly_loan_fee"].ToString()))
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
                        if (!string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["has_advance_fee"].ToString()))
                        {
                            if (bool.Parse(dataSet.Tables[0].Rows[0]["has_advance_fee"].ToString()))
                            {
                                loan.AdvanceFee = 1;
                            }
                            else
                            {
                                loan.AdvanceFee = 0;
                            }
                            if (!string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["payment_due_method"].ToString()))
                            {
                                if (dataSet.Tables[0].Rows[0]["payment_due_method"].ToString().Contains("Vehicle Payoff"))
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
    }
}