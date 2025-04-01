using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Unmehta.WebPortal.Data.Appointment;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class FrontAppointmentRepository : IFrontAppointmentRepository
    {

        private string SqlConnectionSTring;
        public FrontAppointmentRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllDepartmentForAppointmentResult> GetAllDepartmentForAppointment()
        {
            using (FrontAppointmentDataContext db = new FrontAppointmentDataContext(SqlConnectionSTring))
            {
                return db.GetAllDepartmentForAppointment().ToList();
            }
        }
        public List<GetAllVisitTypeForAppointmentResult> GetAllVisitTypeForAppointment()
        {
            using (FrontAppointmentDataContext db = new FrontAppointmentDataContext(SqlConnectionSTring))
            {
                return db.GetAllVisitTypeForAppointment().ToList();
            }
        }

        public List<GetAllUnitByDeptIdForAppointmentResult> GetAllUnitByDeptIdForAppointment(int deptId)
        {
            using (FrontAppointmentDataContext db = new FrontAppointmentDataContext(SqlConnectionSTring))
            {
                return db.GetAllUnitByDeptIdForAppointment(deptId).ToList();
            }
        }

        public List<GetWeekNoFromDeptIdForAppointmentResult> GetWeekNoFromDeptIdForAppointment(long unitId)
        {
            using (FrontAppointmentDataContext db = new FrontAppointmentDataContext(SqlConnectionSTring))
            {
                return db.GetWeekNoFromDeptIdForAppointment(unitId).ToList();
            }
        }


        public List<GetSlotListFromWeekNoForAppointmentResult> GetSlotListFromWeekNoForAppointment(int weekNo, int unitId, DateTime appointmentDate)
        {
            using (FrontAppointmentDataContext db = new FrontAppointmentDataContext(SqlConnectionSTring))
            {
                return db.GetSlotListFromWeekNoForAppointment(weekNo, unitId,appointmentDate,null).ToList();
            }
        }

        public List<GetAllDoctorBySlotIdDeptIdForAppointmentResult> GetAllDoctorBySlotIdDeptIdForAppointment(int deptId,int unitId)
        {
            using (FrontAppointmentDataContext db = new FrontAppointmentDataContext(SqlConnectionSTring))
            {
                return db.GetAllDoctorBySlotIdDeptIdForAppointment(  deptId,unitId).ToList();
            }
        }

        public bool CheckSloatAlreadyBookOrNot(DateTime appointmentDate, int unitId, int sloatId, int docId)
        {
            using (FrontAppointmentDataContext db = new FrontAppointmentDataContext(SqlConnectionSTring))
            {
                var data = db.CheckSloatAlreadyBookOrNot(appointmentDate, unitId, docId, sloatId).FirstOrDefault();
                if(data != null)
                {
                return data.SlotAvailability!= "Available" ? true:false;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CheckSameSloatAlreadyBookOrNot(DateTime appointmentDate, long unitId, long sloatId, string mobileNo)
        {
            using (FrontAppointmentDataContext db = new FrontAppointmentDataContext(SqlConnectionSTring))
            {
                var data = db.CheckSameSloatAlreadyBookOrNot(mobileNo, appointmentDate, unitId, 0, sloatId ).FirstOrDefault();
                if (data != null)
                {
                    return data.Column1 == 1 ? true : false;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool InsertOrUpdateHospitalAppointment(FrontAppointmentModel objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (FrontAppointmentDataContext db = new FrontAppointmentDataContext(SqlConnectionSTring))
                {
                    if (!CheckSameSloatAlreadyBookOrNot(objData.AppointmentDate.Value, (int)objData.UnitId.Value, (int)objData.SlotId, objData.MobileNo))
                    {
                        if (objData.DoctorId == null)
                        {
                            var data = db.Insertorupdatepatientappointmentmaster(0, objData.PatientName, objData.EMail, objData.MobileNo, objData.VisitTypeId, objData.UNMId, objData.ReasonForVisit, (int)objData.UnitId, objData.DoctorId, (int)objData.SlotId, objData.AppointmentDate, SessionWrapper.PatientMobileNo).FirstOrDefault();
                            objData.Id = Convert.ToInt32(data.RecId);
                            strError = "Your Appointment book Successfully";
                            isError = false;
                        }
                        else if (!CheckSloatAlreadyBookOrNot(objData.AppointmentDate.Value, (int)objData.UnitId.Value, (int)objData.SlotId, (int)objData.DoctorId))
                        {
                            var data = db.Insertorupdatepatientappointmentmaster(0, objData.PatientName, objData.EMail, objData.MobileNo, objData.VisitTypeId, objData.UNMId, objData.ReasonForVisit, (int)objData.UnitId, (int)objData.DoctorId, (int)objData.SlotId, objData.AppointmentDate, SessionWrapper.PatientMobileNo).FirstOrDefault();
                            objData.Id = Convert.ToInt32(data.RecId);
                            strError = "Your Appointment book Successfully";
                            isError = false;
                        }
                        else
                        {
                            strError = "Appointment for this slot or doctor is not available";
                            isError = true;
                        }
                    }
                    else
                    {
                        strError = "Same Appointment Already Exist";
                        isError = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        #region Doctor LogIn

        public bool CheckFacultyIdExists(int facultyId)
        {
            using (AppointmentDoctorDataContext db = new AppointmentDoctorDataContext(SqlConnectionSTring))
            {
                var data = db.CheckFacultyIdExists(facultyId).FirstOrDefault();
                if (data != null)
                {
                    return data.FacultyIdExists != 0 ? true : false;
                }
                else
                {
                    return false;
                }
            }
        }
        public GetDoctorIdByUsrIdResult GetDoctorIdByUsrId(int userId)
        {
            using (AppointmentDoctorDataContext db = new AppointmentDoctorDataContext(SqlConnectionSTring))
            {
                var data = db.GetDoctorIdByUsrId(userId).FirstOrDefault();
                if (data != null)
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool InsertOrUpdateUserWithFacultyDetails(DoctorRegisterModel objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (AppointmentDoctorDataContext db = new AppointmentDoctorDataContext(SqlConnectionSTring))
                {
                    //if (!CheckFacultyIdExists((int)objData.Id))
                    {
                        var data = db.InsertOrUpdateUserWithFacultyDetails(objData.RoleId, objData.UserName, objData.Password, objData.Id);
                        objData.Id = Convert.ToInt32(data);
                        strError = "Doctor User Detail updated Successfully";
                        isError = false;
                    }
                    //else
                    //{
                    //    strError = "Same Appointment Already Exist";
                    //    isError = true;
                    //}
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        #endregion

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

    }
}
