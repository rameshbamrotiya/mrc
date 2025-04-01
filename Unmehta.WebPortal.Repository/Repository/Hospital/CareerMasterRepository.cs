using System;
using System.Collections.Generic;
using System.Linq;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class CareerMasterRepository : ICareerMasterRepository
    {
        private string SqlConnectionSTring;
        public CareerMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<CareerMasterGridModel> GetAllTblCareer(int lgId)
        {
            using (CareerMasterDataContext db = new CareerMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllCareerMaster(lgId).Select(x => new CareerMasterGridModel
                {
                    Id = x.Id,
                    LanguageId = x.LanguageId,
                    DepartmentId = x.DepartmentId,
                    DepartmentName = x.DepartmentName,
                    DesignationId = x.DesignationId,
                    DesignationName = x.DesignationName,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    TotalVacancy = x.TotalVacancy,
                    RequirementType = x.RequirementType,
                    FileName = x.FileName,
                    FilePath = x.FilePath,
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }

        public CareerMasterGridModel GetTblCareerById(int lgId, int lgLangId)
        {
            using (CareerMasterDataContext db = new CareerMasterDataContext(SqlConnectionSTring))
            {
                return GetAllTblCareer(lgLangId).Where(x=> x.Id==lgId).Select(x => new CareerMasterGridModel
                {
                    Id = x.Id,
                    LanguageId = x.LanguageId,
                    LanguageName = x.LanguageName,
                    DepartmentId = x.DepartmentId,
                    DepartmentName = x.DepartmentName,
                    DesignationId = x.DesignationId,
                    DesignationName = x.DesignationName,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    TotalVacancy = x.TotalVacancy,
                    RequirementType = x.RequirementType,
                    FileName = x.FileName,
                    FilePath = x.FilePath,
                    IsVisible = x.IsVisible
                }).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateTblCareer(CareerMasterGridModel objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (CareerMasterDataContext db = new CareerMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateCareerMaster(objData.Id, objData.LanguageId, objData.DepartmentId, objData.DesignationId, objData.ShortDescription, objData.Description, objData.TotalVacancy, objData.RequirementType, objData.FileName, objData.FilePath, objData.IsVisible, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveTblCareer(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (CareerMasterDataContext db = new CareerMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveCareerMaster(lgId, SessionWrapper.UserDetails.UserName);
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
