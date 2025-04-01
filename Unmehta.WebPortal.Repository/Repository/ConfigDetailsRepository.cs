using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Data.Common;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;

namespace Unmehta.WebPortal.Repository.Repository
{
    public class ConfigDetailsRepository : IConfigDetailsRepository
    {

        private string SqlConnectionSTring;
        public ConfigDetailsRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public bool GetConfigDetails(string strParaName,out ConfigDetailsModel mdlConfigDetailsModel, out string strError)
        {
            bool isError = false;
            mdlConfigDetailsModel = new ConfigDetailsModel();
            strError = "";
            using (ConfigDetailsDataContext db = new ConfigDetailsDataContext(SqlConnectionSTring))
            {
                try
                {
                    mdlConfigDetailsModel= db.GetConfigdetailsByName(strParaName).Select(x=> new ConfigDetailsModel() {
                        Id=x.Id,
                        Description=x.Description,
                        ParameterName=x.ParameterName,
                        ParameterValue=x.ParameterValue
                    }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    isError = true;
                    strError = ex.ToString();
                }
            }
            return isError;
        }

        public bool GetPaymentConfigDetails(string strParaName, out ConfigDetailsModel mdlConfigDetailsModel, out string strError)
        {
            bool isError = false;
            mdlConfigDetailsModel = new ConfigDetailsModel();
            strError = "";
            using (ConfigDetailsDataContext db = new ConfigDetailsDataContext(SqlConnectionSTring))
            {
                try
                {
                    mdlConfigDetailsModel = db.GetPaymentConfigdetailsByName(strParaName).Select(x => new ConfigDetailsModel()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        ParameterName = x.ParameterName,
                        ParameterValue = x.ParameterValue
                    }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    isError = true;
                    strError = ex.ToString();
                }
            }
            return isError;
        }


        public bool InsertOrUpdateDailyVisitEntry(string userLogDescription,bool logInOrOut, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (ConfigDetailsDataContext db = new ConfigDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertUserLogInLogDetails(SessionWrapper.UserDetails.Id, userLogDescription, logInOrOut);
                    
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

        public List<GetAllCountryLangIdResult> GetAllCoutry(long lgLang=1)
        {
            using (ConfigDetailsDataContext db = new ConfigDetailsDataContext(SqlConnectionSTring))
            {
                var lstData = db.GetAllCountryLangId((int?)lgLang).ToList();
                return lstData;
            }
        }

        public GetSMSTemplateByNameResult GetSMSTemplateByName(string strName)
        {
            using (CMSDataContext db = new CMSDataContext(SqlConnectionSTring))
            {
                var lstData = db.GetSMSTemplateByName(strName).FirstOrDefault();
                return lstData;
            }
        }

        public List<GetAllStateLangIdResult> GetAllState(long lgCountryId,long lgLang = 1)
        {
            using (ConfigDetailsDataContext db = new ConfigDetailsDataContext(SqlConnectionSTring))
            {
                var lstData = db.GetAllStateLangId((int?)lgCountryId,(int?)lgLang).ToList();
                return lstData;
            }
        }
        public List<GetAllCityLangIdResult> GetAllCity(long lgStateId, long lgLang = 1)
        {
            using (ConfigDetailsDataContext db = new ConfigDetailsDataContext(SqlConnectionSTring))
            {
                var lstData = db.GetAllCityLangId((int?)lgStateId, (int?)lgLang).ToList();
                return lstData;
            }
        }


        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
