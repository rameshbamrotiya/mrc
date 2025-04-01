using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class OtherFacilitiesRepository : IOtherFacilitiesRepository
    {
        private string SqlConnectionSTring;
        public OtherFacilitiesRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        #region Main
        public List<GetAllOtherFacilitiesMasterResult> GetAllOtherFacilitiesMaster(long lgLangId)
        {
            using (OtherFacilitiesMasterDataContext db = new OtherFacilitiesMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllOtherFacilitiesMaster(lgLangId).ToList();
            }
        }

        public GetAllOtherFacilitiesMasterResult GetOtherFacilitiesMaster(long lgid,long lgLangId)
        {
            return GetAllOtherFacilitiesMaster(lgLangId).Where(x=> x.OurFacillityId== lgid).FirstOrDefault();            
        }

        public bool InsertOrUpdateAboutNursingCareMaster(GetAllOtherFacilitiesMasterResult objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (OtherFacilitiesMasterDataContext db = new OtherFacilitiesMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateOtherFacilitiesMaster(objData.Id, objData.LanguageId, objData.Title, objData.VideoLink, objData.SideImage, objData.IsVisible, SessionWrapper.UserDetails.UserName).FirstOrDefault();
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
                        objData.OurFacillityId = dataIsDone.RecId;
                        objData.Id =(long) dataIsDone.RecSubId;
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

        public bool RemoveAboutNursingCareMaster(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (OtherFacilitiesMasterDataContext db = new OtherFacilitiesMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveOtherFacilitiesMaster(lgId, SessionWrapper.UserDetails.UserName);
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
        #endregion

        #region Sub Details
        public List<GetAllOurFacilitiesMasterSubDetailsResult> GetAllOurFacilitiesMasterSubDetails(long lgId,long lgLangId)
        {
            using (OtherFacilitiesMasterDataContext db = new OtherFacilitiesMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllOurFacilitiesMasterSubDetails(lgId,lgLangId).ToList();
            }
        }

        public GetAllOurFacilitiesMasterSubDetailsResult GetOurFacilitiesMasterSubDetails(long lgid, long lgMainid, long lgLangId)
        {
            return GetAllOurFacilitiesMasterSubDetails(lgMainid,lgLangId).Where(x => x.Id == lgid).FirstOrDefault();
        }

        public bool InsertOrUpdateOurFacilitiesMasterSubDetails(GetAllOurFacilitiesMasterSubDetailsResult objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (OtherFacilitiesMasterDataContext db = new OtherFacilitiesMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateOurFacilitiesMasterSubDetails(objData.Id, objData.OurFacillityId, objData.LanguageId, objData.SequanceNo, objData.Title, objData.Description, SessionWrapper.UserDetails.UserName).FirstOrDefault();
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
                        objData.Id =(long) dataIsDone.RecId;
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

        public bool RemoveOurFacilitiesMasterSubDetails(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (OtherFacilitiesMasterDataContext db = new OtherFacilitiesMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveOurFacilitiesMasterSubDetails(lgId, SessionWrapper.UserDetails.UserName);
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

        public bool OurFacilitiesMasterSubDetailsSwap(string cmd, decimal? col_menu_level, int col_parent_id,out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (OtherFacilitiesMasterDataContext db = new OtherFacilitiesMasterDataContext(SqlConnectionSTring))
                {
                    db.OurFacilitiesMasterSubDetailsSwap(cmd, col_menu_level, col_parent_id);
                    strError = "Record Swap Successfully";
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

        #region Sub Image Details
        public List<GetAllOurFacilitiesMasterSubDetailsImageResult> GetAllOurFacilitiesMasterSubDetailsImage(long lgId, long lgLangId)
        {
            using (OtherFacilitiesMasterDataContext db = new OtherFacilitiesMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllOurFacilitiesMasterSubDetailsImage(lgId, lgLangId).ToList();
            }
        }

        public bool InsertOurFacilitiesMasterSubDetailsImage(GetAllOurFacilitiesMasterSubDetailsImageResult objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (OtherFacilitiesMasterDataContext db = new OtherFacilitiesMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOurFacilitiesMasterSubDetails(objData.Id, objData.OurFacillityId, objData.OurFacillitySubId, objData.LanguageId, objData.ImageName);
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

        public bool RemoveOurFacilitiesMasterSubDetailsImage(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (OtherFacilitiesMasterDataContext db = new OtherFacilitiesMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveOurFacilitiesMasterSubDetailsImage(lgId);
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

        #endregion

        public void Dispose()
        {

        }
    }
}
