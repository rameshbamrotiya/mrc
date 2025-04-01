using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DocumentUploadMasterDAL : DBConnection
    {
        protected bool Insert(DocumentUploadMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_DocumentMaster_Insert");
                //command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                //objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                //if (objbo.IsExist > 0) return false; else
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Update(DocumentUploadMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_DocumentMaster_Update");
                //command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                //objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                //if (objbo.IsExist > 0) return false; else
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(DocumentUploadMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_DocumentMaster_Delete");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(DocumentUploadMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_DocumentMaster_Select");
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
