using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class RighttoInformationMasterDAL : DBConnection
    {
        protected bool Insert(RighttoInformationMasterBO objbo, DataTable dt)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_RighttoInformationMaster_Insert";
                command.Parameters.AddWithValue("@dtDocs", dt);
                command.Parameters.AddWithValue("@RIID", objbo.RIID);
                command.Parameters.AddWithValue("@Type", objbo.Type);
                command.Parameters.AddWithValue("@RIMID", objbo.RIMID);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@modify_by", objbo.modify_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@LanguageID", objbo.LanguageID);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@enabled", objbo.enabled);
                int rowaffect = ExecuteNonQuery(command);
                if (rowaffect > 0) return true; else return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(RighttoInformationMasterBO objbo)
        {
            try
            {
                //SqlCommand command = new SqlCommand();
                //command.CommandType = CommandType.StoredProcedure;
                //command.CommandText = "PROC_RighttoInformationMaster_Delete";
                //command.Parameters.AddWithValue("@RTI_id", objbo.RTI_id);
                //ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(RighttoInformationMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_RighttoInformationMaste_Select";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageID);
                command.Parameters.AddWithValue("@Type", objbo.Type);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }


        protected DataSet getContaint(RighttoInformationMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_RighttoInformationMaste_Select";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageID);
                command.Parameters.AddWithValue("@Type", objbo.Type);
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
