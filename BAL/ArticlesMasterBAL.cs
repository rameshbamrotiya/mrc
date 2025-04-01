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
    public class ArticlesMasterBAL :ArticlesMasterDAL
    {
        public bool InsertRecord(ArticlesMasterBO objBO)
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
        public bool UpdateRecord(ArticlesMasterBO objBO)
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
        public bool DeleteRecord(ArticlesMasterBO objBO)
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
        public DataSet SelectRecord(ArticlesMasterBO objBO)
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
        public DataSet SelectRecordArticleDepartment(ArticlesMasterBO objBO)
        {
            try
            {
                return SelectArticleDepartment(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordArticleType(ArticlesMasterBO objBO)
        {
            try
            {
                return SelectArticleType(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordPublicationType(ArticlesMasterBO objBO)
        {
            try
            {
                return SelectPublicationType(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
