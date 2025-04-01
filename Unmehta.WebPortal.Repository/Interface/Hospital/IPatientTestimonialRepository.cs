using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IPatientTestimonialRepository: IDisposable
    {
        List<GetAllPatientTestimonialMasterResult> GetAllPatientTestimonial();
        GetAllPatientTestimonialMasterResult GetAllPatientTestimonialsById(long lgId);
        bool InsertOrUpdatePatientTestimonial(PatientTestimonialModel objData, out string strError);
        bool RemovePatientTestimonial(long lgId, out string strError);
    }
}
