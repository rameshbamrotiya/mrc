using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class SMSLogDAL : DBConnection
    {
        protected bool InsertSMSLog(SMSlogBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Insert_tbl_SMSlog";
                command.Parameters.AddWithValue("@MobileNo", objbo.MobileNo);
                command.Parameters.AddWithValue("@TransectionId", objbo.TransectionId);
                command.Parameters.AddWithValue("@Message", objbo.Message);
                command.Parameters.AddWithValue("@Status", objbo.Status);
                command.Parameters.AddWithValue("@RequestURL", objbo.RequestURL);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
    }
}
