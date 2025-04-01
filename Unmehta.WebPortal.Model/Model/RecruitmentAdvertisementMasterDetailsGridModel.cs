using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class RecruitmentAdvertisementMasterDetailsGridModel
    {
        public long? AdvertisementId { get; set; }
        public long? QualificationId { get; set; }        
        public string QualificationName { get; set; }
        public long EducationTypeId { get; set; }
        public string EducationTypeName { get; set; }
    }
}
