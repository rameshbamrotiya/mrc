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
    public class NursingCareAccordionTypeMasterBAL : NursingCareAccordionTypeMasterDAL
    {
        public bool InsertRecord(NursingCareAccordionTypeMasterBO objBO)
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
        public bool UpdateRecord(NursingCareAccordionTypeMasterBO objBO)
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
        public bool DeleteRecord(NursingCareAccordionTypeMasterBO objBO)
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
        public DataSet SelectRecord(NursingCareAccordionTypeMasterBO objBO)
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
        public bool InsertAccrodianSubRecord(NursingCareAccordionTypeSubMasterBO objBO)
        {
            try
            {
                return InsertAccrodianSub(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateAccrodianSubRecord(NursingCareAccordionTypeSubMasterBO objBO)
        {
            try
            {
                return UpdateAccrodianSub(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteAccrodianSubRecord(NursingCareAccordionTypeSubMasterBO objBO)
        {
            try
            {
                return DeleteAccrodianSub(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAccrodianSubRecord(NursingCareAccordionTypeSubMasterBO objBO)
        {
            try
            {
                return SelectAccrodianSub(objBO);
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
        
        public DataSet SelectRecordAccrodianType(NursingCareAccordionTypeSubMasterBO objBO)
        {
            try
            {
                return SelectAccrodianType(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
