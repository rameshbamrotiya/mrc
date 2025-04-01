using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
   public class FloorMasterBAL: FloorMasterDAL
    {
        public bool InsertRecord(FloorMasterBO objBO)
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
        public bool UpdateRecord(FloorMasterBO objBO)
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
        public DataSet SelectFloorDetailsById(FloorMasterBO objBO)
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
        public DataSet SelectFloorByLanguage(FloorMasterBO objBO)
        {
            try
            {
                return GetFloorLanguageWise(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(FloorMasterBO objBO)
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
