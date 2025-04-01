using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class VideocategoryDAL : DBConnection
    {
        protected bool InsertBlock(VideocategoryBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertVideocategoryDetails";
                command.Parameters.AddWithValue("@VideoCategoryName", objbo.VideoCategoryName);
                command.Parameters.AddWithValue("@languageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@enabled", objbo.Enabled);
                command.Parameters.AddWithValue("@added_by", objbo.added_by);
                command.Parameters.AddWithValue("@modified_by", objbo.modified_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@TagList", objbo.TagList);
                command.Parameters.AddWithValue("@ThumbnillPath", objbo.ThumbnillPath);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool UpdateBock(VideocategoryBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateVideocategoryDetails";
                command.Parameters.AddWithValue("@VideoCategoryName", objbo.VideoCategoryName);
                command.Parameters.AddWithValue("@languageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@enabled", objbo.Enabled);
                command.Parameters.AddWithValue("@modified_by", objbo.modified_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@VCID", objbo.VCID);
                command.Parameters.AddWithValue("@TagList", objbo.TagList);
                command.Parameters.AddWithValue("@ThumbnillPath", objbo.ThumbnillPath);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet GetBlockDetailsByid(VideocategoryBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetVideocategoryDetails";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@VCID", objbo.VCID);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(VideocategoryBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "VideocategoryDetails_Delete";
                command.Parameters.AddWithValue("@VCID", objbo.VCID);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetBlockLanguageWise(VideocategoryBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetVideocategoryLanguageWise";
                command.Parameters.AddWithValue("@languageID", objbo.LanguageId);
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
