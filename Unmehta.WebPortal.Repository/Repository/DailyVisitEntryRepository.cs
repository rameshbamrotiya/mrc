using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface;

namespace Unmehta.WebPortal.Repository.Repository
{
    public class DailyVisitEntryRepository : IDailyVisitEntryRepository
    {
        private string SqlConnectionSTring;
        public DailyVisitEntryRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }


        public bool InsertOrUpdateDailyVisitEntry(GetAllDailyEntryVisitMasterResult objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (DailyEntryVisitMasterDataContext db = new DailyEntryVisitMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateDailyEntryVisitMaster(objData.Id, objData.DailyCatId, objData.VisitCount, objData.EntryName, objData.FileName, objData.PDFFileName, objData.EntryDate, objData.IsVisable, SessionWrapper.UserDetails.UserName).FirstOrDefault();
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

        public List<GetAllDailyEntryVisitMasterResult> GetAllDailyVisitEntry()
        {
            using (DailyEntryVisitMasterDataContext db = new DailyEntryVisitMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllDailyEntryVisitMaster().ToList();
            }
        }

        public GetAllDailyEntryVisitMasterResult GetAllDailyVisitEntryById(long lgId)
        {
            using (DailyEntryVisitMasterDataContext db = new DailyEntryVisitMasterDataContext(SqlConnectionSTring))
            {
                return GetAllDailyVisitEntry().Where(x => x.Id == lgId).FirstOrDefault();
            }
        }
        
        public bool RemoveDailyVisitEntry(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (DailyEntryVisitMasterDataContext db = new DailyEntryVisitMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.RemoveDailyEntryVisitMaster(lgId, SessionWrapper.UserDetails.UserName);
                    if (dataIsDone != null)
                    {
                       
                            strError = "Record Removed Successfully";
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


        public bool InsertOrUpdateDailyVisitCategory(GetAllDailyVisitCategoryMasterResult objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (DailyEntryVisitMasterDataContext db = new DailyEntryVisitMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateDailyVisitCategoryMaster(objData.Id, objData.DailyCatagoryName, SessionWrapper.UserDetails.UserName).FirstOrDefault();
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

        public List<GetAllDailyVisitCategoryMasterResult> GetAllDailyVisitCategory()
        {
            using (DailyEntryVisitMasterDataContext db = new DailyEntryVisitMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllDailyVisitCategoryMaster().ToList();
            }
        }

        public GetAllDailyVisitCategoryMasterResult GetAllDailyVisitCategoryById(long lgId)
        {
            using (DailyEntryVisitMasterDataContext db = new DailyEntryVisitMasterDataContext(SqlConnectionSTring))
            {
                return GetAllDailyVisitCategory().FirstOrDefault(x => x.Id == lgId);
            }
        }

        public bool RemoveDailyVisitCategoryMaster(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (DailyEntryVisitMasterDataContext db = new DailyEntryVisitMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.RemoveDailyVisitCategoryMaster(lgId, SessionWrapper.UserDetails.UserName);
                    if (dataIsDone != null)
                    {

                        strError = "Record Removed Successfully";
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


        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
