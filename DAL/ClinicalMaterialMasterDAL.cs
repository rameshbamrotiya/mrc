using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ClinicalMaterialMasterDAL : DBConnection
    {
        protected bool InsertPP(ClinicalMaterialMasterBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateClinicalMaterialMaster";
                command.Parameters.AddWithValue("@Id", objbo.CMId);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@MetaTitle", objbo.metatitle);
                command.Parameters.AddWithValue("@MetaDescription", objbo.metadescription);
                command.Parameters.AddWithValue("@Description", objbo.description);
                command.Parameters.AddWithValue("@IsVisible", objbo.IsVisible);
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
        protected DataSet Select(ClinicalMaterialMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ClinicalMaterialMaster_Select";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected DataSet GetClinicalMaterialMasterDetailsData(ClinicalMaterialMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ClinicalMaterialMasterDetails";
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
