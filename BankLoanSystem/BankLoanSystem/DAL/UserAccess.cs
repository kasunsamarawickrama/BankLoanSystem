using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BankLoanSystem.DAL
{
    /// <summary>
    /// 
    /// 
    /// UpdatedBy : nadeeka
    /// UpdatedDate: 2016/03/03
    /// removed existing connection open method in all the functions
    /// call DataHandler class to save user object
    /// </summary>
    public class UserAccess
    {
        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/16
        /// 
        /// Insert User details
        /// 
        /// argument : user (User)
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/03
        /// removed existing connection open method and set parameter's to object list and pass stored procedure name to
        /// call DataHandler class to save user object
        /// </summary>
        /// <returns>1</returns>
        public int InsertUser(User user)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_Id", user.UserId });
            paramertList.Add(new object[] { "@user_name", user.UserName });
            paramertList.Add(new object[] { "@password", user.Password });
            paramertList.Add(new object[] { "@first_name", user.FirstName });
            paramertList.Add(new object[] { "@last_name", user.LastName });
            paramertList.Add(new object[] { "@email", user.Email });
            paramertList.Add(new object[] { "@phone_no", user.PhoneNumber });
            paramertList.Add(new object[] { "@status", user.Status });
            paramertList.Add(new object[] { "@is_delete", user.IsDelete });
            paramertList.Add(new object[] { "@created_by", user.CreatedBy });
            paramertList.Add(new object[] { "@create_Date", DateTime.Now });
            paramertList.Add(new object[] { "@branch_id", user.BranchId });
            paramertList.Add(new object[] { "@role_id", user.RoleId });
            paramertList.Add(new object[] { "@Company_id", user.Company_Id });
            paramertList.Add(new object[] { "@step_status", user.step_status });

            try
            {
                return dataHandler.ExecuteSQL("spInsertUser", paramertList) ? 1 : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<User> GetUserListByCompany(int companyId)
        {
            List<User> users = new List<User>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_id", companyId });

            DataSet dataSet = dataHandler.GetDataSet("spGetUsersbyCompany2", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    User user = new User();
                    user.UserId = Convert.ToInt32(dataRow["user_id"].ToString());
                    user.UserName = dataRow["user_name"].ToString();
                    user.Password = dataRow["password"].ToString();
                    user.FirstName = dataRow["first_name"].ToString();
                    user.LastName = dataRow["last_name"].ToString();
                    user.NewEmail = dataRow["email"].ToString();
                    user.PhoneNumber = dataRow["phone_no"].ToString();
                    user.BranchId = Convert.ToInt32(dataRow["branch_id"].ToString());
                    user.RoleId = Convert.ToInt32(dataRow["role_id"].ToString());

                    users.Add(user);
                }

                return users;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/13
        /// 
        /// retreive User Detail By UserId
        /// 
        /// argument : user_id (int)
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/03
        /// removed existing connection open method and set parameter to object list and pass stored procedure name
        /// call DataHandler class method and getting dataset object,
        /// create and return user object using that dataset
        /// 
        /// </summary>
        /// <returns>User object</returns>
        public User retreiveUserByUserId(int id)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_Id", id });

            DataSet dataSet = dataHandler.GetDataSet("spRetrieveUserByUserId", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                User user = new User();
                DataRow dataRow = dataSet.Tables[0].Rows[0];

                user.UserId = int.Parse(dataRow["user_id"].ToString());
                user.FirstName = dataRow["first_name"].ToString();
                user.LastName = dataRow["last_name"].ToString();
                user.Email = dataRow["email"].ToString();
                user.PhoneNumber = dataRow["phone_no"].ToString();
                user.Status = (bool)dataRow["status"];
                user.CreatedDate = (DateTime)dataRow["created_date"];

                user.IsDelete = (bool)dataRow["is_delete"];
                user.CreatedBy = int.Parse(dataRow["created_by"].ToString());
                if (dataRow["branch_id"].ToString() != "")
                {
                    user.BranchId = int.Parse(dataRow["branch_id"].ToString());
                }
                else
                {
                    user.BranchId = 0;
                }
                user.RoleId = int.Parse(dataRow["role_id"].ToString());
                user.UserName = dataRow["user_name"].ToString();
                user.UneditUserName = dataRow["user_name"].ToString();
                user.Password = dataRow["password"].ToString();
                if (dataRow["company_id"].ToString() != "")
                {
                    user.Company_Id = int.Parse(dataRow["company_id"].ToString());
                }


                return user;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/13
        /// 
        /// update User Details userName, firstName, lastName, email, phone, isActive, branchId, password
        /// 
        /// argument : user_id (int)
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/03
        /// removed existing connection open method and set parameter's to object list and pass stored procedure name to
        /// call DataHandler class to update given user details
        /// </summary>
        /// <returns>boolean value</returns>
        public bool updateUserDetails(int userId, string userName, string firstName, string lastName, string email,
            string phone, bool isActive, int branchId, DateTime modifiedDate)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_Id", userId });
            paramertList.Add(new object[] { "@user_name", userName });
            paramertList.Add(new object[] { "@first_name", firstName });
            paramertList.Add(new object[] { "@last_name", lastName });
            paramertList.Add(new object[] { "@email", email });
            paramertList.Add(new object[] { "@phone_no", phone });
            paramertList.Add(new object[] { "@status", isActive });
            paramertList.Add(new object[] { "@modified_date", DateTime.Now });
            paramertList.Add(new object[] { "@branch_id", branchId });

            try
            {
                return dataHandler.ExecuteSQL("spUpdateUserDetails", paramertList) ? true : false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/13
        /// 
        /// update User Details  userName, firstName, lastName, email, phone, password
        /// 
        /// argument : user_id (int)
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/03
        /// removed existing connection open method and set parameter's to object list and pass stored procedure name to
        /// call DataHandler class to update given user details
        /// </summary>
        /// <returns>User object</returns>
        public bool updateProfileDetails(int userId, string userName, string firstName, string lastName, string email,
            string phone, DateTime modifiedDate)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_Id", userId });
            paramertList.Add(new object[] { "@user_name", userName });
            paramertList.Add(new object[] { "@first_name", firstName });
            paramertList.Add(new object[] { "@last_name", lastName });
            paramertList.Add(new object[] { "@email", email });
            paramertList.Add(new object[] { "@phone_no", phone });
            paramertList.Add(new object[] { "@modified_date", DateTime.Now });

            try
            {
                return dataHandler.ExecuteSQL("spUpdateProfileDetails", paramertList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/13
        /// 
        /// Check 
        /// 
        /// argument : userName (string)
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/03
        /// removed existing connection open method and set parameter to object list and pass stored procedure name
        /// call DataHandler class method and getting boolean value,
        /// 
        /// </summary>
        /// <returns>true/false</returns>
        public bool IsUniqueUserName(string userName)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_name", userName });
            return dataHandler.GetDataExistance("spIsUniqueUserName", paramertList);
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/24
        /// 
        /// Check 
        /// 
        /// argument : email (string)
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/03
        /// removed existing connection open method and set parameter to object list and pass stored procedure name
        /// call DataHandler class method and getting boolean value,
        /// 
        /// </summary>
        /// <returns>true/false</returns>
        public bool IsUniqueEmail(string email)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@email", email });
            return dataHandler.GetDataExistance("spIsUniqueEmail", paramertList);
        }

        /// <summary>
        /// CreatedBy : irfan
        /// CreatedDate: 2016/01/16
        /// 
        /// get user id using email
        /// 
        /// argument : email (string)
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/03
        /// removed existing connection open method and set parameter to object list and pass stored procedure name
        /// call DataHandler class method and getting dataset object,
        /// return user id using dataset object
        /// </summary>
        /// <returns>1</returns>
        public int getUserId(string email)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@email", email });

            DataSet dataSet = dataHandler.GetDataSet("spGetUserIdByemail", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                int userId = int.Parse(dataSet.Tables[0].Rows[0]["user_id"].ToString());
                return userId;
            }
            return 0;
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/19
        /// 
        /// activated user account 
        /// 
        /// argument : userId (int)
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/03
        /// removed existing connection open method and set parameter's to object list and pass stored procedure name to
        /// call DataHandler class to update given user details
        /// 
        /// 
        /// </summary>
        /// <returns>0 or 1</returns>
        public int UpdateUserSatus(int userId, string activationCode)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_Id", userId });
            paramertList.Add(new object[] { "@activation_code", activationCode });

            try
            {
                return dataHandler.ExecuteSQL("spUpdateUserStatus", paramertList) ? 1 : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/21
        /// 
        /// Insert new created user to user_activation table
        /// 
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/03
        /// removed existing connection open method and set parameter's to object list and pass stored procedure name to
        /// call DataHandler class to insert activation code for user
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="activationCode"></param>
        /// <returns></returns>
        public int InsertUserActivation(int userId, string activationCode)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_Id", userId });
            paramertList.Add(new object[] { "@activation_code", activationCode });

            try
            {
                return dataHandler.ExecuteSQL("spInsertUserActivation", paramertList) ? 1 : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/20
        /// 
        /// get Company Employee Details  userName, password
        /// 
        /// argument : user_id (int)
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/03
        /// removed existing connection open method and set parameter to object list and pass stored procedure name
        /// call DataHandler class method and getting dataset object,
        /// return user name using dataset object
        /// 
        /// </summary>
        /// <returns>Company EMployee Name</returns>
        public string getCompanyEmployeeName(int userId)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@system_admin_id", userId });

            DataSet dataSet = dataHandler.GetDataSet("spGetCompanyEmployeeDetails", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                return dataSet.Tables[0].Rows[0]["user_name"].ToString();

            }
            return "";
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/02/08
        /// 
        /// Get Users by user role
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/03
        /// removed existing connection open method and set parameter to object list and pass stored procedure name to
        /// call DataHandler class method and getting dataset object,
        /// create and return user object list using that dataset
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="curUserRoleId"></param>
        /// <returns>user object list</returns>
        public List<User> GetUserList(int companyId, int curUserRoleId)
        {
            List<User> users = new List<User>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_id", companyId });

            DataSet dataSet = dataHandler.GetDataSet("spGetUsersbyCompany", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    User user = new User();
                    user.UserId = Convert.ToInt32(dataRow["user_id"].ToString());
                    user.UserName = dataRow["user_name"].ToString();
                    user.Password = dataRow["password"].ToString();
                    user.FirstName = dataRow["first_name"].ToString();
                    user.LastName = dataRow["last_name"].ToString();
                    user.NewEmail = dataRow["email"].ToString();
                    user.PhoneNumber = dataRow["phone_no"].ToString();
                    user.BranchId = Convert.ToInt32(dataRow["branch_id"].ToString());
                    user.RoleId = Convert.ToInt32(dataRow["role_id"].ToString());

                    users.Add(user);
                }

                return users;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 2016/03/30
        /// 
        /// Insert Dealser User details
        /// 
        /// argument : dealer user (DealerUserModel)
        /// 
        ///         
        /// </summary>
        /// <returns>1</returns>
        public int InsertDealerUser(DealerUserModel dealerUser)
        {
            // if (this.InsertUser(dealerUser) == 1)
            //{                
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_Id", dealerUser.UserId });
            paramertList.Add(new object[] { "@user_name", dealerUser.UserName });
            paramertList.Add(new object[] { "@password", dealerUser.Password });
            paramertList.Add(new object[] { "@first_name", dealerUser.FirstName });
            paramertList.Add(new object[] { "@last_name", dealerUser.LastName });
            paramertList.Add(new object[] { "@email", dealerUser.Email });
            paramertList.Add(new object[] { "@phone_no", dealerUser.PhoneNumber });
            paramertList.Add(new object[] { "@status", dealerUser.Status });
            paramertList.Add(new object[] { "@is_delete", dealerUser.IsDelete });
            paramertList.Add(new object[] { "@created_by", dealerUser.CreatedBy });
            paramertList.Add(new object[] { "@create_Date", DateTime.Now });
            //paramertList.Add(new object[] { "@branch_id", dealerUser.BranchId });
            paramertList.Add(new object[] { "@role_id", dealerUser.RoleId });
            paramertList.Add(new object[] { "@Company_id", dealerUser.Company_Id });
            paramertList.Add(new object[] { "@request_user_id", dealerUser.UserIdForSendReq });
            paramertList.Add(new object[] { "@request_email", dealerUser.UserEmailForSendReq });
            //paramertList.Add(new object[] { "@dealer_company_id ", dealerUser.NonRegCompanyId });
            paramertList.Add(new object[] { "@dealer_branch_id", dealerUser.NonRegBranchId });
            paramertList.Add(new object[] { "@loan_id", dealerUser.LoanId });
            paramertList.Add(new object[] { "@activation_code", dealerUser.ActivationCode });

            try
            {
                return dataHandler.ExecuteSQLWithIntOutPutParam("spInsertDealerUser", paramertList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetStepStatusByUserBranchId(int branchId)
        {
            int step = -1;
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@branch_id", branchId });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetStepStatusByUserBranchId", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    DataRow dataRow = dataSet.Tables[0].Rows[0];
                    step = int.Parse(dataRow["step_status"].ToString());
                    return step;
                }
                else
                {
                    return -1;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy  : kasun
        /// CreatedDate: 2016/04/04
        /// 
        /// Update user right details in user permission table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int updateUserRightDetails(User user, int loggedUser)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_Id", user.UserId });
            paramertList.Add(new object[] { "@logged_user_Id", loggedUser });
            paramertList.Add(new object[] { "@loan_id", user.LoanId });
            paramertList.Add(new object[] { "@right_id", user.UserRights });
            paramertList.Add(new object[] { "@modify_date", DateTime.Now });

            try
            {
                return dataHandler.ExecuteSQLReturn("spUpdateUserRightDetails", paramertList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:4/22/2016
        /// Get all types of users for company
        /// </summary>
        /// <param name="company_Id"></param>
        /// <returns></returns>
        public List<User> GetAllUsersByBranchId(int role_id, int branch_Id)
        {
            List<User> users = new List<User>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@role_id", role_id });
            paramertList.Add(new object[] { "@branch_id", branch_Id });
            DataSet dataSet = dataHandler.GetDataSet("spGetAllUsersByBranchId", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    User user = new User();
                    user.UserId = Convert.ToInt32(dataRow["user_id"].ToString());
                    user.UserName = dataRow["user_name"].ToString();
                    user.Password = dataRow["password"].ToString();
                    user.FirstName = dataRow["first_name"].ToString();
                    user.LastName = dataRow["last_name"].ToString();
                    user.Email = dataRow["email"].ToString();
                    user.PhoneNumber = dataRow["phone_no"].ToString();
                    user.Status = bool.Parse(dataRow["status"].ToString());
                    user.BranchId = Convert.ToInt32(dataRow["branch_id"].ToString());
                    user.RoleId = Convert.ToInt32(dataRow["role_id"].ToString());

                    users.Add(user);
                }

                return users;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:4/22/2016
        /// Get super admins
        /// </summary>
        /// <param name="company_Id"></param>
        /// <returns></returns>
        public List<User> GetSuperAdminsByCompanyId(int company_Id)
        {
            List<User> users = new List<User>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_id", company_Id });

            DataSet dataSet = dataHandler.GetDataSet("spGetSuperAdminsByCompanyId", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    User user = new User();
                    user.UserId = Convert.ToInt32(dataRow["user_id"].ToString());
                    user.UserName = dataRow["user_name"].ToString();
                    user.Password = dataRow["password"].ToString();
                    user.FirstName = dataRow["first_name"].ToString();
                    user.LastName = dataRow["last_name"].ToString();
                    user.Email = dataRow["email"].ToString();
                    user.Status = bool.Parse(dataRow["status"].ToString());
                    user.PhoneNumber = dataRow["phone_no"].ToString();
                    user.BranchId = Convert.ToInt32(dataRow["branch_id"].ToString());
                    user.RoleId = Convert.ToInt32(dataRow["role_id"].ToString());

                    users.Add(user);
                }

                return users;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:4/23/2016
        /// edit user at dashboard
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int UpdateUser(User user, int userId)
        {
            if (user != null)
            {
                DataHandler dataHandler = new DataHandler();
                List<object[]> paramertList = new List<object[]>();


                paramertList.Add(new object[] { "@user_id", user.UserId });
                paramertList.Add(new object[] { "@current_password", user.CurrentPassword });
                paramertList.Add(new object[] { "@password", user.Password });
                paramertList.Add(new object[] { "@first_name", user.FirstName });
                paramertList.Add(new object[] { "@last_name", user.LastName });
                paramertList.Add(new object[] { "@email", user.Email });
                if (user.RoleId == 1)
                {
                    paramertList.Add(new object[] { "@phone_no", user.PhoneNumber });
                }
                else
                {
                    paramertList.Add(new object[] { "@phone_no", user.PhoneNumber2 });
                }

                if (user.Status)
                {
                    paramertList.Add(new object[] { "@status", 1 });
                }
                else {
                    paramertList.Add(new object[] { "@status", 0 });
                }

                paramertList.Add(new object[] { "@modified_date", DateTime.Now });
                paramertList.Add(new object[] { "@modified_by", userId });

                try
                {
                    return dataHandler.ExecuteSQLReturn("spUpdateUser", paramertList);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}