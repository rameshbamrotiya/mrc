using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using ClassLib.BO;
using ClassLib.DAL;

namespace ClassLib.BL
{
    public class FacilityInECMSDetailsBAL : FacilityInECMSDetailsDAL
    {

        #region Constructor

        public FacilityInECMSDetailsBAL()
        {
        }

        #endregion Constructors

        #region Insert method  
        public bool InsertRecord(FacilityInECMSDetailsBO objFacilityInECMSDetails , DataTable dt)
        {
            try
            {
                return Insert(objFacilityInECMSDetails,dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Update method  
        public bool UpdateRecord(FacilityInECMSDetailsBO objFacilityInECMSDetails, DataTable dt)
        {
            try
            {
                return Update(objFacilityInECMSDetails,dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Delete method  
        public bool DeleteRecord(FacilityInECMSDetailsBO objFacilityInECMSDetails)
        {
            try
            {
                return Delete(objFacilityInECMSDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Select method  
        public DataSet SelectRecord(FacilityInECMSDetailsBO objFacilityInECMSDetails)
        {
            try
            {
                return SelectById(objFacilityInECMSDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectFacilityDetailsRecord(int Id )
        {
            try
            {
                return SelectFacilityDetailsById(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectFacilityDetailsRecordByFIEMID(int Id)
        {
            try
            {
                return SelectFacilityDetailsByFIEMID(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateFacilityDetailsRecord(FacilityInECMSDetailsBO objBO)
        {
            try
            {
                return UpdateFacilityDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Select All method  
        public DataTable SelectAllRecord()
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
        #endregion

    }
}
