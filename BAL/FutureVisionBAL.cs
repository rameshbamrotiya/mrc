using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class FutureVisionBAL:FutureVisionDAL
    {
        public bool InsertRecord(FutureVisionBO objBO, DataTable dt)
        {
            try
            {
                return InsertFutureVision(objBO, dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(FutureVisionBO objBO, DataTable dt)
        {
            try
            {
                return UpdateFutureVision(objBO, dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectFutureVisionDetails()
        {
            try
            {
                return GetAllFutureVisionDetails();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectFutureVisionDetailsByID(FutureVisionBO objbo)
        {
            try
            {
                return GetFutureVisionDetailsByID(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(FutureVisionBO objBO)
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
        public bool UpdatePageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            try
            {
                return UpdateOrder(cmd, col_menu_level, col_parent_id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SequenceNo()
        {
            try
            {
                return SelectSequenceNo();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
