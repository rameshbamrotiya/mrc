using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class PatientCareCategoryDAL : DBConnection
    {
        protected bool InsertPatientCareCategory(PatientCareCategoryBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertPatientCareCategoryDetails";
                command.Parameters.AddWithValue("@CategoryName", objbo.CategoryName);
                command.Parameters.AddWithValue("@languageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@SequenceNo", objbo.SequenceNo);
                command.Parameters.AddWithValue("@enabled", objbo.Enabled);
                command.Parameters.AddWithValue("@added_by", objbo.added_by);
                command.Parameters.AddWithValue("@modified_by", objbo.modified_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@FormType", objbo.FormType);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool UpdatePatientCareCategory(PatientCareCategoryBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdatePatientCarecatgeoryDetails";
                command.Parameters.AddWithValue("@CategoryName", objbo.CategoryName);
                command.Parameters.AddWithValue("@languageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@SequenceNo", objbo.SequenceNo);
                command.Parameters.AddWithValue("@enabled", objbo.Enabled);
                command.Parameters.AddWithValue("@modified_by", objbo.modified_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@CategoryID", objbo.CategoryID);
                command.Parameters.AddWithValue("@FormType", objbo.FormType);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet GetPatientCareCategoryByID(PatientCareCategoryBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetPatinetCareCategoryBYID";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@CategoryId", objbo.CategoryID);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetCategoryLanguagewise(PatientCareCategoryBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetPatientCareCategoryLanguageWise";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);

                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(PatientCareCategoryBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PatientCareCategoryMaster_Delete";
                command.Parameters.AddWithValue("@CategoryID", objbo.CategoryID);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetPatinetCareTypeById(PatientCareCategoryBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetPatinetCareTypeBYID";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@CategoryId", objbo.CategoryID);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectSequenceNo()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Get_SequenceNo_PatientCareCategoryDetails";
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
