using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CareerMasterBO
    {
        public int? Id { get; set; }
        public string JobTitle { get; set; }
        public int? LoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? IsExist { get; set; }

        // Ragsrding For tbl_CandidateRegistration
        public int? JobId { get; set; }
        public string RegistarionId { get; set; }
        public string OTPNo { get; set; }
        public string EmailId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MotherName { get; set; }
        public char Gender { get; set; }
        public DateTime? DateOFBirth { get; set; }
        public string Nationality { get; set; }
        public string MaritalStaus { get; set; }
        public string CasteCategory { get; set; }
        public string PhysicallyChallenged { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string ContactNo { get; set; }
        public string AlternateContactNo { get; set; }
        public string PhotographName { get; set; }
        public string PhotographPath { get; set; }
        public string SignatureName { get; set; }
        public string SignaturePath { get; set; }
        public string DateofBirthProofName { get; set; }
        public string DateofBirthProofPath { get; set; }
        public int Flag { get; set; }
    }

    public class CareerEducationMasterBO
    {
        // Ragsrding For tbl_CandidateEducationDetails
        public int? CandidateId { get; set; }
        public string HighestQualification { get; set; }
        public string OtherEducationorQualification { get; set; }
        public string TotalYearorExperience { get; set; }
        public string CurrentJobDesignation { get; set; }
        public string JobResponsibilities { get; set; }
        public string WorkSpecialization { get; set; }
        public string CurrentSalary { get; set; }
        public string ExpectedSalary { get; set; }
        public string UploadResumeName { get; set; }
        public string UplodResumePath { get; set; }
        public int Flag { get; set; }
    }

    public class CareerGraduationEducationMasterBO
    {
        // Ragsrding For tbl_CandidateEducationDetails
        public long? Id { get; set; }
        public long? EducationId { get; set; }
        public string DegreeName { get; set; }
        public decimal PercentageOrPercentile { get; set; }
        public long? EducationClassId { get; set; }
        public string ExamBody { get; set; }
        public int? NoOfTrials { get; set; }
        public string PassingMonth { get; set; }
        public string PassingYear { get; set; }
        public bool IsPostGraduate { get; set; }
        public string RegistarionId { get; set; }
        public int JobId { get; set; }
        public string CertificateFileName { get; set; }
        public string CertificateFilePath { get; set; }
    }

    public class CareerLanguageMasterBo
    {
        public long? Id { get; set; }
        public bool Isengread { get; set; }
        public bool IsengWrite { get; set; }
        public bool IsengSpeak { get; set; }
        public bool Isgujread { get; set; }
        public bool IsgujWrite { get; set; }
        public bool IsgujSpeak { get; set; }
        public bool Ishinread { get; set; }
        public bool IshinWrite { get; set; }
        public bool IshinSpeak { get; set; }
    }

    public class CareerProfessionalExperienceMasterBO
    {
        // Ragsrding For tbl_CandidateEducationDetails
        public long? Id { get; set; }
        public long? priviousId { get; set; }
        public int? CandidateId { get; set; }
        public int JobId { get; set; }
        public int JobType { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string CurrentJobPost { get; set; }
        public string EmployerName { get; set; }
        public string EmployerAddress { get; set; }
        public string EmployerContact { get; set; }
        public string EmployerPINNo { get; set; }
        public string JobDescription { get; set; }
        public string MonthlySalary { get; set; }
        public string UplodCertificateName { get; set; }
        public string UplodCertificatePath { get; set; }
    }

    public class CareerCertificateCourseMasterBO
    {
        // Ragsrding For tbl_CandidateEducationDetails
        public long? Id { get; set; }
        public int? CandidateId { get; set; }
        public int JobId { get; set; }
        public string CertificateCourseName { get; set; }
        public string CertificateNo { get; set; }
        public string UplodCertificateCourseName { get; set; }
        public string UplodCertificateCoursePath { get; set; }
    }

    public class SessionFileUploadModel
    {
        public string photoUploadName { get; set; }
        public string photoUploadpath { get; set; }
        public string signatureUploadName { get; set; }
        public string signatureUploadPath { get; set; }
        public string dobUploadName { get; set; }
        public string dobUploadPath { get; set; }
        public string graduationUploadFileName { get; set; }
        public string graduationUploadPath { get; set; }
        public string postgraduationUploadFileName { get; set; }
        public string postgraduationUploadPath { get; set; }
    }

    public class EmailOTPBO
    {
        public string EmailId { get; set; }
        public string otp { get; set; }
    }

    public class ScrutinyMasterBO
    {
        public int? CandidateId { get; set; }
        public string RegistrationId { get; set; }
        public string EmailId { get; set; }
        public string AdvertiseNo { get; set; }
        public int? JobId { get; set; }
        public int? ActionType { get; set; }
        public DateTime InterviewDate { get; set; }
        public string InterviewFromTime { get; set; }
        public string ReportingTime { get; set; }
        public string InterviewToTime { get; set; }
        public string InterviewAddress { get; set; }
        public int? flag { get; set; }
        public string UserName { get; set; }
    }
    public class UploadCVCareer
    {
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Location { get; set; }
        public string FilePath { get; set; }
        public string Designation { get; set; }
    }
}
