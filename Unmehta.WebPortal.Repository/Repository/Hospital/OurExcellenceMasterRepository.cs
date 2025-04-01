using System;
using System.Collections.Generic;
using System.Linq;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class OurExcellenceMasterRepository : IOurExcellenceMasterRepository
    {
        private string SqlConnectionSTring;
        public OurExcellenceMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<OurExcellenceMasterGridModel> GetAllTblOurExcellenceMaster(long lgLangId)
        {
            using (OurExcellenceMasterDataContext db = new OurExcellenceMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllOurExcellenceMaster(lgLangId).Select(x => new OurExcellenceMasterGridModel
                {
                    Id = x.Id,
                    LanguageId = lgLangId,
                    DepartmentId = x.DepartmentId,
                    MetaTitle=x.MetaTitle,
                    MetaDescription=x.MetaDescription,
                    DepartmentName = x.DepartmentName,
                    ImageName = x.ImageName,
                    FileFullPath = x.FileFullPath,
                    AddImage=x.AddImage,
                    ExternalVideoLink=x.ExternalVideoLink,
                    SequanceNo = (long)x.SequenceNo,
                    IsVisible = x.IsVisible,
                    IsFacility=x.IsFacility,
                    IsAddInOtherDepartment=x.IsAddInOtherDepartment
                }).ToList();
            }
        }

        public OurExcellenceMasterGridModel GetTblOurExcellenceMasterById(long lgId, long lgLangId)
        {
            using (OurExcellenceMasterDataContext db = new OurExcellenceMasterDataContext(SqlConnectionSTring))
            {
                return GetAllTblOurExcellenceMaster(lgLangId).FirstOrDefault(x => x.Id == lgId);
            }
        }
        
        public GetAllOurExcellenceMasterResult GetTblOurExcellenceMasterInformationById(long lgId, long lgLangId)
        {
            using (OurExcellenceMasterDataContext db = new OurExcellenceMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllOurExcellenceMaster(lgLangId).FirstOrDefault(x => x.Id == lgId);
            }
        }

        public bool UpdateTblOurExcellenceInformationMaster(long Id, string hODName, string hODImage, string hODDesignetion,string introductionDesc, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (OurExcellenceMasterDataContext db = new OurExcellenceMasterDataContext(SqlConnectionSTring))
                {
                    #region Swap Logic
                    if (!isError)
                    {
                        var dataIsDone = db.UpdateOurExcellenceInformationMaster(Id, hODName, hODImage, hODDesignetion, introductionDesc, SessionWrapper.UserDetails.UserName);
                        if (dataIsDone != null)
                        {
                            strError = "Record Updated Successfully";
                            isError = false;

                        }
                        else
                        {
                            strError = "Some issue face";
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

        public bool InsertOrUpdateTblOurExcellenceMaster(OurExcellenceMasterGridModel objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (OurExcellenceMasterDataContext db = new OurExcellenceMasterDataContext(SqlConnectionSTring))
                {

                    #region Swap Logic
                    if (objData.Id != 0)
                    {
                        var dataList = GetAllTblOurExcellenceMaster((long)objData.LanguageId);
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
                                                var swapValueUpdate = db.InsertOrUpdateOurExcellenceMaster(SwapDetail.Id, objData.LanguageId,SwapDetail.MetaTitle,SwapDetail.MetaDescription, (int)SwapDetail.DepartmentId, SwapDetail.ImageName, SwapDetail.AddImage, SwapDetail.SideImageURL, SwapDetail.ExternalVideoLink, (int)objData.SwapFromSequanceNo, SwapDetail.IsVisible, SwapDetail.IsFacility, SwapDetail.IsAddInOtherDepartment, SessionWrapper.UserDetails.UserName);
                                                objData.SequanceNo = objData.SwapToSequanceNo;
                                                if (!isError)
                                                {
                                                    var dataIsDone = db.InsertOrUpdateOurExcellenceMaster(objData.Id, objData.LanguageId, objData.MetaTitle, objData.MetaDescription, (int)objData.DepartmentId, objData.FileFullPath, objData.AddImage, objData.SideImageURL,  objData.ExternalVideoLink, (int)objData.SequanceNo, objData.IsVisible, objData.IsFacility, objData.IsAddInOtherDepartment, SessionWrapper.UserDetails.UserName);
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
                                            var dataIsDone = db.InsertOrUpdateOurExcellenceMaster(objData.Id, objData.LanguageId, objData.MetaTitle, objData.MetaDescription, (int)objData.DepartmentId, objData.FileFullPath, objData.AddImage, objData.SideImageURL, objData.ExternalVideoLink, (int)objData.SequanceNo, objData.IsVisible, objData.IsFacility, objData.IsAddInOtherDepartment, SessionWrapper.UserDetails.UserName);
                                            if (dataIsDone != null)
                                            {
                                                if (objData.SwapToSequanceNo < objData.SwapFromSequanceNo)
                                                {
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    List<OurExcellenceMasterGridModel> lstLop = dataList.Where(x => x.SequanceNo >= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo++;
                                                        var swapValueUpdate = db.InsertOrUpdateOurExcellenceMaster(data.Id, objData.LanguageId, data.MetaTitle, data.MetaDescription, (int)data.DepartmentId, data.ImageName, data.AddImage, data.SideImageURL, data.ExternalVideoLink, (int)currentSwqNo, data.IsVisible, data.IsFacility, data.IsAddInOtherDepartment, SessionWrapper.UserDetails.UserName);
                                                    }
                                                }
                                                else
                                                {
                                                    List<OurExcellenceMasterGridModel> lstLop = dataList.Where(x => x.SequanceNo <= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo--;
                                                        var swapValueUpdate = db.InsertOrUpdateOurExcellenceMaster(data.Id, objData.LanguageId,data.MetaTitle, data.MetaDescription, (int)data.DepartmentId, data.FileFullPath, data.AddImage, data.SideImageURL, data.ExternalVideoLink, (int)currentSwqNo, data.IsVisible, data.IsFacility, data.IsAddInOtherDepartment, SessionWrapper.UserDetails.UserName);

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
                                    var dataIsDone = db.InsertOrUpdateOurExcellenceMaster(objData.Id, objData.LanguageId, objData.MetaTitle, objData.MetaDescription, (int)objData.DepartmentId, objData.FileFullPath, objData.AddImage, objData.SideImageURL,  objData.ExternalVideoLink, (int)objData.SequanceNo, objData.IsVisible, objData.IsFacility, objData.IsAddInOtherDepartment, SessionWrapper.UserDetails.UserName);
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
                                    }
                                    isError = false;
                                }
                            }
                        }
                        else
                        {
                            if (!isError)
                            {
                                var dataIsDone = db.InsertOrUpdateOurExcellenceMaster(objData.Id, objData.LanguageId, objData.MetaTitle, objData.MetaDescription, (int)objData.DepartmentId, objData.FileFullPath, objData.AddImage, objData.SideImageURL, objData.ExternalVideoLink, (int)objData.SequanceNo, objData.IsVisible, objData.IsFacility, objData.IsAddInOtherDepartment, SessionWrapper.UserDetails.UserName);
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
                                }
                                isError = false;
                            }
                        }

                    }
                    else
                    {
                        if (!isError)
                        {
                            var dataIsDone = db.InsertOrUpdateOurExcellenceMaster(objData.Id, objData.LanguageId, objData.MetaTitle, objData.MetaDescription, (int)objData.DepartmentId, objData.FileFullPath, objData.AddImage, objData.SideImageURL, objData.ExternalVideoLink, (int)objData.SequanceNo, objData.IsVisible, objData.IsFacility, objData.IsAddInOtherDepartment, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveTblOurExcellenceMaster(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (OurExcellenceMasterDataContext db = new OurExcellenceMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveOurExcellenceMaster(lgId, SessionWrapper.UserDetails.UserName);
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
