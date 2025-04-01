using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class PostApplicationMasterBO
    {
        public int? Id { get; set; }
        public int? PostId { get; set; }
        public string GraduationQual { get; set; }
        public string PostGraduationQual { get; set; }
        public int? IsGraduationRequired { get; set; }
        public int? IsPostGraduationRequired { get; set; }
        public int? ExperienceRequired { get; set; }
        public int? AgeLimit { get; set; }
        public DateTime? AgeLimitCalDate { get; set; }
        public int? CalculateExperienceOn { get; set; }
        public string UserName { get; set; }
        public int Flag { get; set; }
    }

    public class QualificationMasterBo
    {
        public long? Id { get; set; }
        public string EducationDetailName { get; set; }
        public bool IsPostGraduate { get; set; }
        public bool IsVisible { get; set; }
        public string UserName { get; set; }
        public int Flag { get; set; }
    }

    public class JobApplicationMasterBo
    {
        public int? Id { get; set; }
        public string JobTitle { get; set; }
        public string AdvertiseNo { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string JobDescription { get; set; }
        public string JobFile { get; set; }
        public string JobFilePath { get; set; }
        public bool IsActive { get; set; }
        public int Flag { get; set; }
    }
}
