using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class AccredationMasterBAL:AccredationMasterDAL
    {
        public bool InsertRecord(AccredationMasterBO objBO)
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
        public bool InsertRecordsub(AccredationMasterBO objBO)
        {
            try
            {
                return InsertSub(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAccredationDetails(AccredationMasterBO objbo)
        {
            try
            {
                return GetAllAccredationDetails(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(AccredationMasterBO objBO)
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
        public bool DeleteRecord(AccredationMasterBO objBO)
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
        public bool DeleteRecordAccredationDesc(AccredationMasterBO objBO)
        {
            try
            {
                return DeleteAccredationdesc(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecord(long languageId)
        {
            try
            {
                return Select(languageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordAccredation(long languageId, int Acc_id)
        {
            try
            {
                return SelectAccredition(languageId, Acc_id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordsub(long aMid, long languageId)
        {
            try
            {
                return Selectsub(aMid,languageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordsubselect(long aMid, long languageId)
        {
            try
            {
                return Selectsubselect(aMid, languageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
