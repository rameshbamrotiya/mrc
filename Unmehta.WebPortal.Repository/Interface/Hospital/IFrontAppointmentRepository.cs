using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Appointment;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IFrontAppointmentRepository :IDisposable
    {
        List<GetAllVisitTypeForAppointmentResult> GetAllVisitTypeForAppointment();

        List<GetAllDepartmentForAppointmentResult> GetAllDepartmentForAppointment();

        List<GetAllUnitByDeptIdForAppointmentResult> GetAllUnitByDeptIdForAppointment(int deptId);

        List<GetWeekNoFromDeptIdForAppointmentResult> GetWeekNoFromDeptIdForAppointment(long unitId);

        List<GetSlotListFromWeekNoForAppointmentResult> GetSlotListFromWeekNoForAppointment(int weekNo, int unitId, DateTime appointmentDate);

        List<GetAllDoctorBySlotIdDeptIdForAppointmentResult> GetAllDoctorBySlotIdDeptIdForAppointment(int deptId, int unitId);

        bool CheckSloatAlreadyBookOrNot(DateTime appointmentDate, int unitId, int sloatId, int docId);

        bool InsertOrUpdateHospitalAppointment(FrontAppointmentModel objData, out string strError);

        bool InsertOrUpdateUserWithFacultyDetails(DoctorRegisterModel objData, out string strError);

        GetDoctorIdByUsrIdResult GetDoctorIdByUsrId(int userId);

        bool CheckFacultyIdExists(int facultyId);

    }
}
