using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class OurExcellenceMasterFacilityRepositry : IOurExcellenceMasterFacilityRepositry
    {
        private string SqlConnectionSTring;
        public OurExcellenceMasterFacilityRepositry(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllOurExcellenceMasterFacilityDetailsResult> GetAllOurExcellenceMasterFacility(long lgOurExcId)
        {
            using (OurExcellenceMasterFacilityDetailsDataContext db = new OurExcellenceMasterFacilityDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllOurExcellenceMasterFacilityDetails(lgOurExcId).ToList();
            }
        }

        public GetAllOurExcellenceMasterFacilityDetailsResult GetOurExcellenceMasterFacility(long lgOurExcId, long lgId)
        {
            using (OurExcellenceMasterFacilityDetailsDataContext db = new OurExcellenceMasterFacilityDetailsDataContext(SqlConnectionSTring))
            {
                return GetAllOurExcellenceMasterFacility(lgOurExcId).Where(x => x.Id == lgId).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateOurExcellenceMasterFacility(OurExcellenceMasterFacilityModel objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (OurExcellenceMasterFacilityDetailsDataContext db = new OurExcellenceMasterFacilityDetailsDataContext(SqlConnectionSTring))
                {
                    #region Swap Logic
                    if (objData.Id != 0)
                    {
                        var dataList = GetAllOurExcellenceMasterFacility(objData.OurExcId);
                        if (objData.SwapToSequanceNo != null && objData.SwapFromSequanceNo != null)
                        {

                            if (objData.SwapToSequanceNo > 0 && objData.SwapFromSequanceNo > 0)
                            {
                                long minSqNo = (long)dataList.FirstOrDefault().SequenceNo, maxSequNo = (long)dataList.OrderByDescending(x => x.SequenceNo).FirstOrDefault().SequenceNo;

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
                                            var SwapDetail = dataList.Where(x => x.SequenceNo == objData.SwapToSequanceNo).FirstOrDefault();
                                            if (SwapDetail != null)
                                            {
                                                var swapValueUpdate = db.InsertOrUpdateOurExcellenceMasterFacilityDetails(SwapDetail.Id, SwapDetail.OurExcId, SwapDetail.FacilityId, objData.SwapFromSequanceNo, SessionWrapper.UserDetails.UserName);
                                                objData.SequanceNo = objData.SwapToSequanceNo;
                                                if (!isError)
                                                {
                                                    var dataIsDone = db.InsertOrUpdateOurExcellenceMasterFacilityDetails(objData.Id, objData.OurExcId, objData.FacilityId, objData.SequanceNo, SessionWrapper.UserDetails.UserName);
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
                                            var dataIsDone = db.InsertOrUpdateOurExcellenceMasterFacilityDetails(objData.Id, objData.OurExcId, objData.FacilityId, objData.SequanceNo, SessionWrapper.UserDetails.UserName);
                                            if (dataIsDone != null)
                                            {

                                                if (objData.SwapToSequanceNo < objData.SwapFromSequanceNo)
                                                {
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    List<GetAllOurExcellenceMasterFacilityDetailsResult> lstLop = GetAllOurExcellenceMasterFacility(objData.OurExcId).Where(x => x.SequenceNo >= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo++;
                                                        var swapValueUpdate = db.InsertOrUpdateOurExcellenceMasterFacilityDetails(data.Id, data.OurExcId, data.FacilityId, data.SequenceNo, SessionWrapper.UserDetails.UserName);

                                                    }
                                                }
                                                else
                                                {
                                                    List<GetAllOurExcellenceMasterFacilityDetailsResult> lstLop = GetAllOurExcellenceMasterFacility(objData.OurExcId).Where(x => x.SequenceNo <= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo--;
                                                        //var swapValueUpdate = db.InsertOrUpdateGoverningBoardMasterDesignationDetail(data.Id, objData.LangId, objData.GovBoardId, data.DesignatedPersonName, data.FilePath, data.DesignationId, currentSwqNo, SessionWrapper.UserDetails.UserName);
                                                        var swapValueUpdate = db.InsertOrUpdateOurExcellenceMasterFacilityDetails(data.Id, data.OurExcId, data.FacilityId, data.SequenceNo, SessionWrapper.UserDetails.UserName);

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
                                var dataIsDone = db.InsertOrUpdateOurExcellenceMasterFacilityDetails(objData.Id, objData.OurExcId, objData.FacilityId, objData.SequanceNo, SessionWrapper.UserDetails.UserName);
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
                            var dataIsDone = db.InsertOrUpdateOurExcellenceMasterFacilityDetails(objData.Id, objData.OurExcId, objData.FacilityId, objData.SequanceNo, SessionWrapper.UserDetails.UserName);
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
                        var dataIsDone = db.InsertOrUpdateOurExcellenceMasterFacilityDetails(objData.Id, objData.OurExcId, objData.FacilityId, objData.SequanceNo, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveOurExcellenceMasterFacility(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (OurExcellenceMasterFacilityDetailsDataContext db = new OurExcellenceMasterFacilityDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveOurExcellenceMasterFacilityDetails(lgId, SessionWrapper.UserDetails.UserName);
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
