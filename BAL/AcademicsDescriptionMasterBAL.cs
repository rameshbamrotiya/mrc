using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class AcademicsDescriptionMasterBAL : AcademicsDescriptionMasterDAL
    {
        public bool InsertRecord(AcademicsDescriptionMasterBO objBO)
        {
            try
            {
                return InsertAcademicsDescription(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(AcademicsDescriptionMasterBO objBO)
        {
            try
            {
                return UpdateAcademicsDescription(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAcademicsDescriptionDetails()
        {
            try
            {
                return GetAllAcademicsDescriptionMasterDetails();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAcademicsDescriptionDetailsByLanguage(AcademicsDescriptionMasterBO objbo)
        {
            try
            {
                return GetAllAcademicsDescriptionByLangaugeId(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAcademicsDescriptionByID(AcademicsDescriptionMasterBO objbo)
        {
            try
            {
                return GetAcademicsDescriptionDetailsByID(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(AcademicsDescriptionMasterBO objBO)
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
    }
}
