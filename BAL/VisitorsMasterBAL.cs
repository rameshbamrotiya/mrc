using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class VisitorsMasterBAL:VisitorsMasterDAL
    {
        public bool InsertRecord(VisitorsMasterBO objBO)
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
        public bool InsertRecordFacilities(VisitorsMasterBO objBO)
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
        public DataSet SelectRecord(long lgLangId)
        {
            try
            {
                return Select(lgLangId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordFacility(long VisitorId, long lgLangId)
        {
            try
            {
                return SelectFacility(VisitorId, lgLangId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordFacilityEdit(long Id, long lgLangId)
        {
            try
            {
                return SelectFacilityEdit(Id, lgLangId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(int Id,int UserId)
        {
            try
            {
                return Delete(Id, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordFacilityFront(long lgLangId)
        {
            try
            {
                return SelectFacilityFront(lgLangId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
