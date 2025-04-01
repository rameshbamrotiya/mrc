
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class DoctorQualificationMasterDetailsRepository : IDoctorQualificationMasterDetailsRepository
    {
        private string SqlConnectionSTring;
        public DoctorQualificationMasterDetailsRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllDoctorQualificationMasterDetailsByDocIdResult> GetAllDoctorQualificationMasterDetails(long docId=0)
        {
            using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllDoctorQualificationMasterDetailsByDocId(docId).ToList();
            }
        }

        public GetAllDoctorQualificationMasterDetailsByDocIdResult GetDoctorQualificationMasterById(long docId,long lgId)
        {
            using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
            {
                return GetAllDoctorQualificationMasterDetails(docId).Where(x=> x.Id==lgId).FirstOrDefault();
            }
        }        

        public bool InsertOrUpdateDoctorQualificationMasterDetails(GetAllDoctorQualificationMasterDetailsByDocIdResult objData,long lgDocId, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateDoctorQualificationMasterDetails(objData.Id, lgDocId,objData.QualificationName,objData.QualificationShortName ,SessionWrapper.UserDetails.UserName);
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
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
        
        public bool RemoveDoctorQualificationMasterDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveDoctorQualificationMasterDetails(lgId, SessionWrapper.UserDetails.UserName);
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
