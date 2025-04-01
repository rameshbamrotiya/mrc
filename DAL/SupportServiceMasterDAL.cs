using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class SupportServiceMasterDAL:DBConnection
    {
        protected bool InsertSupportservice(SupportServiceMasterBO objbo,out long SSID)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertSupportServiceDetails";
                command.Parameters.AddWithValue("@SSName", objbo.SSName);
                //command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                //command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@languageId", objbo.Language);
                command.Parameters.AddWithValue("@SS_level_id", objbo.SS_level_id);
                command.Parameters.AddWithValue("@SSImg", objbo.SSImg);
                command.Parameters.AddWithValue("@SSIcon", objbo.SSIcon);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@enabled", objbo.enabled);
                command.Parameters.AddWithValue("@added_by", objbo.added_by);
                command.Parameters.AddWithValue("@modified_by", objbo.modified_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                //ExecuteNonQuery(command);
                DataTable ds = ExecuteQuery(command).Tables[0];

                SSID = Convert.ToInt32(ds.Rows[0]["SSID"].ToString());

                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool InsertSupportserviceImage(long lgSupportServiceId, string strImagePath)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertSupportServiceImageDetails";

                command.Parameters.AddWithValue("@SupportServiceId", lgSupportServiceId);
                command.Parameters.AddWithValue("@ImagePath", strImagePath);

                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        protected bool DeleteSupportserviceImage(long lgSupportServiceId)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "RemoveAllFromSupportServiceImageDetails";
                command.Parameters.AddWithValue("@ServiceId", lgSupportServiceId);
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

        protected DataTable GetAllSupportServiceImageDetails(long lgSupportServiceId)
        {
            try
            {

                DataTable ds = new DataTable();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllSupportServiceImageDetails";
                command.Parameters.AddWithValue("@SupportServiceId", lgSupportServiceId);

                ds = ExecuteQuery(command).Tables[0];

                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet GetRecordBySupportserviceId(SupportServiceMasterBO objbo)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetSupportSystemById";
                command.Parameters.AddWithValue("@SSId", objbo.SSId);
                command.Parameters.AddWithValue("@Languageid", objbo.Language);

                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool UpdateSupportserviceRecord(SupportServiceMasterBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateSupportServiceDetails";
                command.Parameters.AddWithValue("@SSId", objbo.SSId);
                command.Parameters.AddWithValue("@SSName", objbo.SSName);
                //command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                //command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@SS_level_id", objbo.SS_level_id);
                command.Parameters.AddWithValue("@languageId", objbo.Language);
                command.Parameters.AddWithValue("@SSImg", objbo.SSImg);
                command.Parameters.AddWithValue("@SSIcon", objbo.SSIcon);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@enabled", objbo.enabled);
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
        protected bool Delete(SupportServiceMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_SupportServiceMaster_Delete";
                command.Parameters.AddWithValue("@SSId", objbo.SSId);
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
        protected bool UpdateOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_SupportServiceMasterOrder_Update";
                command.Parameters.Add("@cmd", SqlDbType.NVarChar).Value = cmd;
                command.Parameters.Add("@currentColMenuLevel", SqlDbType.Decimal).Value = decimal.Parse(col_menu_level, CultureInfo.InvariantCulture);
                command.Parameters.Add("@SSId", SqlDbType.Int).Value = int.Parse(col_parent_id, CultureInfo.InvariantCulture);
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
                command.CommandText = "Get_SequenceNo_SupportService";
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
