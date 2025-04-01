using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    [Serializable()]
    public class CandidateDetailsModel
    {
        public long? Id { get; set; }
        public long? Advertisementid { get; set; }
        public string RegistrationId { get; set; }     
        public string FirstName { get; set; }        
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MotherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long? Age { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public string AadharCard { get; set; }
        public string PermenentAddress  { get; set; }
        public string PermenentPincode  { get; set; }
        public string PermenentCountry   { get; set; }
        public string PermenentState   { get; set; }
        public string PermenentCity { get; set; }
        public string PermenentPhoneR   { get; set; }
        public string PermenentPhoneM   { get; set; }
        public string PresentAddress    { get; set; }
        public string PresentPincode    { get; set; }
        public string PresentCountry { get; set; }
        public string CountryCode { get; set; }
        public string CountryCode1 { get; set; }
        public string PresentState { get; set; }
        public string PresentCity { get; set; }
        public string PresentPhoneR     { get; set; }
        public string PresentPhoneM     { get; set; }
        public long? CasteId { get; set; }
        public int? Gender { get; set; }
        public string Religion { get; set; }
        public string MaritalStatus { get; set; }
        public string SpouseFirstName { get; set; }
        public string SpouseMiddleName { get; set; }
        public string SpouseSurname { get; set; }
        public DateTime? SpouseDOB { get; set; }
        public string SpouseContact { get; set; }
        public string EmergencyContactPersonName { get; set; }
        public string EmergencyContactNo { get; set; }
        public string Password { get; set; }
        public string PhotographName { get; set; }
        public string PhotographPath { get; set; }
        public string SignatureName { get; set; }
        public string SignaturePath { get; set; }
        public string RoleInLastEmployment { get; set; }
        public decimal CurrentSalaryPerMonth { get; set; }
        public decimal ExpectedSalary { get; set; }
        public string ExtraActivities { get; set; }
        public string MemberShipOfAnySpecialBodies { get; set; }
        public bool? HaveChronicIllness { get; set; }
        public string ChronicIllnessDetails { get; set; }
        public bool? IsOffenceRegistered { get; set; }
        public string OffneceDetails { get; set; }
        public string DescribeYourself   { get; set; }
        public string BiggestAchivement  { get; set; }
        public string YourBiggestFailure { get; set; }
        public string VisionInNextYears { get; set; }
        public string Remarks { get; set; }
        public string AdvertisementAwarenessSource { get; set; }
        public string UNMICRCContact { get; set; }
    }

    [Serializable()]
    public class CandidateEducationDetailsModel
    {
        public long? Id { get; set; }
        public long? CandidateId { get; set; }
        public long? EducationId { get; set; }
        public long? EducationTypeId { get; set; }
        public string RegisrationId { get; set; }
        public string NameOfSchoolCollege { get; set; }
        public string BoardUniversity { get; set; }
        public string PassingYear { get; set; }
        public string MajorSubjects { get; set; }
        public decimal PercentageOrPercentile { get; set; }
        public string Division { get; set; }
        public bool? IsVisible { get; set; }
        public string CertificateFileName { get; set; }
        public string CertificateFilePath { get; set; }
    }

    [Serializable()]
    public class CandidateExperienceDetailsModel
    {
        public long? Id { get; set; }
        public long? CandidateId { get; set; }
        public int? JobType { get; set; }
        public int? PostTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Experience { get; set; }
        public string Designation { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationAddress { get; set; }
        public string ReportingAuthority { get; set; }
        public decimal SalaryPerMonth { get; set; }
        public string ReasonForChange { get; set; }
        public string PostName { get; set; }
        public string DepartmentName { get; set; }
        public string ExperienceCertificateFileName { get; set; }
        public string ExperienceCertificateFilePath { get; set; }
    }

    [Serializable()]
    public class CandidateDetailsForExperienceModel
    {
        public long? CandidateId { get; set; }
        public string RoleInLastEmployment { get; set; }
        public Decimal CurrentSalaryPerMonth { get; set; }
        public Decimal ExpectedSalary { get; set; }
    }

    [Serializable()]
    public class GetCandidateExperienceTotalModel
    {
        public long? CandidateId { get; set; }
        public long? Years { get; set; }
        public long? Months { get; set; }
        public long? Days { get; set; }
    }

    [Serializable()]
    public class CandidateDetailsForAcademicModel
    {
        public long? CandidateId { get; set; }
        public bool IsBasicComputerLiteracy { get; set; }
        public string Other { get; set; }
    }
}
