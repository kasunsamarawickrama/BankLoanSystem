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
            List<UserRole> userRoleList = new List<UserRole>();

            using (
                var con =
                    new SqlConnection(
                        ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                var command = new SqlCommand("spGetAllUserRole", con) { CommandType = CommandType.StoredProcedure };
                con.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserRole role = new UserRole
                        {
                            RoleId = Convert.ToInt32(reader["role_id"]),
                            RoleName = reader["role_name"].ToString()
                        };
                        userRoleList.Add(role);
                    }
                }
            }

            return userRoleList;
        } 
    }
}