using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class StateDAL:DBConnection
    {
        protected bool Insert(StateBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_StateMaster_Insert";
                command.Parameters.AddWithValue("@State_Name", objbo.State_Name);
                command.Parameters.AddWithValue("@Country_id", objbo.Country_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objbo.IsExist > 0) return false; else return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Update(StateBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_StateMaster_Update";
                command.Parameters.AddWithValue("@Country_id", objbo.Country_id);
                command.Parameters.AddWithValue("@State_id", objbo.State_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@State_Name", objbo.State_Name);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objbo.IsExist > 0) return false; else return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(StateBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_StateMaster_Delete";
                command.Parameters.AddWithValue("@State_id", objbo.State_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(StateBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_StateMaster_Select";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@State_id", objbo.State_id);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetCountryLanguageWise(StateBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetCountryLanguageWise";
                command.Parameters.AddWithValue("@Language_id", objbo.LanguageId);
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
