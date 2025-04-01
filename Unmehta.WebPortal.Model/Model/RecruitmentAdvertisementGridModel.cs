using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class RecruitmentAdvertisementGridModel
    {
        public long? Id { get; set; }
        public string AdvertisementName { get; set; }
        public string AdvertisementDesc { get; set; }
        public string PostCode { get; set; }
        public string AdvertisementCode { get; set; }
        public string PostType { get; set; }
        public string RecrutmentType { get; set; }
        public long? Noopening { get; set; }
        public int? Designation { get; set; }
        public long? MaxExp { get; set; }
        public string AdvertisementType { get; set; }
        public string Gender { get; set; }        
        public DateTime? AgeLimitCalOn { get; set; }
        public DateTime? PublishDate { get; set; }
        public string FileName { get; set; }
        public int? MaxAge { get; set; }
        public long? MinExp { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
        public int? IsNewIcon { get; set; }
        public DateTime? InterviewDate { get; set; }
        public string InterviewTime { get; set; }
        public string ReportingTime { get; set; }
    }

    public class RecruitmentAdvertisementDetailGridModel
    {
        public long? Id { get; set; }
        public string EducationName { get; set; }

    }
}
