using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;

namespace Unmehta.WebPortal.Repository.Repository
{
    public class DesignationRepository : IDesignationRepository
    {
        private string SqlConnectionSTring;
        public DesignationRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<DesignationGridModel> GetAllTblDesignation()
        {
            using (FacultyDataContext db = new FacultyDataContext(SqlConnectionSTring))
            {
                return db.GetAllDesignationMaster().Select(x => new DesignationGridModel
                {
                    Id = x.Id,
                    LanguageId = x.LanguageId,
                    LanguageName = x.LanguageName,
                    DesignationName = x.DesignationName,
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }

        public List<DesignationGridModel> GetAllTblDesignationLang(long lgId)
        {
            using (FacultyDataContext db = new FacultyDataContext(SqlConnectionSTring))
            {
                return db.GetAllDesignationMasterLang(lgId).Select(x => new DesignationGridModel
                {
                    Id = x.Id,
                    DesignationName = x.DesignationName,
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }

        public DesignationGridModel GetTblDesignationById(long lgId)
        {
            using (FacultyDataContext db = new FacultyDataContext(SqlConnectionSTring))
            {
                return db.GetDesignationMasterById((int)lgId).Select(x => new DesignationGridModel
                {
                    Id = x.Id,
                    LanguageId = x.LanguageId,
                    LanguageName = x.LanguageName,
                    DesignationName = x.DesignationName,
                    IsVisible = x.IsVisible
                }).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateTblDesignation(DesignationGridModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (FacultyDataContext db = new FacultyDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateDesignationMaster(objData.Id, objData.LanguageId, objData.DesignationName, objData.IsVisible, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveTblDesignation(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (FacultyDataContext db = new FacultyDataContext(SqlConnectionSTring))
                {
                    db.RemoveDesignationMaster((int)lgId, SessionWrapper.UserDetails.UserName);
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
