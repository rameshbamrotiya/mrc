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
    public class StudentEducationQualificationDAL : DBConnection
    {

        protected DataTable SelectAll()
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentEducationQualificationMaster";
                //command.Parameters.AddWithValue("@col_menu_id", objbo.parentid);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataTable SelectAllEducationType()
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentEducationTypeMaster";
                //command.Parameters.AddWithValue("@col_menu_id", objbo.parentid);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool Remove(long Id, string Username)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "RemoveStudentEducationQualificationMaster";
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

        protected bool InsertOrUpdate(StudentEducationQualificationBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateStudentEducationQualificationMaster";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@EducationDetailName", objbo.EducationDetailName);
                command.Parameters.AddWithValue("@EducationType", objbo.EducationType);
                command.Parameters.AddWithValue("@Preference", objbo.Preference);
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
