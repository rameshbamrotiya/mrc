using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IScheduleNewsLetterRepository : IDisposable
    {
        #region Main
        List<GetAllNewsLetterSubscriberResult> GetAllNewsLetterSubscriber();

        List<GetAllScheduleNewsLetterMasterResult> GetAllScheduleNewsLetterMaster();

        List<GetAllSendEMailNewsLetterSubscriberResult> GetAllSendEMailNewsLetterSubscriber();

        GetAllScheduleNewsLetterMasterResult GetScheduleNewsLetterMaster(long lgid);

        bool InsertOrUpdateScheduleNewsLetterMaster(GetAllScheduleNewsLetterMasterResult objData, out string strError);

        bool RemoveScheduleNewsLetterMaster(int lgId, out string strError);
        #endregion

        #region Main Log
        List<GetAllScheduleNewsLetterMasterLogResult> GetAllScheduleNewsLetterMasterLog();

        bool InsertScheduleNewsLetterMasterLog(GetAllScheduleNewsLetterMasterLogResult objData, out string strError);
        #endregion

        #region Main Email Log
        List<GetAllScheduleNewsLetterMasterEmailLogResult> GetAllScheduleNewsLetterMasterEmailLog(long lgId);

        bool InsertScheduleNewsLetterMasterEmailLog(GetAllScheduleNewsLetterMasterEmailLogResult objData, out string strError);
        #endregion
    }
}
