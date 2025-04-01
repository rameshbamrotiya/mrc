using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class ScheduleNewsLetterRepository: IScheduleNewsLetterRepository
    {

        private string SqlConnectionSTring;

        public ScheduleNewsLetterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        #region Main
        public List<GetAllNewsLetterSubscriberResult> GetAllNewsLetterSubscriber()
        {
            using (ScheduleNewsLetterMasterDataContext db = new ScheduleNewsLetterMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllNewsLetterSubscriber().ToList();
            }
        }

        public List<GetAllSendEMailNewsLetterSubscriberResult> GetAllSendEMailNewsLetterSubscriber()
        {
            using (ScheduleNewsLetterMasterDataContext db = new ScheduleNewsLetterMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllSendEMailNewsLetterSubscriber().ToList();
            }
        }

        public List<GetAllScheduleNewsLetterMasterResult> GetAllScheduleNewsLetterMaster()
        {
            using (ScheduleNewsLetterMasterDataContext db = new ScheduleNewsLetterMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllScheduleNewsLetterMaster().ToList();
            }
        }

        public GetAllScheduleNewsLetterMasterResult GetScheduleNewsLetterMaster(long lgid)
        {
            return GetAllScheduleNewsLetterMaster().Where(x => x.Id == lgid).FirstOrDefault();
        }

        public bool InsertOrUpdateScheduleNewsLetterMaster(GetAllScheduleNewsLetterMasterResult objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (ScheduleNewsLetterMasterDataContext db = new ScheduleNewsLetterMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateScheduleNewsLetterMaster(objData.Id, objData.MailSubject, objData.MailDescription, objData.DocId, objData.StartDate, objData.IsActive, SessionWrapper.UserDetails.UserName).FirstOrDefault();
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            strError = "Record Updated Successfully";
                            isError = false;
                        }
                        objData.Id = (long)dataIsDone.RecId;
                    }
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveScheduleNewsLetterMaster(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (ScheduleNewsLetterMasterDataContext db = new ScheduleNewsLetterMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveScheduleNewsLetterMaster(lgId, SessionWrapper.UserDetails.UserName);
                    db.SubmitChanges();
                    strError = "Record Removed Successfully";
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
        #endregion

        #region Main Log

        public List<GetAllScheduleNewsLetterMasterLogResult> GetAllScheduleNewsLetterMasterLog()
        {
            using (ScheduleNewsLetterMasterDataContext db = new ScheduleNewsLetterMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllScheduleNewsLetterMasterLog().ToList();
            }
        }

        public bool InsertScheduleNewsLetterMasterLog(GetAllScheduleNewsLetterMasterLogResult objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (ScheduleNewsLetterMasterDataContext db = new ScheduleNewsLetterMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertScheduleNewsLetterMasterLog(objData.ScheduleId, objData.MailSubject, objData.MailDescription, objData.DocId, objData.StartDate,objData.TriggerDateTime, objData.LogDescription).FirstOrDefault();
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            strError = "Record Updated Successfully";
                            isError = false;
                        }
                        objData.Id = (long)dataIsDone.RecId;
                    }
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        #endregion

        #region Main Email Log

        public List<GetAllScheduleNewsLetterMasterEmailLogResult> GetAllScheduleNewsLetterMasterEmailLog(long lgId)
        {
            using (ScheduleNewsLetterMasterDataContext db = new ScheduleNewsLetterMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllScheduleNewsLetterMasterEmailLog(lgId).ToList();
            }
        }

        public bool InsertScheduleNewsLetterMasterEmailLog(GetAllScheduleNewsLetterMasterEmailLogResult objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (ScheduleNewsLetterMasterDataContext db = new ScheduleNewsLetterMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertScheduleNewsLetterMasterEmailLog(objData.LogId, objData.ScheduleId, objData.MailSubject, objData.MailDescription, objData.DocId, objData.StartDate, objData.TriggerDateTime, objData.LogDescription, objData.FullName, objData.EmailId, objData.MobileNo, objData.Location, objData.flag);
                   
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        #endregion

        public void Dispose()
        {

        }

    }
}
