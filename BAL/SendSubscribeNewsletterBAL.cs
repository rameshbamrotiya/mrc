using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL;
using System.Data;

namespace BAL
{
    public class SendSubscribeNewsletterBAL:SendSubscribeNewsletterDAL
    {
        public bool InsertRecordDoc(SendSubscribeNewsletterDocBO objBO)
        {
            try
            {
                return InsertDoc(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecordDoc(SendSubscribeNewsletterDocBO objBO)
        {
            try
            {
                return UpdateDoc(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecordDoc(SendSubscribeNewsletterDocBO objBO)
        {
            try
            {
                return DeleteDoc(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordDoc(SendSubscribeNewsletterDocBO objBO)
        {
            try
            {
                return SelectDoc(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetSubscribe_Newsletter()
        {
            try
            {
                return GetAllSubscribeNewsletter();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetSendSubscribeNewsletterLog()
        {
            try
            {
                return GetALLSendSubscribeNewsletterLog();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetAllDocument(string spName)
        {
            try
            {
                return GetDocumentName(spName);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertRecordNewsLeter(SendSubscribeNewsletterBO objBo)
        {
            try
            {
                return InsertNewsLeter(objBo);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
