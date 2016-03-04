using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using BankLoanSystem.Models;

namespace BankLoanSystem.DAL
{
    public class Connection
    {
        public SqlConnection m_Connection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString);

        public bool ConnectDB()
        {
            try
            {
                if (m_Connection.State == ConnectionState.Closed || m_Connection.State == ConnectionState.Broken)
                    m_Connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void DisconnectDB()
        {
            if (m_Connection.State == ConnectionState.Open)
                m_Connection.Close();
        }
    }

    //public class DataHandler
    //{
    //    private Connection mConn = new Connection();
    //    private SqlDataAdapter mDataAdapter;
    //    private SqlCommand mCommand;
    //    private DataSet mDataSet = new DataSet();

    //    public DataSet GetDataSet(string SQL, List<object[]> mPara)
    //    {
    //        try
    //        {
    //            mConn.DisconnectDB();
    //            mConn.ConnectDB();
    //            if (mConn.ConnectDB() == true)
    //            {
    //                mCommand = new SqlCommand(SQL, mConn.m_Connection);
    //                mCommand.CommandType = CommandType.StoredProcedure;

    //                if (mPara != null)
    //                {
    //                    foreach (object[] Parameters in mPara)
    //                    {
    //                        mCommand.Parameters.AddWithValue(Parameters[0].ToString(), Parameters[1]);
    //                    }
    //                }

    //                mDataAdapter = new SqlDataAdapter(mCommand);
    //                mDataSet.Clear();
    //                mDataAdapter.Fill(mDataSet);
    //                mDataAdapter.Dispose();
    //                mPara = null;
    //                return mDataSet;
    //            }
    //            else
    //                return null;
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }

    //    public DataSet GetDataSet(string SQL, int startingRecord, int pageSize, string tableName, List<object[]> mPara)
    //    {
    //        try
    //        {
    //            mConn.DisconnectDB();
    //            mConn.ConnectDB();
    //            if (mConn.ConnectDB() == true)
    //            {
    //                mCommand = new SqlCommand(SQL, mConn.m_Connection);
    //                mCommand.CommandType = CommandType.StoredProcedure;

    //                if (mPara != null)
    //                {
    //                    foreach (object[] Parameters in mPara)
    //                    {
    //                        mCommand.Parameters.AddWithValue(Parameters[0].ToString(), Parameters[1]);
    //                    }
    //                }

    //                mDataAdapter = new SqlDataAdapter(mCommand);
    //                mDataSet.Clear();
    //                mDataAdapter.Fill(mDataSet, startingRecord, pageSize, tableName);
    //                mDataAdapter.Dispose();
    //                mPara = null;
    //                return mDataSet;
    //            }
    //            else
    //                return null;
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }

    //    public DataSet GetDataSet(string SQL)
    //    {
    //        try
    //        {
    //            mConn.DisconnectDB();
    //            mConn.ConnectDB();
    //            if (mConn.ConnectDB() == true)
    //            {
    //                mCommand = new SqlCommand(SQL, mConn.m_Connection);
    //                mCommand.CommandType = CommandType.StoredProcedure;
    //                mDataAdapter = new SqlDataAdapter(mCommand);
    //                mDataSet.Clear();
    //                mDataAdapter.Fill(mDataSet);
    //                mDataAdapter.Dispose();
    //                return mDataSet;
    //            }
    //            else
    //                return null;
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }

    //    public bool ExecuteSQL(string SQL, List<object[]> mPara)
    //    {
    //        try
    //        {
    //            mConn.DisconnectDB();
    //            mConn.ConnectDB();
    //            if (mConn.ConnectDB() == true)
    //            {
    //                mCommand = new SqlCommand(SQL, mConn.m_Connection);
    //                mCommand.CommandText = SQL;
    //                mCommand.CommandType = CommandType.StoredProcedure;

    //                if (mPara != null)
    //                {
    //                    foreach (object[] Parameters in mPara)
    //                    {
    //                        mCommand.Parameters.AddWithValue(Parameters[0].ToString(), Parameters[1]);
    //                    }
    //                }
    //                mCommand.ExecuteNonQuery();
    //                return true;
    //            }
    //            else
    //                return false;
    //        }
    //        catch
    //        {
    //            return false;
    //        }

    //    }

    //    public SqlDataReader GetDataReader(string StrSp, List<object[]> mPara)
    //    {
    //        try
    //        {
    //            mConn.DisconnectDB();
    //            mConn.ConnectDB();

    //            if (mConn.ConnectDB())
    //            {
    //                mCommand = new SqlCommand(StrSp, mConn.m_Connection);
    //                mCommand.CommandType = CommandType.StoredProcedure;
    //                try
    //                {
    //                    if (mPara != null)
    //                    {
    //                        foreach (object[] Parameters in mPara)
    //                        {
    //                            mCommand.Parameters.AddWithValue(Parameters[0].ToString(), Parameters[1]);
    //                        }
    //                    }
    //                    return mCommand.ExecuteReader();
    //                }
    //                catch
    //                {
    //                    return null;
    //                }
    //            }
    //            return (null);
    //        }
    //        catch
    //        {
    //            return (null);
    //        }
    //    }

    //    public void InsertUser(User user)
    //    {
    //        List<object[]> mPara = new List<object[]>();
    //        mPara.Add(new object[] { "@user_Id", user.UserId });
    //        mPara.Add(new object[] { "@user_name", user.UserName });
    //        mPara.Add(new object[] { "@password", user.Password });
    //        mPara.Add(new object[] { "@first_name", user.FirstName });
    //        mPara.Add(new object[] { "@last_name", user.LastName });
    //        mPara.Add(new object[] { "@email", user.Email });
    //        mPara.Add(new object[] { "@phone_no", user.PhoneNumber });
    //        mPara.Add(new object[] { "@status", user.Status });
    //        mPara.Add(new object[] { "@is_delete", user.IsDelete });
    //        mPara.Add(new object[] { "@created_by", user.CreatedBy });
    //        mPara.Add(new object[] { "@create_Date", DateTime.Now });
    //        mPara.Add(new object[] { "@branch_id", user.BranchId });
    //        mPara.Add(new object[] { "@role_id", user.RoleId });
    //        mPara.Add(new object[] { "@Company_id", user.Company_Id });

    //        //try
    //        //{
    //        //    if (dh.ExecuteSQL("spInsertUser", mPara))
    //        //    {
    //        //        return 1;
    //        //    }
    //        //    else
    //        //    {
    //        //        return 0;
    //        //    }
    //        //}
    //        //catch
    //        //{
    //        //    return 0;
    //        //}
    //    }
    //}
}