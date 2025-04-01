using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    [Serializable()]
    public class CandidateCourseDetailsModel
    {
        public long? Id { get; set; }
        public long? CandidateId { get; set; }
        public string CourseTitle { get; set; }
        public string Duration { get; set; }
        public string InstituteName { get; set; }
        public string City { get; set; }
        public bool? IsVisible { get; set; }
    }

    [Serializable()]
    public class CandidateFamilyDetailsModel 
    {
        public long? Id { get; set; }
        public long? RowId { get; set; }
        public string strId { get; set; }
        public string RegistrationId { get; set; }
        public long? CandidateId { get; set; }
        public string MemberName { get; set; }
        public long? Age { get; set; }
        public long? RelationId { get; set; }
        public string RelationName { get; set; }
        public string Occupation { get; set; }
        public decimal MonthlyIncome { get; set; }
        public bool? IsVisible { get; set; }
    }

    [Serializable()]
    public class CandidateProfessionalReferralDetailsModel
    {
        public long? Id { get; set; }
        public long? CandidateId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string MobileNo { get; set; }
        public string RelationShip { get; set; }
        public double YearsKnown { get; set; }
        public string Address { get; set; }
        public string Regid { get; set; }
    }
}
