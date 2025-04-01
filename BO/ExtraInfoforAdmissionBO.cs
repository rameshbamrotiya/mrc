using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ExtraInfoforAdmissionBO
    {
        public long Id { get; set; }
        public long? CandidateId { get; set; }
        public long? CourseId { get; set; }
        public string UNMICRCContact { get; set; }
        public string courseheard { get; set; }
        public string ChronicalIllness { get; set; }
        public string BloodGroup { get; set; }
        public string Allergic { get; set; }
        public string MonthYear { get; set; }
        public string EmergencyContactNo { get; set; }
        public string EmergencyContactPersonName { get; set; }
        public string EmergencyContactPersonRelation { get; set; }
        public string EmergencyContactPersonAdd { get; set; }
        public string CourtOfLaw { get; set; }
        public string Accommodation { get; set; }
        public string ExtraActivities { get; set; }
        public string ExtraActivitiesSocial { get; set; }
        public string SurgeryInfo { get; set; }
        public string AboutCourse { get; set; }
        public string AboutCourseOther { get; set; }
        public string modified_by { get; set; }
        public string ip_add { get; set; }
        public int? IsExist { get; set; }
        public string UNMICRCContactYN { get; set; }
        public string ChronicalIllnessYN { get; set; }
        public string SurgeryInfoYN { get; set; }
        public string AllergicYN { get; set; }
        public string CourtOfLawYN { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string MobileNo { get; set; }
        public string RelationShip { get; set; }
        public double YearsKnown { get; set; }
        public string Address { get; set; }
    }
}
