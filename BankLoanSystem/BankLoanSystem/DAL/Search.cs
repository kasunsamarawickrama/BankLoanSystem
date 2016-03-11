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
                    if (unitElement.IdentificationNumber.ToLower().StartsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().StartsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().StartsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().StartsWith(identificationNumber))
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
                    if (unitElement.IdentificationNumber.ToLower().StartsWith(identificationNumber) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().StartsWith(identificationNumber) && unitElement.Make.ToLower().StartsWith(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().StartsWith(identificationNumber) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().StartsWith(identificationNumber) && unitElement.Make.ToLower().StartsWith(make) && unitElement.Model.ToLower().StartsWith(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.ToLower().StartsWith(identificationNumber) && unitElement.Year.ToString().ToLower().StartsWith(year) && unitElement.Model.ToLower().StartsWith(vehicleModel))
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