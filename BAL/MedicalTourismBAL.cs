using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class MedicalTourismBAL:MedicalTourismDAL
    {
        public bool InsertRecord(MedicalTourismBO objBO)
        {
            try
            {
                return Insert(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(MedicalTourismBO objBO)
        {
            try
            {
                return Update(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(MedicalTourismBO objBO)
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
        public DataSet SelectRecord(MedicalTourismBO objBO)
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
        public bool InsertMTDocRecord(MedicalTourismDocumentBO objBO)
        {
            try
            {
                return InsertMTDoc(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateMTDocRecord(MedicalTourismDocumentBO objBO)
        {
            try
            {
                return UpdateMTDoc(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteMTDocRecord(MedicalTourismDocumentBO objBO)
        {
            try
            {
                return DeleteMTDoc(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectMTDocRecord(MedicalTourismDocumentBO objBO)
        {
            try
            {
                return SelectMTDoc(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetMTDocDetailsById(MedicalTourismDocumentBO objBO)
        {
            try
            {
                return GetMTDocDetailsByMTDocId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool InsertFacilitiesRecord(MedicalTourismFacilitiesBO objBO)
        {
            try
            {
                return InsertFacilities(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateFacilitiesRecord(MedicalTourismFacilitiesBO objBO)
        {
            try
            {
                return UpdateFacilities(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteFacilitiesRecord(MedicalTourismFacilitiesBO objBO)
        {
            try
            {
                return DeleteFacilities(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectFacilitiesRecord(MedicalTourismFacilitiesBO objBO)
        {
            try
            {
                return SelectFacilities(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAccrodianSubRecordFront(int LanguageId)
        {
            try
            {
                return SelectAccrodianSubFront(LanguageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetIntroductionDetailsByLanguage(MedicalTourismDocumentBO objBO)
        {
            try
            {
                return GetIntroductionDetailsByLanguageId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectDocumentRecord(MedicalTourismDocumentBO objBO)
        {
            try
            {
                return SelectDocument(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetFacilitiesDetailsByLanguage(MedicalTourismDocumentBO objBO)
        {
            try
            {
                return GetFacilitiesDetailsByLanguageId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
