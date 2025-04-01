using System;
using System.Data;
using System.Data.SqlClient;
using BO;
namespace DAL
{
  public  class UserRightsDAL : DBConnection
    {
        protected DataSet SelectMenuRecord(UserRightsBO objbo)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_SelectResource_RoleIdWise";
                command.Parameters.AddWithValue("@roleid", objbo.roleid );
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteMenuByRole(UserRightsBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_User_Right_Delete_By_Roleid";
                command.Parameters.AddWithValue("@roleid", objbo.roleid);                
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected  bool InsertMenuByRole(UserRightsBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_User_Right_MasterInsert"; 
                command.Parameters.AddWithValue("@col_menu_id", objbo.MenuId);
                command.Parameters.AddWithValue("@col_user_type_id", objbo.roleid);
                command.Parameters.AddWithValue("@col_isAdd", objbo.CanAdd);
                command.Parameters.AddWithValue("@col_isUpdate", objbo.CanUpdate);
                command.Parameters.AddWithValue("@col_isView", objbo.CanView);
                command.Parameters.AddWithValue("@col_isDelete", objbo.CanDelete);
                command.Parameters.AddWithValue("@enabled", true);
                command.Parameters.AddWithValue("@added_by", 45);
               // command.Parameters.AddWithValue("@added_date", objbo.added_date);
                command.Parameters.AddWithValue("@modified_by", 45);
               // command.Parameters.AddWithValue("@modified_date", objbo.added_date);
                command.Parameters.AddWithValue("@ip_add", objbo.ipaddress);

                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        protected DataSet SelectResourceRights(MenuBO objbo)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_Select_Resource_ByRoleIdRights";
                command.Parameters.AddWithValue("@user_type_id", objbo.user_type_id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet SelectResource()
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_Resource_SelectAll";
                //command.Parameters.AddWithValue("@col_menu_id", objbo.parentid);
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
