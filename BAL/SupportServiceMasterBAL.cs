using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class SupportServiceMasterBAL:SupportServiceMasterDAL
    {
        public bool InsertRecord(SupportServiceMasterBO objBO, out long SSID)
        {
            try
            {
                return InsertSupportservice(objBO,out SSID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteSupportImage(long lgId)
        {
            try
            {
                return DeleteSupportserviceImage(lgId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertSupportImage(long lgId,string strPath)
        {
            try
            {
                return InsertSupportserviceImage(lgId, strPath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetAllSupportImageDetails(long lgId)
        {
            try
            {
                return GetAllSupportServiceImageDetails(lgId);
            }
            catch (Exception)
            {
                throw;
            }
        }




        public DataSet SelectSupportserviceByID(SupportServiceMasterBO objBO)
        {
            try
            {
                return GetRecordBySupportserviceId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool UpdateRecord(SupportServiceMasterBO objBO)
        {
            try
            {
                return UpdateSupportserviceRecord(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(SupportServiceMasterBO objBO)
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
