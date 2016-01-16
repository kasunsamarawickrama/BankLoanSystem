﻿using BankLoanSystemTFN.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystemTFN.DAL
{
    public class Branch
    {

        public List<BranchModel> getBranches(int companyId) {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetBranchesByCompanyId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@companyId", SqlDbType.Int).Value = companyId;

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        List<BranchModel> branchesLists = new List<BranchModel>();
                        

                        while (reader.Read())
                        {
                            BranchModel branch = new BranchModel();
                            branch.BranchId = int.Parse(reader["branch_id"].ToString());
                            branch.BranchName = reader["branch_name"].ToString();
                            branch.BranchCode = reader["branch_code"].ToString();

                            branchesLists.Add(branch);

                        }
                        return branchesLists;

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
    }
}