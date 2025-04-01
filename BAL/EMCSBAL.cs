using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class EMCSBAL : EMCSDAL
    {
        public bool InsertRecord(EMCSBO objBO)
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
        public bool UpdateRecord(EMCSBO objBO, DataTable dt)
        {
            try
            {
                return Update(objBO, dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(EMCSBO objBO)
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
        public DataSet SelectRecord(EMCSBO objBO)
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
        public DataSet SelectRecordSidemenu(int LanguageId)
        {
            try
            {
                return SelectSidemenu(LanguageId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectFacilityInECMSDetails(int LanguageId)
        {
            try
            {
                return SelectFacilityInECMS(LanguageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
