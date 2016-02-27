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
                    if (unitElement.IdentificationNumber.Contains(identificationNumber) && unitElement.Year.ToString().Contains(year) && unitElement.Make.Contains(make) && unitElement.Model.Contains(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.Contains(identificationNumber) && unitElement.Year.ToString().Contains(year) && unitElement.Make.Contains(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.Contains(identificationNumber) && unitElement.Year.ToString().Contains(year))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.Contains(identificationNumber))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(vehicleModel) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Year.ToString().Contains(year) && unitElement.Make.Contains(make) && unitElement.Model.Contains(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(vehicleModel) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Make.Contains(make) && unitElement.Model.Contains(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(vehicleModel) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Model.Contains(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Year.ToString().Contains(year))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Make.Contains(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.Year.ToString().Contains(year) && unitElement.Make.Contains(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.Contains(identificationNumber) && unitElement.Model.Contains(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.Contains(identificationNumber) && unitElement.Make.Contains(make))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.Contains(identificationNumber) && unitElement.Model.Contains(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.Contains(identificationNumber) && unitElement.Make.Contains(make) && unitElement.Model.Contains(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
                else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                {
                    if (unitElement.IdentificationNumber.Contains(identificationNumber) && unitElement.Year.ToString().Contains(year) && unitElement.Model.Contains(vehicleModel))
                    {
                        resultList.Add(unitElement);
                    }
                }
            }
            return resultList;
        }
    }
}