using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Data.Common;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IConfigDetailsRepository : IDisposable
    {
        bool GetConfigDetails(string strParaName, out ConfigDetailsModel mdlConfigDetailsModel, out string strError);

        bool GetPaymentConfigDetails(string strParaName, out ConfigDetailsModel mdlConfigDetailsModel, out string strError);

        List<GetAllCountryLangIdResult> GetAllCoutry(long lgLang = 1);

        bool InsertOrUpdateDailyVisitEntry(string userLogDescription, bool logInOrOut, out string strError);

        List<GetAllStateLangIdResult> GetAllState(long lgCountryId, long lgLang = 1);

        List<GetAllCityLangIdResult> GetAllCity(long lgStateId, long lgLang = 1);

        GetSMSTemplateByNameResult GetSMSTemplateByName(string strName);

    }
}
