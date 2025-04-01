using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class SchemeDAL : DBConnection
    {
        protected bool InsertScheme(SchemeBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertSchemeDetails";
                command.Parameters.AddWithValue("@SchemeName", objbo.SchemeName);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@languageId", objbo.Language);
                command.Parameters.AddWithValue("@scheme_level_id", objbo.scheme_level_id);
                command.Parameters.AddWithValue("@ChartId", null);
                command.Parameters.AddWithValue("@SchemeLogo", objbo.SchemeLogo);
                command.Parameters.AddWithValue("@SchemeBanner", objbo.SchemeBanner);
                command.Parameters.AddWithValue("@ContactPerson", objbo.ContactPerson);
                command.Parameters.AddWithValue("@Location", objbo.Location);
                command.Parameters.AddWithValue("@HelpDeskNo", objbo.HelpDeskNo);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@WebsiteUrl", objbo.WebsiteUrl);
                command.Parameters.AddWithValue("@enabled", objbo.enabled);
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

        protected DataSet GetRecordBySchemeId(SchemeBO objbo)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetSchemeById";
                command.Parameters.AddWithValue("@SchemeID", objbo.SchemeId);
                command.Parameters.AddWithValue("@Languageid", objbo.Language);

                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool UpdateSchemeRecord(SchemeBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateSchemeDetails";
                command.Parameters.AddWithValue("@Schemeid", objbo.SchemeId);
                command.Parameters.AddWithValue("@SchemeName", objbo.SchemeName);
                command.Parameters.AddWithValue("@MetaDescription", objbo.MetaDescription);
                command.Parameters.AddWithValue("@MetaTitle", objbo.MetaTitle);
                command.Parameters.AddWithValue("@scheme_level_id", objbo.scheme_level_id);
                command.Parameters.AddWithValue("@ChartId", null);
                command.Parameters.AddWithValue("@languageId", objbo.Language);
                command.Parameters.AddWithValue("@SchemeLogo", objbo.SchemeLogo);
                command.Parameters.AddWithValue("@SchemeBanner", objbo.SchemeBanner);
                command.Parameters.AddWithValue("@ContactPerson", objbo.ContactPerson);
                command.Parameters.AddWithValue("@Location", objbo.Location);
                command.Parameters.AddWithValue("@HelpDeskNo", objbo.HelpDeskNo);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@WebsiteUrl", objbo.WebsiteUrl);
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

        protected bool Delete(SchemeBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_SchemeMaster_Delete";
                command.Parameters.AddWithValue("@SchemeId", objbo.SchemeId);
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
                command.CommandText = "tbl_SchemeMasterOrder_Update";
                command.Parameters.Add("@cmd", SqlDbType.NVarChar).Value = cmd;
                command.Parameters.Add("@currentColMenuLevel", SqlDbType.Decimal).Value = decimal.Parse(col_menu_level, CultureInfo.InvariantCulture);
                command.Parameters.Add("@Schemeid", SqlDbType.Int).Value = int.Parse(col_parent_id, CultureInfo.InvariantCulture);
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
                command.CommandText = "Get_SequenceNo_scheme";
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet GetSchemeLanguageWise(SchemeBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetSchemeLanguageWise";
                command.Parameters.AddWithValue("@languageID", objbo.Language);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected long InsertOrUpdateSchemaChartDetails(long lgId, long lgSchemaId, long lgChartId, long lgSequanceNo, string strUsername)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateSchemaChartDetails";
                command.Parameters.AddWithValue("@Id", lgId);
                command.Parameters.AddWithValue("@SchemeId", lgSchemaId);
                command.Parameters.AddWithValue("@ChartId", lgChartId);
                command.Parameters.AddWithValue("@SequanceNo", lgSequanceNo);
                command.Parameters.AddWithValue("@Username", strUsername);
                ds = ExecuteQuery(command);
                return Convert.ToInt32(ds.Tables[0].Rows[0]["RecId"]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool RemoveSchemaChartDetails(long lgSchemaId,string strUsername)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "RemoveSchemaChartDetails";
                command.Parameters.AddWithValue("@Id", lgSchemaId);
                command.Parameters.AddWithValue("@Username", strUsername);

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

        protected bool SchemaChartOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SchemaChartDetailsOrder_Update";
                command.Parameters.Add("@cmd", SqlDbType.NVarChar).Value = cmd;
                command.Parameters.Add("@SequanceNo", SqlDbType.Decimal).Value = decimal.Parse(col_menu_level, CultureInfo.InvariantCulture);
                command.Parameters.Add("@Schemeid", SqlDbType.Int).Value = int.Parse(col_parent_id, CultureInfo.InvariantCulture);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        protected DataTable GetAllSchemaChartDetail(long lgSchemaId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"GetAllSchemaChartDetails";
                command.Parameters.AddWithValue("@SchemaId", lgSchemaId);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
