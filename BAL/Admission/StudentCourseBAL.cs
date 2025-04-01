using BO.Admission;
using DAL.Admission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Admission
{
    public class StudentCourseBAL : StudentCourseDAL, IDisposable
    {

        public DataTable GetAll()
        {
            try
            {
                return SelectAll();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetAllStudentRegistrationPaymentStatusDetails()
        {
            try
            {
                return GetAllStudentRegistrationPaymentStatus();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetAllStudentCourseConfiguration()
        {
            try
            {
                return SelectAllStudentCourseConfiguration();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetSubAll(long Id)
        {
            try
            {
                return SelectAllSub(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveData(long Id, string Username)
        {
            bool ret = false;
            try
            {
                return Remove(Id, Username);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        public bool RemoveSubData(long Id, string Username)
        {
            bool ret = false;
            try
            {
                return RemoveSub(Id, Username);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        public bool InsertOrUpdateData(StudentCourseBO objbo)
        {
            bool ret = false;
            try
            {
                return InsertOrUpdate(objbo);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        public bool InsertOrUpdateSubData(StudentSubCourseBO objbo)
        {
            bool ret = false;
            try
            {
                return InsertOrUpdateSub(objbo);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

		public DataTable GetAlltEducationQualificationById(long Id)
        {
            try
            {
                return GetStudentEducationQualificationById(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveEducationQualificationById(long Id)
        {
            bool ret = false;
            try
            {
                return RemoveStudentEducationQualificationById(Id);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        public bool InsertEducationQualification(long CourseId, long QualificationId)
        {
            bool ret = false;
            try
            {
                return InsertStudentEducationQualification(CourseId, QualificationId);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        public DataTable GetAlltMinimumEducationTypeDetailsById(long Id)
        {
            try
            {
                return GetMinimumEducationTypeDetailsById(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemovetMinimumEducationTypeDetailsById(long Id)
        {
            bool ret = false;
            try
            {
                return RemoveMinimumEducationTypeDetailsById(Id);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        public bool InserttMinimumEducationTypeDetails(long CourseId, long QualificationId)
        {
            bool ret = false;
            try
            {
                return InsertMinimumEducationTypeDetails(CourseId, QualificationId);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
		public DataSet GetAllActiveStudentCourseMaster()
        {
            try
            {
                return GetActiveStudentCourseMaster();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet Student_CheckFinalSubmitFlag(long StudentId, long CourseId)
        {
            try
            {
                return StudentCheckFinalSubmitFlag(StudentId, CourseId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertOrUpdateCourseConfigurationData(StudentCourseConfigurationBO objbo)
        {
            bool ret = false;
            try
            {
                return InsertOrUpdateCourseConfiguration(objbo);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        public bool RemoveCourseConfigurationData(long Id, string Username)
        {
            bool ret = false;
            try
            {
                return RemoveCourseConfiguration(Id, Username);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        public void Dispose()
        {

        }
    }
}
