using BO;
using DAL;
using System;
using System.Data;
namespace BAL
{
   public class PatientCareCategoryBAL:PatientCareCategoryDAL
    {
        public bool InsertRecord(PatientCareCategoryBO objBO)
        {
            try
            {
                return InsertPatientCareCategory(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(PatientCareCategoryBO objBO)
        {
            try
            {
                return UpdatePatientCareCategory(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectCategoryById(PatientCareCategoryBO objBO)
        {
            try
            {
                return GetPatientCareCategoryByID(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectCategoryLanguagewise(PatientCareCategoryBO objBO)
        {
            try
            {
                return GetCategoryLanguagewise(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(PatientCareCategoryBO objBO)
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
        public DataSet GetPatinetCareTypeDetails(PatientCareCategoryBO objBO)
        {
            try
            {
                return GetPatinetCareTypeById(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SequenceNo()
        {
            try
            {
                return SelectSequenceNo();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
