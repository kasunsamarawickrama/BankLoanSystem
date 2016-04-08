using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankLoanSystem.DAL
{
    public class Search
    {
        public List<Unit> GetSearchResultsList(List<Unit> unitList, string identificationNumber, string year, string make, string vehicleModel)
        {
            List<Models.Unit> resultList = new List<Models.Unit>();


            foreach (Models.Unit unitElement in unitList)
            {
                if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Make.ToLower().StartsWith(make) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {

                    if (unitElement.Year.ToString().ToLower().StartsWith(year))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Make.ToLower().StartsWith(make) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
            }
            return resultList;
        }

        public List<CurtailmentShedule> GetSearchCurtailmentList(CurtailmentScheduleModel curtailmentList, string identificationNumber, string year, string make, string vehicleModel )
        {

            List<CurtailmentShedule> searchList = new List<CurtailmentShedule>();

            foreach ( CurtailmentShedule curtailmentShedule in curtailmentList.CurtailmentScheduleInfoModel)
            {
                if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IDNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && curtailmentShedule.Make.ToLower().StartsWith(make) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IDNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && curtailmentShedule.Make.ToLower().StartsWith(make))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IDNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Year.ToString().ToLower().StartsWith(year))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IDNumber.ToLower().EndsWith(identificationNumber))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && curtailmentShedule.Make.ToLower().StartsWith(make) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.Make.ToLower().StartsWith(make) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {

                    if (curtailmentShedule.Year.ToString().ToLower().StartsWith(year))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.Make.ToLower().StartsWith(make))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && curtailmentShedule.Make.ToLower().StartsWith(make))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IDNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IDNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Make.ToLower().StartsWith(make))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IDNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IDNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Make.ToLower().StartsWith(make) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IDNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
            }
            return searchList;
        }
        public List<UnitPayOffModel> GetSearchPayOffList(List<UnitPayOffModel> unitList, string identificationNumber, string year, string make, string vehicleModel)
        {
            List<Models.UnitPayOffModel> resultList = new List<Models.UnitPayOffModel>();


            foreach (Models.UnitPayOffModel unitElement in unitList)
            {
                if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Make.ToLower().StartsWith(make) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {

                    if (unitElement.Year.ToString().ToLower().StartsWith(year))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Make.ToLower().StartsWith(make) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
            }
            return resultList;
        }
    }
}