using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Model.Model.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IPatientFeedbacksRepository : IDisposable
    {
        List<PatientFeedbackGridModel> GetAllTblPatientFeedback(long lgId);

        PatientFeedbackGridModel GetTblPatientFeedbackById(int lgId);

        bool InsertOrUpdateTblPatientFeedback(PatientFeedbackGridModel objData, out string strError);

        bool RemoveTblPatientFeedback(int lgId, out string strError);
    }
}
