using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
   public class PatientCareSubCategoryDAL:DBConnection
    {
        protected bool InsertPatientCareSubCategory(PatientCareSubCategoryBO objbo,DataTable dt)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertPatientCareSubCategoryDetails";
                command.Parameters.AddWithValue("@CategoryName", objbo.CategoryName);
                command.Parameters.AddWithValue("@languageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@SequenceNo", objbo.SequenceNo);
                command.Parameters.AddWithValue("@enabled", objbo.Enabled);
                command.Parameters.AddWithValue("@added_by", objbo.added_by);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@modified_by", objbo.modified_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@CategoryID", objbo.CategoryID);
                command.Parameters.AddWithValue("@dtimg", dt);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool UpdatePatientCareSubCategory(PatientCareSubCategoryBO objbo,DataTable dt)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdatePatientCareSubcatgeoryDetails";
                command.Parameters.AddWithValue("@CategoryName", objbo.CategoryName);
                command.Parameters.AddWithValue("@languageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@SequenceNo", objbo.SequenceNo);
                command.Parameters.AddWithValue("@enabled", objbo.Enabled);
                command.Parameters.AddWithValue("@modified_by", objbo.modified_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@CategoryID", objbo.CategoryID);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@SubCategoryID", objbo.SubCategoryID);
                command.Parameters.AddWithValue("@dtimg", dt);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool Delete(PatientCareSubCategoryBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PatientCareSubcatgeoryDetails_Delete";
                command.Parameters.AddWithValue("@SubCategoryID", objbo.SubCategoryID);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteImage(PatientCareSubCategoryBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PatientCareSubcatgeoryDetailsImage_Delete";
                command.Parameters.AddWithValue("@Img_id", objbo.Img_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetPatientCareSubCategoryByID(PatientCareSubCategoryBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetPatinetCareSubCategoryBYID";
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
        protected DataSet GetSubCategoryLanguagewise(PatientCareSubCategoryBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetPatientCareSubCategoryLanguageWise";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);

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
