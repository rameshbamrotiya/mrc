using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class PatinetCareSubCategoryBAL:PatientCareSubCategoryDAL
    {
        public bool InsertRecord(PatientCareSubCategoryBO objBO,DataTable dt)
        {
            try
            {
                return InsertPatientCareSubCategory(objBO,dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(PatientCareSubCategoryBO objBO)
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
        public bool DeleteRecordImage(PatientCareSubCategoryBO objBO)
        {
            try
            {
                return DeleteImage(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(PatientCareSubCategoryBO objBO,DataTable dt)
        {
            try
            {
                return UpdatePatientCareSubCategory(objBO,dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectCategoryById(PatientCareSubCategoryBO objBO)
        {
            try
            {
                return GetPatientCareSubCategoryByID(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectCategoryLanguagewise(PatientCareSubCategoryBO objBO)
        {
            try
            {
                return GetSubCategoryLanguagewise(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
