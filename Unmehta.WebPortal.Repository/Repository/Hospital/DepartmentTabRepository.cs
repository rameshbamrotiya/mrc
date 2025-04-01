using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class DepartmentTabRepository : IDepartmentTabRepository
    {

        private string SqlConnectionSTring;

        public DepartmentTabRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }
        
        public List<GetAllOurExcellenceMasterResult> GetAllDepartmentMaster(long lgLangId)
        {
            using (OurExcellenceMasterDataContext db = new OurExcellenceMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllOurExcellenceMaster(lgLangId).ToList();
            }
        }

        #region Department Tab 

        public List<GetAllDepartmentTabTypeResult> GetAllDepartmentTabType()
        {
            using (DepartmentTabDataContext db = new DepartmentTabDataContext(SqlConnectionSTring))
            {
                return db.GetAllDepartmentTabType().ToList();
            }
        }

        public List<DepartmentTabGridModel> GetAllDepartmentTabList(long lgId, long lgLangId)
        {
            using (DepartmentTabDataContext db = new DepartmentTabDataContext(SqlConnectionSTring))
            {
                return db.GetAllDepartmentTabListMasterOurExcIdAndLangId(lgId, lgLangId).Select(x => new DepartmentTabGridModel
                {
                    Id = x.Id,
                    IsVisable = x.IsVisable,
                    IsDelete = x.IsDelete,
                    DepartmentId = x.DepartmentId,
                    LanguageId = x.LanguageId,
                    OurExcId = x.OurExcId,
                    ParentTabId = x.ParentTabId,
                    ParentTabName=x.ParentTabName,
                    TabTypeName=x.TabTypeName,
                    SequanceNo = x.SequanceNo,
                    TabName = x.TabName,
                    TabTypeId=x.TabTypeId,
                    TabVisable= (x.TabTypeId==4? false:true),//((x.TabTypeId==null? false : (x.TabTypeId!=0?false:true))),
                    CreateBy = x.CreateBy,
                    CreateDate = x.CreateDate,
                    UpdateBy = x.UpdateBy,
                    UpdateDate = x.UpdateDate,
                    DeleteBy = x.DeleteBy,
                    DeleteDate = x.DeleteDate
                }).ToList();
            }
        }
        
        public DepartmentTabGridModel GetAllDepartmentTabById(long lgId, long lgMainId, long lgLangId)
        {
            return GetAllDepartmentTabList(lgMainId, lgLangId).FirstOrDefault(x=> x.Id==lgId);
        }

        public bool InsertOrUpdateDepartmentTabMaster(DepartmentTabGridModel objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (DepartmentTabDataContext db = new DepartmentTabDataContext(SqlConnectionSTring))
                {

                    #region Swap Logic
                    if (objData.Id != 0)
                    {
                        var dataList = GetAllDepartmentTabList((long)objData.OurExcId, (long)objData.LanguageId);

                        if (objData.SwapToSequanceNo != null && objData.SwapFromSequanceNo != null)
                        {

                            if (objData.SwapToSequanceNo > 0 && objData.SwapFromSequanceNo > 0)
                            {
                                long minSqNo = (long)dataList.FirstOrDefault().SequanceNo, maxSequNo = (long)dataList.OrderByDescending(x => x.SequanceNo).FirstOrDefault().SequanceNo;

                                if (!(minSqNo < objData.SwapToSequanceNo || maxSequNo >= objData.SwapToSequanceNo))
                                {
                                    strError = "Swap Sequence No Must be Between " + minSqNo + " to " + maxSequNo;
                                    isError = false;
                                }
                                else
                                {
                                    if (objData.SwapType == "Swap")
                                    {
                                        if (!(minSqNo < objData.SwapFromSequanceNo || maxSequNo >= objData.SwapFromSequanceNo))
                                        {
                                            strError = "Current Sequence No Must be Between " + minSqNo + " to " + maxSequNo;
                                            isError = false;
                                        }
                                        else
                                        {
                                            var SwapDetail = dataList.Where(x => x.SequanceNo == objData.SwapToSequanceNo).FirstOrDefault();
                                            if (SwapDetail != null)
                                            {
                                                var swapValueUpdate = db.InsertOrUpdateDepartmentTabListMaster(SwapDetail.Id, SwapDetail.OurExcId, objData.LanguageId, SwapDetail.DepartmentId, SwapDetail.ParentTabId, SwapDetail.TabTypeId, SwapDetail.TabName, objData.SwapFromSequanceNo, SwapDetail.IsVisable, SessionWrapper.UserDetails.UserName);
                                                objData.SequanceNo = objData.SwapToSequanceNo;
                                                if (!isError)
                                                {
                                                    var dataIsDone = db.InsertOrUpdateDepartmentTabListMaster(objData.Id, objData.OurExcId, objData.LanguageId, objData.DepartmentId, objData.ParentTabId, objData.TabTypeId, objData.TabName, objData.SequanceNo, objData.IsVisable, SessionWrapper.UserDetails.UserName);
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
                                                strError = "Swap Sequence No Details Dose Not Exist " + "Swap Sequence No Must be Between " + minSqNo + " to " + maxSequNo;
                                                isError = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objData.SequanceNo = objData.SwapToSequanceNo;
                                        if (!isError)
                                        {
                                            var dataIsDone = db.InsertOrUpdateDepartmentTabListMaster(objData.Id, objData.OurExcId, objData.LanguageId, objData.DepartmentId,  objData.ParentTabId,objData.TabTypeId, objData.TabName, objData.SequanceNo, objData.IsVisable, SessionWrapper.UserDetails.UserName);
                                            if (dataIsDone != null)
                                            {
                                                if (objData.SwapToSequanceNo < objData.SwapFromSequanceNo)
                                                {
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    List<DepartmentTabGridModel> lstLop = dataList.Where(x => x.SequanceNo >= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo++;
                                                        
                                                        var swapValueUpdate = db.InsertOrUpdateDepartmentTabListMaster(data.Id, data.OurExcId, objData.LanguageId, data.DepartmentId, data.ParentTabId, data.TabTypeId, data.TabName, currentSwqNo, data.IsVisable, SessionWrapper.UserDetails.UserName);
                                                    }
                                                }
                                                else
                                                {
                                                    List<DepartmentTabGridModel> lstLop = dataList.Where(x => x.SequanceNo <= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo--;
                                                        var swapValueUpdate = db.InsertOrUpdateDepartmentTabListMaster(data.Id, data.OurExcId, objData.LanguageId, data.DepartmentId, data.ParentTabId, data.TabTypeId, data.TabName, currentSwqNo, data.IsVisable, SessionWrapper.UserDetails.UserName);

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
                                    var dataIsDone = db.InsertOrUpdateDepartmentTabListMaster(objData.Id, objData.OurExcId, objData.LanguageId, objData.DepartmentId, objData.ParentTabId, objData.TabTypeId, objData.TabName, objData.SequanceNo, objData.IsVisable, SessionWrapper.UserDetails.UserName);
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
                                var dataIsDone = db.InsertOrUpdateDepartmentTabListMaster(objData.Id, objData.OurExcId, objData.LanguageId, objData.DepartmentId, objData.ParentTabId, objData.TabTypeId, objData.TabName, objData.SequanceNo, objData.IsVisable, SessionWrapper.UserDetails.UserName);
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
                            var dataIsDone = db.InsertOrUpdateDepartmentTabListMaster(objData.Id, objData.OurExcId, objData.LanguageId, objData.DepartmentId, objData.ParentTabId, objData.TabTypeId, objData.TabName, objData.SequanceNo, objData.IsVisable, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveDepartmentTabById(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (DepartmentTabDataContext db = new DepartmentTabDataContext(SqlConnectionSTring))
                {
                    db.RemoveDepartmentTabListMaster(lgId, SessionWrapper.UserDetails.UserName);

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

        #endregion

        #region Department Tab Details

        public List<GetAllDeparmentTabDetailsTabIdResult> GetAllDeparmentTabDetailListByTabId(long lgTabId,long lgLanguageId)
        {
            using (DepartmentTabDataContext db = new DepartmentTabDataContext(SqlConnectionSTring))
            {
                return db.GetAllDeparmentTabDetailsTabId(lgTabId, lgLanguageId).ToList();
            }
        }

        public bool InsertOrUpdateDeparmentTabDetail(GetAllDeparmentTabDetailsTabIdResult objData,  out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {

                using (DepartmentTabDataContext db = new DepartmentTabDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateDeparmentTabDetails(objData.Id, objData.TabId, objData.TabTypeId, objData.LanguageId, objData.SequanceNo, objData.IntroductionDesc, objData.PopupImageName, objData.PopupBasicShortDesc, objData.PopupDesc, objData.ParentTabId, objData.StatasticId, objData.FacultyId, objData.IsVisable, SessionWrapper.UserDetails.UserName);
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
        
        public bool RemoveDeparmentTabDetailById(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (DepartmentTabDataContext db = new DepartmentTabDataContext(SqlConnectionSTring))
                {
                    db.RemoveDeparmentTabDetails(lgId);
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

        #endregion
        
        public void Dispose()
        {

        }
    }
}
