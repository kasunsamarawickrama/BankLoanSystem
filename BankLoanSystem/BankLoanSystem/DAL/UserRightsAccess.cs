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

        public List<Right> getRights(int userId)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spRightsByUserId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        List<Right> RightsLists = new List<Right>();

                        while (reader.Read())
                        {
                            Right right = new Right();
                            right.rightId = int.Parse(reader["id"].ToString());
                            right.active = true;
                            right.description = reader["description"].ToString();

                            RightsLists.Add(right);

                        }
                        return RightsLists;

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