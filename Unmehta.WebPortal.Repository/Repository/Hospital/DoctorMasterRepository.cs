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
    public class DoctorMasterRepository : IDoctorMasterRepository
    {
        private string SqlConnectionSTring;
        public DoctorMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }


        public List<GetAllDoctorByLanguageIdResult> GetAllDoctorForDropDownByLangId(long lgLangId)
        {
            using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllDoctorByLanguageId(lgLangId).ToList();
            }
        }

        public List<GetAllDoctorMasterResult> GetAllDoctor()
        {
            using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllDoctorMaster().ToList();
            }
        }

        public GetDoctorMasterByDocIdAndLangIdResult GetDoctorByIdAndLagId(long lgId, long lgLangId)
        {
            using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetDoctorMasterByDocIdAndLangId(lgId, lgLangId).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateDoctor(DoctorMasterModel objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
                {
                    #region Swap Logic
                    if (objData.Id != 0)
                    {
                        var dataList = GetAllDoctor();
                        if (objData.SwapToSequanceNo != null && objData.SwapFromSequanceNo != null)
                        {

                            if (objData.SwapToSequanceNo > 0 && objData.SwapFromSequanceNo > 0)
                            {
                                long minSqNo = (long)dataList.FirstOrDefault().SequanceNo, maxSequNo = (long)dataList.OrderByDescending(x => x.SequanceNo).FirstOrDefault().SequanceNo;

                                if (!(minSqNo < objData.SwapToSequanceNo || maxSequNo >= objData.SwapToSequanceNo))
                                {
                                    strError = "Swap Sequance No Mustbe Between " + minSqNo + " to " + maxSequNo;
                                    isError = false;
                                }
                                else
                                {
                                    if (objData.SwapType == "Swap")
                                    {
                                        if (!(minSqNo < objData.SwapFromSequanceNo || maxSequNo >= objData.SwapFromSequanceNo))
                                        {
                                            strError = "Current Sequance No Mustbe Between " + minSqNo + " to " + maxSequNo;
                                            isError = false;
                                        }
                                        else
                                        {
                                            var SwapDetail = dataList.Where(x => x.SequanceNo == objData.SwapToSequanceNo).FirstOrDefault();
                                            if (SwapDetail != null)
                                            {
                                                var swapValueUpdate = db.InsertOrUpdateDoctorMaster(SwapDetail.Id, 1, objData.SwapFromSequanceNo, SwapDetail.DoctorFirstName, SwapDetail.DoctorMiddleName, SwapDetail.DoctorLastName, SwapDetail.DoctorDescription, SwapDetail.DoctorShotDescription, SwapDetail.DoctorProfilePic, SwapDetail.IsActive, SessionWrapper.UserDetails.UserName);
                                                objData.SequanceNo = objData.SwapToSequanceNo;
                                                if (!isError)
                                                {
                                                    var dataIsDone = db.InsertOrUpdateDoctorMaster(objData.Id, objData.LangId, objData.SequanceNo, objData.DoctorFirstName, objData.DoctorMiddleName, objData.DoctorLastName, objData.DoctorDescription, objData.DoctorShotDescription, objData.DoctorProfilePic, objData.IsActive, SessionWrapper.UserDetails.UserName);
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
                                            else
                                            {
                                                strError = "Swap Sequance No Details Dose Not Exist " + "Swap Sequance No Mustbe Between " + minSqNo + " to " + maxSequNo;
                                                isError = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objData.SequanceNo = objData.SwapToSequanceNo;
                                        if (!isError)
                                        {
                                            var dataIsDone = db.InsertOrUpdateDoctorMaster(objData.Id, objData.LangId, objData.SequanceNo, objData.DoctorFirstName, objData.DoctorMiddleName, objData.DoctorLastName, objData.DoctorDescription, objData.DoctorShotDescription, objData.DoctorProfilePic, objData.IsActive, SessionWrapper.UserDetails.UserName);
                                            if (dataIsDone != null)
                                            {
                                                if (objData.SwapToSequanceNo < objData.SwapFromSequanceNo)
                                                {
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    List<GetAllDoctorMasterResult> lstLop = dataList.Where(x => x.SequanceNo >= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo++;
                                                        var swapValueUpdate = db.InsertOrUpdateDoctorMaster(data.Id, objData.LangId, currentSwqNo, data.DoctorFirstName, data.DoctorMiddleName, data.DoctorLastName, data.DoctorDescription, data.DoctorShotDescription, data.DoctorProfilePic, data.IsActive, SessionWrapper.UserDetails.UserName);
                                                    }
                                                }
                                                else
                                                {
                                                    List<GetAllDoctorMasterResult> lstLop = dataList.Where(x => x.SequanceNo <= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo--;
                                                        var swapValueUpdate = db.InsertOrUpdateDoctorMaster(data.Id, objData.LangId, currentSwqNo, data.DoctorFirstName, data.DoctorMiddleName, data.DoctorLastName, data.DoctorDescription, data.DoctorShotDescription, data.DoctorProfilePic, data.IsActive, SessionWrapper.UserDetails.UserName);

                                                    }
                                                }
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
                                }
                            }
                            else
                            {
                                if (!isError)
                                {
                                    var dataIsDone = db.InsertOrUpdateDoctorMaster(objData.Id, objData.LangId, objData.SequanceNo, objData.DoctorFirstName, objData.DoctorMiddleName, objData.DoctorLastName, objData.DoctorDescription, objData.DoctorShotDescription, objData.DoctorProfilePic, objData.IsActive, SessionWrapper.UserDetails.UserName);
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
                        }
                        else
                        {
                            if (!isError)
                            {
                                var dataIsDone = db.InsertOrUpdateDoctorMaster(objData.Id, objData.LangId, objData.SequanceNo, objData.DoctorFirstName, objData.DoctorMiddleName, objData.DoctorLastName, objData.DoctorDescription, objData.DoctorShotDescription, objData.DoctorProfilePic, objData.IsActive, SessionWrapper.UserDetails.UserName);
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

                    }
                    else
                    {
                        if (!isError)
                        {
                            var dataIsDone = db.InsertOrUpdateDoctorMaster(objData.Id, objData.LangId, objData.SequanceNo, objData.DoctorFirstName, objData.DoctorMiddleName, objData.DoctorLastName, objData.DoctorDescription, objData.DoctorShotDescription, objData.DoctorProfilePic, objData.IsActive, SessionWrapper.UserDetails.UserName);
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
                    #endregion
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveDoctor(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveDoctorMaster(lgId, SessionWrapper.UserDetails.UserName);
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
