using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class ContactUsMasterBAL : ContactUsMasterDAL
    {
        public bool InsertRecord(ContactUsMasterBO objBO)
        {
            try
            {
                return InsertContactUs(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(ContactUsMasterBO objBO)
        {
            try
            {
                return UpdateContactUs(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectContactUsDetails()
        {
            try
            {
                return PGetAllContactUsMasterDetails();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectContactUsDetailFront(long lgLongId)
        {
            try
            {
                return GetAllContactUsMasterDetails(lgLongId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectContactUsDetailsByID(ContactUsMasterBO objbo)
        {
            try
            {
                return GetContactUsDetailsByID(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(ContactUsMasterBO objBO)
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
