using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class ContactUsMasterDAL : DBConnection
    {
        protected bool InsertContactUs(ContactUsMasterBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ContactUsMaster_Insert";
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
                command.Parameters.AddWithValue("@ContactUsDescription", objbo.ContactUsDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool UpdateContactUs(ContactUsMasterBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ContactUsMaster_Update";
                command.Parameters.AddWithValue("@ContactUs_Id", objbo.ContactUs_Id);
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
                command.Parameters.AddWithValue("@ContactUsDescription", objbo.ContactUsDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet PGetAllContactUsMasterDetails()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetAllContactUsMasterDetails";                
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetAllContactUsMasterDetails(long Language_id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllContactUsMasterDetails";
                command.Parameters.AddWithValue("@LanguageId", Language_id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetContactUsDetailsByID(ContactUsMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ContactUsMaster_Select";
                command.Parameters.AddWithValue("@ContactUs_Id", objbo.ContactUs_Id);
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);                
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(ContactUsMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ContactUsMaster_Delete";
                command.Parameters.AddWithValue("@ContactUs_Id", objbo.ContactUs_Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
