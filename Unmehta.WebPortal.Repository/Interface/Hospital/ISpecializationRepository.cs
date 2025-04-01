using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface ISpecializationRepository:IDisposable
    {
        List<SpecializationMasterModel> GetAllSpecialization();
        SpecializationMasterModel GetSpecializationById(long lgId);
        GetSpecializationMasterByIdResult GetSpecializationScheduleById(long lgId);
        bool UpdateSpecializationSchedule(SpecializationScheduleModel objData, out string strError);
        bool InsertOrUpdateSpecialization(SpecializationMasterModel objData, out string strError);
        bool RemoveSpecialization(long lgId, out string strError);
    }
}
