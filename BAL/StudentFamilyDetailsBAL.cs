using System;
using BO;
using DAL;
using System.Data;

namespace BAL
{
    public class StudentFamilyDetailsBAL : StudentFamilyDetailsDAL, IDisposable
    {
        public bool InsertRecord(StudentFamilyDetailsBO objBO)
        {
            try
            {
                return InsertStudentFamilyDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetAllStudentFamilyDetails()
        {
            try
            {
                return SelectAllStudentFamilyDetails();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(StudentFamilyDetailsBO objBO)
        {
            try
            {
                return DeleteStudentFamilyDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecord(StudentFamilyDetailsBO objBO)
        {
            try
            {
                return SelectStudentFamilyDetailsById(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetStudentRegistrationDetailsByStudentId(StudentFamilyDetailsBO objBO)
        {
            try
            {
                return SelectStudentRegistrationDetailsByStudentId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetStudentWorkflowByStudentId(StudentFamilyDetailsBO objBO)
        {
            try
            {
                return SelectStudentWorkflowByStudentId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetStudentVerificationByStudentIdandCourseId(int strStudentId,int strCourseId)
        {
            try
            {
                return SelectStudentVerificationByStudentIdandCourseId(strStudentId, strCourseId);
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
