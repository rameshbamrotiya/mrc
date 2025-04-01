using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class HospitalAppointmentRepository : IHospitalAppointmentRepository
    {

        private string SqlConnectionSTring;
        public HospitalAppointmentRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public GetHospitalAppointmentMasterByIdResult GetAllHospitalAppointmentById(int Id)
        {
            using (HospitalAppointmentDataContext db = new HospitalAppointmentDataContext(SqlConnectionSTring))
            {
                return db.GetHospitalAppointmentMasterById(Id).FirstOrDefault();
            }
        }

        public List<GetAllHospitalAppointmentMasterByMobileResult> GetAllHospitalAppointmentByMobile(string mobileNo)
        {
            using (HospitalAppointmentDataContext db = new HospitalAppointmentDataContext(SqlConnectionSTring))
            {
                return db.GetAllHospitalAppointmentMasterByMobile(mobileNo).ToList();
            }
        }

        public List<GetAllHospitalAppointmentMasterBySpecializationIdResult> GetHospitalAppointmentBySpecializationId(int Id)
        {
            using (HospitalAppointmentDataContext db = new HospitalAppointmentDataContext(SqlConnectionSTring))
            {
                return db.GetAllHospitalAppointmentMasterBySpecializationId(Id).ToList();
            }
        }

        public bool InsertOrUpdateHospitalAppointment(HospitalAppointmentModel objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (HospitalAppointmentDataContext db = new HospitalAppointmentDataContext(SqlConnectionSTring))
                {
                    var data=db.InsertHospitalAppointmentMaster(objData.FirstName, objData.MiddleName, objData.LastName, objData.Gender, objData.MobileNumber, objData.EmailId, objData.SpecialId, objData.AppointmentDate, objData.AppointmentTime, objData.ReasonForVisit, objData.AdditionalInformation, objData.IsFollowUp, false, SessionWrapper.UserDetails.UserName).FirstOrDefault();
                    objData.Id = Convert.ToInt32(data.RecId);
                    strError = "Record Inserted Successfully";
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


        public bool UpdateAppointmentOtpStatus(int  Id, bool IsOtpVerified, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (HospitalAppointmentDataContext db = new HospitalAppointmentDataContext(SqlConnectionSTring))
                {
                    db.UpdateAppointmentMasterOtp(Id, IsOtpVerified);
                    strError = "Otp Verified Success Successfully";
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

        public bool RemoveHospitalAppointment(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (HospitalAppointmentDataContext db = new HospitalAppointmentDataContext(SqlConnectionSTring))
                {
                    db.RemoveHospitalAppointmentMaster(lgId, SessionWrapper.UserDetails.UserName);
                    db.SubmitChanges();
                    strError = "Record Removed Successfully";
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

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
