using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class SubscribeNewsletterMasterDAL : DBConnection
    {
        protected bool Insert(SubscribeNewsletterMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_SubscribeNewsletterMaster_Insert";
                command.Parameters.AddWithValue("@FullName", objbo.FullName);
                command.Parameters.AddWithValue("@EmailId", objbo.EmailId);
                command.Parameters.AddWithValue("@MobileNo", objbo.MobileNo);
                command.Parameters.AddWithValue("@Location", objbo.Location);
                string result = ExecuteScalar(command).ToString();
                if (result == "1")
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectAll()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetAllSubscribeNewsletterMaster";                
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(SubscribeNewsletterMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetSubscribeNewsletterMasterByDate";
                command.Parameters.AddWithValue("@DateFrom", objbo.DateFrom);
                command.Parameters.AddWithValue("@DateTo", objbo.DateTo);
                command.Parameters.AddWithValue("@FullName", objbo.FullName);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
