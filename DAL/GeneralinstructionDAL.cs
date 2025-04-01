using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class GeneralinstructionDAL : DBConnection
    {
        protected bool InsertOrUpdateGeneralInstructionDetails(GeneralinstructionBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateGeneralInstruction";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@IsVisible", objbo.IsVisible);
                command.Parameters.AddWithValue("@Desc", objbo.Desciption);
                command.Parameters.AddWithValue("@DocName", objbo.DocName);
                command.Parameters.AddWithValue("@DocPath", objbo.DocPath);
                command.Parameters.AddWithValue("@UserId", objbo.CreateBy);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objbo.IsExist > 0) return false; else return true;
                //ExecuteNonQuery(command);                
                //return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected bool RemoveGeneralInstruction(long Id, string Username)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "RemoveGeneralInstruction";
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@Username", Username);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataTable SelectAllGeneralInstruction()
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllGeneralInsutrction";
                //command.Parameters.AddWithValue("@col_menu_id", objbo.parentid);
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
