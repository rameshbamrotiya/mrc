using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Repository.Interface.Candidate;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Candidate
{
    public class CandidateDetailsRepository : ICandidateDetailsRepository
    {
        private string SqlConnectionSTring;
        public CandidateDetailsRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        #region Methods For GetAllData
        public List<CandidateDetailsModel> GetAllTblCandidateDetails()
        {
            using (CandidateBasicDetailsDataContext db = new CandidateBasicDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllCandidateDetails().Select(x => new CandidateDetailsModel
                {
                    Id = x.Id,
                    Advertisementid = x.Advertisementid,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    MotherName = x.MotherName,
                    AadharCard = x.AadharCard,
                    RegistrationId = x.RegisrationId,
                    DateOfBirth = Convert.ToDateTime(x.DateOfBirth),
                    Age = x.Age,
                    Mobile = x.Mobile,
                    EmailId = x.EmailId,
                    PermenentAddress = x.ParmenentAddress,
                    PermenentPincode = x.ParmenentPincode,
                    PermenentCountry = x.ParmenentCountry,
                    PermenentState = x.ParmenentState,
                    PermenentCity = x.ParmenentCity,
                    PermenentPhoneR = x.ParmenentPhoneR,
                    PermenentPhoneM = x.ParmenentPhoneM,
                    CountryCode=x.CountryCode,
                    PresentAddress = x.PresentAddress,
                    PresentCountry = x.PresentCountry,
                    PresentState = x.PresentState,
                    PresentCity = x.PresentCity,
                    PresentPincode = x.PresentPincode,
                    PresentPhoneR = x.PresentPhoneR,
                    PresentPhoneM = x.PresentPhoneM,
                    CountryCode1=x.CountryCode1,
                    CasteId = x.CasteId,
                    Gender = x.Gender,
                    Religion = x.Religion,
                    MaritalStatus = x.MaritalStatus,
                    SpouseFirstName = x.SpouseFirstName,
                    SpouseMiddleName = x.SpouseMiddleName,
                    SpouseSurname = x.SpouseSurname,
                    SpouseDOB = Convert.ToDateTime(x.SpouseDOB),
                    SpouseContact = x.SpouseContact,
                    EmergencyContactPersonName = x.EmergencyContactPersonName,
                    EmergencyContactNo = x.EmergencyContactNo,
                    Password = x.Password,
                    PhotographName = x.PhotographName,
                    PhotographPath = x.PhotographPath,
                    SignatureName = x.SignatureName,
                    SignaturePath = x.SignaturePath,
                    RoleInLastEmployment = x.RoleInLastEmployment,
                    CurrentSalaryPerMonth = Convert.ToDecimal(x.CurrentSalaryPerMonth),
                    ExpectedSalary = Convert.ToDecimal(x.ExpectedSalary),
                    ExtraActivities = x.ExtraActivities,
                    MemberShipOfAnySpecialBodies = x.MemberShipOfAnySpecialBodies,
                    HaveChronicIllness = x.HaveChronicIllness,
                    ChronicIllnessDetails = x.ChronicIllnessDetails,
                    IsOffenceRegistered = x.IsOffenceRegistered,
                    OffneceDetails = x.OffneceDetails,
                    DescribeYourself = x.DescribeYourself,
                    BiggestAchivement = x.BiggestAchivement,
                    YourBiggestFailure = x.YourBiggestFailure,
                    VisionInNextYears = x.VisionInNextYears,
                    Remarks = x.Remarks,
                    AdvertisementAwarenessSource = x.AdvertisementAwarenessSource
                }).ToList();
            }
        }

        public List<GetAllCandidateRecruitmentMasterResult> GetAllCandidateRecruitmentMaster()
        {
            using (CandidateBasicDetailsDataContext db = new CandidateBasicDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllCandidateRecruitmentMaster().ToList();
            }
        }

        public List<CandidateEducationDetailsModel> GetAllTblCandidateEducationDetails()
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllCandidateEducationDetails().Select(x => new CandidateEducationDetailsModel
                {
                    Id = x.Id,
                    CandidateId = x.CandidateId,
                    EducationId = x.EducationId,
                    NameOfSchoolCollege = x.NameOfSchoolCollege,
                    BoardUniversity = x.BoardUniversity,
                    PassingYear = x.PassingYear,
                    MajorSubjects = x.MajorSubjects,
                    PercentageOrPercentile = Convert.ToDecimal(x.PercentageOrPercentile),
                    Division = x.Division,
                    IsVisible = x.IsVisible,
                    CertificateFileName = x.CertificateFileName,
                    CertificateFilePath = x.CertificateFilePath
                }).ToList();
            }
        }

        public List<CandidateExperienceDetailsModel> GetAllTblCandidateExperienceDetails()
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllCandidateExperienceDetails().Select(x => new CandidateExperienceDetailsModel
                {
                    Id = x.Id,
                    CandidateId = x.CandidateId,
                    JobType = x.JobType,
                    FromDate = Convert.ToDateTime(x.FromDate),
                    ToDate = Convert.ToDateTime(x.ToDate),
                    Experience = x.Experience,
                    Designation = x.Designation,
                    OrganizationName = x.OrganizationName,
                    OrganizationAddress = x.OrganizationAddress,
                    ReportingAuthority = x.ReportingAuthority,
                    SalaryPerMonth = Convert.ToDecimal(x.SalaryPerMonth),
                    ReasonForChange = x.ReasonForChange,
                    PostTypeId = x.PostTypeId,
                    PostName = x.PostName,
                    DepartmentName = x.DepartmentName,
                    ExperienceCertificateFileName = x.ExperienceCertificateFileName,
                    ExperienceCertificateFilePath = x.ExperienceCertificateFilePath
                }).ToList();
            }
        }

        public List<CandidateCourseDetailsModel> GetAllTblCandidateCourseDetails()
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllCandidateCourseDetails().Select(x => new CandidateCourseDetailsModel
                {
                    Id = x.Id,
                    CandidateId = x.CandidateId,
                    CourseTitle = x.CourseTitle,
                    Duration = x.Duration,
                    InstituteName = x.InstituteName,
                    City = x.City,
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }

        public List<CandidateFamilyDetailsModel> GetAllTblCandidateFamilyDetails()
        {
            using (CandidateBasicDetailsDataContext db = new CandidateBasicDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllCandidateFamilyDetails().Select(x => new CandidateFamilyDetailsModel
                {
                    Id = x.Id,
                    RegistrationId = x.RegisrationId,
                    CandidateId = x.CandidateId,
                    MemberName = x.MemberName,
                    Age = x.Age,
                    RelationId = x.RelationId,
                    Occupation = x.Occupation,
                    MonthlyIncome = Convert.ToDecimal(x.MonthlyIncome),
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }

        public List<CandidateProfessionalReferralDetailsModel> GetAllTblCandidateProfessionalReferralDetails()
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllCandidateProfessionalReferralDetails().Select(x => new CandidateProfessionalReferralDetailsModel
                {
                    Id = x.Id,
                    CandidateId = x.Candidateid,
                    Name = x.Name,
                    Position = x.Position,
                    MobileNo = x.MobileNo,
                    RelationShip = x.RelationShip,
                    YearsKnown = Convert.ToDouble(x.YearsKnown),
                    Address = x.Address
                }).ToList();
            }
        }

        public List<CandidateIndexedDetailsModels> GetAllCandidateIndexedJournalDetails()
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllCandidateIndexedJournal().Select(x => new CandidateIndexedDetailsModels
                {
                    Id = x.Id,
                    CandidateId = x.CandidateId,
                    NameOfTheJournal = x.NameOfTheJournal,
                    Topic = x.Topic,
                    Month = x.Month,
                    MonthName = x.MonthName,
                    Year = x.Year,
                    NationalInternational = x.NationalInternational,
                    PublicationAcceptance = x.PublicationAcceptance
                }).ToList();
            }
        }

        public List<CandidateNonIndexedDetailsModels> GetAllCandidateNonIndexedJournalDetails()
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetAllCandidateNonIndexedJournal().Select(x => new CandidateNonIndexedDetailsModels
                {
                    Id = x.Id,
                    CandidateId = x.CandidateId,
                    NameOfTheJournal = x.NameOfTheJournal,
                    Topic = x.Topic,
                    Month = x.Month,
                    MonthName = x.MonthName,
                    Year = x.Year,
                    NationalInternational = x.NationalInternational,
                    PublicationAcceptance = x.PublicationAcceptance
                }).ToList();
            }
        }

        #endregion

        #region Methods For GetData ById
        public CandidateDetailsModel GetTblCandidateDetailsById(long lgId)
        {
            using (CandidateBasicDetailsDataContext db = new CandidateBasicDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetCandidateDetailsById(lgId).Select(x => new CandidateDetailsModel
                {
                    Id = x.Id,
                    Advertisementid = x.Advertisementid,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    MotherName = x.MotherName,
                    AadharCard = x.AadharCard,
                    RegistrationId = x.RegisrationId,
                    DateOfBirth = Convert.ToDateTime(x.DateOfBirth),
                    Age = x.Age,
                    Mobile = x.Mobile,
                    EmailId = x.EmailId,
                    PermenentAddress = x.ParmenentAddress,
                    PermenentPincode = x.ParmenentPincode,
                    PermenentCountry = x.ParmenentCountry,
                    PermenentState = x.ParmenentState,
                    PermenentCity = x.ParmenentCity,
                    PermenentPhoneR = x.ParmenentPhoneR,
                    PermenentPhoneM = x.ParmenentPhoneM,
                    PresentAddress = x.PresentAddress,
                    PresentCountry = x.PresentCountry,
                    PresentState = x.PresentState,
                    PresentCity = x.PresentCity,
                    PresentPincode = x.PresentPincode,
                    PresentPhoneR = x.PresentPhoneR,
                    PresentPhoneM = x.PresentPhoneM,
                    CasteId = x.CasteId,
                    Gender = x.Gender,
                    Religion = x.Religion,
                    MaritalStatus = x.MaritalStatus,
                    SpouseFirstName = x.SpouseFirstName,
                    SpouseMiddleName = x.SpouseMiddleName,
                    SpouseSurname = x.SpouseSurname,
                    SpouseDOB = Convert.ToDateTime(x.SpouseDOB),
                    SpouseContact = x.SpouseContact,
                    EmergencyContactPersonName = x.EmergencyContactPersonName,
                    EmergencyContactNo = x.EmergencyContactNo,
                    Password = x.UserPassword,
                    PhotographName = x.PhotographName,
                    PhotographPath = x.PhotographPath,
                    SignatureName = x.SignatureName,
                    SignaturePath = x.SignaturePath,
                    RoleInLastEmployment = x.RoleInLastEmployment,
                    CurrentSalaryPerMonth = Convert.ToDecimal(x.CurrentSalaryPerMonth),
                    ExpectedSalary = Convert.ToDecimal(x.ExpectedSalary),
                    ExtraActivities = x.ExtraActivities,
                    MemberShipOfAnySpecialBodies = x.MemberShipOfAnySpecialBodies,
                    HaveChronicIllness = x.HaveChronicIllness,
                    ChronicIllnessDetails = x.ChronicIllnessDetails,
                    IsOffenceRegistered = x.IsOffenceRegistered,
                    OffneceDetails = x.OffneceDetails,
                    DescribeYourself = x.DescribeYourself,
                    BiggestAchivement = x.BiggestAchivement,
                    YourBiggestFailure = x.YourBiggestFailure,
                    VisionInNextYears = x.VisionInNextYears,
                    Remarks = x.Remarks,
                    AdvertisementAwarenessSource = x.AdvertisementAwarenessSource
                }).FirstOrDefault();
            }
        }

        public CandidateEducationDetailsModel GetTblCandidateEducationDetailsById(long lgId)
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetCandidateEducationDetailsById(lgId).Select(x => new CandidateEducationDetailsModel
                {
                    Id = x.Id,
                    CandidateId = x.CandidateId,
                    EducationId = x.EducationId,
                    NameOfSchoolCollege = x.NameOfSchoolCollege,
                    BoardUniversity = x.BoardUniversity,
                    PassingYear = x.PassingYear,
                    MajorSubjects = x.MajorSubjects,
                    PercentageOrPercentile = Convert.ToDecimal(x.PercentageOrPercentile),
                    Division = x.Division,
                    IsVisible = x.IsVisible,
                    CertificateFileName = x.CertificateFileName,
                    CertificateFilePath = x.CertificateFilePath
                }).FirstOrDefault();
            }
        }

        public CandidateExperienceDetailsModel GetTblCandidateExperienceDetailsById(long lgId)
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetCandidateExperienceDetailsById(lgId).Select(x => new CandidateExperienceDetailsModel
                {
                    Id = x.Id,
                    CandidateId = x.CandidateId,
                    JobType = x.JobType,
                    FromDate = Convert.ToDateTime(x.FromDate),
                    ToDate = Convert.ToDateTime(x.ToDate),
                    Designation = x.Designation,
                    OrganizationName = x.OrganizationName,
                    OrganizationAddress = x.OrganizationAddress,
                    ReportingAuthority = x.ReportingAuthority,
                    SalaryPerMonth = Convert.ToDecimal(x.SalaryPerMonth),
                    ReasonForChange = x.ReasonForChange
                }).FirstOrDefault();
            }
        }

        public CandidateCourseDetailsModel GetTblCandidateCourseDetailsById(long lgId)
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetCandidateCourseDetailsById(lgId).Select(x => new CandidateCourseDetailsModel
                {
                    Id = x.Id,
                    CandidateId = x.CandidateId,
                    CourseTitle = x.CourseTitle,
                    Duration = x.Duration,
                    InstituteName = x.InstituteName,
                    City = x.City,
                    IsVisible = x.IsVisible
                }).FirstOrDefault();
            }
        }

        public CandidateFamilyDetailsModel GetTblCandidateFamilyDetailsById(long lgId)
        {
            using (CandidateBasicDetailsDataContext db = new CandidateBasicDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetCandidateFamilyDetailsById(lgId).Select(x => new CandidateFamilyDetailsModel
                {
                    Id = x.Id,
                    RegistrationId = x.RegisrationId,
                    CandidateId = x.CandidateId,
                    MemberName = x.MemberName,
                    Age = x.Age,
                    RelationId = x.RelationId,
                    Occupation = x.Occupation,
                    MonthlyIncome = Convert.ToDecimal(x.MonthlyIncome),
                    IsVisible = x.IsVisible
                }).FirstOrDefault();
            }
        }

        public CandidateProfessionalReferralDetailsModel GetTblCandidateProfessionalReferralDetailsById(long lgId)
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetCandidateProfessionalReferralDetailsById(lgId).Select(x => new CandidateProfessionalReferralDetailsModel
                {
                    Id = x.Id,
                    CandidateId = x.Candidateid,
                    Name = x.Name,
                    Position = x.Position,
                    MobileNo = x.MobileNo,
                    RelationShip = x.RelationShip,
                    YearsKnown = Convert.ToDouble(x.YearsKnown)
                }).FirstOrDefault();
            }
        }

        public CandidateIndexedDetailsModels GetCandidateIndexedJournalDetailsById(long lgId)
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetCandidateIndexedJournalDetailsById(lgId).Select(x => new CandidateIndexedDetailsModels
                {
                    Id = x.Id,
                    CandidateId = x.CandidateId,
                    NameOfTheJournal = x.NameOfTheJournal,
                    Topic = x.Topic,
                    Month = x.Month,
                    Year = x.Year,
                    NationalInternational = x.NationalInternational,
                    PublicationAcceptance = x.PublicationAcceptance
                }).FirstOrDefault();
            }
        }

        public CandidateNonIndexedDetailsModels GetCandidateNonIndexedJournalDetailsById(long lgId)
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetCandidateNonIndexedJournalDetailsById(lgId).Select(x => new CandidateNonIndexedDetailsModels
                {
                    Id = x.Id,
                    CandidateId = x.CandidateId,
                    NameOfTheJournal = x.NameOfTheJournal,
                    Topic = x.Topic,
                    Month = x.Month,
                    Year = x.Year,
                    NationalInternational = x.NationalInternational,
                    PublicationAcceptance = x.PublicationAcceptance
                }).FirstOrDefault();
            }
        }

        public GetCandidateExperienceTotalModel GetTblCandidateExperienceTotalDetailsByCandId(long lgId)
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetCandidateExperienceTotalByCandidateId(lgId).Select(x => new GetCandidateExperienceTotalModel
                {
                    Years = x.Years,
                    Months = x.Months,
                    Days = x.Days
                }).FirstOrDefault();
            }
        }

        public CandidateDetailsForExperienceModel GetCandidateDetailsForExtraExperienceByCanId(long lgId)
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetCandidateDetailsForExtraExperienceByCanId(lgId).Select(x => new CandidateDetailsForExperienceModel
                {
                    RoleInLastEmployment = x.RoleInLastEmployment,
                    CurrentSalaryPerMonth = Convert.ToDecimal(x.CurrentSalaryPerMonth),
                    ExpectedSalary = Convert.ToDecimal(x.ExpectedSalary)
                }).FirstOrDefault();
            }
        }

        public CandidateDetailsForTeachingPostsModel GetCandidateDetailsForExtraTeachingPostsByCanId(long lgId)
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetCandidateDetailsForExtraTeachingPostsByCanId(lgId).Select(x => new CandidateDetailsForTeachingPostsModel
                {
                    IndependentWorkWithResult = x.IndependentWorkWithResult,
                    WorkUnderSupervision = x.WorkUnderSupervision,
                    ConferenceAttendanceAndPaper = x.ConferenceAttendanceAndPaper
                }).FirstOrDefault();
            }
        }

        public CandidateDetailsForAcademicModel GetCandidateDetailsForAcademicByCanId(long lgId)
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return db.GetCandidateDetailsForAcademicByCanId(lgId).Select(x => new CandidateDetailsForAcademicModel
                {
                    IsBasicComputerLiteracy = Convert.ToBoolean(x.IsBasicComputerLiteracy),
                    Other = x.Other
                }).FirstOrDefault();
            }
        }
        #endregion

        #region Methods For GetDataBy CandId
        public List<CandidateEducationDetailsModel> GetAllCandidateEducationDetailsByCandId(long lgId)
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return GetAllTblCandidateEducationDetails().Where(x => x.CandidateId == lgId).ToList();
            }
        }

        public List<CandidateExperienceDetailsModel> GetAllCandidateExperienceDetailsByCandId(long lgId)
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return GetAllTblCandidateExperienceDetails().Where(x => x.CandidateId == lgId).ToList();
            }
        }

        public List<CandidateCourseDetailsModel> GetAllCandidateCourseDetailsByCandId(long lgId)
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return GetAllTblCandidateCourseDetails().Where(x => x.CandidateId == lgId).ToList();
            }
        }

        public List<CandidateFamilyDetailsModel> GetAllCandidateFamilyDetailsByCandId(long lgId)
        {
            using (CandidateBasicDetailsDataContext db = new CandidateBasicDetailsDataContext(SqlConnectionSTring))
            {
                return GetAllTblCandidateFamilyDetails().Where(x => x.CandidateId == lgId).ToList();
            }
        }

        public List<CandidateProfessionalReferralDetailsModel> GetAllCandidateProfessionalReferralDetailsByCandId(long lgId)
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return GetAllTblCandidateProfessionalReferralDetails().Where(x => x.CandidateId == lgId).ToList();
            }
        }

        public List<CandidateIndexedDetailsModels> GetAllCandidateIndexedJournalDetailsByCandId(long lgId)
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return GetAllCandidateIndexedJournalDetails().Where(x => x.CandidateId == lgId).ToList();
            }
        }

        public List<CandidateNonIndexedDetailsModels> GetAllCandidateNonIndexedJournalDetailsByCandId(long lgId)
        {
            using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
            {
                return GetAllCandidateNonIndexedJournalDetails().Where(x => x.CandidateId == lgId).ToList();
            }
        }
        #endregion

        #region Methods For InsertOrUpdate
        public bool InsertOrUpdateTblCandidateDetails(CandidateDetailsModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateBasicDetailsDataContext db = new CandidateBasicDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateCandidateDetails(objData.Id, objData.Advertisementid, objData.RegistrationId, objData.FirstName, objData.MiddleName, objData.LastName, objData.AadharCard, objData.MotherName, objData.DateOfBirth, objData.Age, objData.Mobile, objData.EmailId, objData.PermenentAddress, objData.PermenentPincode, objData.PermenentPhoneR, objData.PermenentPhoneM, objData.CountryCode ,objData.PermenentCountry, objData.PermenentState, objData.PermenentCity, objData.PresentAddress, objData.PresentPincode, objData.PresentPhoneR, objData.PresentPhoneM, objData.CountryCode1, objData.PresentCountry, objData.PresentState, objData.PresentCity, objData.CasteId, objData.Gender, objData.Religion, objData.MaritalStatus, objData.SpouseFirstName, objData.SpouseMiddleName, objData.SpouseSurname, objData.SpouseDOB, objData.SpouseContact, objData.EmergencyContactPersonName, objData.EmergencyContactNo, objData.Password, objData.PhotographName, objData.PhotographPath, objData.SignatureName, objData.SignaturePath, SessionWrapper.UserDetails.UserName);
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            strError = "Record Updated Successfully";
                            isError = false;
                        }
                        objData.Id = dataIsDone.FirstOrDefault().RecId;
                    }
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
        public bool InsertOrUpdateTblCandidateEducationDetails(CandidateEducationDetailsModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateCandidateEducationDetails(objData.Id, objData.CandidateId, objData.EducationId, objData.EducationTypeId, objData.RegisrationId, objData.NameOfSchoolCollege, objData.BoardUniversity, objData.PassingYear, objData.MajorSubjects, objData.PercentageOrPercentile, objData.Division, objData.IsVisible, objData.CertificateFileName, objData.CertificateFilePath, SessionWrapper.UserDetails.UserName);
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            strError = "Record Updated Successfully";
                            isError = false;
                        }
                    }
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
        public bool InsertOrUpdateTblCandidateExperienceDetails(CandidateExperienceDetailsModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateCandidateExperienceDetails(objData.Id, objData.CandidateId, objData.JobType, objData.FromDate, objData.ToDate, objData.Designation, objData.OrganizationName, objData.OrganizationAddress, objData.ReportingAuthority, objData.SalaryPerMonth, objData.ReasonForChange, objData.PostTypeId, objData.DepartmentName, objData.ExperienceCertificateFileName,objData.ExperienceCertificateFilePath, SessionWrapper.UserDetails.UserName);
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            strError = "Record Updated Successfully";
                            isError = false;
                        }
                    }
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
        public bool InsertOrUpdateTblCandidateCourseDetails(CandidateCourseDetailsModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateCandidateCourseDetails(objData.Id, objData.CandidateId, objData.CourseTitle, objData.Duration, objData.InstituteName, objData.City, objData.IsVisible, SessionWrapper.UserDetails.UserName);
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            strError = "Record Updated Successfully";
                            isError = false;
                        }
                    }
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
        public bool InsertOrUpdateTblCandidateFamilyDetails(CandidateFamilyDetailsModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateBasicDetailsDataContext db = new CandidateBasicDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateCandidateFamilyDetails(objData.Id, objData.RegistrationId, objData.CandidateId, objData.MemberName, objData.Age, objData.RelationId, objData.Occupation, objData.MonthlyIncome, objData.IsVisible, SessionWrapper.UserDetails.UserName);
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            strError = "Record Updated Successfully";
                            isError = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
        public bool InsertOrUpdateTblCandidateProfessionalReferralDetails(CandidateProfessionalReferralDetailsModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateCandidateProfessionalReferralDetails(objData.Id, objData.CandidateId, objData.Name, objData.Position, objData.MobileNo, objData.RelationShip, objData.YearsKnown, SessionWrapper.UserDetails.UserName, objData.Address, objData.Regid);
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            strError = "Record Updated Successfully";
                            isError = false;
                        }
                    }
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
        public bool InsertOrUpdateCandidateIndexedJournal(CandidateIndexedDetailsModels objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateCandidateIndexedJournal(objData.Id, objData.CandidateId, objData.NameOfTheJournal, objData.Topic, objData.Month, objData.Year, objData.NationalInternational, objData.PublicationAcceptance, SessionWrapper.UserDetails.UserName);
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            strError = "Record Updated Successfully";
                            isError = false;
                        }
                    }
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
        public bool InsertOrUpdateCandidateNonIndexedJournal(CandidateNonIndexedDetailsModels objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateCandidateNonIndexedJournal(objData.Id, objData.CandidateId, objData.NameOfTheJournal, objData.Topic, objData.Month, objData.Year, objData.NationalInternational, objData.PublicationAcceptance, SessionWrapper.UserDetails.UserName);
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            strError = "Record Updated Successfully";
                            isError = false;
                        }
                    }
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool UpdateCandidateDetailsForExperience(CandidateDetailsForExperienceModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.UpdateCandidateDetailsForExperience(objData.CandidateId, objData.RoleInLastEmployment, objData.CurrentSalaryPerMonth, objData.ExpectedSalary);
                    if (dataIsDone != null)
                    {
                        strError = "Record Inserted Successfully";
                        isError = false;
                    }
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
        public bool UpdateCandidateDetailsForTeachingPosts(CandidateDetailsForTeachingPostsModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.UpdateCandidateDetailsForTeachingPosts(objData.CandidateId, objData.IndependentWorkWithResult, objData.WorkUnderSupervision, objData.ConferenceAttendanceAndPaper);
                    if (dataIsDone != null)
                    {
                        strError = "Record Inserted Successfully";
                        isError = false;
                    }
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
        public bool UpdateCandidateDetailsForAcademic(CandidateDetailsForAcademicModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.UpdateCandidateDetailsForAcademic(objData.CandidateId, objData.IsBasicComputerLiteracy, objData.Other);
                    if (dataIsDone != null)
                    {
                        strError = "Record Inserted Successfully";
                        isError = false;
                    }
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
        public bool UpdateCandidateDetailsExtraInfo(CandidateDetailsModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.UpdateCandidateDetailsExtraInformation(objData.Id, objData.RoleInLastEmployment, objData.CurrentSalaryPerMonth, objData.ExpectedSalary, objData.ExtraActivities, objData.MemberShipOfAnySpecialBodies, objData.HaveChronicIllness, objData.ChronicIllnessDetails, objData.IsOffenceRegistered, objData.OffneceDetails, objData.DescribeYourself, objData.BiggestAchivement, objData.YourBiggestFailure, objData.VisionInNextYears, objData.Remarks, objData.AdvertisementAwarenessSource, objData.EmergencyContactPersonName, objData.EmergencyContactNo, objData.UNMICRCContact);
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            strError = "Record Updated Successfully";
                            isError = false;
                        }
                    }
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
        public bool UnlockProfileForEdit(long candId, long addId, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateBasicDetailsDataContext db = new CandidateBasicDetailsDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.UpdateLock(candId, addId, SessionWrapper.UserDetails.UserName);
                    strError = dataIsDone.FirstOrDefault().Msg;
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
        #endregion

        #region Methods For RemoveRecord
        public bool RemoveTblCandidateDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveCandidateDetails(lgId, SessionWrapper.UserDetails.UserName);
                    db.SubmitChanges();
                    strError = "Record Removed Successfully";
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveTblCandidateEducationDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveCandidateEducationDetails(lgId, SessionWrapper.UserDetails.UserName);
                    db.SubmitChanges();
                    strError = "Record Removed Successfully";
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveTblCandidateExperienceDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveCandidateExperienceDetails(lgId, SessionWrapper.UserDetails.UserName);
                    db.SubmitChanges();
                    strError = "Record Removed Successfully";
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveTblCandidateCourseDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveCandidateCourseDetails(lgId, SessionWrapper.UserDetails.UserName);
                    db.SubmitChanges();
                    strError = "Record Removed Successfully";
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveTblCandidateFamilyDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveCandidateFamilyDetails(lgId, SessionWrapper.UserDetails.UserName);
                    db.SubmitChanges();
                    strError = "Record Removed Successfully";
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveTblCandidateProfessionalReferralDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveCandidateProfessionalReferralDetails(lgId, SessionWrapper.UserDetails.UserName);
                    db.SubmitChanges();
                    strError = "Record Removed Successfully";
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveTblCandidateIndexedJournalDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveCandidateIndexedJournalDetails(lgId, SessionWrapper.UserDetails.UserName);
                    db.SubmitChanges();
                    strError = "Record Removed Successfully";
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveTblCandidateNonIndexedJournalDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (CandidateDetailsDataContext db = new CandidateDetailsDataContext(SqlConnectionSTring))
                {
                    db.RemoveCandidateNonIndexedJournalDetails(lgId, SessionWrapper.UserDetails.UserName);
                    db.SubmitChanges();
                    strError = "Record Removed Successfully";
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
        #endregion

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
