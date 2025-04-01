using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class AccommodationDAL:DBConnection
    {
        protected bool Insert(AccommodationBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AccommodationMaster_Insert";
                command.Parameters.AddWithValue("@Name", objbo.Name);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@Doc_path", objbo.Doc_path);
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
        protected bool Update(AccommodationBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AccommodationMaster_Update";
                command.Parameters.AddWithValue("@Acc_id", objbo.Acc_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Name", objbo.Name);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@Doc_path", objbo.Doc_path);
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
        protected bool Delete(AccommodationBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AccommodationMaster_Delete";
                command.Parameters.AddWithValue("@Acc_id", objbo.Acc_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(AccommodationBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AccommodationMaster_Select";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Acc_id", objbo.Acc_id);
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
