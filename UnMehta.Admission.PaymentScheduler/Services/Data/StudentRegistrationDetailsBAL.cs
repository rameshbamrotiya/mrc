using System;
using BO;
using DAL;
using System.Data;
using BO.Admission;

namespace BAL
{
    public class StudentRegistrationDetailsBAL : StudentRegistrationDetailsDAL, IDisposable
    {
        public bool UpdateStudentRegistrationPayments(string RegId, string TxnId, float amount, string PaymentStatus)
        {
            try
            {
                return UpdateStudentRegistrationPayment(RegId, TxnId, amount, PaymentStatus);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertRecord(StudentRegistrationDetailsBO objBO, StudentRegistrationBO objRegBo)
        {
            try
            {
                return InsertStudentRegistrationDetails(objBO, objRegBo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertWorkflowRecord(StudentRegistrationDetailsBO objBO)
        {
            try
            {
                return InsertWorkflowDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertWorkflowRecordstudent(int strStudentId1, int strCourseId1, int ApplicationStatus, int PersonalInformationId, string username)
        {
            try
            {
                return InsertWorkflowDetailsstudent(strStudentId1, strCourseId1, ApplicationStatus, PersonalInformationId, username);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetAllStudentRegistrationDetails()
        {
            try
            {
                return SelectAllStudentRegistrationDetails();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateStudentRegistrationStatus(long StudentId, long CourseId, string ApplicationStatus)
        {
            try
            {
                return UpdateStudentRegistrationDetails( StudentId,  CourseId,  ApplicationStatus);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateStudentRegistrationDetailsRegIds(string RegId,string TxnId, float amount, string PaymentStatus)
        {
            try
            {
                return UpdateStudentRegistrationDetailsRegId( RegId, TxnId, amount, PaymentStatus);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteRecord(StudentRegistrationDetailsBO objBO)
        {
            try
            {
                return DeleteStudentRegistrationDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecord(StudentRegistrationDetailsBO objBO)
        {
            try
            {
                return SelectStudentRegistrationDetailsById(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetStudentRegistrationDetailPaymentStatus(string strRegId)
        {
            try
            {
                return GetStudentRegistrationDetailsPaymentStatus(strRegId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetAllRegistratedStudent()
        {
            try
            {
                return GetAllRegistratedStudentList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetAllStudentDocumentByStudentCourseId(string strRegId, string courseId)
        {
            try
            {
                return GetDocumentDetailsByStudentCourseId(strRegId, courseId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetAllRegistratedStudentforadmin()
        {
            try
            {
                return GetAllRegistratedStudentListforadmin();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public DataTable GetAllRegistratedStudentPayment(int studentRegId)
        {
            try
            {
                return GetAllRegistratedStudentListPayment(studentRegId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetAllRegistratedStudentPaymentByCourse(long studentRegId, long CourseId)
        {
            try
            {
                return GetAllRegistratedStudentListPaymentByCourse(studentRegId, CourseId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public DataSet StudentCourse(int Id)
        {
            try
            {
                return StudentCourseDetails(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet StudentCourseSelection(int Id)
        {
            try
            {
                return StudentCourseSelectionDetails(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertRecordCourse(int CourseId,int StudentId,string UserId, int CourseMasterId)
        {
            try
            {
                return InsertStudentCourseDetails(CourseId, StudentId, UserId, CourseMasterId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdatePageOrder(string cmd, string col_menu_level, string col_parent_id, string MasterCourseId)
        {
            try
            {
                return UpdateOrder(cmd, col_menu_level, col_parent_id, MasterCourseId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(int Id,int StudentId,int CourseMasterId)
        {
            try
            {
                return Delete(Id, StudentId, CourseMasterId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateStudentSelection(int fromno, int tono,int StudentId, int MasterCourseId)
        {
            try
            {
                return UpdateStudentCourseSelection(fromno, tono, StudentId, MasterCourseId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Dispose()
        {

        }
    }
}
