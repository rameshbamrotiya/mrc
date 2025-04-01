using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class StudentAcademicsBAL : StudentAcademicsDAL
    {
        public bool InsertOrUpdateAcademicsDetails(StudentAcademicsBO objBO)
        {
            try
            {
                return InsertOrUpdateEducationDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertOrUpdateExtraCertificationDetails(StudentExtraCertificationBO objBO)
        {
            try
            {
                return InsertOrUpdatExtraCertification(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertLanguageDetails(StudentLanguageBO objBO)
        {
            try
            {
                return InsertOrUpdatLanguageDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateCoputerDetails(StudentAcademicsBO objBO)
        {
            try
            {
                return UpdateComputerLiteracyDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetExtraCertificationDetailsByID(StudentExtraCertificationBO objBO)
        {
            try
            {
                return GetExtraCertificationDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetEducationType(int courseId)
        {
            try
            {
                return SelectEducationType(courseId).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable SelectRecordEducationName()
        {
            try
            {
                return SelecteducationName().Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable SelectRecordEducationNameByCourse( int courseId)
        {
            try
            {
                return SelecteducationNameByCourse(courseId).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetAllAcademicDetails(StudentAcademicsBO objBO)
        {
            try
            {
                return SelectAllAcademicDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet selectMenu(StudentAcademicsBO objBO)
        {
            try
            {
                return SelectMenuRecord(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet selectExtraDetails(StudentAcademicsBO objBO)
        {
            try
            {
                return SelectExtraDetailsRecord(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(StudentAcademicsBO objBO)
        {
            try
            {
                return DeleteStudentEducationDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteCourseRecord(StudentAcademicsBO objBO)
        {
            try
            {
                return DeleteStudentExtraCertification(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetAllEducationAcademicDetails(StudentAcademicsBO objBO)
        {
            try
            {
                return SelectAllEducationAcademicDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertOrUpdateAcademicsDocDetails(StudentAcademicsDocumentBO objBO)
        {
            try
            {
                return InsertOrUpdateEducationDocDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetAllAcademicDocDetails(StudentAcademicsDocumentBO objBO)
        {
            try
            {
                return SelectAllAcademicDocDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteEducationDocRecord(StudentAcademicsDocumentBO objBO)
        {
            try
            {
                return DeleteStudentEducationDocDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetEducationDocType(StudentAcademicsDocumentBO objbo)
        {
            try
            {
                return SelectEducationDocType(objbo).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetEducationDocTypeCheck(StudentAcademicsDocumentBO objbo)
        {
            try
            {
                return SelectEducationDocTypeCheck(objbo).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
