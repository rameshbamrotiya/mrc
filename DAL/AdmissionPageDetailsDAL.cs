using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class AdmissionPageDetailsDAL : DBConnection
    {
        protected bool Insert(AdmissionPageDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AdmissionPageMaster_Insert";
                command.Parameters.AddWithValue("@CourseName", objbo.CourseName);
                command.Parameters.AddWithValue("@YearOfAdmission", objbo.YearOfAdmission);
                command.Parameters.AddWithValue("@AdmissionFileName", objbo.AdmissionFileName);
                command.Parameters.AddWithValue("@AdmissionFilePath", objbo.AdmissionFilePath);
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@Admission_level_id", objbo.Admission_level_id);
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
        protected bool Update(AdmissionPageDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AdmissionPageMaster_Update";
                command.Parameters.AddWithValue("@AdmissionPageId", objbo.@AdmissionPageId);
                command.Parameters.AddWithValue("@CourseName", objbo.CourseName);
                command.Parameters.AddWithValue("@YearOfAdmission", objbo.YearOfAdmission);
                command.Parameters.AddWithValue("@AdmissionFileName", objbo.AdmissionFileName);
                command.Parameters.AddWithValue("@AdmissionFilePath", objbo.AdmissionFilePath);
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
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
        protected bool Delete(AdmissionPageDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AdmissionPageMaster_Delete";
                command.Parameters.AddWithValue("@AdmissionPageId", objbo.AdmissionPageId);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(AdmissionPageDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AdmissionPageMaster_Select";
                command.Parameters.AddWithValue("@AdmissionPageId", objbo.AdmissionPageId);
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectAdmissionData(int LanguageId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AdmissionPageMaster_Data";
                command.Parameters.AddWithValue("@LanguageId", LanguageId);
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
                command.CommandText = "PROC_AdmissionPageDetailsOrder_Update";
                command.Parameters.Add("@cmd", SqlDbType.NVarChar).Value = cmd;
                command.Parameters.Add("@currentColMenuLevel", SqlDbType.Decimal).Value = decimal.Parse(col_menu_level, CultureInfo.InvariantCulture);
                command.Parameters.Add("@AdmissionPageId", SqlDbType.Int).Value = int.Parse(col_parent_id, CultureInfo.InvariantCulture);
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
                command.CommandText = "PROC_GetSequenceNo_GovApprovel";
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
