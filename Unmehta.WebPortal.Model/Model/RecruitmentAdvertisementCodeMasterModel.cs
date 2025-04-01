using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class RecruitmentAdvertisementCodeMasterModel
    {
        public long? Id { get; set; }
        public string AdvertisementCode { get; set; }
        public string Generalinstructionfile { get; set; }
        public string AdvertisementDesc { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsNewIcon { get; set; }
    }
}
