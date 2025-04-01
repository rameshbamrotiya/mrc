using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class VideoGalleryMasterDAL:DBConnection
    {
        protected bool Insert(VideoGalleryMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_VideoMaster_Insert";
                command.Parameters.AddWithValue("@Video_Name", objbo.Video_Name);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@Video_level_id", objbo.Video_level_id);
                command.Parameters.AddWithValue("@VideoCategoryid", objbo.VideoCategoryid);
                command.Parameters.AddWithValue("@Department_id", objbo.Department_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Video_desc", objbo.Video_desc);
                command.Parameters.AddWithValue("@Video_Path", objbo.Video_Path);
                command.Parameters.AddWithValue("@ThumbImg_path", objbo.Thumbnill_Path);
                command.Parameters.AddWithValue("@Link_Video_Upload", objbo.Link_Video_Upload);
                command.Parameters.AddWithValue("@Is_active_Video", objbo.Is_active_Video);
                command.Parameters.AddWithValue("@Is_download", objbo.Is_download);
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
        protected bool Update(VideoGalleryMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_VideoDetailsMaster_Update";
                command.Parameters.AddWithValue("@Video_id", objbo.Video_id);
                command.Parameters.AddWithValue("@Video_level_id", objbo.Video_level_id);
                command.Parameters.AddWithValue("@Department_id", objbo.Department_id);
                command.Parameters.AddWithValue("@VideoCategoryid", objbo.VideoCategoryid);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Video_Name", objbo.Video_Name);
                command.Parameters.AddWithValue("@Video_desc", objbo.Video_desc);
                command.Parameters.AddWithValue("@Video_Path", objbo.Video_Path);
                command.Parameters.AddWithValue("@ThumbImg_path", objbo.Thumbnill_Path);
                command.Parameters.AddWithValue("@Link_Video_Upload", objbo.Link_Video_Upload);
                command.Parameters.AddWithValue("@Is_active_Video", objbo.Is_active_Video);
                command.Parameters.AddWithValue("@Is_download", objbo.Is_download);
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
        protected bool Delete(VideoGalleryMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_VideoMaster_Delete");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(VideoGalleryMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_VideoMaster_Select";
                command.Parameters.AddWithValue("@Video_id", objbo.Video_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectVideoDetails(VideoGalleryMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                //SqlCommand command = new SqlCommand();
                //CreateParameters(objbo, ref command, "PROC_AlbumMaster_Select");
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_Video_Select";
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
        protected bool UpdateOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_VideoMasterOrder_Update";
                command.Parameters.Add("@cmd", SqlDbType.NVarChar).Value = cmd;
                command.Parameters.Add("@currentColMenuLevel", SqlDbType.Decimal).Value = decimal.Parse(col_menu_level, CultureInfo.InvariantCulture);
                command.Parameters.Add("@video_id", SqlDbType.Int).Value = int.Parse(col_parent_id, CultureInfo.InvariantCulture);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet SelectSequenceNo()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Get_SequenceNo_Video";
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetVideoCategoryLanguageWise(VideoGalleryMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetVideoCategorydrop";
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
