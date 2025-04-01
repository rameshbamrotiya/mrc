using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class CommitteeMasterDAL:DBConnection
    {
        protected bool Insert(CommitteeMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CommitteeMaster_Insert";
                command.Parameters.AddWithValue("@Committee_Desc", objbo.Committee_Desc);
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
        protected bool Update(CommitteeMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CommitteeMaster_Update";
                command.Parameters.AddWithValue("@Committee_id", objbo.Committee_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Committee_Desc", objbo.Committee_Desc);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                //command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                //objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                //if (objbo.IsExist > 0) return false; else return true;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(CommitteeMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CommitteeMaster_Delete";
                command.Parameters.AddWithValue("@Committee_id", objbo.Committee_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(CommitteeMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_CommitteeMaster_Select";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Committee_id", objbo.Committee_id);
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
