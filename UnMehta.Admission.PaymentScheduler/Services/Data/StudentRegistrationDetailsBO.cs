using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StudentRegistrationDetailsBO
    {
        public long? Id { get; set; }
        public long? StudentId { get; set; }
        public long? CourseId { get; set; }
        public string RegistrationId { get; set; }
        public string PresentAddress { get; set; }
        public string PresentPincode { get; set; }
        public string PresentCountry { get; set; }
        public string PresentState { get; set; }
        public string PresentCity { get; set; }
        public string PresentTaluka { get; set; }
        public string PresentPhoneR { get; set; }
        public string PresentPhoneM { get; set; }
        public string ParmenentAddress { get; set; }
        public string ParmenentPincode { get; set; }
        public string ParmenentCountry { get; set; }
        public string ParmenentState { get; set; }
        public string ParmenentCity { get; set; }
        public string ParmenentTaluka { get; set; }
        public string ParmenentPhoneR { get; set; }
        public string ParmenentPhoneM { get; set; }
        public long? Age { get; set; }
        public string PlaceOfBirth { get; set; }
        public long? CasteId { get; set; }
        public string Religion { get; set; }
        public string PhotographName { get; set; }
        public string PhotographPath { get; set; }
        public string SignatureName { get; set; }
        public string SignaturePath { get; set; }
        public string DOBFileName { get; set; }
        public string DOBFilePath { get; set; }
        public string UserName { get; set; }

        public string PersonalInformationId { get; set; }
        public string PersonalInformationRemarks { get; set; }

        public string AddressId { get; set; }
        public string AddressRemarks { get; set; }

        public string DocumentId { get; set; }
        public string DocumentRemarks { get; set; }

        public string FamilyMemberId { get; set; }
        public string FamilyMemberRemarks { get; set; }

        public long? hfStudentWorkflowId { get; set; }

        public string ApplicationStatus { get; set; }

        public string AcademicId { get; set; }
        public string AcademicRemarks { get; set; }
        public string ComputerLiteracyId { get; set; }
        public string ComputerLiteracyRemarks { get; set; }
        public string CoursesId { get; set; }
        public string CoursesRemarks { get; set; }
        public string LanguageId { get; set; }
        public string LanguageRemarks { get; set; }
        public string OtherInfoId { get; set; }
        public string OtherInfoRemarks { get; set; }
        public string lawId { get; set; }
        public string lawReamrks { get; set; }
        public string EmergencyId { get; set; }
        public string EmergencyRemarks { get; set; }
        public string ReferencesId { get; set; }
        public string ReferencesRemarks { get; set; }

        public string EducationDocId { get; set; }
        public string EducationDocRemarks { get; set; }

        public string Title { get; set; }
    }

    public class StudentAllRegistratedBO
    {
        public long? Id { get; set; }
        public string RegistrationId { get; set; }
        public string ApplicationStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string TxnId { get; set; }
        public decimal amount { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public long? StudentId { get; set; }
        public long? CourseId { get; set; }
        public string CourseName { get; set; }
        public string PresentAddress { get; set; }
        public string PresentPincode { get; set; }
        public string PresentCountry { get; set; }
        public string PresentState { get; set; }
        public string PresentCity { get; set; }
        public string PresentPhoneR { get; set; }
        public string PresentPhoneM { get; set; }
        public string ParmenentAddress { get; set; }
        public string ParmenentPincode { get; set; }
        public string ParmenentCountry { get; set; }
        public string ParmenentState { get; set; }
        public string ParmenentCity { get; set; }
        public string ParmenentPhoneR { get; set; }
        public string ParmenentPhoneM { get; set; }
        public long? Age { get; set; }
        public string PlaceOfBirth { get; set; }
        public long? CasteId { get; set; }
        public string CastName { get; set; }
        public string Religion { get; set; }
        public string ReligionName { get; set; }
        public string PhotographName { get; set; }
        public string PhotographPath { get; set; }
        public string SignatureName { get; set; }
        public string SignaturePath { get; set; }
        public string DOBFileName { get; set; }
        public string DOBFilePath { get; set; }
    }

}
