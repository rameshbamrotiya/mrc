using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IRecruitmentCityRepository : IDisposable
    {
        List<RecruitmentCityGridModel> GetAllTblRecruitmentCity();

        RecruitmentCityGridModel GetTblRecruitmentCityById(long lgId);

        bool InsertOrUpdateTblRecruitmentCity(RecruitmentCityGridModel objData, out string strError);

        bool RemoveTblRecruitmentCity(long lgId, out string strError);
    }
}
