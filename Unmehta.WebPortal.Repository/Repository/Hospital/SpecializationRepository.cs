using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class SpecializationRepository : ISpecializationRepository
    {
        private string SqlConnectionSTring;
        public SpecializationRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<SpecializationMasterModel> GetAllSpecialization()
        {
            using (SpecializationMasterDataContext db = new SpecializationMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllSpecializationMaster().Select(x => new SpecializationMasterModel
                {
                    Id = x.Id,
                    DepartmentName = x.DepartmentName,
                    IsActive = x.IsActive
                }).ToList();
            }
        }

        public SpecializationMasterModel GetSpecializationById(long lgId)
        {
            using (SpecializationMasterDataContext db = new SpecializationMasterDataContext(SqlConnectionSTring))
            {
                return db.GetSpecializationMasterById(lgId).Select(x=> new SpecializationMasterModel {
                    Id=x.Id,
                    DepartmentName=x.DepartmentName,
                    IsActive=x.IsActive
                }).FirstOrDefault();
            }
        }

        public GetSpecializationMasterByIdResult GetSpecializationScheduleById(long lgId)
        {
            using (SpecializationMasterDataContext db = new SpecializationMasterDataContext(SqlConnectionSTring))
            {
                return db.GetSpecializationMasterById(lgId).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateSpecialization(SpecializationMasterModel objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (SpecializationMasterDataContext db = new SpecializationMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateSpecializationMaster(objData.Id, objData.DepartmentName, objData.IsActive, SessionWrapper.UserDetails.UserName);
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            strError = "Record Updated Successfully";
                            isError = false;
                        }
                        objData.Id = (long)dataIsDone.FirstOrDefault().RecId;
                    }
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
        
        public bool UpdateSpecializationSchedule(SpecializationScheduleModel objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (SpecializationMasterDataContext db = new SpecializationMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.UpdateSpecializationMasterSchedule(
                        objData.Id,
                        objData.Interval,
                        objData.IsSunday,
                        objData.SunStartTimeHour,
                        objData.SunStartTimeMin,
                        objData.SunStartTimeTT,
                        objData.SunEndTimeHour,
                        objData.SunEndTimeMin,
                        objData.SunEndTimeTT,
                        objData.SunLaunchStartTimeHour,
                        objData.SunLaunchStartTimeMin,
                        objData.SunLaunchStartTimeTT,
                        objData.SunLaunchEndTimeHour,
                        objData.SunLaunchEndTimeMin,
                        objData.SunLaunchEndTimeTT,
                        objData.IsMonday,
                        objData.MonStartTimeHour,
                        objData.MonStartTimeMin,
                        objData.MonStartTimeTT,
                        objData.MonEndTimeHour,
                        objData.MonEndTimeMin,
                        objData.MonEndTimeTT,
                        objData.MonLaunchStartTimeHour,
                        objData.MonLaunchStartTimeMin,
                        objData.MonLaunchStartTimeTT,
                        objData.MonLaunchEndTimeHour,
                        objData.MonLaunchEndTimeMin,
                        objData.MonLaunchEndTimeTT,
                        objData.IsTuesday,
                        objData.TueStartTimeHour,
                        objData.TueStartTimeMin,
                        objData.TueStartTimeTT,
                        objData.TueEndTimeHour,
                        objData.TueEndTimeMin,
                        objData.TueEndTimeTT,
                        objData.TueLaunchStartTimeHour,
                        objData.TueLaunchStartTimeMin,
                        objData.TueLaunchStartTimeTT,
                        objData.TueLaunchEndTimeHour,
                        objData.TueLaunchEndTimeMin,
                        objData.TueLaunchEndTimeTT,
                        objData.IsWednesday,
                        objData.WenStartTimeHour,
                        objData.WenStartTimeMin,
                        objData.WenStartTimeTT,
                        objData.WenEndTimeHour,
                        objData.WenEndTimeMin,
                        objData.WenEndTimeTT,
                        objData.WenLaunchStartTimeHour,
                        objData.WenLaunchStartTimeMin,
                        objData.WenLaunchStartTimeTT,
                        objData.WenLaunchEndTimeHour,
                        objData.WenLaunchEndTimeMin,
                        objData.WenLaunchEndTimeTT,
                        objData.IsThursday,
                        objData.ThuStartTimeHour,
                        objData.ThuStartTimeMin,
                        objData.ThuStartTimeTT,
                        objData.ThuEndTimeHour,
                        objData.ThuEndTimeMin,
                        objData.ThuEndTimeTT,
                        objData.ThuLaunchStartTimeHour,
                        objData.ThuLaunchStartTimeMin,
                        objData.ThuLaunchStartTimeTT,
                        objData.ThuLaunchEndTimeHour,
                        objData.ThuLaunchEndTimeMin,
                        objData.ThuLaunchEndTimeTT,
                        objData.IsFriday,
                        objData.FriStartTimeHour,
                        objData.FriStartTimeMin,
                        objData.FriStartTimeTT,
                        objData.FriEndTimeHour,
                        objData.FriEndTimeMin,
                        objData.FriEndTimeTT,
                        objData.FriLaunchStartTimeHour,
                        objData.FriLaunchStartTimeMin,
                        objData.FriLaunchStartTimeTT,
                        objData.FriLaunchEndTimeHour,
                        objData.FriLaunchEndTimeMin,
                        objData.FriLaunchEndTimeTT,
                        objData.IsSaturday,
                        objData.SatStartTimeHour,
                        objData.SatStartTimeMin,
                        objData.SatStartTimeTT,
                        objData.SatEndTimeHour,
                        objData.SatEndTimeMin,
                        objData.SatEndTimeTT,
                        objData.SatLaunchStartTimeHour,
                        objData.SatLaunchStartTimeMin,
                        objData.SatLaunchStartTimeTT,
                        objData.SatLaunchEndTimeHour,
                        objData.SatLaunchEndTimeMin,
                        objData.SatLaunchEndTimeTT,
                        SessionWrapper.UserDetails.UserName);
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            strError = "Record Updated Successfully";
                            isError = false;
                        }
                        objData.Id = (long)dataIsDone;
                    }
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

        public bool RemoveSpecialization(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (SpecializationMasterDataContext db = new SpecializationMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveSpecializationMaster(lgId, SessionWrapper.UserDetails.UserName);
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
