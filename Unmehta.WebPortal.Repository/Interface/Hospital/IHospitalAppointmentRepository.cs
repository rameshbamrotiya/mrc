using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public interface IHospitalAppointmentRepository : IDisposable
    {
        GetHospitalAppointmentMasterByIdResult GetAllHospitalAppointmentById(int Id);
        List<GetAllHospitalAppointmentMasterByMobileResult> GetAllHospitalAppointmentByMobile(string mobileNo);
        List<GetAllHospitalAppointmentMasterBySpecializationIdResult> GetHospitalAppointmentBySpecializationId(int Id);
        bool InsertOrUpdateHospitalAppointment(HospitalAppointmentModel objData, out string strError);
        bool UpdateAppointmentOtpStatus(int Id, bool IsOtpVerified, out string strError);
        bool RemoveHospitalAppointment(long lgId, out string strError);
    }
}