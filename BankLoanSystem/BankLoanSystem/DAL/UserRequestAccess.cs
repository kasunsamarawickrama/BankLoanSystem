using BankLoanSystem.Models;
using System.Collections.Generic;

namespace BankLoanSystem.DAL
{
    /// <summary>
    /// CreateBy : Asanka Senarathna
    /// CreatedDate: 2016/03/30
    /// Manage User Request messages in daily pages (dashboard page)
    /// call DataHandler class to save user request
    /// </summary>

    public class UserRequestAccess
    {
        /// <summary>
        /// Created by : Asanka Senarathna
        /// Created date :2016/03/30
        /// Insert User request message
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        public int InsertUserRequest(UserRequest userRequest)
        {
            try
            {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_id", userRequest.company_id });
            paramertList.Add(new object[] { "@branch_id", userRequest.branch_id });
            paramertList.Add(new object[] { "@user_id", userRequest.user_id });
            paramertList.Add(new object[] { "@page_name", userRequest.page_name });
            paramertList.Add(new object[] { "@topic", userRequest.topic });
            paramertList.Add(new object[] { "@message", userRequest.message });
            paramertList.Add(new object[] { "@priority_level", userRequest.priority_level });

            return dataHandler.ExecuteSQL("spInsertUserRequest", paramertList) ? 1 : 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Created by :Asanka Senarathna
        /// Createddate : 2016/03/30
        /// Update User Request (Add Answer to User Question)
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        public int UpdateUserRequestAnswer(UserRequest userRequest)
        {
            try
            {
                DataHandler dataHandler = new DataHandler();
                List<object[]> paramertList = new List<object[]>();
                paramertList.Add(new object[] { "@request_id", userRequest.request_id });
                paramertList.Add(new object[] { "@answer", userRequest.answer });
                paramertList.Add(new object[] { "@answer_user_id", userRequest.answer_user_id });
               
                return dataHandler.ExecuteSQL("spUpdateUserRequestAnswer", paramertList) ? 1 : 0;
            }
            catch
            {
                return 0;
            }
        }


    }
}