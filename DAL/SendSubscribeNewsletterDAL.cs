using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace DAL
{
    public class SendSubscribeNewsletterDAL:DBConnection
    {
        protected bool InsertDoc(SendSubscribeNewsletterDocBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_SendSubscribeNewsletterDOC_Insert";
                command.Parameters.AddWithValue("@SSN_Name", objbo.SSN_Name);
                command.Parameters.AddWithValue("@SSN_DocPath", objbo.SSN_DocPath);
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
        protected bool UpdateDoc(SendSubscribeNewsletterDocBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_SendSubscribeNewsletterDOC_Update";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@SSN_Name", objbo.SSN_Name);
                command.Parameters.AddWithValue("@SSN_DocPath", objbo.SSN_DocPath);
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
        protected bool DeleteDoc(SendSubscribeNewsletterDocBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_SendSubscribeNewsletterDOC_Delete";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectDoc(SendSubscribeNewsletterDocBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_SendSubscribeNewsletterDOC_Select";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataTable GetALLSendSubscribeNewsletterLog()
        {
            try
            {
                DataTable ds = new DataTable();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetALLSendSubscribeNewsletterLog";
                ds = ExecuteQuery(command).Tables[0];
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet GetAllSubscribeNewsletter()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetAllSubscribeNewsletterMaster";
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetDocumentName(string spName)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = spName;
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertNewsLeter(SendSubscribeNewsletterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_SendSubscribeNewsletterMailLog_Insert";
                command.Parameters.AddWithValue("@SSN_Id", objbo.SSN_Id);
                command.Parameters.AddWithValue("@FullName", objbo.FullName);
                command.Parameters.AddWithValue("@EmailId", objbo.EmailId);
                command.Parameters.AddWithValue("@MobileNo", objbo.MobileNo);
                command.Parameters.AddWithValue("@Location", objbo.Location);
                command.Parameters.AddWithValue("@DocId", objbo.DocId);
                command.Parameters.AddWithValue("@MailDescription", objbo.MailDescription);
                command.Parameters.AddWithValue("@MailSubject", objbo.MailSubject);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@flag", objbo.flag);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
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
