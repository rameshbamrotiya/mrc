using BO;
using System;

using System.Data;
using System.Data.SqlClient;
using System.Globalization;


namespace DAL
{
    public class MenuDAL : DBConnection
    {
        protected DataSet Select(MenuBO objbo)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_Mnu_MasterSelectByUserTypeID";
                command.Parameters.AddWithValue("@user_type_id", objbo.user_type_id);

                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectMenuByID(MenuBO objbo)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_Menu_SelectByMenuID";
                command.Parameters.AddWithValue("@col_menu_id", objbo.col_menu_id);
                command.Parameters.AddWithValue("@Languageid", objbo.Language);

                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectParentResource(MenuBO objbo)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_Menu_SelectParentResource";
                command.Parameters.AddWithValue("@col_parent_id", objbo.col_parent_id);
                command.Parameters.AddWithValue("@Languageid", objbo.Language);

                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet selectMenu(MenuBO objbo)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_Menu_MasterSelectAll";
                command.Parameters.AddWithValue("@Languageid", objbo.Language);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SearchMenu(MenuBO objbo)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_Menu_Search";
                command.Parameters.AddWithValue("@col_menu_name", objbo.col_menu_name);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertMenu(MenuBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_MenuMaster_Insert";
                command.Parameters.AddWithValue("@col_menu_name", objbo.col_menu_name);
                command.Parameters.AddWithValue("@col_menu_url", objbo.col_menu_url);
                command.Parameters.AddWithValue("@col_parent_id", objbo.col_parent_id);
                command.Parameters.AddWithValue("@col_menu_rank", objbo.col_menu_rank);
                command.Parameters.AddWithValue("@enabled", objbo.enabled);
                command.Parameters.AddWithValue("@added_by", objbo.added_by);
                command.Parameters.AddWithValue("@modified_by", objbo.modified_by);
                command.Parameters.AddWithValue("@Templateid", objbo.templateId);
                command.Parameters.AddWithValue("@Tooltip", objbo.tooltip);
                command.Parameters.AddWithValue("@HeaderImage", objbo.HeaderImage);
                command.Parameters.AddWithValue("@MaskingURL", objbo.MaskingURL);
                command.Parameters.AddWithValue("@Languageid", objbo.Language);
                command.Parameters.AddWithValue("@ContentDet", objbo.ContentDet);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@col_menu_type", objbo.col_menu_type);
                command.Parameters.AddWithValue("@IsDisabledTranslate", objbo.IsDisabledTranslate);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool UpdateMenu(MenuBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_MenuMaster_Update";
                command.Parameters.AddWithValue("@recid", objbo.recid);
                command.Parameters.AddWithValue("@col_menu_id", objbo.col_menu_id);
                command.Parameters.AddWithValue("@col_menu_name", objbo.col_menu_name);
                command.Parameters.AddWithValue("@col_menu_url", objbo.col_menu_url);
                command.Parameters.AddWithValue("@col_parent_id", objbo.col_parent_id);
                command.Parameters.AddWithValue("@col_menu_rank", objbo.col_menu_rank);
                command.Parameters.AddWithValue("@HeaderImage", objbo.HeaderImage);
                command.Parameters.AddWithValue("@enabled", objbo.enabled);
                command.Parameters.AddWithValue("@added_by", objbo.added_by);
                command.Parameters.AddWithValue("@modified_by", objbo.modified_by);
                command.Parameters.AddWithValue("@Templateid", objbo.templateId);
                command.Parameters.AddWithValue("@Tooltip", objbo.tooltip);
                command.Parameters.AddWithValue("@MaskingURL", objbo.MaskingURL);
                command.Parameters.AddWithValue("@Languageid", objbo.Language);
                command.Parameters.AddWithValue("@ContentDet", objbo.ContentDet);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@col_menu_type", objbo.col_menu_type);
                command.Parameters.AddWithValue("@IsDisabledTranslate", objbo.IsDisabledTranslate);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool UpdateMenuResource(MenuBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_Resources_Update";
                command.Parameters.AddWithValue("@col_menu_id", objbo.col_menu_id);
                command.Parameters.AddWithValue("@col_menu_url", objbo.col_menu_url);
                command.Parameters.AddWithValue("@col_menu_name", objbo.col_menu_name);
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
        protected bool UpdateOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Tbl_menumasterorder_update";
                command.Parameters.Add("@cmd", SqlDbType.NVarChar).Value = cmd;
                command.Parameters.Add("@currentColMenuLevel", SqlDbType.Decimal).Value = decimal.Parse(col_menu_level, CultureInfo.InvariantCulture);
                command.Parameters.Add("@col_parent_id", SqlDbType.Int).Value = int.Parse(col_parent_id, CultureInfo.InvariantCulture);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        protected bool SelectTranslateStatus(int languageId, string uRl)
        {
            bool status = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Tbl_menumasterTranslateStatus";
                command.Parameters.AddWithValue("@languageId", languageId);
                command.Parameters.AddWithValue("@uRl", uRl);
                var returnParameter = command.Parameters.Add("@Exist", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                command.Connection = GetConnection();
                command.ExecuteNonQuery();
                CloseConnection();
                var result = Convert.ToInt32( returnParameter.Value);
                if (result > 0)
                    status = true;
                else
                    status = false;
            }
            catch (Exception ex)
            {
                throw;
            }
           

            return status;
        }
    }
}
