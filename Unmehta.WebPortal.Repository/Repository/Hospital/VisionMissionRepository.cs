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
    public class VisionMissionRepository : IVisionMissionRepository
    {
        private string SqlConnectionSTring;
        public VisionMissionRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllVisionMissionMasterByLangIdResult> GetAllVisionMissionMaster(long lgLangId)
        {
            using (VisionMissionMasterDataContext db = new VisionMissionMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllVisionMissionMasterByLangId(lgLangId).ToList();
            }
        }

        public GetAllVisionMissionMasterByLangIdResult GetVisionMissionMasterByLangId(long lgId, long lgLangId)
        {

            return GetAllVisionMissionMaster(lgLangId).Where(x => x.Id == lgId).FirstOrDefault();

        }

        public bool InsertOrUpdateVisionMissionMaster(GetAllVisionMissionMasterByLangIdResult objData, long lgLangId, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (VisionMissionMasterDataContext db = new VisionMissionMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateVisionMissionMaster(objData.Id, lgLangId, objData.MetaTitle,objData.MetaDescription, objData.Descr,
                         SessionWrapper.UserDetails.UserName, objData.IsVisible);
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

        public bool RemoveVisionMissionMaster(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (VisionMissionMasterDataContext db = new VisionMissionMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveVisionMissionMaster(lgId, SessionWrapper.UserDetails.UserName);
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

        public bool InsertUnitDoctor(GetAllVisionMissionMasterImageDetailsByVisionIdAndLangIdResult objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (VisionMissionMasterDataContext db = new VisionMissionMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertVisionMissionMasterImageDetails(objData.VisionId, objData.LanguageId, objData.ImageName, SessionWrapper.UserDetails.UserName);
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
                using (VisionMissionMasterDataContext db = new VisionMissionMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveVisionMissionMasterImageDetails(lgId);
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

        public List<VisionMissionImageModel> GetAllVisionMissionMasterImageDetailsByLangId(long lgId, long lgLangId)
        {
            using (VisionMissionMasterDataContext db = new VisionMissionMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllVisionMissionMasterImageDetailsByVisionIdAndLangId(lgId, lgLangId).Select(x =>
                new VisionMissionImageModel
                {
                    Id = x.Id,
                    ImageName = x.ImageName
                }
                ).ToList();
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
