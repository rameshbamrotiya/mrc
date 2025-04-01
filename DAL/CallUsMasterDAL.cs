using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class CallUsMasterDAL : DBConnection
    {
        protected bool Insert(CallUsMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CallUsMaster_Insert";
                command.Parameters.AddWithValue("@CallUsType_id", objbo.CallUsType_id);
                command.Parameters.AddWithValue("@Title", objbo.Title);
                command.Parameters.AddWithValue("@CallUsLevelid", objbo.CallUsLevelid);
                command.Parameters.AddWithValue("@Time", objbo.Time);
                command.Parameters.AddWithValue("@Number", objbo.Number);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Update(CallUsMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CallUsMaster_Update";
                command.Parameters.AddWithValue("@CallUs_id", objbo.CallUs_id);
                command.Parameters.AddWithValue("@CallUsType_id", objbo.CallUsType_id);
                command.Parameters.AddWithValue("@Title", objbo.Title);
                command.Parameters.AddWithValue("@CallUsLevelid", objbo.CallUsLevelid);
                command.Parameters.AddWithValue("@Time", objbo.Time);
                command.Parameters.AddWithValue("@Number", objbo.Number);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(CallUsMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CallUsMaster_Delete";
                command.Parameters.AddWithValue("@CallUs_id", objbo.CallUs_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(CallUsMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CallUsMaster_Select";
                command.Parameters.AddWithValue("@CallUs_id", objbo.CallUs_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool UpdateOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_CallUsMasterOrder_Update";
                command.Parameters.Add("@cmd", SqlDbType.NVarChar).Value = cmd;
                command.Parameters.Add("@currentColMenuLevel", SqlDbType.Decimal).Value = decimal.Parse(col_menu_level, CultureInfo.InvariantCulture);
                command.Parameters.Add("@CallUsLevelid", SqlDbType.Int).Value = int.Parse(col_parent_id, CultureInfo.InvariantCulture);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet SelectSequenceNo()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Get_SequenceNo_CallUsMasterDetails";
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectCallUsType(CallUsMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllCallUsType";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet GetCallUsTypeByLanguageId(CallUsMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetCallUsTypeByLanguageId";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetCallUsListById(CallUsMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetCallUsMasterList";
                command.Parameters.AddWithValue("@CallUsType_id", objbo.CallUs_id);
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
