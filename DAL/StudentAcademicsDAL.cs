using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class StudentAcademicsDAL : DBConnection
    {
        protected bool InsertOrUpdateEducationDetails(StudentAcademicsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateStudentEducationDetails";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@StudentId", objbo.StudentId);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
                command.Parameters.AddWithValue("@EducationId", objbo.EducationId);
                command.Parameters.AddWithValue("@EducationTypeId", objbo.EducationTypeId);
                command.Parameters.AddWithValue("@NameOfSchoolCollege", objbo.NameOfSchoolCollege);
                command.Parameters.AddWithValue("@BoardUniversity", objbo.BoardUniversity);
                command.Parameters.AddWithValue("@PassingYear", objbo.PassingYear);
                command.Parameters.AddWithValue("@Stream", objbo.Stream);
                command.Parameters.AddWithValue("@PercentageOrPercentile", objbo.PercentageOrPercentile);
                command.Parameters.AddWithValue("@Division", objbo.Division);
                command.Parameters.AddWithValue("@NoOfTrials", objbo.NoOfTrials);
                command.Parameters.AddWithValue("@IsVisible", 1);
                command.Parameters.AddWithValue("@UserName", objbo.UserName);
                command.Parameters.AddWithValue("@PassingMonth", objbo.PassingMonth);
                command.Parameters.AddWithValue("@MarksheetPath", objbo.MarksheetPath);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objbo.IsExist > 0) return false; else return true;
                //ExecuteNonQuery(command);                
                //return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected bool InsertOrUpdatExtraCertification(StudentExtraCertificationBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateStudentExtraCertification";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@CandidateId", objbo.CandidateId);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
                command.Parameters.AddWithValue("@CourseTitle", objbo.CourseTitle);
                command.Parameters.AddWithValue("@Duration", objbo.Duration);
                command.Parameters.AddWithValue("@InstituteName", objbo.InstituteName);
                command.Parameters.AddWithValue("@CITY", objbo.CITY);
                command.Parameters.AddWithValue("@IsVisible", objbo.IsVisible);
                command.Parameters.AddWithValue("@UserName", objbo.UserName);

                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertOrUpdatLanguageDetails(StudentLanguageBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertStudentLanguageDetails";
                command.Parameters.AddWithValue("@LanguageID", objbo.LanguageID);
                command.Parameters.AddWithValue("@CanRead", objbo.CanRead);
                command.Parameters.AddWithValue("@CanSpeak", objbo.CanSpeak);
                command.Parameters.AddWithValue("@Canwrite", objbo.Canwrite);
                command.Parameters.AddWithValue("@CandidateId", objbo.CandidateId);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
                command.Parameters.AddWithValue("@RegisrationId", objbo.RegisrationId);
                command.Parameters.AddWithValue("@Userid", objbo.Userid);

                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool UpdateComputerLiteracyDetails(StudentAcademicsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateCandidateDetailsForComputerLiteracy";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
                command.Parameters.AddWithValue("@IsComputerLiteracy", objbo.IsComputerLiteracy);
                command.Parameters.AddWithValue("@ComputerDetails", objbo.ComputerDetails);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetExtraCertificationDetails(StudentExtraCertificationBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentExtraCertificationDetails";
                command.Parameters.AddWithValue("@CandidateId", objbo.CandidateId);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectEducationType(int courseId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllEducationTypeMasterForAdmission";
                command.Parameters.AddWithValue("@CourseId", courseId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelecteducationName()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllAdmissionEducationMaster";
                //command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet SelecteducationNameByCourse(int CourseID)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllAdmissionEducationMasterByCourseID";
                command.Parameters.AddWithValue("@CourseId", CourseID);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectAllAcademicDetails(StudentAcademicsBO objBO)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentAcademicDetails";
                command.Parameters.AddWithValue("@CandidateId", objBO.StudentId);
                command.Parameters.AddWithValue("@CourseId", objBO.CourseId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet SelectMenuRecord(StudentAcademicsBO objBO)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "StudentLanguageDetailsByStudentId";
                command.Parameters.AddWithValue("@StudentId", objBO.StudentId);
                command.Parameters.AddWithValue("@CourseId", objBO.CourseId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectExtraDetailsRecord(StudentAcademicsBO objBO)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetExtraStudentRegistrationDetailsByStudentId";
                command.Parameters.AddWithValue("@StudentId", objBO.StudentId);
                command.Parameters.AddWithValue("@CourseId", objBO.CourseId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteStudentEducationDetails(StudentAcademicsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "RemoveStudentEducationDetails";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@UserName", objbo.UserName);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteStudentExtraCertification(StudentAcademicsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "RemoveStudentExtraCertification";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@UserName", objbo.UserName);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectAllEducationAcademicDetails(StudentAcademicsBO objBO)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentEducationDetails";
                command.Parameters.AddWithValue("@StudentId", objBO.StudentId);
                command.Parameters.AddWithValue("@CourseId", objBO.CourseId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertOrUpdateEducationDocDetails(StudentAcademicsDocumentBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateStudentEducationDocDetails";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@StudentId", objbo.StudentId);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
                command.Parameters.AddWithValue("@EducationTypeId", objbo.EducationTypeId);
                command.Parameters.AddWithValue("@DocName", objbo.DocName);
                command.Parameters.AddWithValue("@UserName", objbo.UserName);
                command.Parameters.AddWithValue("@DocPath", objbo.DocPath);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objbo.IsExist > 0) return false; else return true;
                //ExecuteNonQuery(command);                
                //return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected DataSet SelectAllAcademicDocDetails(StudentAcademicsDocumentBO objBO)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentAcademicDocDetails";
                command.Parameters.AddWithValue("@CandidateId", objBO.StudentId);
                command.Parameters.AddWithValue("@CourseId", objBO.CourseId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteStudentEducationDocDetails(StudentAcademicsDocumentBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "RemoveStudentEducationDocDetails";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@UserName", objbo.UserName);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectEducationDocType(StudentAcademicsDocumentBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentAcademicDocRequire";
                //command.Parameters.AddWithValue("@CandidateId", objbo.StudentId);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectEducationDocTypeCheck(StudentAcademicsDocumentBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentAcademicDocRequireCheck";
                //command.Parameters.AddWithValue("@CandidateId", objbo.StudentId);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
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
