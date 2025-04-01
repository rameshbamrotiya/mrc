using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class NursingCareBAL : NursingCareDAL
    {
        public bool InsertRecord(NursingCareBO objBO, DataTable dt)
        {
            try
            {
                return InsertNursingCare(objBO, dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(NursingCareBO objBO, DataTable dt)
        {
            try
            {
                return UpdateNursingCare(objBO, dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectNursingCareDetails()
        {
            try
            {
                return GetAllNursingCareDetails();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectNursingCareDetailsByID(NursingCareBO objbo)
        {
            try
            {
                return GetNursingCareDetailsByID(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(NursingCareBO objBO)
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
        public DataSet GetImageGrid(int Id)
        {
            try
            {
                return GetImageGridData(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
