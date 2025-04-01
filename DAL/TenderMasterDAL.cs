using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TenderMasterDAL : DBConnection
    {
        protected DataSet SelectByTenderID(TenderMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GILTender_Documents_SelectByTenderID";
                command.Parameters.AddWithValue("@TenderID", objbo.TenderID);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet DocumentsSelect(TenderMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GILTender_Documents_Select";
                command.Parameters.AddWithValue("@TenderID", objbo.TenderID);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }


        protected DataSet TenderMasterSelectByTenderID(TenderMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_TenderMaster_SelectByTenderID";
                command.Parameters.AddWithValue("@TenderID", objbo.TenderID);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool InsertTenderMaster(TenderMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_GILTender_TenderMaster_Insert");
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                command.Parameters.AddWithValue("@TenderID", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());

                if (!string.IsNullOrEmpty(Convert.ToString(command.Parameters["@TenderID"].Value)) && Convert.ToString(command.Parameters["@TenderID"].Value) != "0")
                    objbo.TenderID = int.Parse(Convert.ToString(command.Parameters["@TenderID"].Value));

                if (objbo.IsExist > 0) return false; else return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool UpdateTenderMaster(TenderMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_GILTender_TenderMaster_Update");
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objbo.IsExist > 0) return false; else return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool InsertTenderDocument(TenderMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "PROC_GILTender_Documents_Insert";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TenderID", objbo.TenderID);
                command.Parameters.AddWithValue("@DocName", objbo.DocName);
                command.Parameters.AddWithValue("@DocType", objbo.DocType);
                command.Parameters.AddWithValue("@DocPath", objbo.DocPath);
                command.Parameters.AddWithValue("@IsNewIcon", objbo.IsNewIcon);
                command.Parameters.AddWithValue("@Status", objbo.Status);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objbo.IsExist > 0) return false; else return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool InsertTenderDocumentDetail(string DocumentDetail, int TenderID)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "PROC_TenderDocumentDetail_Insert";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DocumentDetail", DocumentDetail);
                command.Parameters.AddWithValue("@TenderID", TenderID);
                int a = ExecuteNonQuery(command);
                if (a < 0) return false; else return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool DeleteRecords(TenderMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();

                command.CommandText = "RemoveTenders";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TenderId", objbo.TenderID);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool DeleteDocument(int DocID)
        {
            try
            {
                SqlCommand command = new SqlCommand();

                command.CommandText = "PROC_GILTender_Documents_Delete";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DocID", DocID);
                ExecuteNonQuery(command);
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
                command.CommandText = "PROC_TenderMasterOrder_Update";
                command.Parameters.Add("@cmd", SqlDbType.NVarChar).Value = cmd;
                command.Parameters.Add("@currentColMenuLevel", SqlDbType.Decimal).Value = decimal.Parse(col_menu_level, CultureInfo.InvariantCulture);
                command.Parameters.Add("@TenderId", SqlDbType.Int).Value = int.Parse(col_parent_id, CultureInfo.InvariantCulture);
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
                command.CommandText = "Get_SequenceNo_TenderMaster";
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
