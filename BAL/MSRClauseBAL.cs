using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
   public class MSRClauseBAL:MSRClauseDAL
    {
        public bool InsertRecord(MSRClauseBO objBO)
        {
            try
            {
                return InsertMSRClause(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool RemoveMsrClauseMasterDetails(MSRClauseBO objBO)
        {
            try
            {
                return RemoveMsrClauseMaster(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecord(MSRClauseBO objBO)
        {
            try
            {
                return Select(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(MSRClauseBO objBO)
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
        public DataSet SelectRecordFront(int languageid)
        {
            try
            {
                return SelectFront(languageid);
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