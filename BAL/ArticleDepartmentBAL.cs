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
    public class ArticleDepartmentBAL:ArticleDepartmentDAL
    {
        public bool InsertRecord(ArticleDepartmentBO objBO)
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
        public bool UpdateRecord(ArticleDepartmentBO objBO)
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
        public bool DeleteRecord(ArticleDepartmentBO objBO)
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
        public DataSet SelectRecord(ArticleDepartmentBO objBO)
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
        public DataSet SelectRecordbylanguage(int AD_id,int LanguageId)
        {
            try
            {
                return SelectByLanguage(AD_id,LanguageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectResearchDetails(int AD_id, int LanguageId)
        {
            try
            {
                return SelectResearchDetailsByLanguage(AD_id, LanguageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectResearchDetailspublicationname(int LanguageId)
        {
            try
            {
                return SelectResearchpublicationnameByLanguage(LanguageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
