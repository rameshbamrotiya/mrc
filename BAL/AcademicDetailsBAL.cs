using System;
using BO;
using DAL;
using System.Data;

namespace BAL
{
    public class AcademicDetailsBAL:AcademicDetailsDAL
    {
        public bool InsertRecord(AcademicDetailsBO objBO)
        {
            try
            {
                return InsertAcademicDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectMenuResourceWise()
        {
            try
            {
                return SelectResource();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetAllAcademicDetails(AcademicDetailsBO objBO)
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
        public bool DeleteRecord(AcademicDetailsBO objBO)
        {
            try
            {
                return Delete(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecord(AcademicDetailsBO objBO)
        {
            try
            {
                return Select(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordEducation(AcademicDetailsBO objBO)
        {
            try
            {
                return SelectEducation(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet selectMenu(AcademicDetailsBO objBO)
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
        public DataTable SelectRecordEducationType()
        {
            try
            {
                return SelectEducationType().Tables[0];
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
    }
}
