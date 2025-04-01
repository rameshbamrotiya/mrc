using BO.Admission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Admission
{
    public class StudentAdvertisementDAL : DBConnection
    {

        protected DataTable SelectAll()
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentAdvertisementMaster";
                //command.Parameters.AddWithValue("@col_menu_id", objbo.parentid);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool Remove(long Id,string Username)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "RemoveStudentAdvertisementMaster";
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

        protected bool InsertOrUpdate(StudentAdvertisementBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateStudentAdvertisementMaster";
                command.Parameters.AddWithValue("@Id", objbo.Id );
                command.Parameters.AddWithValue("@Name", objbo.Name);
                command.Parameters.AddWithValue("@Code", objbo.Code);
                command.Parameters.AddWithValue("@PublishDate", objbo.PublishDate);
                command.Parameters.AddWithValue("@Desciption", objbo.Desciption);
                command.Parameters.AddWithValue("@IsVisible", objbo.IsVisible);
                command.Parameters.AddWithValue("@Username", objbo.UpdateBy);
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
