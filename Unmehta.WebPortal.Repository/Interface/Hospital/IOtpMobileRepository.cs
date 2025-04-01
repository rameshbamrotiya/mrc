using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IOtpMobileRepository : IDisposable
    {
        bool InsertOrUpdateOtpMobileMaster(CMSMobileOtpManage objData, out string strError);

        CMSMobileOtpManage GetOptByMobileNo(string mobileNo, int otpExpire);
    }
}
