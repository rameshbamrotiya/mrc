using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class FeedbackDAL : DBConnection
    {
        protected bool Insert(FeedbackBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FeedbackMaster_Insert";
                command.Parameters.AddWithValue("@FullName", objbo.FullName);
                command.Parameters.AddWithValue("@EmailId", objbo.EmailId);
                command.Parameters.AddWithValue("@VisitType", objbo.VisitType);
                command.Parameters.AddWithValue("@IsUnmicrc", objbo.unmericfeedback);
                command.Parameters.AddWithValue("@VisitNumber", objbo.VisitNumber);
                command.Parameters.AddWithValue("@MobileNo", objbo.MobileNo);
                command.Parameters.AddWithValue("@Country", objbo.Country);
                command.Parameters.AddWithValue("@State", objbo.State);
                command.Parameters.AddWithValue("@City", objbo.City);
                command.Parameters.AddWithValue("@FeedbackDescription", objbo.FeedbackDescription);
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
                command.CommandText = "PROC_GetAllFeedbackMaster";                
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(FeedbackBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetFeedbackMasterByDate";
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
