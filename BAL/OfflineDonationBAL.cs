using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class OfflineDonationBAL:OfflineDonationDAL
    {
        public bool InsertRecord(OfflineDonationBO objBO)
        {
            try
            {
                return InsertOfflineDonation(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectOfflineDonationByID(OfflineDonationBO objBO)
        {
            try
            {
                return GetRecordByOfflineDonationId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(OfflineDonationBO objBO)
        {
            try
            {
                return UpdateOfflineDonationRecord(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(OfflineDonationBO objBO)
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
