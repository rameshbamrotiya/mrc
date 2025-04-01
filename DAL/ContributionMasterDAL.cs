using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class ContributionMasterDAL : DBConnection
    {
        protected bool InsertContribution(ContributionMasterBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ContributionMaster_Insert";
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
                command.Parameters.AddWithValue("@PageDescription", objbo.PageDescription);
                command.Parameters.AddWithValue("@OfflineDonationDescription", objbo.OfflineDonationDescription);
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
        protected bool UpdateContribution(ContributionMasterBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ContributionMaster_Update";
                command.Parameters.AddWithValue("@Contribution_Id", objbo.Contribution_Id);
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);
                command.Parameters.AddWithValue("@PageDescription", objbo.PageDescription);
                command.Parameters.AddWithValue("@OfflineDonationDescription", objbo.OfflineDonationDescription);
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
        protected DataSet GetAllContributionMasterDetails()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetAllContributionMasterDetails";                
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetContributionDetailsByID(ContributionMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ContributionMaster_Select";
                command.Parameters.AddWithValue("@Contribution_Id", objbo.Contribution_Id);
                command.Parameters.AddWithValue("@LanguageId", objbo.Language_id);                
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(ContributionMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ContributionMaster_Delete";
                command.Parameters.AddWithValue("@Contribution_Id", objbo.Contribution_Id);
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
