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
    public class OurExcellenceMasterStatisticsRepositry: IOurExcellenceMasterStatisticsRepositry
    {

        private string SqlConnectionSTring;
        public OurExcellenceMasterStatisticsRepositry(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllOurExcellenceMasterStaticsDetailsResult> GetAllOurExcellenceMasterStatistics(long lgOurExcId)
        {
            using (OurExcellenceMasterStatisticsDetailsDataContext db = new OurExcellenceMasterStatisticsDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllOurExcellenceMasterStaticsDetails(lgOurExcId).ToList();
            }
        }

        public GetAllOurExcellenceMasterStaticsDetailsResult GetOurExcellenceMasterStatistics(long lgOurExcId,long lgId)
        {
            using (OurExcellenceMasterStatisticsDetailsDataContext db = new OurExcellenceMasterStatisticsDetailsDataContext(SqlConnectionSTring))
            {
                return GetAllOurExcellenceMasterStatistics(lgOurExcId).Where(x=> x.Id==lgId).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateOurExcellenceMasterStatistics(OurExcellenceMasterStatisticsModel objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (OurExcellenceMasterStatisticsDetailsDataContext db = new OurExcellenceMasterStatisticsDetailsDataContext(SqlConnectionSTring))
                {
                    #region Swap Logic
                    if (objData.Id != 0)
                    {
                        var dataList = GetAllOurExcellenceMasterStatistics(objData.OurExcId);
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
                                                var swapValueUpdate = db.InsertOrUpdateOurExcellenceMasterStaticsDetails(SwapDetail.Id, SwapDetail.OurExcId, SwapDetail.StatisticsId, SwapDetail.StatisticsType, objData.SwapFromSequanceNo, SessionWrapper.UserDetails.UserName);
                                                objData.SequanceNo = objData.SwapToSequanceNo;
                                                if (!isError)
                                                {
                                                    var dataIsDone = db.InsertOrUpdateOurExcellenceMasterStaticsDetails(objData.Id, objData.OurExcId, objData.StatisticsId, objData.StatisticsType, objData.SequanceNo, SessionWrapper.UserDetails.UserName);
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
                                            var dataIsDone = db.InsertOrUpdateOurExcellenceMasterStaticsDetails(objData.Id, objData.OurExcId, objData.StatisticsId, objData.StatisticsType, objData.SequanceNo, SessionWrapper.UserDetails.UserName);
                                            if (dataIsDone != null)
                                            {

                                                if (objData.SwapToSequanceNo < objData.SwapFromSequanceNo)
                                                {
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    List<GetAllOurExcellenceMasterStaticsDetailsResult> lstLop = GetAllOurExcellenceMasterStatistics(objData.OurExcId).Where(x => x.SequenceNo >= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo++;
                                                        var swapValueUpdate = db.InsertOrUpdateOurExcellenceMasterStaticsDetails(data.Id, data.OurExcId, data.StatisticsId, data.StatisticsType, data.SequenceNo, SessionWrapper.UserDetails.UserName);

                                                    }
                                                }
                                                else
                                                {
                                                    List<GetAllOurExcellenceMasterStaticsDetailsResult> lstLop = GetAllOurExcellenceMasterStatistics(objData.OurExcId).Where(x => x.SequenceNo <= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo--;
                                                        //var swapValueUpdate = db.InsertOrUpdateGoverningBoardMasterDesignationDetail(data.Id, objData.LangId, objData.GovBoardId, data.DesignatedPersonName, data.FilePath, data.DesignationId, currentSwqNo, SessionWrapper.UserDetails.UserName);
                                                        var swapValueUpdate = db.InsertOrUpdateOurExcellenceMasterStaticsDetails(data.Id, data.OurExcId, data.StatisticsId, data.StatisticsType, data.SequenceNo, SessionWrapper.UserDetails.UserName);

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
                                var dataIsDone = db.InsertOrUpdateOurExcellenceMasterStaticsDetails(objData.Id, objData.OurExcId, objData.StatisticsId, objData.StatisticsType, objData.SequanceNo, SessionWrapper.UserDetails.UserName);
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
                            var dataIsDone = db.InsertOrUpdateOurExcellenceMasterStaticsDetails(objData.Id, objData.OurExcId, objData.StatisticsId, objData.StatisticsType, objData.SequanceNo, SessionWrapper.UserDetails.UserName);
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
                        var dataIsDone = db.InsertOrUpdateOurExcellenceMasterStaticsDetails(objData.Id, objData.OurExcId, objData.StatisticsId, objData.StatisticsType, objData.SequanceNo, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveOurExcellenceMasterStatics(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (OurExcellenceMasterStatisticsDetailsDataContext db = new OurExcellenceMasterStatisticsDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveOurExcellenceMasterStaticsDetails(lgId, SessionWrapper.UserDetails.UserName);
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
