using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class GoverningBoardMasterRepository: IGoverningBoardMasterRepository
    {
        private string SqlConnectionSTring;
        public GoverningBoardMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public GetAllGoverningBoardMasterResult GetGoverningBoardByLangId(long lgLangId)
        {
            using (GoverningBoardMasterDataContext db = new GoverningBoardMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllGoverningBoardMaster(lgLangId).FirstOrDefault();
            }
        }

        public List<GetAllByGoverningBoardMasterDesignationDetailByLangIdAndGovBoardIdResult> GetGoverningBoardMasterDesignationDetailDetails(long lgAccId, long lgLangId)
        {
            using (GoverningBoardMasterDataContext db = new GoverningBoardMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllByGoverningBoardMasterDesignationDetailByLangIdAndGovBoardId( lgLangId, lgAccId).OrderBy(x=> x.SequanceNo).ToList();
            }
        }

        public bool InsertOrUpdateGoverningBoardMaster(GoverningBoardMasterModel objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (GoverningBoardMasterDataContext db = new GoverningBoardMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateGoverningBoardMaster(objData.Id, objData.LangId,objData.MetaTitle,objData.MetaDescription, objData.PageDescription, objData.IsActive, SessionWrapper.UserDetails.UserName);
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
        
        public bool InsertOrUpdateGoverningBoardDesignationMaster(GoverningBoardMasterDesignatedDetailsModel objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (GoverningBoardMasterDataContext db = new GoverningBoardMasterDataContext(SqlConnectionSTring))
                {
                    #region Swap Logic
                    if (objData.Id != 0)
                    {
                        var dataList = GetGoverningBoardMasterDesignationDetailDetails(objData.GovBoardId,objData.LangId);
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
                                                var swapValueUpdate = db.InsertOrUpdateGoverningBoardMasterDesignationDetail(SwapDetail.Id, objData.LangId, objData.GovBoardId, SwapDetail.DesignatedPersonName, SwapDetail.FilePath, null,SwapDetail.DesignationName, objData.SwapFromSequanceNo, SwapDetail.IsActive, SessionWrapper.UserDetails.UserName);
                                                objData.SequanceNo = objData.SwapToSequanceNo;
                                                if (!isError)
                                                {
                                                    var dataIsDone = db.InsertOrUpdateGoverningBoardMasterDesignationDetail(objData.Id, objData.LangId, objData.GovBoardId, objData.DesignatedName, objData.FilePath, null, objData.DesignationName, objData.SequanceNo, objData.IsActive, SessionWrapper.UserDetails.UserName);
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
                                            var dataIsDone = db.InsertOrUpdateGoverningBoardMasterDesignationDetail(objData.Id, objData.LangId, objData.GovBoardId, objData.DesignatedName, objData.FilePath,null, objData.DesignationName, objData.SequanceNo, objData.IsActive, SessionWrapper.UserDetails.UserName);
                                            if (dataIsDone != null)
                                            {

                                                if (objData.SwapToSequanceNo < objData.SwapFromSequanceNo)
                                                {
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    List<GetAllByGoverningBoardMasterDesignationDetailByLangIdAndGovBoardIdResult> lstLop = GetGoverningBoardMasterDesignationDetailDetails(objData.GovBoardId, objData.LangId).Where(x => x.SequanceNo >= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo++;
                                                        var swapValueUpdate = db.InsertOrUpdateGoverningBoardMasterDesignationDetail(data.Id, objData.LangId, objData.GovBoardId, data.DesignatedPersonName, data.FilePath,null, data.DesignationName, currentSwqNo, data.IsActive, SessionWrapper.UserDetails.UserName);

                                                    }
                                                }
                                                else
                                                {
                                                    List<GetAllByGoverningBoardMasterDesignationDetailByLangIdAndGovBoardIdResult> lstLop = GetGoverningBoardMasterDesignationDetailDetails(objData.GovBoardId, objData.LangId).Where(x => x.SequanceNo <= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo--;
                                                        var swapValueUpdate = db.InsertOrUpdateGoverningBoardMasterDesignationDetail(data.Id, objData.LangId, objData.GovBoardId, data.DesignatedPersonName, data.FilePath,null, data.DesignationName, currentSwqNo, data.IsActive, SessionWrapper.UserDetails.UserName);

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
                                var dataIsDone = db.InsertOrUpdateGoverningBoardMasterDesignationDetail(objData.Id, objData.LangId, objData.GovBoardId, objData.DesignatedName, objData.FilePath,null, objData.DesignationName, objData.SequanceNo, objData.IsActive, SessionWrapper.UserDetails.UserName);
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
                            var dataIsDone = db.InsertOrUpdateGoverningBoardMasterDesignationDetail(objData.Id, objData.LangId, objData.GovBoardId, objData.DesignatedName, objData.FilePath,null, objData.DesignationName, objData.SequanceNo, objData.IsActive, SessionWrapper.UserDetails.UserName);
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
                        var dataIsDone = db.InsertOrUpdateGoverningBoardMasterDesignationDetail(objData.Id, objData.LangId, objData.GovBoardId, objData.DesignatedName, objData.FilePath, null,objData.DesignationName, objData.SequanceNo, objData.IsActive, SessionWrapper.UserDetails.UserName);
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


        public bool RemoveGoverningBoardDesignationMaster(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (GoverningBoardMasterDataContext db = new GoverningBoardMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveGoverningBoardMasterDesignationDetail(lgId, SessionWrapper.UserDetails.UserName);
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
