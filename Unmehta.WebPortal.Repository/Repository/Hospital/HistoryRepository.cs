using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using System.Data;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class HistoryRepository : IHistoryRepository
    {
        private string SqlConnectionSTring;
        public HistoryRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<HistoryMasterGridModel> GetAllTblHistory(long lgId)
        {
            using (HistoryMasterDataContext db = new HistoryMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllHistoryMaster(lgId).Select(x => new HistoryMasterGridModel
                {
                    Id = x.Id,
                    LanguageId = x.LanguageId,
                    Year = x.Year,
                    HistoryTitle = x.HistoryTitle,
                    HistoryDescription = x.HistoryDescription,
                    MetaTitle=x.MetaTitle,
                    MetaDescription=x.MetaDescription,
                    HistoryPhotoName= x.HistoryImage,
                    //HistoryPhotoName = x.HistoryPhotoName,
                    //HistoryPhotoPath = x.HistoryPhotoPath,
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }

        public HistoryMasterGridModel GetTblHistoryById(long lgId, long lgLangId)
        {
            using (HistoryMasterDataContext db = new HistoryMasterDataContext(SqlConnectionSTring))
            {
                return GetAllTblHistory((int)lgLangId).Where(x=> x.Id== lgId).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateTblHistory(HistoryMasterModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (HistoryMasterDataContext db = new HistoryMasterDataContext(SqlConnectionSTring))
                {
                            var dataIsDone = db.InsertOrUpdateHistoryMaster((int)objData.Id, objData.LanguageId, objData.MetaTitle, objData.MetaDescription, objData.Year, objData.HistoryTitle, objData.HistoryDescription
                        ,objData.HistoryPhotoName , objData.IsVisible, SessionWrapper.UserDetails.UserName);
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
                                objData.Id = (int)dataIsDone.FirstOrDefault().RecId;
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

        public bool RemoveTblHistory(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (HistoryMasterDataContext db = new HistoryMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveHistoryMaster(lgId, SessionWrapper.UserDetails.UserName);
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
