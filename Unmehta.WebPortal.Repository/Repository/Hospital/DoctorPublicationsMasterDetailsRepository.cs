
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class DoctorPublicationsMasterDetailsRepository : IDoctorPublicationsMasterDetailsRepository
    {
        private string SqlConnectionSTring;
        public DoctorPublicationsMasterDetailsRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllDoctorPublicationsMasterDetailsByDocIdResult> GetAllDoctorPublicationsMasterDetails(long docId=0)
        {
            using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllDoctorPublicationsMasterDetailsByDocId(docId).ToList();
            }
        }

        public GetAllDoctorPublicationsMasterDetailsByDocIdResult GetDoctorPublicationsMasterById(long docId,long lgId)
        {
            using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
            {
                return GetAllDoctorPublicationsMasterDetails(docId).Where(x=> x.Id==lgId).FirstOrDefault();
            }
        }        

        public bool InsertOrUpdateDoctorPublicationsMasterDetails(GetAllDoctorPublicationsMasterDetailsByDocIdResult objData,long lgDocId, out string strError)
        {
            //_Education_Publications obj_Education_Publicationss = new _Education_Publications();
            bool isError = false;
            strError = "";
            try
            {
                using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateDoctorPublicationsMasterDetails(objData.Id, lgDocId,objData.Publications,SessionWrapper.UserDetails.UserName);
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
        
        public bool RemoveDoctorPublicationsMasterDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveDoctorPublicationsMasterDetails(lgId, SessionWrapper.UserDetails.UserName);
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
