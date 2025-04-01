using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OnlineEventRegistrtionBO
    {
        public int? Id { get; set; }
        public int? EventId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhysicalDisability { get; set; }
        public string ExplainTypeofDisability { get; set; }
        public string PhysicalActivity { get; set; }
        public string TypeOfIdentity { get; set; }
        public string IdentityNumber { get; set; }
        public string Residential { get; set; }        
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string EducationQualification { get; set; }
        public string OrganizationName { get; set; }
        public string Designation { get; set; }
        public string EmployeeId { get; set; }
        public DateTime JoiningDate { get; set; }
        public string NoOfOrganizationYouWorkWith { get; set; }
        public string NoOfCNEYouAttend { get; set; }
        public string NoOfCMEYouAttend { get; set; }
        public string WorkExperience { get; set; }
        public string GNCNo { get; set; }
        public string RegistrtionNo { get; set; }
        public string WorkProfession { get; set; }
    }
}
