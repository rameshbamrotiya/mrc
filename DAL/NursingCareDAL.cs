using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class NursingCareDAL : DBConnection
    {
        protected bool InsertNursingCare(NursingCareBO objbo, DataTable dt)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_NursingCareMaster_Insert";
                command.Parameters.AddWithValue("@NursingCare_id", objbo.NursingCare_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@NursingCareDetail_id", objbo.NursingCareDetail_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@MainImgpath", objbo.MainImgpath);
                command.Parameters.AddWithValue("@dtNursing", dt);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool UpdateNursingCare(NursingCareBO objbo, DataTable dt)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_NursingCareMaster_Update";
                command.Parameters.AddWithValue("@NursingCare_id", objbo.NursingCare_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@NursingCareDetail_id", objbo.NursingCareDetail_id);
                command.Parameters.AddWithValue("@dtNursing", dt);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet GetAllNursingCareDetails()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetAllNursingCareDetails";                
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetNursingCareDetailsByID(NursingCareBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_NursingCareMaster_Select";
                //command.Parameters.AddWithValue("@NursingCare_id", objbo.NursingCare_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);                
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(NursingCareBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_NursingCareMaster_Delete";
                command.Parameters.AddWithValue("@NursingCare_id", objbo.NursingCare_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetImageGridData(int Id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_Get_NursingCareImageMaster_ByID";
                command.Parameters.AddWithValue("@NursingCareDetail_id", Id);
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
