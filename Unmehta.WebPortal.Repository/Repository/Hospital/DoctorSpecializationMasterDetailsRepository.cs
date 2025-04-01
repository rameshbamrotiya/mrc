using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class DoctorSpecializationMasterDetailsRepository : IDoctorSpecializationMasterDetailsRepository
    {
        private string SqlConnectionSTring;
        public DoctorSpecializationMasterDetailsRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllDoctorSpecializationMasterDetailsByDocIdResult> GetAllDoctorSpecializationMasterDetails(long docId=0)
        {
            using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllDoctorSpecializationMasterDetailsByDocId(docId).ToList();
            }
        }

        public GetAllDoctorSpecializationMasterDetailsByDocIdResult GetDoctorSpecializationMasterDetailsById(long docId,long lgId)
        {
            using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
            {
                return GetAllDoctorSpecializationMasterDetails(docId).Where(x=> x.Id==lgId).FirstOrDefault();
            }
        }        

        public bool InsertOrUpdateDoctorSpecializationMasterDetails(GetAllDoctorSpecializationMasterDetailsByDocIdResult objData,long lgDocId, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateDoctorSpecializationMasterDetails(objData.Id, lgDocId, objData.SpecializationId ,SessionWrapper.UserDetails.UserName);
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
        
        public bool RemoveDoctorSpecializationMasterDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveDoctorSpecializationMaster(lgId, SessionWrapper.UserDetails.UserName);
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
