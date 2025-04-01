using System;
using System.Data;
using System.Data.SqlClient;
using BO;

namespace DAL
{
    public class StudentFamilyDetailsDAL : DBConnection
    {
        protected bool InsertStudentFamilyDetails(StudentFamilyDetailsBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateStudentFamilyDetails";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@StudentId", objbo.StudentId);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
                command.Parameters.AddWithValue("@MemberName", objbo.MemberName);
                command.Parameters.AddWithValue("@Age", objbo.Age);
                command.Parameters.AddWithValue("@RelationId", objbo.RelationId);
                command.Parameters.AddWithValue("@Occupation", objbo.Occupation != null ? objbo.Occupation : "");
                command.Parameters.AddWithValue("@MonthlyIncome", objbo.MonthlyIncome);
                command.Parameters.AddWithValue("@IsVisible", objbo.IsVisible);
                command.Parameters.AddWithValue("@UserName", "admin");
                command.Parameters.AddWithValue("@FamilyContactNumber", objbo.FamilyContactNumber);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet SelectAllStudentFamilyDetails()
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentFamilyDetails";
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteStudentFamilyDetails(StudentFamilyDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "RemoveStudentFamilyDetails";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@UserName", "Admin");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectStudentFamilyDetailsById(StudentFamilyDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetStudentFamilyDetailsById";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectStudentRegistrationDetailsByStudentId(StudentFamilyDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetStudentRegistrationDetailsByStudentId";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectStudentWorkflowByStudentId(StudentFamilyDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetStudentWorkflowByStudentId";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectStudentVerificationByStudentIdandCourseId(int strStudentId, int strCourseId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetStudentVerificationByStudentIdandCourseId";
                command.Parameters.AddWithValue("@StudentId", strStudentId);
                command.Parameters.AddWithValue("@CourseId", strCourseId);
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
