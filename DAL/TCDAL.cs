using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class TCDAL:DBConnection
    {
        protected bool InsertTC(TCBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateTCMaster";
                command.Parameters.AddWithValue("@Id", objbo.TCid);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@MetaTitle", objbo.metatitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.metadescription);
                command.Parameters.AddWithValue("@Description", objbo.description);
                command.Parameters.AddWithValue("@UserName", objbo.user_id);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet Select(TCBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_TCMaster_Select";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
