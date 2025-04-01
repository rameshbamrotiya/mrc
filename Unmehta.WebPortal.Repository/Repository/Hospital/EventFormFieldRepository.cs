using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class EventFormFieldRepository : IEventFormFieldRepository
    {
        private string SqlConnectionSTring;
        public EventFormFieldRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllEventFormFieldMasterByEventIdResult> GetAlllongAboutUsMaster(long lgEventId)
        {
            using (EventFormFieldDataContext db = new EventFormFieldDataContext(SqlConnectionSTring))
            {
                return db.GetAllEventFormFieldMasterByEventId(lgEventId).ToList();
            }
        }
        public List<GetAllOnlineEventRegistrtionResult> GetAllOnlineEventRegistrtion(long lgEventId)
        {
            using (EventFormFieldDataContext db = new EventFormFieldDataContext(SqlConnectionSTring))
            {
                return db.GetAllOnlineEventRegistrtion().Where(x=> x.EventId==lgEventId).ToList();
            }
        }

        public bool InsertEventFormFieldMaster(GetAllEventFormFieldMasterByEventIdResult objData)
        {
            bool isError = false;
            try
            {
                using (EventFormFieldDataContext db = new EventFormFieldDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertEventFormFieldMaster(objData.EventId, objData.SequanceNo, objData.ColumnName, objData.IsVisible, false);
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            //strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            //strError = "Record Updated Successfully";
                            isError = false;
                        }
                    }
                    isError = false;
                }
        }
            catch (Exception ex)
            {
                isError = true;
                //strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveEventFormFieldMasterByEventId(int lgId)
        {
            bool isError = false;
            //strError = "";
            try
            {
                using (EventFormFieldDataContext db = new EventFormFieldDataContext(SqlConnectionSTring))
                {
                    db.RemoveEventFormFieldMasterByEventId(lgId);
                    db.SubmitChanges();
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                //strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveRecordOnlineEventRegistrtion(int lgId)
        {
            bool isError = false;
            //strError = "";
            try
            {
                using (EventFormFieldDataContext db = new EventFormFieldDataContext(SqlConnectionSTring))
                {
                    db.RemoveRecordOnlineEventRegistrtion(lgId,SessionWrapper.UserDetails.UserName);
                    db.SubmitChanges();
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                //strError = ex.ToString();
            }
            return isError;
        }

        public void Dispose()
        {

        }
    }
}
