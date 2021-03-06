﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BankLoanSystem.Models;

namespace BankLoanSystem.DAL
{
    public class RoleAccess
    {
        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/13
        /// 
        /// Get all user role to list
        /// 
        /// argument : None
        /// 
        /// </summary>
        /// <returns>List<UserRole></returns>
        public List<UserRole> GetAllUserRoles()
        {
            DataHandler dataHandler = new DataHandler();
           
            List<UserRole> userRoleList = new List<UserRole>();
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetAllUserRole");
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        UserRole role = new UserRole
                        {
                            RoleId = Convert.ToInt32(dataRow["role_id"]),
                            RoleName = dataRow["role_name"].ToString()
                        };
                        userRoleList.Add(role);
                    }
                    return userRoleList;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

          
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:4/27/2016
        /// </summary>
        /// <param name="company_Id"></param>
        /// <returns></returns>
        public List<UserRole> GetAllUserRoles(int company_Id)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_id", company_Id });

            List<UserRole> userRoleList = new List<UserRole>();
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetAllUserRolsByCompany", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        UserRole role = new UserRole
                        {
                            RoleId = Convert.ToInt32(dataRow["role_id"]),
                            RoleName = dataRow["role_name"].ToString()
                        };
                        userRoleList.Add(role);
                    }
                    return userRoleList;
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
    }
}