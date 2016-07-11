using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankLoanSystem.DAL
{
    public class Search
    {
        /// <summary>
        /// Frontend Page: Advance unit search section
        /// Title:search units which match with given searching combination
        /// Designed:Piyumi Perera
        /// User story:
        /// Developed: Piyumi Perera
        /// Date created:
        /// </summary>
        /// <param name="unitList"></param>
        /// <param name="identificationNumber"></param>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="vehicleModel"></param>
        /// <returns></returns>
        public List<Unit> GetSearchResultsList(List<Unit> unitList, string identificationNumber, string year, string make, string vehicleModel)
        {
            List<Models.Unit> resultList = new List<Models.Unit>();

            //traverse through each element in list
            foreach (Models.Unit unitElement in unitList)
            {
                //search by identification number,year,make,model
                if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make) && !string.IsNullOrEmpty(unitElement.Model) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by identification number,year,make
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by identification number,year
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by identification number
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by year,make,model
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make) && !string.IsNullOrEmpty(unitElement.Model) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by make,model
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Make.ToLower().StartsWith(make) && !string.IsNullOrEmpty(unitElement.Model) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by model
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (!string.IsNullOrEmpty(unitElement.Model) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by year
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {

                    if (unitElement.Year.ToString().ToLower().StartsWith(year))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by make
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by year,make
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by identification number,model
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && !string.IsNullOrEmpty(unitElement.Model) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by identification number,make
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
               
                //search by identification number,make,model
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Make.ToLower().StartsWith(make) && !string.IsNullOrEmpty(unitElement.Model) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by identification number,year,model
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year) && !string.IsNullOrEmpty(unitElement.Model) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by year,model
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Year.ToString().ToLower().StartsWith(year) && !string.IsNullOrEmpty(unitElement.Model) && unitElement.Model.ToLower().StartsWith(vehicleModel))
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
            //traverse through each element in list
            foreach ( CurtailmentShedule curtailmentShedule in curtailmentList.CurtailmentScheduleInfoModel)
            {
                //search by identification number,year,make,model
                if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IDNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && curtailmentShedule.Make.ToLower().StartsWith(make) && !string.IsNullOrEmpty(curtailmentShedule.Model) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by identification number,year,make
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IDNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && curtailmentShedule.Make.ToLower().StartsWith(make))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by identification number,year
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IDNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Year.ToString().ToLower().StartsWith(year))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by identification number
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IDNumber.ToLower().EndsWith(identificationNumber))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by year,make,model
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && curtailmentShedule.Make.ToLower().StartsWith(make) && !string.IsNullOrEmpty(curtailmentShedule.Model) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by make,model
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.Make.ToLower().StartsWith(make) && !string.IsNullOrEmpty(curtailmentShedule.Model) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by model
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (!string.IsNullOrEmpty(curtailmentShedule.Model) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by year
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {

                    if (curtailmentShedule.Year.ToString().ToLower().StartsWith(year))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by make
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.Make.ToLower().StartsWith(make))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by year,make
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && curtailmentShedule.Make.ToLower().StartsWith(make))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by identification number,model
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IDNumber.ToLower().EndsWith(identificationNumber) && !string.IsNullOrEmpty(curtailmentShedule.Model) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by identification number,make
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IDNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Make.ToLower().StartsWith(make))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
              
                //search by identification number,make,model
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IDNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Make.ToLower().StartsWith(make) && !string.IsNullOrEmpty(curtailmentShedule.Model) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by identification number,year,model
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IDNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && !string.IsNullOrEmpty(curtailmentShedule.Model) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by year,model
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && !string.IsNullOrEmpty(curtailmentShedule.Model) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
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

            //traverse through each element in list
            foreach (Models.UnitPayOffModel unitElement in unitList)
            {
                //search by identification number,year,make,model
                if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make) && !string.IsNullOrEmpty(unitElement.Model) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by identification number,year,make
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by identification number,year
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by identification number
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by year,make,model
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make) && !string.IsNullOrEmpty(unitElement.Model) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by make,model
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Make.ToLower().StartsWith(make) && !string.IsNullOrEmpty(unitElement.Model) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by model
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (!string.IsNullOrEmpty(unitElement.Model) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by year
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {

                    if (unitElement.Year.ToString().ToLower().StartsWith(year))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by make
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by year,make
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by identification number,model
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && !string.IsNullOrEmpty(unitElement.Model) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by identification number,make
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by identification number,make,model
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Make.ToLower().StartsWith(make) && !string.IsNullOrEmpty(unitElement.Model) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by identification number,year,model
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().EndsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year) && !string.IsNullOrEmpty(unitElement.Model) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                //search by year,model
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Year.ToString().ToLower().StartsWith(year) && !string.IsNullOrEmpty(unitElement.Model) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
            }
            return resultList;
        }

        public List<Fees> GetSearchFeeList(List<Fees> curtailmentList, string identificationNumber, string year, string make, string vehicleModel)
        {

            List<Fees> searchList = new List<Fees>();
            //traverse through unit list
            foreach (Fees curtailmentShedule in curtailmentList)
            {
                //search by identification number,year,make,model
                if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IdentificationNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && curtailmentShedule.Make.ToLower().StartsWith(make) && !string.IsNullOrEmpty(curtailmentShedule.Model) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by identification number,year,make
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IdentificationNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && curtailmentShedule.Make.ToLower().StartsWith(make))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by identification number,year
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IdentificationNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Year.ToString().ToLower().StartsWith(year))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by identification number
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IdentificationNumber.ToLower().EndsWith(identificationNumber))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by year,make,model
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && curtailmentShedule.Make.ToLower().StartsWith(make) && !string.IsNullOrEmpty(curtailmentShedule.Model) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by make,model
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.Make.ToLower().StartsWith(make) && !string.IsNullOrEmpty(curtailmentShedule.Model) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by model
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (!string.IsNullOrEmpty(curtailmentShedule.Model) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by year
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {

                    if (curtailmentShedule.Year.ToString().ToLower().StartsWith(year))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by make
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.Make.ToLower().StartsWith(make))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by year,make
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && curtailmentShedule.Make.ToLower().StartsWith(make))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by identification number,model
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IdentificationNumber.ToLower().EndsWith(identificationNumber) && !string.IsNullOrEmpty(curtailmentShedule.Model) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by identification number,make
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IdentificationNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Make.ToLower().StartsWith(make))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by identification number,make,model
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IdentificationNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Make.ToLower().StartsWith(make) && !string.IsNullOrEmpty(curtailmentShedule.Model) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by identification number,year,model
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.IdentificationNumber.ToLower().EndsWith(identificationNumber) && curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && !string.IsNullOrEmpty(curtailmentShedule.Model) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
                //search by year,model
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (curtailmentShedule.Year.ToString().ToLower().StartsWith(year) && !string.IsNullOrEmpty(curtailmentShedule.Model) && curtailmentShedule.Model.ToLower().StartsWith(vehicleModel))
                    {
                        searchList.Add(curtailmentShedule);
                    }
                }
            }
            return searchList;
        }
    }
}