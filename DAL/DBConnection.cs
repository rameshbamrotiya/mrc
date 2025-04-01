
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace DAL
{

    public class DBConnection : Connection
    {
        public string SqlConnectionString
        {
            get
            {
                return base.ConnectionString;
            }
        }
    }

    public abstract class Connection
    {
        #region Variable Declaration
        private SqlConnection connection;
        private SqlTransaction trans;
        #endregion

        public Connection()
        {
            
        }
        /// <summary>
        /// Get connection string from application setting
        /// </summary>
        protected string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["UNMehtaConnectionString"].ConnectionString;
            }
        }
        /// <summary>
        ///  Open Connection of database
        /// </summary>
        /// <returns></returns>
        protected SqlConnection GetConnection()
        {
            try
            {              
                connection = new SqlConnection(ConnectionString);
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
        /// Start a databse connection with the specified isolation and transaction name
        /// </summary>
        protected void BeginTransaction()
        {
            try
            {
                connection = GetConnection();
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
                SqlDataAdapter adapter = new SqlDataAdapter(strQuery, GetConnection());
                adapter.Fill(ds, str_table);
                CloseConnection();
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
                SqlDataAdapter adapter = new SqlDataAdapter(strQuery, GetConnection());
                adapter.Fill(ds);
                CloseConnection();
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
        /// <returns>DataSet</returns>
        protected DataSet ExecuteQuery(SqlCommand command)
        {
            try
            {
                DataSet ds = new DataSet();
                command.Connection = GetConnection();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                CloseConnection();
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
                command.Connection = GetConnection();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds, str_table);
                CloseConnection();
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
        protected DataSet ExecuteQueryTransaction(SqlCommand command)
        {
            try
            {
                DataSet ds = new DataSet();


                command.Connection = connection;
                command.Transaction = trans;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                CloseConnection();
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
                int result=command.ExecuteNonQuery();
                CloseConnection();
                return result;
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
                int result = command.ExecuteNonQuery();
                CloseConnection();
                return result;
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
                int result = command.ExecuteNonQuery();
                CloseConnection();
                return result;
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
                int result = command.ExecuteNonQuery();
                CommitTransaction();
                return result;
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
                SqlCommand command = new SqlCommand(strQuery, GetConnection());
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
                command.Connection = GetConnection();
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
                SqlCommand command = new SqlCommand(strQuery, GetConnection());
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
        /// Executes the query, and returns the number of row affected.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected int ExecuteNonQuery(SqlCommand command)
        {
            try
            {
                command.Connection = GetConnection();
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
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
                    if (PInfo[i].GetValue(bo, null) != null )
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
        private static string Decrypt(string strText, string sDecrKey)
        {
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byte[] inputByteArray = new byte[strText.Length + 1];

            byKey = System.Text.Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        public static string DecryptText(string Text, string Key)
        {
            return Decrypt(Text, Key);
        }
    }

}
