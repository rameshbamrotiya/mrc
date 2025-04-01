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
    public class DepartmentRepository : IDepartmentRepository
    {
        private string SqlConnectionSTring;
        public DepartmentRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<DepartmentGridModels> GetAllTblDepartment(long lgLangID)
        {
            using (FacultyDataContext db = new FacultyDataContext(SqlConnectionSTring))
            {
                return db.GetAllDepartmentMaster(lgLangID).Select(x => new DepartmentGridModels
                {
                    Id = x.DeptId,
                    DepartmentName = x.DepartmentName,
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }
        
        public List<DepartmentGridModels> GetAllDepartmentFront(long lgLangID)
        {
            using (FacultyDataContext db = new FacultyDataContext(SqlConnectionSTring))
            {
                return db.GetAllDepartmentMaster(lgLangID).Select(x => new DepartmentGridModels
                {
                    Id = x.Id,
                    DeptId = x.DeptId,
                    DepartmentName = x.DepartmentName,
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }

        public List<DepartmentGridModels> GetAllTblDepartmentForDropDown(long lgLangID)
        {
            using (FacultyDataContext db = new FacultyDataContext(SqlConnectionSTring))
            {
                return db.GetAllDepartmentMaster(lgLangID).Where(x=> x.IsVisible==true).Select(x => new DepartmentGridModels
                {
                    Id = x.Id,
                    DepartmentName = x.DepartmentName,
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }
        public DepartmentGridModels GetTblDepartmentById(int lgId, long lgLangID)
        {
            using (FacultyDataContext db = new FacultyDataContext(SqlConnectionSTring))
            {
                return GetAllTblDepartment(lgLangID).Where (x=> x.Id==lgId).Select(x => new DepartmentGridModels
                {
                    Id = x.Id,
                    LanguageId = x.LanguageId,
                    LanguageName = x.LanguageName,
                    DepartmentName = x.DepartmentName,
                    IsVisible = x.IsVisible
                }).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateTblDepartment(DepartmentGridModels objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (FacultyDataContext db = new FacultyDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateDepartmentMaster(objData.Id, objData.LanguageId, objData.DepartmentName, objData.IsVisible, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveTblDepartment(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (FacultyDataContext db = new FacultyDataContext(SqlConnectionSTring))
                {
                    db.RemoveDepartmentMaster(lgId, SessionWrapper.UserDetails.UserName);
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
