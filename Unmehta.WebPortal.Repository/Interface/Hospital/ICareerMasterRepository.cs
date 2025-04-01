using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Model.Model.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface ICareerMasterRepository : IDisposable
    {
        List<CareerMasterGridModel> GetAllTblCareer(int lgId);

        CareerMasterGridModel GetTblCareerById(int lgId, int lgLangId);

        bool InsertOrUpdateTblCareer(CareerMasterGridModel objData, out string strError);

        bool RemoveTblCareer(int lgId, out string strError);
    }
}
