using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class ComplaintEnquiryRepository : IComplaintEnquiryRepository
    {
        private string SqlConnectionSTring;
        public ComplaintEnquiryRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllComplaintEnquiryMasterResult> GetAllDoctorForDropDownByLangId(bool isEnquiry)
        {
            using (ComplaintEnquiryMasterDataContext db = new ComplaintEnquiryMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllComplaintEnquiryMaster(isEnquiry).ToList();
            }
        }

        public bool InsertOrUpdateComplaintEnquiry(GetAllComplaintEnquiryMasterResult objData,bool isEnquiry, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (ComplaintEnquiryMasterDataContext db = new ComplaintEnquiryMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateComplaintEnquiryMaster(objData.Id, objData.FullName, objData.EmailId, objData.MobileNo, objData.CountryId
                        , objData.StateId, objData.CityId, objData.Message, isEnquiry, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveComplaintEnquiry(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (ComplaintEnquiryMasterDataContext db = new ComplaintEnquiryMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveComplaintEnquiryMaster(lgId, SessionWrapper.UserDetails.UserName);
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
