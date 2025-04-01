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
    public class FAQMasterBAL : FAQMasterDAL
    {
        public bool InsertRecord(FAQMasterBO objBO)
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
        public bool UpdateRecord(FAQMasterBO objBO)
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
        public bool DeleteRecord(FAQMasterBO objBO)
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
        public DataSet SelectRecord(FAQMasterBO objBO)
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
        public DataSet GetFAQDetailsByLanguage(FAQMasterBO objBO)
        {
            try
            {
                return GetFAQDetailsByLanguageId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertAccredationRecord(FAQAccredationMasterBO objBO)
        {
            try
            {
                return InsertAccredation(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateAccredationRecord(FAQAccredationMasterBO objBO)
        {
            try
            {
                return UpdateAccredation(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteAccredationRecord(FAQAccredationMasterBO objBO)
        {
            try
            {
                return DeleteAccredation(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAccredationRecord(FAQAccredationMasterBO objBO)
        {
            try
            {
                return SelectAccredation(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetAccredationDetailsById(FAQAccredationMasterBO objBO)
        {
            try
            {
                return GetAccredationDetailsByAccredationId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
