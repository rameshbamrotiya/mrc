using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IAboutUsMasterRepository : IDisposable
    {
        List<AboutUsMasterGridModel> GetAlllongAboutUsMaster(long lgLangId);

        AboutUsMasterGridModel GetlongAboutUsMasterById(long lgLangId, long lgId);

        bool InsertOrUpdatelongAboutUsMaster(AboutUsMasterGridModel objData, out string strError);

        bool RemovelongAboutUsMaster(long lgId, out string strError);

        List<AboutUsMasterDesignationGridModel> GetAlllongAboutUsDesignationMaster(long lgId, long lgLangId);

        AboutUsMasterDesignationGridModel GetlongAboutUsDesignationMasterById(long lgAboutId, long lgId, long lgLangId);

        bool InsertOrUpdatelongAboutUsDesignation(AboutUsMasterDesignationGridModel objData, long lgLangId, out string strError);

        bool RemovelongAboutUsDesignationMaster(long lgId, out string strError);
    }
}
