using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BankLoanSystem.Models;

namespace BankLoanSystem.DAL
{
    public class UserRightsAccess
    {
        /// <summary>
        /// CreatedBy : Kasun Samarawickrama
        /// CreatedDate: 2016/01/16
        /// 
        /// Get all rights in database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Right list</returns>
        /// 
        public List<Right> getRights()
        {
            List<Right> RightsLists = new List<Right>();
            DataHandler dataHandler = new DataHandler();
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetRights");
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        Right right = new Right();
                        right.rightId = dataRow["right_id"].ToString();
                        right.active = false;
                        right.description = dataRow["description"].ToString();

                        RightsLists.Add(right);
                    }

                    return RightsLists;
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
        /// CreatedBy : Kasun Samarawickrama
        /// CreatedDate: 2016/01/17
        /// 
        /// Get user permission permission string which contain rightId's
        /// </summary>
        /// <param name="userId"> Profile edit users id</param>
        /// <returns>Right List,  but first Right contain the string, If List have more than 1 value it is going to be an unAuthorize one</returns>
        /// 
        public List<Right> getRightsString(int userId, int loanId)
        {
            List<Right> RightsLists = new List<Right>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@userId", userId });
            paramertList.Add(new object[] { "@loanId", loanId });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetRightsStringByUserId", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        Right right = new Right();
                        right.userId = int.Parse(dataRow["user_id"].ToString());
                        right.rightsPermissionString = dataRow["right_id"].ToString();
                        right.reportRightsPermissionString = dataRow["report_rights"].ToString();
                        RightsLists.Add(right);
                    }

                    return RightsLists;
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
        /// CreatedBy : Kasun Samarawickrama
        /// CreatedDate: 2016/01/17
        /// 
        /// Rew Right string Updating
        /// </summary>
        /// <param name="returnRight"></param>
        /// <param name="writerId"></param>
        /// <returns>boolian value</returns>
        /// 
        public bool postNewRights(Right returnRight)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@editor_id", returnRight.userId });
            paramertList.Add(new object[] { "@permission", returnRight.rightsPermissionString });
            paramertList.Add(new object[] { "@modified_user", returnRight.editorId });
            paramertList.Add(new object[] { "@DateNow", DateTime.Now });            

            try
            {
                return dataHandler.ExecuteSQL("spUpdateRightsStringByUserId", paramertList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Frontend Page:Bottom Link Bar of each page in floor plan management section
        /// Title: return user rights for given loan code and user id
        /// Designed: Irfan MAM
        /// User Story:
        /// Developed: Piyumi P
        /// Date created:
        /// </summary>
        /// <param name="loanCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Right> GetUserRightsByLoanCode(string loanCode,int userId) 
        {
            List<Right> resltList = new List<Right>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", userId });
            paramertList.Add(new object[] { "@loan_code", loanCode });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetUserRightsByLoanCode", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        Right right = new Right();
                        right.userId = int.Parse(dataRow["user_id"].ToString());
                        right.rightsPermissionString = dataRow["right_id"].ToString();
                        
                        resltList.Add(right);
                    }

                    return resltList;
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
        /// CreatedDate:6/22/2016
        /// Get all report details
        /// </summary>
        /// <returns></returns>
        public List<Right> getReportRights()
        {
            List<Right> RightsLists = new List<Right>();
            DataHandler dataHandler = new DataHandler();
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetReportRights");
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        Right right = new Right();
                        right.rightId = dataRow["report_id"].ToString();
                        right.active = false;
                        right.description = dataRow["report_description"].ToString();
                        right.name = dataRow["report_name"].ToString();
                        right.ImagePath = dataRow["image_path"].ToString();
                        right.DealerView = bool.Parse(dataRow["dealer_can_view"].ToString());
                        RightsLists.Add(right);
                    }

                    return RightsLists;
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