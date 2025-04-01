using System;
using System.Data;
using System.Data.SqlClient;
using BO;
using System.Globalization;
using BO.Admission;

namespace DAL
{
    public class StudentRegistrationDetailsDAL : DBConnection
    {
        protected bool InsertStudentRegistrationDetails(StudentRegistrationDetailsBO objbo, StudentRegistrationBO objregbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateStudentRegistrationDetails";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@FirstName", objregbo.FirstName);
                command.Parameters.AddWithValue("@MiddleName", objregbo.MiddleName);
                command.Parameters.AddWithValue("@LastName", objregbo.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", objregbo.DateOfBirth);
                command.Parameters.AddWithValue("@Email", objregbo.Email);
                command.Parameters.AddWithValue("@Gender", objregbo.Gender);
                command.Parameters.AddWithValue("@MaritalStatus", objregbo.MaritalStatus);
                command.Parameters.AddWithValue("@StudentId", objbo.StudentId);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
                command.Parameters.AddWithValue("@RegistrationId", objbo.RegistrationId);
                command.Parameters.AddWithValue("@PresentAddress", objbo.PresentAddress);
                command.Parameters.AddWithValue("@PresentPincode", objbo.PresentPincode);
                command.Parameters.AddWithValue("@PresentCountry", objbo.PresentCountry);
                command.Parameters.AddWithValue("@PresentState", objbo.PresentState);
                command.Parameters.AddWithValue("@PresentCity", objbo.PresentCity);
                command.Parameters.AddWithValue("@PresentTaluka", objbo.PresentTaluka);
                command.Parameters.AddWithValue("@PresentPhoneR", objbo.PresentPhoneR !=null ? objbo.PresentPhoneR : "");
                command.Parameters.AddWithValue("@PresentPhoneM", objbo.PresentPhoneM);
                command.Parameters.AddWithValue("@ParmenentAddress", objbo.ParmenentAddress);
                command.Parameters.AddWithValue("@ParmenentPincode", objbo.ParmenentPincode);
                command.Parameters.AddWithValue("@ParmenentCountry", objbo.ParmenentCountry);
                command.Parameters.AddWithValue("@ParmenentState", objbo.ParmenentState);
                command.Parameters.AddWithValue("@ParmenentCity", objbo.ParmenentCity);
                command.Parameters.AddWithValue("@ParmenentTaluka", objbo.ParmenentTaluka);
                command.Parameters.AddWithValue("@ParmenentPhoneR", objbo.ParmenentPhoneR !=null ? objbo.ParmenentPhoneR : "");
                command.Parameters.AddWithValue("@ParmenentPhoneM", objbo.ParmenentPhoneM);
                command.Parameters.AddWithValue("@Age", objbo.Age);
                command.Parameters.AddWithValue("@PlaceOfBirth", objbo.PlaceOfBirth !=null ? objbo.PlaceOfBirth :"");
                command.Parameters.AddWithValue("@CasteId", objbo.CasteId);
                command.Parameters.AddWithValue("@Religion", objbo.Religion);
                command.Parameters.AddWithValue("@PhotographName", objbo.PhotographName);
                command.Parameters.AddWithValue("@PhotographPath", objbo.PhotographPath);
                command.Parameters.AddWithValue("@SignatureName", objbo.SignatureName);
                command.Parameters.AddWithValue("@SignaturePath", objbo.SignaturePath);
                command.Parameters.AddWithValue("@DOBFileName", objbo.DOBFileName);
                command.Parameters.AddWithValue("@DOBFilePath", objbo.DOBFilePath);
                command.Parameters.AddWithValue("@UserName", objbo.UserName);
                command.Parameters.AddWithValue("@NamePrefix", objbo.Title);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        protected bool InsertWorkflowDetails(StudentRegistrationDetailsBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "StudentWorkflowinsertUpdate";
                command.Parameters.AddWithValue("@hfStudentDetailsId", objbo.hfStudentWorkflowId);
                command.Parameters.AddWithValue("@StudentId", objbo.StudentId);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId); 
                command.Parameters.AddWithValue("@ApplicationStatus", objbo.ApplicationStatus != null ? objbo.ApplicationStatus : "");
                command.Parameters.AddWithValue("@PersonalInformationId", objbo.PersonalInformationId != null ? objbo.PersonalInformationId : "");
                command.Parameters.AddWithValue("@PersonalInformationRemarks", objbo.PersonalInformationRemarks != null ? objbo.PersonalInformationRemarks : "");
                command.Parameters.AddWithValue("@AddressId", objbo.AddressId != null ? objbo.AddressId : "");
                command.Parameters.AddWithValue("@AddressRemarks", objbo.AddressRemarks != null ? objbo.AddressRemarks : "");
                command.Parameters.AddWithValue("@DocumentId", objbo.DocumentId != null ? objbo.DocumentId : "");
                command.Parameters.AddWithValue("@DocumentRemarks", objbo.DocumentRemarks != null ? objbo.DocumentRemarks : "");
                command.Parameters.AddWithValue("@FamilyMemberId", objbo.FamilyMemberId != null ? objbo.FamilyMemberId : "");               
                command.Parameters.AddWithValue("@FamilyMemberRemarks", objbo.FamilyMemberRemarks != null ? objbo.FamilyMemberRemarks : "");
                command.Parameters.AddWithValue("@AcademicId", objbo.AcademicId != null ? objbo.AcademicId : "");
                command.Parameters.AddWithValue("@AcademicRemarks", objbo.AcademicRemarks != null ? objbo.AcademicRemarks : "");
                command.Parameters.AddWithValue("@EducationDocId", objbo.EducationDocId != null ? objbo.EducationDocId : "");
                command.Parameters.AddWithValue("@EducationDocRemarks", objbo.EducationDocRemarks != null ? objbo.EducationDocRemarks : "");
                command.Parameters.AddWithValue("@ComputerLiteracyId", objbo.ComputerLiteracyId != null ? objbo.ComputerLiteracyId : "");
                command.Parameters.AddWithValue("@ComputerLiteracyRemarks", objbo.ComputerLiteracyRemarks != null ? objbo.ComputerLiteracyRemarks : "");
                command.Parameters.AddWithValue("@CoursesId", objbo.CoursesId != null ? objbo.CoursesId : "");
                command.Parameters.AddWithValue("@CoursesRemarks", objbo.CoursesRemarks != null ? objbo.CoursesRemarks : "");
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId != null ? objbo.LanguageId : "");
                command.Parameters.AddWithValue("@LanguageRemarks", objbo.LanguageRemarks != null ? objbo.LanguageRemarks : "");
                command.Parameters.AddWithValue("@OtherInfoId", objbo.OtherInfoId != null ? objbo.OtherInfoId : "");
                command.Parameters.AddWithValue("@OtherInfoRemarks", objbo.OtherInfoRemarks != null ? objbo.OtherInfoRemarks : "");
                command.Parameters.AddWithValue("@lawId", objbo.lawId != null ? objbo.lawId : "");
                command.Parameters.AddWithValue("@lawReamrks", objbo.lawReamrks != null ? objbo.lawReamrks : "");
                command.Parameters.AddWithValue("@EmergencyId", objbo.EmergencyId != null ? objbo.EmergencyId : "");
                command.Parameters.AddWithValue("@EmergencyRemarks", objbo.EmergencyRemarks != null ? objbo.EmergencyRemarks : "");
                command.Parameters.AddWithValue("@ReferencesId", objbo.ReferencesId != null ? objbo.ReferencesId : "");
                command.Parameters.AddWithValue("@ReferencesRemarks", objbo.ReferencesRemarks != null ? objbo.ReferencesRemarks : "");
                command.Parameters.AddWithValue("@CreateBy", objbo.UserName != null ? objbo.UserName : "");                
                command.Parameters.AddWithValue("@IP", objbo.ReferencesRemarks != null ? objbo.ReferencesRemarks : "");
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        protected bool InsertWorkflowDetailsstudent(int strStudentId1, int strCourseId1, int ApplicationStatus, int PersonalInformationId, string username)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Proc_StudentWorkflowinsertUpdate";
                command.Parameters.AddWithValue("@hfStudentDetailsId", PersonalInformationId);
                command.Parameters.AddWithValue("@StudentId", strStudentId1);
                command.Parameters.AddWithValue("@CourseId", strCourseId1);
                command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                command.Parameters.AddWithValue("@CreateBy", username);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        protected bool UpdateStudentRegistrationDetails(long StudentId,long CourseId,string ApplicationStatus)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateStudentRegistrationDetails";
                command.Parameters.AddWithValue("@StudentId", StudentId);
                command.Parameters.AddWithValue("@CourseId", CourseId);
                command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        protected bool UpdateStudentRegistrationDetailsRegId(string RegId,string TxnId,float amount, string PaymentStatus)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateStudentRegistrationDetailsRegistrationId";
                command.Parameters.AddWithValue("@RegistrationId", RegId);
                command.Parameters.AddWithValue("@TxnId", TxnId);
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@PaymentStatus", PaymentStatus);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool UpdateStudentRegistrationPayment(string RegId,string TxnId,float amount, string PaymentStatus)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateStudentRegistrationPayment";
                command.Parameters.AddWithValue("@RegistrationId", RegId);
                command.Parameters.AddWithValue("@TxnId", TxnId);
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@PaymentStatus", PaymentStatus);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        protected DataSet SelectAllStudentRegistrationDetails()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentRegistrationDetails";
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteStudentRegistrationDetails(StudentRegistrationDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "RemoveStudentRegistrationDetails";
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

        protected DataSet SelectStudentRegistrationDetailsById(StudentRegistrationDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetStudentRegistrationDetailsById";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataTable GetStudentRegistrationDetailsPaymentStatus(string strRegId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetStudentRegistrationDetailsPaymentStatusRegistrationId";
                command.Parameters.AddWithValue("@RegistrationId", strRegId);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataTable GetAllRegistratedStudentList()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllRegistratedStudentList";
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataTable GetDocumentDetailsByStudentCourseId(string strRegId,string courseId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentRegistrationDocumentForZIP";
                command.Parameters.AddWithValue("@StudentId", strRegId);
                command.Parameters.AddWithValue("@CourseId", courseId);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataTable GetAllRegistratedStudentListforadmin()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllRegistratedStudentListforadmin";
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataTable GetAllRegistratedStudentListPayment(int studentRegId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllRegistratedStudentPaymentList";
                command.Parameters.AddWithValue("@RegId", studentRegId);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataTable GetAllRegistratedStudentListPaymentByCourse(long studentRegId, long CourseId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllRegistratedStudentPaymentListByCourse";
                command.Parameters.AddWithValue("@RegId", studentRegId);
                command.Parameters.AddWithValue("@CourseId", CourseId);

                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet StudentCourseDetails(int Id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetStudentCourseDetails";
                command.Parameters.AddWithValue("@Id", Id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet StudentCourseSelectionDetails(int Id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetStudentCourseSelectionDetails";
                command.Parameters.AddWithValue("@Id", Id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertStudentCourseDetails(int CourseId,int StudentId, string UserId, int CourseMasterId)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertStudentCourseDetails";
                command.Parameters.AddWithValue("@StudentId", StudentId);
                command.Parameters.AddWithValue("@CourseId", CourseId);
                command.Parameters.AddWithValue("@UserName", UserId);
                command.Parameters.AddWithValue("@CourseMasterId", CourseMasterId);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool UpdateOrder(string cmd, string col_menu_level, string col_parent_id, string MasterCourseId)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "tbl_StudentCourseSelection_Update";
                command.Parameters.Add("@cmd", SqlDbType.NVarChar).Value = cmd;
                command.Parameters.Add("@currentColMenuLevel", SqlDbType.Decimal).Value = decimal.Parse(col_menu_level, CultureInfo.InvariantCulture);
                command.Parameters.Add("@Id", SqlDbType.Int).Value = int.Parse(col_parent_id, CultureInfo.InvariantCulture);
                command.Parameters.Add("@MasterCourseId", SqlDbType.Int).Value = int.Parse(MasterCourseId, CultureInfo.InvariantCulture);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool Delete(int Id, int StudentId, int CourseMasterId)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "StudentCourseSelectionDelete";
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@studentId", StudentId);
                command.Parameters.AddWithValue("@MastercourseId", CourseMasterId);
                ExecuteNonQuery(command);
                return true;
                //CreateParameters(objbo, ref command, "PROC_AwardMaster_Delete");
                //ExecuteNonQuery(command);
                //return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool UpdateStudentCourseSelection(int fromno, int tono,int StudentId,int MasterCourseId)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "StudentCourseSelectionUpdate";
                command.Parameters.AddWithValue("@FromNo", fromno);
                command.Parameters.AddWithValue("@ToNo", tono);
                command.Parameters.AddWithValue("@StudentId", StudentId);
                command.Parameters.AddWithValue("@MasterCourseId", MasterCourseId);
                ExecuteNonQuery(command);
                return true;
                //CreateParameters(objbo, ref command, "PROC_AwardMaster_Delete");
                //ExecuteNonQuery(command);
                //return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
