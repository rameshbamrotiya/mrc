using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class AwardsDAL:DBConnection
    {
        protected bool Insert(AwardsBO objbo, DataTable dt)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AwardMaster_Insert";
                command.Parameters.AddWithValue("@Album_Name", objbo.Album_Name);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Album_desc", objbo.Album_desc);
                command.Parameters.AddWithValue("@AwardShortdesc", objbo.ShortDescription);
                command.Parameters.AddWithValue("@AccredationDesc", objbo.AccredationDesc);
                command.Parameters.AddWithValue("@AwardMonthYear", objbo.AwardMonthYear);
                command.Parameters.AddWithValue("@Type", objbo.Type);
                command.Parameters.AddWithValue("@Is_active_album", objbo.Is_active_album);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@Award_level_id", objbo.Award_level_id);
                command.Parameters.AddWithValue("@dtimg", dt);
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
        protected bool Update(AwardsBO objbo, DataTable dt)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_AwardDetailsMaster_Update";
                command.Parameters.AddWithValue("@Award_id", objbo.Award_id);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Album_Name", objbo.Album_Name);
                command.Parameters.AddWithValue("@Album_desc", objbo.Album_desc);
                command.Parameters.AddWithValue("@Type", objbo.Type);
                command.Parameters.AddWithValue("@AwardShortdesc", objbo.ShortDescription);
                command.Parameters.AddWithValue("@AccredationDesc", objbo.AccredationDesc);
                command.Parameters.AddWithValue("@AwardMonthYear", objbo.AwardMonthYear);
                command.Parameters.AddWithValue("@Is_active_album", objbo.Is_active_album);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@Award_level_id", objbo.Award_level_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@dtimg", dt);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objbo.IsExist > 0) return false; else return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected bool Delete(AwardsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_AwardMaster_Delete");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(AwardsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_AwardMaster_Select");
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectIMG(AwardsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_AwardMasterIMG_Select");
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
                command.CommandText = "tbl_AwardMasterOrder_Update";
                command.Parameters.Add("@cmd", SqlDbType.NVarChar).Value = cmd;
                command.Parameters.Add("@currentColMenuLevel", SqlDbType.Decimal).Value = decimal.Parse(col_menu_level, CultureInfo.InvariantCulture);
                command.Parameters.Add("@Award_id", SqlDbType.Int).Value = int.Parse(col_parent_id, CultureInfo.InvariantCulture);
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
                command.CommandText = "Get_SequenceNo_Award";
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
