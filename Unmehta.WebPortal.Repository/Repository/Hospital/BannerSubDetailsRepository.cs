using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class BannerSubDetailsRepository : IBannerSubDetailsRepository
    {
        private string SqlConnectionSTring;
        public BannerSubDetailsRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllBannerSubDetailsByBannerIdResult> GetAllBannerSubDetails(long lgBannerId)
        {
            using (BannerSubDetailsDataContext db = new BannerSubDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllBannerSubDetailsByBannerId(lgBannerId).ToList();
            }
        }

        public GetAllBannerSubDetailsByBannerIdResult GetlBannerSubDetailsById(long lgId, long lgBannerId)
        {
            return GetAllBannerSubDetails(lgBannerId).Where(x => x.Id == lgId).FirstOrDefault();
        }

        public bool InsertOrUpdateBannerSubDetails(GetAllBannerSubDetailsByBannerIdResult objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (BannerSubDetailsDataContext db = new BannerSubDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateBannerSubDetails(objData.Id, objData.BannerId, objData.BannerDescription, objData.TextXPosition, objData.TextYPosition, SessionWrapper.UserDetails.UserName);
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
                        objData.Id = (long)dataIsDone;
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

        public bool RemoveBannerSubDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (BannerSubDetailsDataContext db = new BannerSubDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveBannerSubDetails(lgId, SessionWrapper.UserDetails.UserName);
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
