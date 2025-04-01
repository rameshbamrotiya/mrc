using System;
using System.Data;
using System.Data.SqlClient;
using BO;
using System.Globalization;

namespace DAL
{
    public class StudentGenerateMeritNumberDAL : DBConnection
    {
        protected DataTable GetAllStudentForGenerateMeritNumbersList(long CourseId,string strCondition="")
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentDetailsForGenerateMeritNo";
                command.Parameters.AddWithValue("@MainCourseId", CourseId);
                command.Parameters.AddWithValue("@OrderBy", string.IsNullOrWhiteSpace(strCondition)?"": strCondition);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataTable GetAllMeritListByCourse()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllMeritListByCourse";
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool UpdateStudentRegMeritNo(long Id, long MeritNo, string MeritNoRefNo, string Username)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateStudentRegistrationDetailsMeritNo";
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@MeritNo", MeritNo);
                command.Parameters.AddWithValue("@MeritNoRefNo", MeritNoRefNo);
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


        protected bool UpdateStudentRegGroupName(long Id, string GroupName, string GroupRefNo, string Username)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateStudentRegistrationDetailsGroupName";
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@GroupName", GroupName);
                command.Parameters.AddWithValue("@GroupRefNo", GroupRefNo);
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

        protected void InsertLog(long RefId, bool IsFilterOrSort, string ColumnName, string ColumnValue)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertMeritFilterSortHistryLogMaster";
                command.Parameters.AddWithValue("@RefId", RefId);
                command.Parameters.AddWithValue("@IsFilterOrSort", IsFilterOrSort);
                command.Parameters.AddWithValue("@ColumnName", ColumnName);
                command.Parameters.AddWithValue("@ColumnValue", ColumnValue);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
