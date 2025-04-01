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
    public class EventMasterBAL:EventMasterDAL
    {
        public DataSet SelectEventType(int LangId)
        {
            try
            {
                return GetEventType(LangId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertRecord(EventMasterBO objBO, DataTable dt, DataTable dtunit)
        {
            try
            {
                return Insert(objBO, dt, dtunit);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(EventMasterBO objBO, DataTable dt, DataTable dtunit)
        {
            try
            {
                return Update(objBO, dt, dtunit);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(EventMasterBO objBO)
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
        public DataSet SelectRecord(EventMasterBO objBO)
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
        public DataSet SelectRecordPatronlist(EventMasterBO objBO)
        {
            try
            {
                return SelectPatronlist(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordSocialmediaLinks(EventMasterBO objBO)
        {
            try
            {
                return SelectSocialmediaLinks(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecordPatronlist(EventMasterBO objBO)
        {
            try
            {
                return DeletePatronlist(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecordSocialmediaLinks(EventMasterBO objBO)
        {
            try
            {
                return DeleteSocialmediaLinks(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectEventFront(string EventName = "", int EventType = 0, int eventid = 0, int LanguageId = 0)
        {
            try
            {
                return SelectEventFrontByLanguage(EventName, EventType,eventid, LanguageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
