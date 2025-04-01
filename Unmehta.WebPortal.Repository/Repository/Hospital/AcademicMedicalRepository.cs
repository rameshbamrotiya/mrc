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
    public class AcademicMedicalRepository : IAcademicMedicalRepository
    {
        private string SqlConnectionSTring;
        public AcademicMedicalRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllAcademicMedicalMasterResult> GetAllAcademicMedical(long lgLangId = 1)
        {
            using (AcademicMedicalDataContext db = new AcademicMedicalDataContext(SqlConnectionSTring))
            {
                return db.GetAllAcademicMedicalMaster(lgLangId).ToList();
            }
        }


        public List<GetAcademicMedicalMasterDoctorDetailsByAccIdAndLanIdResult> GetAcademicMedicalMasterDoctorDetails(long lgAccId,long lgLangId)
        {
            using (AcademicMedicalDataContext db = new AcademicMedicalDataContext(SqlConnectionSTring))
            {
                return db.GetAcademicMedicalMasterDoctorDetailsByAccIdAndLanId(lgAccId,lgLangId).ToList();
            }
        }


        public List<GetAcademicMedicalMasterByIdAndLangIdResult> GetAcademicMedicalMasterByIdAndLgId(long lgAccId, long lgLangId)
        {
            using (AcademicMedicalDataContext db = new AcademicMedicalDataContext(SqlConnectionSTring))
            {
                return db.GetAcademicMedicalMasterByIdAndLangId(lgAccId, lgLangId).ToList();
            }
        }

        public List<GetAllDoctorByLanguageIdResult> GetAllAcademicMedicalDoctor(long lgLangId)
        {
            using (IDoctorMasterRepository pbjList = new DoctorMasterRepository(SqlConnectionSTring))
            {
                return pbjList.GetAllDoctorForDropDownByLangId(lgLangId).ToList();
            }
        }
        
        public bool InsertOrUpdateAcademicMedical(AcademicMedicalModel objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (AcademicMedicalDataContext db = new AcademicMedicalDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateAcademicMedicalMaster(objData.Id, objData.LangId,objData.MetaTitle,objData.MetaDescription,objData.AcademicsName,objData.AcademicsFullName,objData.AcademicsDescription, objData.ImagePath, SessionWrapper.UserDetails.UserName);
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
        
        public bool InsertAcademicMedical(AcademicMedicalDoctorModel objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (AcademicMedicalDataContext db = new AcademicMedicalDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertAcademicMedicalMasterDoctorDetails(objData.AccId, objData.LangId, objData.YearId, SessionWrapper.UserDetails.UserName, objData.StudentName, objData.DegreeHold, objData.Photo);
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = "Record Inserted Successfully";
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

        public bool RemoveAcademicMedical(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (AcademicMedicalDataContext db = new AcademicMedicalDataContext(SqlConnectionSTring))
                {
                    db.RemoveAcademicMedicalMasterById(lgId, SessionWrapper.UserDetails.UserName);
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
        
        public bool RemoveAcademicMedicalDoctorDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (AcademicMedicalDataContext db = new AcademicMedicalDataContext(SqlConnectionSTring))
                {
                    db.RemoveAcademicMedicalMasterDoctorDetails(lgId, SessionWrapper.UserDetails.UserName);
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
