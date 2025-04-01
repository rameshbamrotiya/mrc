using System;
using System.Data;
using System.Data.SqlClient;
using BO;

namespace DAL
{
    public class AllocateSeatDAL : DBConnection
    {
        protected bool AllocateSEatAsPerStudentChoiceFilling(string RoundNo,string UserName,out DataTable dt)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "AllocateSEatAsPerStudentChoiceFilling";
                command.Parameters.AddWithValue("@RoundNo", RoundNo);
                command.Parameters.AddWithValue("@UserName", UserName);
                dt=ExecuteQuery(command).Tables[0];
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }


        protected DataTable GetAllAssignSeatList()
        {
            try
            {

                DataTable ds = new DataTable();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllAssignSeatList";
                //command.Parameters.AddWithValue("@col_menu_id", objbo.parentid);
                ds = ExecuteQuery(command).Tables[0];
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
