using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class UnitTeamRepository : IUnitTeamRepository
    {
        private string SqlConnectionSTring;
        public UnitTeamRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllUnitMasterResult> GetAllUnit()
        {
            using (UnitMasterDataContext db = new UnitMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllUnitMaster().ToList();
            }
        }
        
        public UnitTeamModel GetUnitByLangId(long lgId, long lgLangId)
        {
            using (UnitMasterDataContext db = new UnitMasterDataContext(SqlConnectionSTring))
            {
                return db.GetUnitMasterByIdAndLangId(lgId, lgLangId).Select(x=> new UnitTeamModel {
                    Id=x.Id,
                    SpecilizationId=x.SpecilizationId,
                    LangId=lgLangId,
                    WeekNo=x.WeekNo,
                    UnitName=x.UnitName,
                    StartTimeHour=x.StartTimeHour,
                    StartTimeMin=x.StartTimeMin,
                    StartTimeTT=x.StartTimeTT,
                    EndTimeHour=x.EndTimeHour,
                    EndTimeMin=x.EndTimeMin,
                    EndTimeTT=x.EndTimeTT,
                    IsActive=x.IsActive
                }).FirstOrDefault();
            }
        }
        public UnitTeamModel GetConfigUnitByLangId(long lgId, long lgLangId)
        {
            using (UnitMasterDataContext db = new UnitMasterDataContext(SqlConnectionSTring))
            {
                return db.GetConfigUnitMasterByIdAndLangId(lgId, lgLangId).Select(x => new UnitTeamModel
                {
                    Id = x.Id,
                    SpecilizationId = x.SpecilizationId,
                    doctorid=x.doctorid,
                    //LangId = lgLangId,
                    //WeekNo = x.WeekNo,
                    UnitName = x.UnitName,
                    //StartTimeHour = x.StartTimeHour,
                    //StartTimeMin = x.StartTimeMin,
                    //StartTimeTT = x.StartTimeTT,
                    //EndTimeHour = x.EndTimeHour,
                    //EndTimeMin = x.EndTimeMin,
                    //EndTimeTT = x.EndTimeTT,
                    IsActive = x.IsActive
                }).FirstOrDefault();
            }
        }
        public bool InsertOrUpdateUnit(UnitTeamModel objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (UnitMasterDataContext db = new UnitMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateUnitMaster(objData.Id, objData.SpecilizationId,objData.LangId,objData.UnitName,objData.WeekNo,
                        objData.StartTimeHour,objData.StartTimeMin,objData.StartTimeTT,objData.EndTimeHour,objData.EndTimeMin,objData.EndTimeTT,
                         SessionWrapper.UserDetails.UserName, objData.IsActive);


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

        public bool RemoveUnit(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (UnitMasterDataContext db = new UnitMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveUnitMaster(lgId, SessionWrapper.UserDetails.UserName);
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
        
        public bool InsertUnitDoctor(UnitDoctorMasterModel objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (UnitMasterDataContext db = new UnitMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertUnitDoctorMaster(objData.UnitDetailId, objData.LangId, objData.UnitId, objData.DoctorId,
                         SessionWrapper.UserDetails.UserName);
                    if (dataIsDone != null)
                    {
                        objData.Id = (long)dataIsDone.FirstOrDefault().RecId;
                        
                            strError = "Record Inserted Successfully";
                            isError = false;
                       
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
        
        public bool RemoveUnitDoctor(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (UnitMasterDataContext db = new UnitMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveUnitDoctorMaster(lgId, SessionWrapper.UserDetails.UserName);
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

        public List<GetAllUnitDoctorMasterByIdResult> GetAllUnitDoctorMasterByLangId(long lgId, long lgLangId)
        {
            using (UnitMasterDataContext db = new UnitMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllUnitDoctorMasterById(lgId, lgLangId).ToList();
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
