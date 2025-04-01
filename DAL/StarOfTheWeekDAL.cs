using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class StarOfTheWeekDAL:DBConnection
    {
        protected bool Insert(StarOfTheWeekBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_StarOfTheWeekMaster_Insert";
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.Metatitle);
                command.Parameters.AddWithValue("@Week", objbo.Week);
                command.Parameters.AddWithValue("@Imgpath", objbo.Imgpath);
                command.Parameters.AddWithValue("@FromDate", objbo.FromDate);
                command.Parameters.AddWithValue("@ToDate", objbo.ToDate);
                command.Parameters.AddWithValue("@StarOfThe", objbo.StarOfThe);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
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
        protected bool Update(StarOfTheWeekBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_StarOfTheWeekMaster_Update";
                command.Parameters.AddWithValue("@S_id", objbo.S_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.Metatitle);
                command.Parameters.AddWithValue("@Week", objbo.Week);
                command.Parameters.AddWithValue("@Imgpath", objbo.Imgpath);
                command.Parameters.AddWithValue("@FromDate", objbo.FromDate);
                command.Parameters.AddWithValue("@ToDate", objbo.ToDate);
                command.Parameters.AddWithValue("@StarOfThe", objbo.StarOfThe);
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
        protected bool Delete(StarOfTheWeekBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_StarOfTheWeekMaster_Delete";
                command.Parameters.AddWithValue("@S_id", objbo.S_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(StarOfTheWeekBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_StarOfTheWeekMaster_Select";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@S_id", objbo.S_id);
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
