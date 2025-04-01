using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ArticleDepartmentDAL : DBConnection
    {
        protected bool Insert(ArticleDepartmentBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ArticleDepartmentMaster_Insert";
                command.Parameters.AddWithValue("@AD_Name", objbo.AD_Name);
                command.Parameters.AddWithValue("@AD_Title", objbo.AD_Title);
                command.Parameters.AddWithValue("@Imgpath", objbo.Imgpath);
                command.Parameters.AddWithValue("@Iconpath", objbo.Iconpath);
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
        protected bool Update(ArticleDepartmentBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ArticleDepartmentMaster_Update";
                command.Parameters.AddWithValue("@AD_id", objbo.AD_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@AD_Name", objbo.AD_Name);
                command.Parameters.AddWithValue("@AD_Title", objbo.AD_Title);
                command.Parameters.AddWithValue("@Imgpath", objbo.Imgpath);
                command.Parameters.AddWithValue("@Iconpath", objbo.Iconpath);
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
        protected bool Delete(ArticleDepartmentBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ArticleDepartmentMaster_Delete";
                command.Parameters.AddWithValue("@AD_id", objbo.AD_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(ArticleDepartmentBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ArticleDepartmentMaster_Select";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@AD_id", objbo.AD_id);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectByLanguage(int AD_id, int LanguageId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_Research_Selectallbylanguage";
                command.Parameters.AddWithValue("@LanguageId", LanguageId);
                command.Parameters.AddWithValue("@AD_id", AD_id);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectResearchDetailsByLanguage(int AD_id, int LanguageId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ResearchDetails_Selectallbylanguage";
                command.Parameters.AddWithValue("@LanguageId", LanguageId);
                command.Parameters.AddWithValue("@AD_id", AD_id);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectResearchpublicationnameByLanguage(int LanguageId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ResearchDetails_publicationname";
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
    }
}
