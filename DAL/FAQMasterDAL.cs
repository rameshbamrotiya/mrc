using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class FAQMasterDAL : DBConnection
    {
        protected bool Insert(FAQMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FAQMaster_Insert";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@FAQDescription", objbo.FAQDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Update(FAQMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FAQMaster_Update";
                command.Parameters.AddWithValue("@FAQ_Id", objbo.FAQ_Id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@FAQDescription", objbo.FAQDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(FAQMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FAQMaster_Delete";
                command.Parameters.AddWithValue("@FAQ_Id", objbo.FAQ_Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(FAQMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FAQMaster_Select";
                command.Parameters.AddWithValue("@FAQ_Id", objbo.FAQ_Id);
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
        protected DataSet GetFAQDetailsByLanguageId(FAQMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetFAQMasterDetailsByLanguageId";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertAccredation(FAQAccredationMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FAQAccredationDetails_Insert";
                command.Parameters.AddWithValue("@FAQDetails_Id", objbo.FAQDetails_Id);
                command.Parameters.AddWithValue("@Language_id", objbo.Language_id);
                command.Parameters.AddWithValue("@AccredationTitle", objbo.AccredationTitle);
                command.Parameters.AddWithValue("@AccredationDescription", objbo.AccredationDescription);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@Added_by", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool UpdateAccredation(FAQAccredationMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FAQAccredationDetails_Update";
                command.Parameters.AddWithValue("@Accredation_Id", objbo.Accredation_Id);
                command.Parameters.AddWithValue("@FAQDetails_Id", objbo.FAQDetails_Id);
                command.Parameters.AddWithValue("@Language_id", objbo.Language_id);
                command.Parameters.AddWithValue("@AccredationTitle", objbo.AccredationTitle);
                command.Parameters.AddWithValue("@AccredationDescription", objbo.AccredationDescription);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@Added_by", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteAccredation(FAQAccredationMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FAQAccredationDetails_Delete";
                command.Parameters.AddWithValue("@Accredation_Id", objbo.Accredation_Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectAccredation(FAQAccredationMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FAQAccredationDetails_Select";
                command.Parameters.AddWithValue("@FAQDetails_Id", objbo.FAQDetails_Id);
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
        protected DataSet GetAccredationDetailsByAccredationId(FAQAccredationMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetAllFAQAccredationDetails";
                command.Parameters.AddWithValue("@Accredation_Id", objbo.Accredation_Id);
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
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
