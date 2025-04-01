using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class MedicalTourismAccordionMasterBAL:MedicalTourismAccordionMasterDAL
    {
        public bool InsertRecord(MedicalTourismAccordionMasterBO objBO, DataTable dt)
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
        public bool UpdateRecord(MedicalTourismAccordionMasterBO objBO, DataTable dt)
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
        public bool DeleteRecord(MedicalTourismAccordionMasterBO objBO)
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
        public DataSet SelectRecord(MedicalTourismAccordionMasterBO objBO)
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
        public DataSet SelectRecordMTADetails(MedicalTourismAccordionSubMasterBO objBO)
        {
            try
            {
                return SelectSubMTADetails(objBO);
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
        public DataSet FillMTATypeById(int langId, int TypeId)
        {
            try
            {
                return FillMTATypeId(langId, TypeId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet FillMTAType(int langId)
        {
            try
            {
                return GetMTAType(langId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public bool InsertSubMTARecord(MedicalTourismAccordionSubMasterBO objBO)
        //{
        //    try
        //    {
        //        return InsertSubMTA(objBO);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public bool UpdateSubMTARecord(MedicalTourismAccordionSubMasterBO objBO)
        {
            try
            {
                return UpdateSubMTA(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetSubMTAGrid(int MTADetailId)
        {
            try
            {
                return GetSubMTAGridData(MTADetailId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectSubMTARecord(int Id)
        {
            try
            {
                return SelectSubMTA(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteSubMTARecord(MedicalTourismAccordionSubMasterBO objBO)
        {
            try
            {
                return DeleteSubMTA(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
