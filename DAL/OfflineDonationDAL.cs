using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class OfflineDonationDAL:DBConnection
    {
        protected bool InsertOfflineDonation(OfflineDonationBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOfflineDonationDetails";
                command.Parameters.AddWithValue("@FirstName", objbo.FirstName);
                command.Parameters.AddWithValue("@LastName", objbo.LastName);
                command.Parameters.AddWithValue("@Email", objbo.Email);
                command.Parameters.AddWithValue("@MoNo", objbo.MoNo);
                command.Parameters.AddWithValue("@PanNo", objbo.PanNo);
                command.Parameters.AddWithValue("@Amount", objbo.Amount);
                command.Parameters.AddWithValue("@Address", objbo.Address);
                //command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                //command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@languageId", objbo.Language);
                //command.Parameters.AddWithValue("@SS_level_id", objbo.SS_level_id);
                command.Parameters.AddWithValue("@ReciptPath", objbo.ReciptPath);
                command.Parameters.AddWithValue("@Status", objbo.Status);
                command.Parameters.AddWithValue("@added_by", objbo.added_by);
                command.Parameters.AddWithValue("@modified_by", objbo.modified_by);
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
        protected DataSet GetRecordByOfflineDonationId(OfflineDonationBO objbo)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetOfflineDonationById";
                command.Parameters.AddWithValue("@ODId", objbo.ODId);
                command.Parameters.AddWithValue("@Languageid", objbo.Language);

                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool UpdateOfflineDonationRecord(OfflineDonationBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateOfflineDonationDetails";
                command.Parameters.AddWithValue("@ODId", objbo.ODId);
                command.Parameters.AddWithValue("@FirstName", objbo.FirstName);
                command.Parameters.AddWithValue("@LastName", objbo.LastName);
                command.Parameters.AddWithValue("@Email", objbo.Email);
                command.Parameters.AddWithValue("@MoNo", objbo.MoNo);
                command.Parameters.AddWithValue("@PanNo", objbo.PanNo);
                command.Parameters.AddWithValue("@Amount", objbo.Amount);
                command.Parameters.AddWithValue("@Address", objbo.Address);
                command.Parameters.AddWithValue("@added_by", objbo.added_by);
                //command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                //command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@languageId", objbo.Language);
                //command.Parameters.AddWithValue("@SS_level_id", objbo.SS_level_id);
                command.Parameters.AddWithValue("@ReciptPath", objbo.ReciptPath);
                command.Parameters.AddWithValue("@Status", objbo.Status);
                command.Parameters.AddWithValue("@modified_by", objbo.modified_by);
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
        protected bool Delete(OfflineDonationBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_OfflineDonationMaster_Delete";
                command.Parameters.AddWithValue("@ODId", objbo.ODId);
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
