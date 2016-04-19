using BankLoanLocal.Models;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BankLoanSystem.DAL
{
    public class UserManageAccess
    {
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/13
        /// Retrieve user_name and created person of users
        /// </summary>
        /// <param name="userType"></param>
        /// <returns>userLogin object</returns>
        public List<UserLogin> getUserByType(int levelId, int userId)
        {
            List<UserLogin> UserList = new List<UserLogin>();
            DataHandler dataHandler = new DataHandler();
            int userRole = getUserRole(userId);
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@level_id", levelId });
            paramertList.Add(new object[] { "@user_id", userId });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetUserLoginDetailsByType");
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        UserLogin user = new UserLogin();
                        user.loginId = userId;
                        user.userId = int.Parse(dataRow["user_id"].ToString());
                        user.userName = dataRow["user_name"].ToString();

                        user.createdBy = int.Parse(dataRow["created_by"].ToString());
                        user.createdByRole = getUserRole(user.createdBy);

                        user.createdName = getUserNameById(user.createdBy);
                        user.roleId = int.Parse(dataRow["role_id"].ToString());

                        if (userRole == 1)
                        {
                            user.isEdit = true;
                        }

                        else if ((userRole == 2) && (levelId == 1))
                        {
                            user.isEdit = false;
                        }
                        else if ((userRole == 2) && (levelId == 2))
                        {
                            user.isEdit = true;
                        }
                        else if ((userRole == 2) && (levelId == 3))
                        {
                            user.isEdit = true;
                        }

                        UserList.Add(user);
                    }

                    return UserList;
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

        /// <summary>
        /// CreatedBy: Piyumi
        /// CreatedDate:2016/1/18/
        /// Delete user details of a selected user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true/false</returns>
        public bool deleteUser(int id)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", id });

            try
            {
                return dataHandler.ExecuteSQL("spDeleteUser", paramertList);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/17
        /// Get userName by userId
        /// </summary>
        /// <param name="createdBy"></param>
        /// <returns>userName</returns>
        public string getUserNameById(int createdBy)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", createdBy });

            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetUserNameByUserId", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    return dataSet.Tables[0].Rows[0]["user_name"].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/13
        /// 
        /// Get user details of a given user
        /// 
        /// EditedBy: Kanishka
        /// EditedDate:2016/2/26
        /// 
        /// Add user company code and branch code
        /// 
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns>GetDetails Object</returns>
        public GetDetails getUserById(int user_id)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();

            paramertList.Add(new object[] { "@user_id", user_id });

            //Company company = new Company();
            try
            {
                DataSet dsCompany = dataHandler.GetDataSet("spGetUserDetailsById", paramertList);
                if (dsCompany != null && dsCompany.Tables.Count != 0)
                {
                    GetDetails dtl = new GetDetails();
                    //company.CompanyId = int.Parse(dsCompany.Tables[0].Rows[0]["company_Id"].ToString());
                    //company.CompanyName = dsCompany.Tables[0].Rows[0]["company_name"].ToString();
                    //company.CompanyCode = dsCompany.Tables[0].Rows[0]["company_code"].ToString();
                    //company.CompanyAddress1 = dsCompany.Tables[0].Rows[0]["company_address_1"].ToString();
                    //company.CompanyAddress2 = dsCompany.Tables[0].Rows[0]["company_address_2"].ToString();
                    //company.StateId = int.Parse(dsCompany.Tables[0].Rows[0]["stateId"].ToString());
                    //company.City = dsCompany.Tables[0].Rows[0]["city"].ToString();
                    //company.Zip = dsCompany.Tables[0].Rows[0]["zip"].ToString();

                    //string[] zipWithExtention = company.Zip.Split('-');

                    //if (zipWithExtention[0] != null) company.ZipPre = zipWithExtention[0];
                    //if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) company.Extension = zipWithExtention[1];

                    //company.Email = dsCompany.Tables[0].Rows[0]["email"].ToString();
                    //company.PhoneNum1 = dsCompany.Tables[0].Rows[0]["phone_num_1"].ToString();
                    //company.PhoneNum2 = dsCompany.Tables[0].Rows[0]["phone_num_2"].ToString();
                    //company.PhoneNum3 = dsCompany.Tables[0].Rows[0]["phone_num_3"].ToString();
                    //company.Fax = dsCompany.Tables[0].Rows[0]["fax"].ToString();
                    //company.WebsiteUrl = dsCompany.Tables[0].Rows[0]["website_url"].ToString();
                    //company.TypeId = int.Parse(dsCompany.Tables[0].Rows[0]["company_type"].ToString());

                    dtl.userId = int.Parse(dsCompany.Tables[0].Rows[0]["user_id"].ToString());
                    dtl.firstName = dsCompany.Tables[0].Rows[0]["first_name"].ToString();
                    dtl.lastName = dsCompany.Tables[0].Rows[0]["last_name"].ToString();
                    dtl.email = dsCompany.Tables[0].Rows[0]["email"].ToString();
                    dtl.phoneNo = dsCompany.Tables[0].Rows[0]["phone_no"].ToString();
                    if (bool.Parse(dsCompany.Tables[0].Rows[0]["status"].ToString()))
                    {

                        dtl.status = "Active";
                    }
                    else
                    {
                        dtl.status = "Not Active";
                    }
                    DateTime day = DateTime.Parse(dsCompany.Tables[0].Rows[0]["created_date"].ToString());
                    dtl.createdDate = day.ToShortDateString();

                    dtl.roleName = dsCompany.Tables[0].Rows[0]["role_name"].ToString();
                    if (dsCompany.Tables[0].Rows[0]["branch_name"].ToString() != "")
                    {
                        dtl.branchName = dsCompany.Tables[0].Rows[0]["branch_name"].ToString();
                        dtl.BranchId = Convert.ToInt32(dsCompany.Tables[0].Rows[0]["branch_id"]);
                        dtl.BranchCode = dsCompany.Tables[0].Rows[0]["BranchCode"].ToString();
                    }
                    else
                    {
                        dtl.branchName = "";
                        dtl.BranchId = 0;
                        dtl.BranchCode = "";
                    }
                    dtl.CompanyCode = dsCompany.Tables[0].Rows[0]["CompanyCode"].ToString();

                    return dtl;
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

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/17
        /// Get User Role By userId 
        /// </summary>
        /// <param name="idval"></param>
        /// <returns>role</returns>
        public int getUserRole(int idval)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", idval });

            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetUserRole", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    return int.Parse(dataSet.Tables[0].Rows[0]["role_id"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }            
        }

        /// <summary>
        /// CreatedBy:irfan
        /// CreatedDate:2016/1/20
        /// Get User Role By userId 
        /// </summary>
        /// <param name="idval"></param>
        /// <returns>role name</returns>
        public string getUserRoleName(int idval)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", idval });

            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetUserRole", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    return dataSet.Tables[0].Rows[0]["role_name"].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }            
        }

        /// <summary>
        /// CreatedBy:Nadeeka
        /// CreatedDate:2016/4/4
        /// Retrieve user detaild by attached branch id and role id
        /// </summary>
        /// <param name="roleId"></param>
        /// /// <param name="nonRegBranchId"></param>
        /// <returns>user object</returns>
        public List<User> getUsersByRoleBranch(int roleId, int branchId)
        {
            List<User> UserList = new List<User>();
            DataHandler dataHandler = new DataHandler();

            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@branch_id", branchId });
            paramertList.Add(new object[] { "@role_id", roleId });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetUserDetailsByRoleBranch", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        User user = new User();

                        user.UserId = int.Parse(dataRow["user_id"].ToString());
                        user.UserName = dataRow["user_name"].ToString();
                        user.FirstName = dataRow["first_name"].ToString();
                        user.LastName = dataRow["last_name"].ToString();

                        UserList.Add(user);
                    }

                    return UserList;
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
