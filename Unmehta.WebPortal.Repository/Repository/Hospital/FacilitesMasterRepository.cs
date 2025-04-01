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
    public class FacilitesMasterRepository : IFacilitesMasterRepository
    {
        private string SqlConnectionSTring;
        public FacilitesMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<FacilitesMasterGridModel> GetAllFacilitesMaster(long lgLangId)
        {
            using (FacilitesMasterDataContext db = new FacilitesMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllFacilitesMaster(lgLangId).Select(x => new FacilitesMasterGridModel
                {
                    Id = x.Id,
                    FacilitesName = x.FacilitesName,
                    Status = x.Status,
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }

        public FacilitesMasterGridModel GetFacilitesMasterById(long lgId, long lgLangId)
        {
            using (FacilitesMasterDataContext db = new FacilitesMasterDataContext(SqlConnectionSTring))
            {
                return GetAllFacilitesMaster(lgLangId).Where(x=> x.Id== lgId).Select(x => new FacilitesMasterGridModel
                {
                    Id = x.Id,
                    FacilitesName = x.FacilitesName,
                    Status = x.Status,
                    IsVisible = x.IsVisible
                }).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateFacilitesMaster(FacilitesMasterGridModel objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (FacilitesMasterDataContext db = new FacilitesMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateFacilitesMaster(objData.Id, objData.LanguageId, objData.FacilitesName, objData.Status, objData.IsVisible,SessionWrapper.UserDetails.UserName);
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
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveFacilitesMaster(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (FacilitesMasterDataContext db = new FacilitesMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveFacilitesMaster(lgId, SessionWrapper.UserDetails.UserName);
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


        public List<FacilitesMasterImageGridModel> GetAllFacilitesImageMaster(long lgId, long lgLangId)
        {
            using (FacilitesMasterDataContext db = new FacilitesMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllFacilitesImageMasterByFacilitiesIdAndLanguageId(lgId, lgLangId).Select(x=> 
                new FacilitesMasterImageGridModel
                {
                    Id=x.Id,
                    FacilitiesId= lgId,
                    LanguageId= lgLangId,
                    SequanceNo=(long)x.SequanceNo,
                    FacilitesFileInfo=x.FacilitesFileInfo,
                    FacilitesFileName=x.FacilitesFileName,
                    IsVisible =(bool)x.IsVisible
                }
                ).ToList();
            }
        }


        public FacilitesMasterImageGridModel GetFacilitesImageMasterById(long lgId, long lgFacilitesId, long lgLangId)
        {
          
                return GetAllFacilitesImageMaster(lgFacilitesId, lgLangId).Where(x=> x.Id== lgId).FirstOrDefault();
          
        }

        public bool InsertOrUpdateFacilitesMasterImageDetails(FacilitesMasterImageGridModel objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (FacilitesMasterDataContext db = new FacilitesMasterDataContext(SqlConnectionSTring))
                {
                    #region Swap Logic
                    if (objData.Id != 0)
                    {
                        var dataList = GetAllFacilitesImageMaster(objData.FacilitiesId, objData.LanguageId);
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
                                                var swapValueUpdate = db.InsertOrUpdateFacilitesImageMaster(SwapDetail.Id, SwapDetail.LanguageId, SwapDetail.FacilitiesId, SwapDetail.FacilitesFileInfo, SwapDetail.FacilitesFileName, objData.SwapFromSequanceNo, SwapDetail.IsVisible, SessionWrapper.UserDetails.UserName);
                                                objData.SequanceNo = objData.SwapToSequanceNo;
                                                if (!isError)
                                                {
                                                    var dataIsDone = db.InsertOrUpdateFacilitesImageMaster(objData.Id, objData.LanguageId, objData.FacilitiesId, objData.FacilitesFileInfo, objData.FacilitesFileName, objData.SequanceNo, objData.IsVisible, SessionWrapper.UserDetails.UserName);
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
                                            var dataIsDone = db.InsertOrUpdateFacilitesImageMaster(objData.Id, objData.LanguageId, objData.FacilitiesId, objData.FacilitesFileInfo, objData.FacilitesFileName, objData.SequanceNo, objData.IsVisible, SessionWrapper.UserDetails.UserName);
                                            if (dataIsDone != null)
                                            {
                                                if (objData.SwapToSequanceNo < objData.SwapFromSequanceNo)
                                                {
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    List<FacilitesMasterImageGridModel> lstLop = dataList.Where(x => x.SequanceNo >= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo++;
                                                        var swapValueUpdate = db.InsertOrUpdateFacilitesImageMaster(data.Id, objData.LanguageId, data.FacilitiesId, data.FacilitesFileInfo, data.FacilitesFileName, currentSwqNo, data.IsVisible, SessionWrapper.UserDetails.UserName);
                                                    }
                                                }
                                                else
                                                {
                                                    List<FacilitesMasterImageGridModel> lstLop = dataList.Where(x => x.SequanceNo <= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo--;
                                                        var swapValueUpdate = db.InsertOrUpdateFacilitesImageMaster(data.Id, objData.LanguageId, data.FacilitiesId, data.FacilitesFileInfo, data.FacilitesFileName, currentSwqNo, data.IsVisible, SessionWrapper.UserDetails.UserName);

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
                                    var dataIsDone = db.InsertOrUpdateFacilitesImageMaster(objData.Id, objData.LanguageId, objData.FacilitiesId, objData.FacilitesFileInfo, objData.FacilitesFileName, objData.SequanceNo, objData.IsVisible, SessionWrapper.UserDetails.UserName);
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
                                var dataIsDone = db.InsertOrUpdateFacilitesImageMaster(objData.Id, objData.LanguageId, objData.FacilitiesId, objData.FacilitesFileInfo, objData.FacilitesFileName, objData.SequanceNo, objData.IsVisible, SessionWrapper.UserDetails.UserName);
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
                            var dataIsDone = db.InsertOrUpdateFacilitesImageMaster(objData.Id, objData.LanguageId, objData.FacilitiesId, objData.FacilitesFileInfo, objData.FacilitesFileName, objData.SequanceNo, objData.IsVisible, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveFacilitesMasterImageDetails(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (FacilitesMasterDataContext db = new FacilitesMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveFacilitesMasterImageDetails(lgId, SessionWrapper.UserDetails.UserName);
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
