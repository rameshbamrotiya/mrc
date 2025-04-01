using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class StarOfRepository : IStarOfRepository
    {

        private string SqlConnectionSTring;

        public StarOfRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        #region Main
        public List<GetAllStarOfDetailsResult> GetAllStarOfDetails(long lgLangId)
        {
            using (StarOfMasterDataContext db = new StarOfMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllStarOfDetails(lgLangId).ToList();
            }
        }

        public GetAllStarOfDetailsResult GetOtherFacilitiesMaster(long lgid, long lgLangId)
        {
            return GetAllStarOfDetails(lgLangId).Where(x => x.StarId == lgid).FirstOrDefault();
        }

        public bool InsertOrUpdateStarOfMaster(GetAllStarOfDetailsResult objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (StarOfMasterDataContext db = new StarOfMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateStarOfMaster(objData.Id, objData.LanguageId, objData.StarPageTitle, objData.StarPageMonthTabName, objData.StarPageWeekTabName, objData.StarAccordMonthTitle, objData.StarAccordWeekTitle, SessionWrapper.UserDetails.UserName).FirstOrDefault();
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
                        objData.StarId = dataIsDone.SubId;
                        objData.Id = (long)dataIsDone.RecId;
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

        public bool RemoveStarOfDetailsById(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (StarOfMasterDataContext db = new StarOfMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveStarOfDetailsById(lgId, SessionWrapper.UserDetails.UserName);
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
        public List<GetAllStarOfAccordDetailsByStartIdResult> GetAllStarOfAccordDetailsByStartId(long lgId, long lgLangId)
        {
            using (StarOfMasterDataContext db = new StarOfMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllStarOfAccordDetailsByStartId(lgId, lgLangId).ToList();
            }
        }

        public GetAllStarOfAccordDetailsByStartIdResult GetStarOfAccordDetailsByStartId(long lgid, long lgMainid, long lgLangId)
        {
            return GetAllStarOfAccordDetailsByStartId(lgMainid, lgLangId).Where(x => x.Id == lgid).FirstOrDefault();
        }

        public bool InsertOrUpdateStarOfAccordDetails(GetAllStarOfAccordDetailsByStartIdResult objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (StarOfMasterDataContext db = new StarOfMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateStarOfAccordDetails(objData.Id, objData.StarId, objData.LanguageId, objData.AccordTitle, objData.SequanceNo, objData.TypeMonthOrWeek, objData.IsVisible, SessionWrapper.UserDetails.UserName).FirstOrDefault();
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
                        objData.Id = (long)dataIsDone.RecId;
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

        public bool RemoveStarOfAccordDetailsById(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (StarOfMasterDataContext db = new StarOfMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveStarOfAccordDetailsById(lgId, SessionWrapper.UserDetails.UserName);
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

        public bool StarOfAccordDetailsSwap(string cmd, decimal? col_menu_level, int col_parent_id, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (StarOfMasterDataContext db = new StarOfMasterDataContext(SqlConnectionSTring))
                {
                    db.StarOfAccordDetailsSwap(cmd, col_menu_level, col_parent_id);
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

        public List<GetAllStarOfAccordSubImageDetailsByAccordIdResult> GetAllStarOfAccordSubImageDetailsByAccordId(long lgId, long lgLangId)
        {
            using (StarOfMasterDataContext db = new StarOfMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllStarOfAccordSubImageDetailsByAccordId(lgId, lgLangId).ToList();
            }
        }

        public bool InsertStarOfAccordSubImageDetailsByAccordId(GetAllStarOfAccordSubImageDetailsByAccordIdResult objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (StarOfMasterDataContext db = new StarOfMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertStarOfAccordSubImageDetailsByAccordId(objData.StarId, objData.AccordId, objData.LanguageId, objData.ImageName, objData.Name, objData.Description, objData.IsVisible);
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

        public bool RemoveStarOfAccordSubImageDetailsByAccordId(int lgId, int lgLangId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (StarOfMasterDataContext db = new StarOfMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveStarOfAccordSubImageDetailsByAccordId(lgId, lgLangId);
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
