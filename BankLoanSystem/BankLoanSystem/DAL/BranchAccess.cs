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
        /// CreatedBy: MAM. IRFAN
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
                            branch.BranchName = reader["branch_name"].ToString() + " - " + reader["branch_code"].ToString();
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
        /// CreatedDate: 2016/1/17
        /// Insert branch details
        /// </summary>
        /// <param name="branch object"></param>
        /// <param name="id"></param>
        /// <returns>true/false</returns>
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
                        cmd.Parameters.Add("@branch_address", SqlDbType.VarChar).Value = branch.BranchAddress1;
                        cmd.Parameters.Add("@state", SqlDbType.VarChar).Value = branch.BranchState;
                        cmd.Parameters.Add("@city", SqlDbType.VarChar).Value = branch.BranchCity;
                        cmd.Parameters.Add("@zip", SqlDbType.VarChar).Value = branch.BranchZip;
                        cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = branch.BranchEmail;
                        cmd.Parameters.Add("@phone_num", SqlDbType.VarChar).Value = branch.BranchPhoneNum1;
                        cmd.Parameters.Add("@fax", SqlDbType.VarChar).Value = branch.BranchFax;
                        cmd.Parameters.Add("@created_date", SqlDbType.DateTime).Value = branch.BranchCreatedDate;
                        cmd.Parameters.Add("@company_id", SqlDbType.VarChar).Value = branch.BranchCompany;
                        con.Open();
                        
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
        /// <summary>
        /// CreatedBy: Piyumi
        /// CreatedDate: 2016/1/26
        /// Insert branch details
        /// </summary>
        /// <param name="branch object"></param>
        /// <param name="id"></param>
        /// <returns>true/false</returns>
        public bool insertFirstBranchDetails(CompanyBranchModel userCompany3, int id)
        {
            string companyCode = getCompanyCodeByUserId(id);
            //branch.BranchCode = createBranchCode(companyCode);
            userCompany3.MainBranch.BranchCompany = getCompanyIdByUserId(id);
            userCompany3.MainBranch.BranchCreatedDate = DateTime.Now;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spInsertBranch", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@branch_code", SqlDbType.VarChar).Value = userCompany3.MainBranch.BranchCode;
                        cmd.Parameters.Add("@branch_name", SqlDbType.VarChar).Value = userCompany3.MainBranch.BranchName;
                        cmd.Parameters.Add("@branch_address_1", SqlDbType.VarChar).Value = userCompany3.MainBranch.BranchAddress1;
                        cmd.Parameters.Add("@branch_address_2", SqlDbType.VarChar).Value = userCompany3.MainBranch.BranchAddress2;
                        cmd.Parameters.Add("@state", SqlDbType.VarChar).Value = userCompany3.MainBranch.BranchState;
                        cmd.Parameters.Add("@city", SqlDbType.VarChar).Value = userCompany3.MainBranch.BranchCity;
                        if (userCompany3.MainBranch.Extention != null)
                        {
                            userCompany3.MainBranch.BranchZip = userCompany3.MainBranch.ZipPre + "-" + userCompany3.MainBranch.Extention;
                        }
                        else
                        {
                            userCompany3.MainBranch.BranchZip = userCompany3.MainBranch.ZipPre;
                        }
                        cmd.Parameters.Add("@zip", SqlDbType.VarChar).Value = userCompany3.MainBranch.BranchZip;
                        cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = userCompany3.MainBranch.BranchEmail;
                        cmd.Parameters.Add("@phone_num_1", SqlDbType.VarChar).Value = userCompany3.MainBranch.BranchPhoneNum1;
                        cmd.Parameters.Add("@phone_num_2", SqlDbType.VarChar).Value = userCompany3.MainBranch.BranchPhoneNum2;
                        cmd.Parameters.Add("@phone_num_3", SqlDbType.VarChar).Value = userCompany3.MainBranch.BranchPhoneNum3;
                        cmd.Parameters.Add("@fax", SqlDbType.VarChar).Value = userCompany3.MainBranch.BranchFax;
                        cmd.Parameters.Add("@created_date", SqlDbType.DateTime).Value = userCompany3.MainBranch.BranchCreatedDate;
                        cmd.Parameters.Add("@company_id", SqlDbType.VarChar).Value = userCompany3.MainBranch.BranchCompany;
                        con.Open();

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

        /// <summary>
        /// CreatedBy: Piyumi
        /// CreatedDate: 2016/1/17
        /// Get CompanyId By UserId
        /// </summary>
        /// <param name="id"></param>
        /// <returns>compId</returns>
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
        /// CreatedDate:2016/1/17
        /// Create a branch code for a company
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns>branchCode</returns>
        public string createBranchCode(string companyCode)
        {
            try
            {
                string branchCode = "";
                int latestBranchId = getLatestBranchId(companyCode);
                
                if ((latestBranchId>=0)&&(latestBranchId < 9))
                {
                    branchCode = companyCode + "_0" + (latestBranchId + 1).ToString();
                }
                else
                {
                    branchCode = companyCode + "_" + (latestBranchId + 1).ToString();
                }
                
                return branchCode;
            }
            
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/17
        /// Get branchId of a company
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns>topId</returns>
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
        /// CreatedDate:17/1/2016
        /// Get companyId by userId 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BranchCompanyCode</returns>
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