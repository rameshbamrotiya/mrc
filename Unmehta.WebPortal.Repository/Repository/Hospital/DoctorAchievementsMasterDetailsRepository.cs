
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class DoctorAchievementsMasterDetailsRepository : IDoctorAchievementsMasterDetailsRepository
    {
        private string SqlConnectionSTring;
        public DoctorAchievementsMasterDetailsRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllDoctorAchivementsMasterDetailsByDocIdResult> GetAllDoctorAchievementsMasterDetails(long docId=0)
        {
            using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllDoctorAchivementsMasterDetailsByDocId(docId).ToList();
            }
        }

        public GetAllDoctorAchivementsMasterDetailsByDocIdResult GetDoctorAchievementsMasterById(long docId,long lgId)
        {
            using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
            {
                return GetAllDoctorAchievementsMasterDetails(docId).Where(x=> x.Id==lgId).FirstOrDefault();
            }
        }        

        public bool InsertOrUpdateDoctorAchievementsMasterDetails(GetAllDoctorAchivementsMasterDetailsByDocIdResult objData,long lgDocId, out string strError)
        {
            //_Education_Achievements obj_Education_Achievementss = new _Education_Achievements();
            bool isError = false;
            strError = "";
            try
            {
                using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateDoctorAchivementsMasterDetails(objData.Id, lgDocId,objData.AchievementsName,SessionWrapper.UserDetails.UserName);
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
        
        public bool RemoveDoctorAchievementsMasterDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (DoctorMasterWithDetailsDataContext db = new DoctorMasterWithDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveDoctorAchivementsMasterDetails(lgId, SessionWrapper.UserDetails.UserName);
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
