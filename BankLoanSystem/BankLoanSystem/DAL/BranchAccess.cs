using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.DAL
{
    public class BranchAccess
    {

        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/16
        /// 
        /// Getting all branches
        /// 
        /// </summary>
        /// <returns> a list contain all branches</returns>
        /// 
        public List<Branch> getBranches(int companyId) {

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

                        List<Branch> branchesLists = new List<Branch>();
                        

                        while (reader.Read())
                        {
                            Branch branch = new Branch();
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

        /// <summary>
        /// CreatedBy: Piyumi
        /// Date: 17/1/2016
        /// Description:This method is created to insert branch details
        /// </summary>
        /// <param name="branch"></param>
        /// <param name="id"></param>
        public bool insertBranchDetails(Branch branch, int id)
        {
            string companyCode = getCompanyCodeByUserId(id);
            branch.BranchCode = createBranchCode(companyCode);
            branch.BranchCompany = getCompanyIdByUserId(id);
            branch.BranchCreatedDate = DateTime.Now;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spInsertBranch", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@branch_code", SqlDbType.VarChar).Value = branch.BranchCode;
                        cmd.Parameters.Add("@branch_name", SqlDbType.VarChar).Value = branch.BranchName;
                        cmd.Parameters.Add("@branch_address", SqlDbType.VarChar).Value = branch.BranchAddress;
                        cmd.Parameters.Add("@state", SqlDbType.VarChar).Value = branch.BranchState;
                        cmd.Parameters.Add("@city", SqlDbType.VarChar).Value = branch.BranchCity;
                        cmd.Parameters.Add("@zip", SqlDbType.VarChar).Value = branch.BranchZip;
                        cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = branch.BranchEmail;
                        cmd.Parameters.Add("@phone_num", SqlDbType.VarChar).Value = branch.BranchPhoneNum;
                        cmd.Parameters.Add("@fax", SqlDbType.VarChar).Value = branch.BranchFax;
                        cmd.Parameters.Add("@created_date", SqlDbType.DateTime).Value = branch.BranchCreatedDate;
                        cmd.Parameters.Add("@company_id", SqlDbType.VarChar).Value = branch.BranchCompany;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);


                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        cmd.ExecuteNonQuery();
                        int countVal = (int)returnParameter.Value;

                        if (countVal == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;

                        }
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

        public int getCompanyIdByUserId(int id)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetCompanyCodeByUserId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@user_id", SqlDbType.VarChar).Value = id;

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        int compId = 0;

                        while (reader.Read())
                        {

                            compId = int.Parse(reader["company_id"].ToString());

                        }
                        return compId;

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
        /// CreatedBy:Piyumi
        /// Date:1/17/2016
        /// Description: This method is for creating a branch code for a company
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns>branchCode</returns>
        public string createBranchCode(string companyCode)
        {
            try
            {
                int latestBranchId = getLatestBranchId(companyCode);
                string branchCode = companyCode + "_" + (latestBranchId + 1).ToString();
                return branchCode;
            }
            
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// Date:1/17/2016
        /// Description: This method is for retrieving branchId of a company
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public int getLatestBranchId(string companyCode)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetTopBranchIdByCompanyCode", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@company_code", SqlDbType.VarChar).Value = companyCode;

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        int topId =0;

                        while (reader.Read())
                        {
                            
                            topId = int.Parse(reader["branch_id"].ToString());

                        }
                        return topId;

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
        /// CreatedBy:Piyumi
        /// Date:17/1/2016
        /// Description:This method is created for retrieving companyId when userId is given
        /// </summary>
        /// <param name="id"></param>
        /// <returns>companyId</returns>
        public string getCompanyCodeByUserId(int id)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
            {
                using (SqlCommand cmd = new SqlCommand("spGetCompanyCodeByUserId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = id;

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    Branch branch = new Branch();

                        while (reader.Read())
                    {
                        
                        branch.BranchCompanyCode = reader["company_code"].ToString();
                       
                    }
                    return branch.BranchCompanyCode;

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