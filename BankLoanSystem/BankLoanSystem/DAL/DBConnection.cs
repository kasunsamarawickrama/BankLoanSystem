using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace BankLoanSystem.DAL
{
    /// <summary>
    /// CreatedBy : Kasun Smarawickrama
    /// CreatedDate: 2016/01/14
    /// 
    /// DB connection Initialize
    /// 
    /// UpdatedBy : Nadeeka
    /// UpdatedDate : 2016/03/03
    /// Implement ConnectDB and DisconnectDB methods
    /// </summary>
    public class DBConnection : IDisposable
    {
        public SqlConnection connection;

        public DBConnection()
        {

            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString);
        }

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate : 2016/03/03
        /// Open database connection
        /// </summary>
        /// <returns></returns>
        public bool ConnectDB()
        {
            try
            {
                if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate : 2016/03/03
        /// Close database connection
        /// </summary>
        /// <returns></returns>
        public void DisconnectDB()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            catch (Exception exp)
            {
                throw exp;

            }
        }

        public void Dispose()
        {
            connection.Close();
        }
    }

    /// <summary>
    /// CreatedBy :Nadeeka
    /// CreatedDate :2016/03/03
    /// 
    /// Implemented common data hander functions such as insert/update/retrive
    /// </summary>
    public class DataHandler
    {
        private Connection connection = new Connection();
        private SqlDataAdapter dataAdapter;
        private SqlCommand command;
        private DataSet dataSet = new DataSet();

        /// <summary>
        /// CreatedBy :Nadeeka
        /// CreatedDate :2016/03/03
        /// 
        /// Open database connection
        /// add object list to parameters collection in command object
        /// execute given stored procedure
        /// return boolean value
        /// </summary>
        /// <param name="SQL">stored procedure name</param>
        /// <param name="mPara">parameter list</param>
        /// <returns></returns>
        public bool ExecuteSQL(string SQL, List<object[]> mPara)
        {
            try
            {
                connection.DisconnectDB();
                connection.ConnectDB();
                if (connection.ConnectDB() == true)
                {
                    command = new SqlCommand(SQL, connection.m_Connection);
                    command.CommandText = SQL;
                    command.CommandType = CommandType.StoredProcedure;

                    if (mPara != null)
                    {
                        foreach (object[] Parameters in mPara)
                        {
                            command.Parameters.AddWithValue(Parameters[0].ToString(), Parameters[1]);
                        }
                    }
                    command.ExecuteNonQuery();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception exp)
            {
                return false;
            }

        }
        /// <summary>
        /// CreatedBy :Nadeeka
        /// CreatedDate :2016/03/03
        /// 
        /// Open database connection
        /// add object list to parameters collection in command object
        /// execute given stored procedure
        /// return boolean value
        /// </summary>
        /// <param name="SQL">stored procedure name</param>
        /// <param name="mPara">parameter list</param>
        /// <returns></returns>
        public int ExecuteSQLReturn(string SQL, List<object[]> mPara)
        {
            try
            {
                connection.DisconnectDB();
                connection.ConnectDB();
                if (connection.ConnectDB() == true)
                {
                    command = new SqlCommand(SQL, connection.m_Connection);
                    command.CommandText = SQL;
                    command.CommandType = CommandType.StoredProcedure;

                    if (mPara != null)
                    {
                        foreach (object[] Parameters in mPara)
                        {
                            command.Parameters.AddWithValue(Parameters[0].ToString(), Parameters[1]);
                        }

                    }
                    SqlParameter returnParameter = command.Parameters.Add("@ReturnValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    command.ExecuteNonQuery();

                    return int.Parse(returnParameter.Value.ToString());
                }
                else
                    return 0;
            }
            catch (Exception exp)
            {
                throw exp;
                return 0;
            }

        }
        /// <summary>
        /// <summary>
        /// CreatedBy :Nadeeka
        /// CreatedDate :2016/03/03
        /// 
        /// Open database connection
        /// add object list to parameters collection in command object
        /// execute given stored procedure
        /// return dataset 
        /// </summary>
        /// <param name="SQL">stored procedure name</param>
        /// <param name="mPara">parameter list</param>
        /// </summary>        
        /// <returns>dataset object</returns>
        public DataSet GetDataSet(string SQL, List<object[]> mPara)
        {
            try
            {
                connection.DisconnectDB();
                connection.ConnectDB();
                if (connection.ConnectDB() == true)
                {
                    command = new SqlCommand(SQL, connection.m_Connection);
                    command.CommandType = CommandType.StoredProcedure;

                    if (mPara != null)
                    {
                        foreach (object[] Parameters in mPara)
                        {
                            command.Parameters.AddWithValue(Parameters[0].ToString(), Parameters[1]);
                        }
                    }

                    dataAdapter = new SqlDataAdapter(command);
                    dataSet.Clear();
                    dataAdapter.Fill(dataSet);
                    dataAdapter.Dispose();
                    mPara = null;
                    return dataSet;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// CreatedBy :Nadeeka
        /// CreatedDate :2016/03/04
        /// 
        /// Open database connection
        /// add object list to parameters collection in command object
        /// execute given stored procedure
        /// return dataset 
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public DataSet GetDataSetBySQL(string SQL)
        {
            try
            {
                connection.DisconnectDB();
                connection.ConnectDB();
                if (connection.ConnectDB() == true)
                {
                    command = new SqlCommand(SQL, connection.m_Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    dataAdapter = new SqlDataAdapter(command);
                    dataSet.Clear();
                    dataAdapter.Fill(dataSet);
                    dataAdapter.Dispose();
                    return dataSet;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// CreatedBy :Nadeeka
        /// CreatedDate :2016/03/03
        /// 
        /// Open database connection
        /// add object list to parameters collection in command object
        /// execute given stored procedure
        /// return dataset 
        /// </summary>
        /// <param name="SQL">stored procedure name</param>
        /// <returns>dataset object</returns>
        public DataSet GetDataSet(string SQL)
        {
            try
            {
                connection.DisconnectDB();
                connection.ConnectDB();
                if (connection.ConnectDB() == true)
                {
                    command = new SqlCommand(SQL, connection.m_Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    dataAdapter = new SqlDataAdapter(command);
                    dataSet.Clear();
                    dataAdapter.Fill(dataSet);
                    dataAdapter.Dispose();
                    return dataSet;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// <summary>
        /// CreatedBy :Nadeeka
        /// CreatedDate :2016/03/03
        /// 
        /// Open database connection
        /// add object list to parameters collection in command object
        /// execute given stored procedure
        /// return true if record exist, otherwise false 
        /// </summary>
        /// <param name="SQL">stored procedure name</param>
        /// <param name="mPara">parameter list</param>
        /// </summary>        
        /// <returns>boolean value</returns>
        public bool GetDataExistance(string SQL, List<object[]> mPara)
        {
            try
            {
                connection.DisconnectDB();
                connection.ConnectDB();
                if (connection.ConnectDB() == true)
                {
                    command = new SqlCommand(SQL, connection.m_Connection);
                    command.CommandType = CommandType.StoredProcedure;

                    if (mPara != null)
                    {
                        foreach (object[] Parameters in mPara)
                        {
                            command.Parameters.AddWithValue(Parameters[0].ToString(), Parameters[1]);
                        }
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        return reader.HasRows ? false : true;
                    }
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// CreatedBy :Nadeeka
        /// CreatedDate :2016/03/03
        /// 
        /// Open database connection
        /// add object list to parameters collection in command object
        /// execute given stored procedure
        /// return boolean value
        /// </summary>
        /// <param name="SQL">stored procedure name</param>
        /// <param name="mPara">parameter list</param>
        /// <returns>return int value</returns>
        public int ExecuteSQLWithReturnVal(string SQL, List<object[]> mPara)
        {
            try
            {
                connection.DisconnectDB();
                connection.ConnectDB();
                if (connection.ConnectDB() == true)
                {
                    command = new SqlCommand(SQL, connection.m_Connection);
                    command.CommandText = SQL;
                    command.CommandType = CommandType.StoredProcedure;

                    if (mPara != null)
                    {
                        foreach (object[] Parameters in mPara)
                        {
                            command.Parameters.AddWithValue(Parameters[0].ToString(), Parameters[1]);
                        }
                    }
                    SqlParameter returnParameter = command.Parameters.Add("@return", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    command.ExecuteNonQuery();
                    int retunVal = int.Parse(returnParameter.Value.ToString());
                    return retunVal;
                }
                else
                    return 0;
            }
            catch (Exception exp)
            {
                return 0;
            }

        }

        public string ExecuteSQLWithStringReturnVal(string SQL, List<object[]> mPara)
        {
            try
            {
                connection.DisconnectDB();
                connection.ConnectDB();
                if (connection.ConnectDB() == true)
                {
                    command = new SqlCommand(SQL, connection.m_Connection);
                    command.CommandText = SQL;
                    command.CommandType = CommandType.StoredProcedure;

                    if (mPara != null)
                    {
                        foreach (object[] Parameters in mPara)
                        {
                            command.Parameters.AddWithValue(Parameters[0].ToString(), Parameters[1]);
                        }
                    }
                    SqlParameter returnParameter = command.Parameters.Add("@return", SqlDbType.VarChar, 50);
                    returnParameter.Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();
                    return returnParameter.Value.ToString();

                }
                else
                    return null;
            }
            catch (Exception exp)
            {
                return null;
            }

        }

        public Int32 ExecuteSQLWithIntOutPutParam(string SQL, List<object[]> mPara)
        {
            try
            {
                connection.DisconnectDB();
                connection.ConnectDB();
                if (connection.ConnectDB() == true)
                {
                    command = new SqlCommand(SQL, connection.m_Connection);
                    command.CommandText = SQL;
                    command.CommandType = CommandType.StoredProcedure;

                    if (mPara != null)
                    {
                        foreach (object[] Parameters in mPara)
                        {
                            command.Parameters.AddWithValue(Parameters[0].ToString(), Parameters[1]);
                        }
                    }
                    SqlParameter returnParameter = command.Parameters.Add("@return", SqlDbType.Int, 50);
                    returnParameter.Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();
                    return Convert.ToInt32(returnParameter.Value.ToString());

                }
                else
                    return 0;
            }
            catch (Exception exp)
            {
                return 0;
            }

        }
    }
}