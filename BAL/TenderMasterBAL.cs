using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BO;
using BAL;
using System.Data;

namespace BAL
{
    public class TenderMasterBAL : TenderMasterDAL
    {
        public DataSet Documents_SelectByTenderID(TenderMasterBO objBO)
        {
            try
            {
                return SelectByTenderID(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DataSet Documents_Select(TenderMasterBO objBO)
        {
            try
            {
                return DocumentsSelect(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DataSet TenderMaster_SelectByTenderID(TenderMasterBO objBO)
        {
            try
            {
                return TenderMasterSelectByTenderID(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool Insert_TenderMaster(TenderMasterBO objBO)
        {
            try
            {
                return InsertTenderMaster(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update_TenderMaster(TenderMasterBO objBO)
        {
            try
            {
                return UpdateTenderMaster(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool Insert_TenderDocument(TenderMasterBO objBO)
        {
            try
            {
                return InsertTenderDocument(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Insert_TenderDocumentDetail(string DocumentDetail, int TenderID)
        {
            try
            {
                return InsertTenderDocumentDetail(DocumentDetail, TenderID);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool DeleteRecord(TenderMasterBO objBO)
        {
            try
            {
                return DeleteRecords(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool Delete_Document(int DocID)
        {
            try
            {
                return DeleteDocument(DocID);
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
