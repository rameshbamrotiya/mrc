using BO;
using System;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
   public class FloorDirectoryDAL : DBConnection
    {
        protected bool InsertFloorDirectory(FloorDirectoryBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertFloorDetails";
                command.Parameters.AddWithValue("@FloorDirectoryId", 0);
                command.Parameters.AddWithValue("@FloorID", objbo.FloorId);
                command.Parameters.AddWithValue("@BlockID", objbo.BlockID);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Is_active", objbo.Enabled);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@CellValue", objbo.CellValue);
                command.Parameters.AddWithValue("@ToolTip", objbo.ToolTip);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;

                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet GetFloorDetailsById(FloorDirectoryBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetFloorDetails";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@FloorDirectoryId", objbo.FloorDirectoryId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool UpdateSchemeRecord(FloorDirectoryBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateFloorDirectoryDetails";
                command.Parameters.AddWithValue("@FloorDirectoryId", objbo.FloorDirectoryId);
                command.Parameters.AddWithValue("@FloorId", objbo.FloorId);
                command.Parameters.AddWithValue("@BlockID", objbo.BlockID);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Is_active", objbo.Enabled);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@CellValue", objbo.CellValue);
                command.Parameters.AddWithValue("@ToolTip", objbo.ToolTip);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool Delete(FloorDirectoryBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FloorDirectoryMaster_Delete";
                command.Parameters.AddWithValue("@FloorDirectoryId", objbo.FloorDirectoryId);
                ExecuteNonQuery(command);
                //ret = true;

                //SqlCommand command = new SqlCommand();
                //CreateParameters(objbo, ref command, "PROC_SchemeMaster_Delete");
                //ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
