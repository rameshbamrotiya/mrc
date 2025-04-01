using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class FloorDirectoryBAL:FloorDirectoryDAL
    {
        public bool InsertRecord(FloorDirectoryBO objBO)
        {
            try
            {
                return InsertFloorDirectory(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectFloorDetailsById(FloorDirectoryBO objBO)
        {
            try
            {
                return GetFloorDetailsById(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(FloorDirectoryBO objBO)
        {
            try
            {
                return UpdateSchemeRecord(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(FloorDirectoryBO objBO)
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
