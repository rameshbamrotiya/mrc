using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class SpecialityMasterBAL : SpecialityMasterDAL
    {
        public bool InsertRecord(SpecialityMasterBO objBO, DataTable dt)
        {
            try
            {
                return Insert(objBO, dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(SpecialityMasterBO objBO, DataTable dt)
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
        public bool DeleteRecord(SpecialityMasterBO objBO)
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
        public DataSet SelectRecord(SpecialityMasterBO objBO)
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
        public DataSet SelectRecordIMG(SpecialityMasterBO objBO)
        {
            try
            {
                return SelectIMG(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordSub(SpecialityMasterBO objBO)
        {
            try
            {
                return SelectSubrecord(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateSubRecord(SpecialityMasterBO objBO)
        {
            try
            {
                return UpdateSubSpecility(objBO);
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
        public bool DeleteRecordImg(SpecialityMasterBO objBO)
        {
            try
            {
                return DeleteImg(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordSidemenu(int osid, int LanguageId)
        {
            try
            {
                return SelectSidemenu(osid,LanguageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordFacility(int osid, int LanguageId)
        {
            try
            {
                return SelectFacility(osid, LanguageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordStafDetails(int osid, int LanguageId)
        {
            try
            {
                return SelectStafDetails(osid, LanguageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
