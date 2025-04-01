
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class DoctorExpertiseMasterDetailsRepository : IDoctorExpertiseMasterDetailsRepository
    {
        private string SqlConnectionSTring;
        public DoctorExpertiseMasterDetailsRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllDoctorExpertiseMasterDetailsByDocIdResult> GetAllDoctorExpertiseMasterDetails(long docId=0)
        {
            using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllDoctorExpertiseMasterDetailsByDocId(docId).ToList();
            }
        }

        public GetAllDoctorExpertiseMasterDetailsByDocIdResult GetDoctorExpertiseMasterById(long docId,long lgId)
        {
            using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
            {
                return GetAllDoctorExpertiseMasterDetails(docId).Where(x=> x.Id==lgId).FirstOrDefault();
            }
        }        

        public bool InsertOrUpdateDoctorExpertiseMasterDetails(GetAllDoctorExpertiseMasterDetailsByDocIdResult objData,long lgDocId, out string strError)
        {
            //_Education_Expertise obj_Education_Expertises = new _Education_Expertise();
            bool isError = false;
            strError = "";
            try
            {
                using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateDoctorExpertiseMasterDetails(objData.Id, lgDocId,objData.ExpertiseName ,SessionWrapper.UserDetails.UserName);
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
        
        public bool RemoveDoctorExpertiseMasterDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveDoctorExpertiseMasterDetails(lgId, SessionWrapper.UserDetails.UserName);
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
