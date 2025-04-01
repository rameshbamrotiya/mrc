using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IAboutNursingCareMasterRepository
    {
        List<GetAllAboutNursingCareMasterByLanguageIdResult> GetAllAboutNursingCareMaster(int lgId);
        GetAllAboutNursingCareMasterByLanguageIdResult GetAboutNursingCareMasterById(int lgId, int lgLangId);
        bool InsertOrUpdateAboutNursingCareMaster(GetAllAboutNursingCareMasterByLanguageIdResult objData, long lgLangId, out string strError);
        bool RemoveAboutNursingCareMaster(int lgId, out string strError);
        List<GetAllNursingCareMasterPhotoDetailsByLanguageIdResult> GetAllAboutNursingCareMasterPhotoDetail(int lgId);
        GetAllNursingCareMasterPhotoDetailsByLanguageIdResult GetAboutNursingCareMasterPhotoDetailById(int lgId, int lgLangId);
        bool InsertOrUpdateAboutNursingCareMasterPhotoDetail(GetAllNursingCareMasterPhotoDetailsByLanguageIdResult objData, long lgLangId, out string strError);
        bool RemoveAboutNursingCareMasterPhotoDetail(int lgId, out string strError);
    }
}
