using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class HomePagePopupMasterBAL : HomePagePopupMasterDAL
    {
        public bool InsertRecord(HomePagePopupMasterBO objBO)
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
        public bool UpdateRecord(HomePagePopupMasterBO objBO)
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
        public DataSet SelectPopupDetails()
        {
            try
            {
                return GetAllDetails();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectPopupDetailsByID(HomePagePopupMasterBO objbo)
        {
            try
            {
                return GetPopupDetailsByID(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(HomePagePopupMasterBO objBO)
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
