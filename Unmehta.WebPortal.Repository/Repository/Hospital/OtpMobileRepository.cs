using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class OtpMobileRepository : IOtpMobileRepository
    {
        private string SqlConnectionSTring;
        public OtpMobileRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public CMSMobileOtpManage GetOptByMobileNo(string mobileNo,int otpExpire)
        {
            using (OtpMobileMasterDataContext db = new OtpMobileMasterDataContext(SqlConnectionSTring))
            {
                return db.CMSMobileOtpManages.Where(x => x.MobileNo.Trim() == mobileNo &&  x.EntryDate.Value.AddMinutes(otpExpire) >= DateTime.Now).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateOtpMobileMaster(CMSMobileOtpManage objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (OtpMobileMasterDataContext db = new OtpMobileMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateCMSMobileOtpManage(objData.MobileNo, objData.OTPNo);
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = objData.OTPNo+" OTP Created Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            strError = objData.OTPNo + " OTP Updated Successfully";
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

        public void Dispose()
        {
        }
    }
}
