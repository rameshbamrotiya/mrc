using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using BO;

namespace DAL
{
    public class ParaMedicalDAL:DBConnection
    {
        protected bool InsertParaMedicalCourse(ParamedicalBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertParaMedicalCourseDetails";
                command.Parameters.AddWithValue("@Coursename", objbo.CourseName);
                command.Parameters.AddWithValue("@languageId", objbo.languageId);
                command.Parameters.AddWithValue("@coursecode", objbo.coursecode);
                command.Parameters.AddWithValue("@Totalseats", objbo.Totalseats);
                command.Parameters.AddWithValue("@Fees", objbo.Fees);
                command.Parameters.AddWithValue("@CourseDuration", objbo.CourseDuration);
                command.Parameters.AddWithValue("@Description", objbo.description);
                command.Parameters.AddWithValue("@ImagePath", objbo.ImagePath);
                command.Parameters.AddWithValue("@enabled", objbo.enabled);
                command.Parameters.AddWithValue("@added_by", objbo.AddedBy);
                command.Parameters.AddWithValue("@modified_by", objbo.ModifiedBy);
                command.Parameters.AddWithValue("@ip_add", objbo.ipadd);
                

                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool Update(ParamedicalBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ParaMedicalMaster_Update";
                command.Parameters.AddWithValue("@courseid", objbo.courseid);
                command.Parameters.AddWithValue("@Coursename", objbo.CourseName);
                command.Parameters.AddWithValue("@languageId", objbo.languageId);
                command.Parameters.AddWithValue("@coursecode", objbo.coursecode);
                command.Parameters.AddWithValue("@Fees", objbo.Fees);
                command.Parameters.AddWithValue("@Totalseats", objbo.Totalseats);
                command.Parameters.AddWithValue("@CourseDuration", objbo.CourseDuration);
                command.Parameters.AddWithValue("@Description", objbo.description);
                command.Parameters.AddWithValue("@ImagePath", objbo.ImagePath);
                command.Parameters.AddWithValue("@enabled", objbo.enabled);
                command.Parameters.AddWithValue("@added_by", objbo.AddedBy);
                command.Parameters.AddWithValue("@modified_by", objbo.ModifiedBy);
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
        protected DataSet SelectAllCourse(ParamedicalBO objbo)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllParaMEdicalCourses";
                command.Parameters.AddWithValue("@languageid", objbo.languageId);

                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectCourseByIDAndLanguage(ParamedicalBO objbo)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GETParaMedicalCoursesByIDAndLanguage";
                command.Parameters.AddWithValue("@languageid", objbo.languageId);
                command.Parameters.AddWithValue("@courseid", objbo.languageId);
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
