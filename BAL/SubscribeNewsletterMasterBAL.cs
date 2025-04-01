using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class SubscribeNewsletterMasterBAL : SubscribeNewsletterMasterDAL
    {
        public bool InsertRecord(SubscribeNewsletterMasterBO objBO)
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
        public DataSet SelectAllRecord()
        {
            try
            {
                return SelectAll();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecord(SubscribeNewsletterMasterBO objBO)
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
    }
}
