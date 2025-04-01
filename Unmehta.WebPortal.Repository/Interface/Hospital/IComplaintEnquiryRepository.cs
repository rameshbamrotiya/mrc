using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IComplaintEnquiryRepository :IDisposable
    {
        List<GetAllComplaintEnquiryMasterResult> GetAllDoctorForDropDownByLangId(bool isEnquiry);

        bool InsertOrUpdateComplaintEnquiry(GetAllComplaintEnquiryMasterResult objData, bool isEnquiry, out string strError);

        bool RemoveComplaintEnquiry(long lgId, out string strError);
    }
}
