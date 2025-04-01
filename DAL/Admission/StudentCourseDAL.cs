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
    public class StudentCourseDAL : DBConnection
    {

        protected DataTable SelectAll()
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentCourseMaster";
                //command.Parameters.AddWithValue("@col_menu_id", objbo.parentid);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataTable GetAllStudentRegistrationPaymentStatus()
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentRegistrationPaymentStatus";
                //command.Parameters.AddWithValue("@col_menu_id", objbo.parentid);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataTable SelectAllSub(long Id)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentCourseSubNameMasterById";
                command.Parameters.AddWithValue("@Id", Id);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataTable SelectAllStudentCourseConfiguration()
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentCourseConfiguration";
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
                command.CommandText = "RemoveStudentCourseMaster";
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

        protected bool RemoveSub(long Id, string Username)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "RemoveStudentCourseSubNameMaster";
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
        protected bool InsertOrUpdate(StudentCourseBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateStudentCourseMaster";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@Name", objbo.Name);
                command.Parameters.AddWithValue("@Type", objbo.Type);
                command.Parameters.AddWithValue("@Code", objbo.Code);
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
        protected bool InsertOrUpdateSub(StudentSubCourseBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateStudentCourseSubNameMaster";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@CourseName", objbo.CourseName);
                command.Parameters.AddWithValue("@CourseCode", objbo.CourseCode);
                command.Parameters.AddWithValue("@TotalSeat", objbo.TotalSeat);
                command.Parameters.AddWithValue("@CourseDuration", objbo.CourseDuration);

                command.Parameters.AddWithValue("@Information", objbo.Information);
                command.Parameters.AddWithValue("@ImagePath", objbo.ImagePath);
                command.Parameters.AddWithValue("@FeesDescription", objbo.FeesDescription);
                command.Parameters.AddWithValue("@CourseNote", objbo.CourseNote);

                command.Parameters.AddWithValue("@StudentCourseId", objbo.StudentCourseId);
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
		protected DataTable GetStudentEducationQualificationById(long lgId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetStudentEducationQualificationMasterDetailsByAddId";
                command.Parameters.AddWithValue("@Id", lgId);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool RemoveStudentEducationQualificationById(long CourseId)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "RemoveStudentEducationQualificationMasterDetails";
                command.Parameters.AddWithValue("@CourseId", CourseId);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        protected bool InsertStudentEducationQualification(long CourseId,long QualificationId)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertStudentEducationQualificationMasterDetails";
                command.Parameters.AddWithValue("@CourseId", CourseId);
                command.Parameters.AddWithValue("@QualificationId", QualificationId);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        protected DataTable GetMinimumEducationTypeDetailsById(long lgId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetStudentMinimumEducationTypeDetailsByAddId";
                command.Parameters.AddWithValue("@Id", lgId);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool RemoveMinimumEducationTypeDetailsById(long CourseId)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "RemoveStudentMinimumEducationTypeDetails";
                command.Parameters.AddWithValue("@CourseId", CourseId);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        protected bool InsertMinimumEducationTypeDetails(long CourseId,long QualificationId)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertStudentMinimumEducationTypeDetails";
                command.Parameters.AddWithValue("@CourseId", CourseId);
                command.Parameters.AddWithValue("@QualificationTypeId", QualificationId);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        protected DataSet GetActiveStudentCourseMaster()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllActiveStudentCourseMaster";
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet StudentCheckFinalSubmitFlag(long StudentId, long CourseId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "CheckStudentRegistrationDetailsForFinalSubmit";
                command.Parameters.AddWithValue("@StudentId", StudentId);
                command.Parameters.AddWithValue("@CourseId", CourseId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertOrUpdateCourseConfiguration(StudentCourseConfigurationBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateStudentCourseConfiguration";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
                command.Parameters.AddWithValue("@StudentAdvertisementId", objbo.StudentAdvertisementId);
                command.Parameters.AddWithValue("@EntryFees", objbo.EntryFees);                
                command.Parameters.AddWithValue("@MinAge", objbo.MinAge);
                command.Parameters.AddWithValue("@StartDate", objbo.StartDate);
                command.Parameters.AddWithValue("@EndDate", objbo.EndDate);                
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
        protected bool RemoveCourseConfiguration(long Id, string Username)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "RemoveStudentCourseConfiguration";
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
    }
}
