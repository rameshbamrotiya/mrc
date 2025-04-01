using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;

namespace Unmehta.WebPortal.Repository.Repository.Faculty
{
    public class FacultyRepository : IFacultyRepository
    {
        private string SqlConnectionSTring;
        public FacultyRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<FacultyGridModel> GetAllTblFaculty(int lgId)
        {
            using (FacultyDataContext db = new FacultyDataContext(SqlConnectionSTring))
            {
                return db.GetAllFacultyMaster(lgId).Select(x => new FacultyGridModel
                {
                    Id = x.Id,
                    LanguageId = x.LanguageId,
                    FacultyName = x.FacultyName,
                    ImageName = x.ImageName,
                    ImagePath = x.ImagePath,
                    DesignationId = x.DesignationId,
                    DesignationName = x.DesignationName,
                    DepartmentId = x.DepartmentId,
                    DepartmentName = x.DepartmentName,
                    IsVisible = x.IsVisible,
                    Email= x.Email,
                    FacultyDescription = x.FacultyDescription,
                    MobileNumber = x.MobileNumber,
                    sequenceNo =x.sequenceNo,

                }).ToList();
            }
        }

        public FacultyGridModel GetTblFacultyById(int lgId)
        {
            using (FacultyDataContext db = new FacultyDataContext(SqlConnectionSTring))
            {
                return db.GetFacultyMasterById(lgId).Select(x => new FacultyGridModel
                {
                    Id = x.Id,
                    LanguageId = x.LanguageId,
                    FacultyName = x.FacultyName,
                    ImageName = x.ImageName,
                    ImagePath = x.ImagePath,
                    DesignationId = x.DesignationId,
                    DesignationName = x.DesignationName,
                    DepartmentId = x.DepartmentId,
                    DepartmentName = x.DepartmentName,
                    IsVisible = x.IsVisible,
                    Email = x.Email,
                    FacultyDescription = x.FacultyDescription,
                    MobileNumber = x.MobileNumber,
                    sequenceNo = x.sequenceNo
                }).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateTblFaculty(FacultyGridModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (FacultyDataContext db = new FacultyDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateFacultyMaster(objData.Id, objData.LanguageId, objData.FacultyName,objData.FacultyDescription,objData.MobileNumber, objData.Email, objData.DesignationName, objData.ImageName, objData.ImagePath, objData.DepartmentId, objData.IsVisible, SessionWrapper.UserDetails.UserName,Convert.ToDecimal(objData.sequenceNo));
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

        public bool RemoveTblFaculty(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (FacultyDataContext db = new FacultyDataContext(SqlConnectionSTring))
                {
                    db.RemoveFacultyMaster(lgId, SessionWrapper.UserDetails.UserName);
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
