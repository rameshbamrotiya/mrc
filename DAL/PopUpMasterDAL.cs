using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PopUpMasterDAL:DBConnection
    {
        protected DataSet PopUpMasterSelectBypagename(PopUpMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_PopUpMaster_SelectByPageName";
                command.Parameters.AddWithValue("@page_name", objbo.page_name);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool UpdatePopUpMaster(PopUpMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_PopUpMaster_Update");
                command.Parameters.AddWithValue("@page_name", objbo.page_name);
                int affectRows = ExecuteNonQuery(command);
                if (affectRows > 0) return true; else return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected DataSet HomepagePopupDetail()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetHomePagePopupDetails";
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
