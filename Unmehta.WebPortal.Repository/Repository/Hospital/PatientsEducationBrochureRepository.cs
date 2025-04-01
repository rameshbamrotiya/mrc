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
    public class PatientsEducationBrochureRepository : IPatientsEducationBrochureRepository
    {
        private string SqlConnectionSTring;
        public PatientsEducationBrochureRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllPatientsEducationBrochureMasterResult> GetAllPatientsEducationBrochure(long lgLangId)
        {
            using (PatientsEducationBrochureDataContext db = new PatientsEducationBrochureDataContext(SqlConnectionSTring))
            {
                return db.GetAllPatientsEducationBrochureMaster(lgLangId).ToList();
            }
        }
        
        public GetAllPatientsEducationBrochureMasterResult GetPatientsEducationBrochureById(long lgId, long lgLangId)
        {
            return GetAllPatientsEducationBrochure(lgLangId).Where(x => x.Id == lgId).FirstOrDefault();
        }

        public bool InsertOrUpdatePatientsEducationBrochure(PatientBrochuerModel objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (PatientsEducationBrochureDataContext db = new PatientsEducationBrochureDataContext(SqlConnectionSTring))
                {
                    #region Swap Logic
                    if (objData.Id != 0)
                    {
                        var dataList = GetAllPatientsEducationBrochure(1);
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
                                                var swapValueUpdate = db.InsertOrUpdatePatientsEducationBrochureMaster(SwapDetail.Id, 1, objData.SwapFromSequanceNo, SwapDetail.Name, SwapDetail.FrontImage, SwapDetail.Pdf, SwapDetail.IsAvailable, SessionWrapper.UserDetails.UserName);
                                                objData.SequanceNo = objData.SwapToSequanceNo;
                                                if (!isError)
                                                {
                                                    var dataIsDone = db.InsertOrUpdatePatientsEducationBrochureMaster(objData.Id, objData.LangId, objData.SequanceNo, objData.Name, objData.FrontImage, objData.Pdf, objData.IsAvailable, SessionWrapper.UserDetails.UserName);
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
                                            var dataIsDone = db.InsertOrUpdatePatientsEducationBrochureMaster(objData.Id, objData.LangId, objData.SequanceNo, objData.Name, objData.FrontImage, objData.Pdf, objData.IsAvailable, SessionWrapper.UserDetails.UserName);
                                            if (dataIsDone != null)
                                            {
                                                if (objData.SwapToSequanceNo < objData.SwapFromSequanceNo)
                                                {
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    List<GetAllPatientsEducationBrochureMasterResult> lstLop = dataList.Where(x => x.SequanceNo >= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo++;
                                                        var swapValueUpdate = db.InsertOrUpdatePatientsEducationBrochureMaster(data.Id, objData.LangId, currentSwqNo, data.Name, data.FrontImage, data.Pdf, data.IsAvailable, SessionWrapper.UserDetails.UserName);
                                                    }
                                                }
                                                else
                                                {
                                                    List<GetAllPatientsEducationBrochureMasterResult> lstLop = dataList.Where(x => x.SequanceNo <= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo--;
                                                        var swapValueUpdate = db.InsertOrUpdatePatientsEducationBrochureMaster(data.Id, objData.LangId, currentSwqNo, data.Name, data.FrontImage, data.Pdf, data.IsAvailable, SessionWrapper.UserDetails.UserName);

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

                                    var dataIsDone = db.InsertOrUpdatePatientsEducationBrochureMaster(objData.Id, objData.LangId, objData.SequanceNo, objData.Name, objData.FrontImage, objData.Pdf, objData.IsAvailable, SessionWrapper.UserDetails.UserName);
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

                                var dataIsDone = db.InsertOrUpdatePatientsEducationBrochureMaster(objData.Id, objData.LangId, objData.SequanceNo, objData.Name, objData.FrontImage, objData.Pdf, objData.IsAvailable, SessionWrapper.UserDetails.UserName);
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

                            var dataIsDone = db.InsertOrUpdatePatientsEducationBrochureMaster(objData.Id, objData.LangId, objData.SequanceNo, objData.Name, objData.FrontImage, objData.Pdf, objData.IsAvailable, SessionWrapper.UserDetails.UserName);
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

        public bool RemovePatientsEducationBrochure(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (PatientsEducationBrochureDataContext db = new PatientsEducationBrochureDataContext(SqlConnectionSTring))
                {
                    db.RemovePatientsEducationBrochure(lgId, SessionWrapper.UserDetails.UserName);
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
