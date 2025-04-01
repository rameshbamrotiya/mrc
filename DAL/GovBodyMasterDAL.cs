using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class GovBodyMasterDAL:DBConnection
    {
        protected bool Insert(GovBodyMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GovBodyMaster_Insert";
                command.Parameters.AddWithValue("@Gov_Name", objbo.Gov_Name);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Gov_desc", objbo.Gov_desc);
                command.Parameters.AddWithValue("@Gov_Path", objbo.Gov_Path);
                command.Parameters.AddWithValue("@Is_active_Video", objbo.Is_active_Video);
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
        protected bool Update(GovBodyMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GovBodyDetailsMaster_Update";
                command.Parameters.AddWithValue("@Gov_id", objbo.Gov_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Gov_Name", objbo.Gov_Name);
                command.Parameters.AddWithValue("@Gov_desc", objbo.Gov_desc);
                command.Parameters.AddWithValue("@Gov_Path", objbo.Gov_Path);
                command.Parameters.AddWithValue("@Is_active_Video", objbo.Is_active_Video);
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
        protected bool Delete(GovBodyMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_GovBodyMaster_Delete");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(GovBodyMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_GovBodyMaster_Select");
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectVideoDetails(GovBodyMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                //SqlCommand command = new SqlCommand();
                //CreateParameters(objbo, ref command, "PROC_AlbumMaster_Select");
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_Gov_BOdy_Select";
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
    }
}
