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
    public class CallUsMasterBAL : CallUsMasterDAL
    {
        public bool InsertRecord(CallUsMasterBO objBO)
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
        public bool UpdateRecord(CallUsMasterBO objBO)
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
        public bool DeleteRecord(CallUsMasterBO objBO)
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
        public DataSet SelectRecord(CallUsMasterBO objBO)
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
        public DataSet SelectRecordCallUsType(CallUsMasterBO objBO)
        {
            try
            {
                return SelectCallUsType(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetCallUsType(CallUsMasterBO objBO)
        {
            try
            {
                return GetCallUsTypeByLanguageId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetCallUsList(CallUsMasterBO objBO)
        {
            try
            {
                return GetCallUsListById(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
