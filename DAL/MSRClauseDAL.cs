using BO;
using System;

using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
   public class MSRClauseDAL:DBConnection
    {
        protected bool InsertMSRClause(MSRClauseBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertMsrClauseDetails";
                command.Parameters.AddWithValue("@Particulars", objbo.Particulars);
                command.Parameters.AddWithValue("@LatstupdateDate", objbo.LatstupdateDate);
                command.Parameters.AddWithValue("@enabled", objbo.enabled);
                command.Parameters.AddWithValue("@added_by", objbo.AddedBy);
                command.Parameters.AddWithValue("@modified_by", objbo.ModifiedBy);
                command.Parameters.AddWithValue("@languageId", objbo.languageid); 
                command.Parameters.AddWithValue("@ImagePath", objbo.imagepath);
                command.Parameters.AddWithValue("@ip_add", objbo.ipadd);
                command.Parameters.AddWithValue("@Msr_level_id", objbo.Msr_level_id);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool RemoveMsrClauseMaster(MSRClauseBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "RemoveMsrClauseMaster";
                command.Parameters.AddWithValue("@msrid", objbo.msrid);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet Select(MSRClauseBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MsrClauseMaster_Select";
                command.Parameters.AddWithValue("@msrid", objbo.msrid);
                command.Parameters.AddWithValue("@languageid", objbo.languageid);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
         protected bool Update(MSRClauseBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateMsrClauseDetails";
                command.Parameters.AddWithValue("@msrid", objbo.msrid);
                command.Parameters.AddWithValue("@Particulars", objbo.Particulars);
                command.Parameters.AddWithValue("@LatstupdateDate", objbo.LatstupdateDate);
                command.Parameters.AddWithValue("@enabled", objbo.enabled);
                command.Parameters.AddWithValue("@added_by", objbo.AddedBy);
                command.Parameters.AddWithValue("@modified_by", objbo.ModifiedBy);
                command.Parameters.AddWithValue("@languageId", objbo.languageid);
                command.Parameters.AddWithValue("@ImagePath", objbo.imagepath);
                command.Parameters.AddWithValue("@ip_add", objbo.ipadd);
                ExecuteNonQuery(command);
                ret= true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet SelectFront(int languageid)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_MsrClauseFront";
                command.Parameters.AddWithValue("@languageid", languageid);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception ex)
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
                command.CommandText = "PROC_MsrClauseDetailsMasterOrder_Update";
                command.Parameters.Add("@cmd", SqlDbType.NVarChar).Value = cmd;
                command.Parameters.Add("@currentColMenuLevel", SqlDbType.Decimal).Value = decimal.Parse(col_menu_level, CultureInfo.InvariantCulture);
                command.Parameters.Add("@MsrId", SqlDbType.Int).Value = int.Parse(col_parent_id, CultureInfo.InvariantCulture);
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
                command.CommandText = "Get_SequenceNo_MsrClauseDetails";
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
