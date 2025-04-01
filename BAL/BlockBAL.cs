using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
   public class BlockBAL:BlockDAL
    {
        public bool InsertRecord(BlockBO objBO)
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
        public bool UpdateRecord(BlockBO objBO)
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
        public DataSet SelectBlockDetailsById(BlockBO objBO)
        {
            try
            {
                return GetBlockDetailsByid(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectBlockByLanguage(BlockBO objBO)
        {
            try
            {
                return GetBlockLanguageWise(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(BlockBO objBO)
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
