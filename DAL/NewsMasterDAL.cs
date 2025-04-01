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
    public class NewsMasterDAL : DBConnection
    {
        protected bool Insert(NewsMasterBO objBO)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objBO, ref command, "PROC_NewsMaster_Insert");
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objBO.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objBO.IsExist > 0) return false; else return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Update(NewsMasterBO objBO)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objBO, ref command, "PROC_NewsMaster_Update");
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objBO.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objBO.IsExist > 0) return false; else return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(NewsMasterBO objBO)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                //CreateParameters(objBO, ref command, "PROC_NewsMaster_Delete");
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_NewsMaster_Delete";
                command.Parameters.AddWithValue("@news_id", objBO.news_id);
                //command.Parameters.AddWithValue("@Language_id", objBO.Language_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(NewsMasterBO objBO)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_NewsMaster_Select";
                command.Parameters.AddWithValue("@news_id", objBO.news_id);
                command.Parameters.AddWithValue("@Language_id", objBO.Language_id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet NewsType()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_NewsType_SelectAll";
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
