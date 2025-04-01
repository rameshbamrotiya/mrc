
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace ClassLib.DAL
{

    public class SqlHelper
    {
        #region Variable Declaration
        private SqlConnection connection;
        private SqlTransaction trans;
        
        #endregion
        /// <summary>
        ///  Open Connection of database
        /// </summary>
        /// <returns></returns>
        protected SqlConnection OpenConnection()
        {
            try
            {
				string connectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception)
            {
                throw;
            }
            return connection;
        }
        /// <summary>
        /// Close the connection to the database
        /// </summary>
        protected void CloseConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        ///  Executes SQL Command directly on the database.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>DataSet / DataTable</returns>
        protected DataSet ExecuteQueryDataSet(SqlCommand command)
        {
            try
            {
                DataSet ds = new DataSet();
                command.Connection = OpenConnection();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        ///  Executes SQL Command directly on the database.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>DataTable</returns>
        protected DataTable ExecuteQueryDataTable(SqlCommand command)
        {
            try
            {
                DataTable dt = new DataTable();
                command.Connection = OpenConnection();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        /// Executes the query, and returns the number of row affected.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected int ExecuteNonQuery(SqlCommand command)
        {
            try
            {
                command.Connection = OpenConnection();
                return command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        /// Executes SQL Command directly on the database and returns objects
        /// </summary>
        /// <param name="strQuery">SQL Query</param>
        /// <param name="str_table">The name of the source table to use for table mapping</param>
        /// <returns>DataSet</returns>
        protected DataSet ExecuteQuery(string strQuery, string str_table)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(strQuery, OpenConnection());
                adapter.Fill(ds, str_table);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        ///  Executes SQL Command directly on the database and returns objects
        /// </summary>
        /// <param name="strQuery">SQL Query</param>
        /// <returns>DataSet</returns>
        protected DataSet ExecuteQuery(string strQuery)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(strQuery, OpenConnection());
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        ///  Executes SQL Command directly on the database.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="str_table"></param>
        /// <returns>DataSet</returns>
        protected DataSet ExecuteQuery(SqlCommand command, string str_table)
        {
            try
            {
                DataSet ds = new DataSet();
                command.Connection = OpenConnection();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds, str_table);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        /// Start a databse connection with the specified isolation and transaction name
        /// </summary>
        protected void BeginTransaction()
        {
            try
            {
                connection = OpenConnection();
                trans = connection.BeginTransaction();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Roll back a transaction from pending state
        /// </summary>
        protected void RollBackTransaction()
        {
            try
            {
                trans.Rollback();
                CloseConnection();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Commit database transaction
        /// </summary>
        protected void CommitTransaction()
        {
            try
            {
                trans.Commit();
                CloseConnection();
            }
            catch (Exception)
            {
                RollBackTransaction();
                throw;
            }

        }
        /// <summary>
        /// Executes SQL Command and return DataSet
        /// </summary>
        /// <param name="command"></param>
        /// <returns>DataSet</returns>
        protected DataSet ExecuteQueryTransaction(SqlCommand command)
        {
            try
            {
                DataSet ds = new DataSet();
                command.Connection = connection;
                command.Transaction = trans;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// Executes SQL Command, and returns the no of row affected.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected int ExecuteNonQueryTransaction(SqlCommand command)
        {
            try
            {
                command.Connection = connection;
                command.Transaction = trans;
                return command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        ///  Executes SQL Command, and returns the no of row affected.
        /// </summary>
        /// <param name="strQuery"></param>
        /// <returns></returns>
        protected int ExecuteNonQueryTransaction(string strQuery)
        {
            try
            {
                SqlCommand command = new SqlCommand(strQuery, connection, trans);
                return command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        ///   Executes SQL Command directly, and returns a single value (from the first column of the first row). 
        /// </summary>
        /// <param name="strQuery"></param>
        /// <returns></returns>
        protected object ExecuteScalarTransaction(string strQuery)
        {
            try
            {
                SqlCommand command = new SqlCommand(strQuery, connection, trans);
                return command.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        ///  Executes SQL Command directly, and returns a single value (from the first column of the first row). 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected object ExecuteScalarTransaction(SqlCommand command)
        {
            try
            {
                command.Connection = connection;
                command.Transaction = trans;
                return command.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// Executes the query, and returns a single value (from the first column of the first row). 
        /// </summary>
        /// <param name="strQuery"></param>
        /// <returns></returns>
        protected object ExecuteScalar(string strQuery)
        {
            try
            {
                SqlCommand command = new SqlCommand(strQuery, OpenConnection());
                return command.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        /// Executes the query, and returns a single value (from the first column of the first row). 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected object ExecuteScalar(SqlCommand command)
        {
            try
            {
                command.Connection = OpenConnection();
                return command.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        /// Executes the query, and returns the number of row affected.
        /// </summary>
        /// <param name="strQuery"></param>
        /// <returns></returns>
        protected int ExecuteNonQuery(string strQuery)
        {
            try
            {
                SqlCommand command = new SqlCommand(strQuery, OpenConnection());
                return command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        ///  Create and set store procedure parameter and value
        /// </summary>
        /// <param name="BO">Business Object</param>
        /// <param name="command">Sql Command</param>
        /// <param name="strprcname">Store Procedure Name</param>
        protected void CreateParameters<T>(T bo, ref SqlCommand command, string strprcname)
        {
            try
            {
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = strprcname;
                Type t = bo.GetType();
                PropertyInfo[] PInfo = t.GetProperties();
                for (int i = 0; i < PInfo.Length; i++)
                {
                    if (PInfo[i].GetValue(bo, null) != null && PInfo[i].GetValue(bo, null).ToString() != string.Empty)
                        command.Parameters.AddWithValue('@' + PInfo[i].Name, PInfo[i].GetValue(bo, null));
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
