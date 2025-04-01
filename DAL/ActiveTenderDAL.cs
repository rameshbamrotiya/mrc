using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ActiveTenderDAL:DBConnection
    {
        protected bool UpdateInActive(ActiveTenderBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_InActiveTenders_Update";
                command.Parameters.AddWithValue("@id" ,objbo.id);
                //command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool UpdateActive(ActiveTenderBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ActiveTenders_Update";
                command.Parameters.AddWithValue("@id", objbo.id);
                //command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
