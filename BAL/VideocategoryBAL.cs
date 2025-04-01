using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class VideocategoryBAL:VideocategoryDAL
    {
        public bool InsertRecord(VideocategoryBO objBO)
        {
            try
            {
                return InsertBlock(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(VideocategoryBO objBO)
        {
            try
            {
                return UpdateBock(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectBlockDetailsById(VideocategoryBO objBO)
        {
            try
            {
                return GetBlockDetailsByid(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(VideocategoryBO objBO)
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
        public DataSet SelectBlockByLanguage(VideocategoryBO objBO)
        {
            try
            {
                return GetBlockLanguageWise(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
