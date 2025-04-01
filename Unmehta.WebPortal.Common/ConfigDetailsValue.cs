using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface;
using Unmehta.WebPortal.Repository.Repository;

namespace Unmehta.WebPortal.Common
{

    public class ConfigDetailsValue
    {
        public static string strSqlConnectionString = ConfigurationManager.ConnectionStrings["UNMehtaConnectionString"].ToString();
        private static T GetFromTable<T>(string key)
        {
            using (IConfigDetailsRepository configDetailsRepository = new ConfigDetailsRepository(strSqlConnectionString))
            {
                string strError = "";

                ConfigDetailsModel objConfigDetailsModel = new ConfigDetailsModel();
                if (!configDetailsRepository.GetConfigDetails(key, out objConfigDetailsModel, out strError))
                {
                    if (objConfigDetailsModel != null)
                    {
                        object obj = (objConfigDetailsModel.ParameterValue == null ? "" : objConfigDetailsModel.ParameterValue).ToString();
                        return (T)obj;
                    }
                    else
                    {
                        object obj = ("DataNotFound" + "|" + false).ToString();
                        return (T)obj;
                    }
                }
                else
                {
                    object obj = (strError + "|" + true).ToString();
                    return (T)obj;
                }
            }
        }

        #region SMS
        public static string SMSAPI
        {
            get
            {
                return GetFromTable<string>("SMSAPI");
            }
        }

        public static string SMSTemplateid
        {
            get
            {
                return GetFromTable<string>("SMSTemplateid");
            }
        }

        public static string SMSSecureKey
        {
            get
            {
                return GetFromTable<string>("SMSSecureKey");
            }
        }

        public static string SMSSenderId
        {
            get
            {
                return GetFromTable<string>("SMSSenderId");
            }
        }

        public static string SMSPassword
        {
            get
            {
                return GetFromTable<string>("SMSPassword");
            }
        }

        public static string SMSUsername
        {
            get
            {
                return GetFromTable<string>("SMSUsername");
            }
        }
        #endregion

        public static string AddRecrutmentFileUploadPath
        {
            get
            {
                return GetFromTable<string>("AddRecrutmentFileUploadPath");
            }
        }
        public static string ZIPTempPath
        {
            get
            {
                return GetFromTable<string>("ZIPTempPath");
            }
        }

        public static string StudentLogInLink
        {
            get
            {
                return GetFromTable<string>("StudentLogInLink");
            }
        }
        public static string ToMailGetFeedback
        {
            get
            {
                return GetFromTable<string>("ToMailGetFeedback");
            }
        }


        public static string DoctorProfilePicUploadPath
        {
            get
            {
                return GetFromTable<string>("DoctorProfilePicUploadPath");
            }
        }

        public static string HeaderImagePath
        {
            get
            {
                return GetFromTable<string>("HeaderImagePath");
            }
        }

        public static string AddOurExcellenceFileUploadHODPath
        {
            get
            {
                return GetFromTable<string>("AddOurExcellenceFileUploadHODPath");
            }
        }

        public static string PatientsEduFrontImage
        {
            get
            {
                return GetFromTable<string>("PatientsEduFrontImage");
            }
        }

        public static string PatientsEduPDF
        {
            get
            {
                return GetFromTable<string>("PatientsEduPDF");
            }
        }


        public static string PatientTestimonialUploadPath
        {
            get
            {
                return GetFromTable<string>("PatientTestimonialUploadPath");
            }
        }

        public static string VideoUploadPath
        {
            get
            {
                return GetFromTable<string>("VideoUploadPath");
            }
        }
        public static string DirectorMessageUploadPath
        {
            get
            {
                return GetFromTable<string>("DirectorMessageUploadPath");
            }
        }
        public static string BannerImageUploadPath
        {
            get
            {
                return GetFromTable<string>("BannerImageUploadPath");
            }
        }
        public static string ChairManMessageUploadPath
        {
            get
            {
                return GetFromTable<string>("ChairManMessageUploadPath");
            }
        }
        public static string AlbumImageUploadPath
        {
            get
            {
                return GetFromTable<string>("AlbumImageUploadPath");
            }
        }

        public static string AddFacultyFileUploadPath
        {
            get
            {
                return GetFromTable<string>("AddFacultyFileUploadPath");
            }
        }

        public static string AcademicMedicalFiles
        {
            get
            {
                return GetFromTable<string>("AcademicMedicalFiles");
            }
        }
        public static string CandidateDetailsPhotograph
        {
            get
            {
                return GetFromTable<string>("CandidateDetailsPhotograph");
            }
        }
        public static string CandidateDetailsSignature
        {
            get
            {
                return GetFromTable<string>("CandidateDetailsSignature");
            }
        }

        public static int SMSOTPExpire
        {
            get
            {
                return Convert.ToInt32(GetFromTable<string>("SMSOTPExpire"));
            }
        }

        public static string GovImagePath
        {
            get
            {
                return GetFromTable<string>("GovImagePath");
            }
        }

        public static string AwardImage
        {
            get
            {
                return GetFromTable<string>("AwardImage");
            }
        }
        public static string RightToInfoPath
        {
            get
            {
                return GetFromTable<string>("RightToInfoPath");
            }
        }


        public static string FacilityInEMCS
        {
            get
            {
                return GetFromTable<string>("FacilityInEMCS");
            }
        }
        public static string VisitorImg
        {
            get
            {
                return GetFromTable<string>("VisitorImg");
            }
        }
        public static string ResearchImage
        {
            get
            {
                return GetFromTable<string>("ResearchImage");
            }
        }
        public static string AccredationImg
        {
            get
            {
                return GetFromTable<string>("AwardImage");
            }
        }
        public static string GovApprovel
        {
            get
            {
                return GetFromTable<string>("GovApprovel");
            }
        }
        public static string CVCareer
        {
            get
            {
                return GetFromTable<string>("CVCareer");
            }
        }
        public static string Event
        {
            get
            {
                return GetFromTable<string>("Event");
            }
        }
        public static string HealthTips
        {
            get
            {
                return GetFromTable<string>("HealthTips");
            }
        }
        public static string Affilation
        {
            get
            {
                return GetFromTable<string>("Affilation");
            }
        }
        public static string StarOfTheWeek_Month
        {
            get
            {
                return GetFromTable<string>("StarOfTheWeek/Month");
            }
        }
        public static string Scheme
        {
            get
            {
                return GetFromTable<string>("Scheme");
            }
        }
        public static string SupportService
        {
            get
            {
                return GetFromTable<string>("SupportService");
            }
        }
        public static string DonationRecipt
        {
            get
            {
                return GetFromTable<string>("DonationRecipt");
            }
        }
        public static string Accommodation
        {
            get
            {
                return GetFromTable<string>("Accommodation");
            }
        }
        public static string BillDeskMerchantId
        {
            get
            {
                return GetFromTable<string>("BillDeskMerchantId");
            }
        }
        public static string BillDeskSecurityCode
        {
            get
            {
                return GetFromTable<string>("BillDeskSecurityCode");
            }
        }
        public static string BillDeskChecksumKey
        {
            get
            {
                return GetFromTable<string>("BillDeskChecksumKey");
            }
        }
        public static string BillDeskURL
        {
            get
            {
                return GetFromTable<string>("BillDeskURL");
            }
        }

        public static string BillDeskTestMerchantId
        {
            get
            {
                return GetFromTable<string>("BillDeskTestMerchantId");
            }
        }
        public static string BillDeskTestSecurityCode
        {
            get
            {
                return GetFromTable<string>("BillDeskTestSecurityCode");
            }
        }
        public static string BillDeskTestChecksumKey
        {
            get
            {
                return GetFromTable<string>("BillDeskTestChecksumKey");
            }
        }
        public static string BillDeskTestURL
        {
            get
            {
                return GetFromTable<string>("BillDeskTestURL");
            }
        }
        public static string BillDeskPaymentMode
        {
            get
            {
                return GetFromTable<string>("BillDeskPaymentMode");
            }
        }
        public static string BillDeskReturnURL
        {
            get
            {
                return GetFromTable<string>("BillDeskReturnURL");
            }
        }
        public static string NewsFiles
        {
            get
            {
                return GetFromTable<string>("NewsFiles");
            }
        }
        public static string GeneralDocument
        {
            get
            {
                return GetFromTable<string>("GeneralDocument");
            }
        }
        public static string MarksheetDoc
        {
            get
            {
                return GetFromTable<string>("MarksheetDoc");
            }
        }
        public static string TendersFiles
        {
            get
            {
                return GetFromTable<string>("TendersFiles");
            }
        }
        public static string ImageUploadPath
        {
            get
            {
                return GetFromTable<string>("ImageUploadPath");
            }
        }
        public static string AddHistoryFileUploadPath
        {
            get
            {
                return GetFromTable<string>("AddHistoryFileUploadPath");
            }
        }
        public static string AddBlogCategoryFileUploadPath
        {
            get
            {
                return GetFromTable<string>("AddBlogCategoryFileUploadPath");
            }
        }
        public static string AddOurExcellenceFileUploadPath
        {
            get
            {
                return GetFromTable<string>("AddOurExcellenceFileUploadPath");
            }
        }
        public static string AddCareerFileUploadPath
        {
            get
            {
                return GetFromTable<string>("AddCareerFileUploadPath");
            }
        }
        public static string AddExecutiveLeadershipFileUploadPath
        {
            get
            {
                return GetFromTable<string>("AddExecutiveLeadershipFileUploadPath");
            }
        }
        public static string AddFacilitesFileUploadPath
        {
            get
            {
                return GetFromTable<string>("AddFacilitesFileUploadPath");
            }
        }
        public static string VisionMissionFilePath
        {
            get
            {
                return GetFromTable<string>("VisionMissionFilePath");
            }
        }
        public static string AffilationFilePath
        {
            get
            {
                return GetFromTable<string>("AffilationFilePath");
            }
        }
        public static string AddAboutUsFileUploadPath
        {
            get
            {
                return GetFromTable<string>("AddAboutUsFileUploadPath");
            }
        }
        public static string AddEquipmentFileUploadPath
        {
            get
            {
                return GetFromTable<string>("AddEquipmentFileUploadPath");
            }
        }
        public static string CandidateExperienceCertificate
        {
            get
            {
                return GetFromTable<string>("CandidateExperienceCertificate");
            }
        }
        public static string SMTPServer
        {
            get
            {
                return GetFromTable<string>("SMTPServer");
            }
        }
        public static string SMTPPort
        {
            get
            {
                return GetFromTable<string>("SMTPPort");
            }
        }
        public static string SMTPAccount
        {
            get
            {
                return GetFromTable<string>("SMTPAccount");
            }
        }
        public static string SMTPPassword
        {
            get
            {
                return GetFromTable<string>("SMTPPassword");
            }
        }
        public static string SMTPFromEmail
        {
            get
            {
                return GetFromTable<string>("SMTPFromEmail");
            }
        }
        public static string StudentPhotograph
        {
            get
            {
                return GetFromTable<string>("StudentPhotograph");
            }
        }
        public static string StudentSignature
        {
            get
            {
                return GetFromTable<string>("StudentSignature");
            }
        }
        public static string StudentDateofBirthProof
        {
            get
            {
                return GetFromTable<string>("StudentDateofBirthProof");
            }
        }
        public static string SMTPIsSecure
        {
            get
            {
                return GetFromTable<string>("SMTPIsSecure");
            }
        }
        public static string SMTPIsTest
        {
            get
            {
                return GetFromTable<string>("SMTPIsTest");
            }
        }

        public static string TestSMTPServer
        {
            get
            {
                return GetFromTable<string>("TestSMTPServer");
            }
        }
        public static string TestSMTPPort
        {
            get
            {
                return GetFromTable<string>("TestSMTPPort");
            }
        }
        public static string TestSMTPAccount
        {
            get
            {
                return GetFromTable<string>("TestSMTPAccount");
            }
        }
        public static string TestSMTPPassword
        {
            get
            {
                return GetFromTable<string>("TestSMTPPassword");
            }
        }
        public static string TestSMTPFromEmail
        {
            get
            {
                return GetFromTable<string>("TestSMTPFromEmail");
            }
        }
        public static string TestSMTPIsSecure
        {
            get
            {
                return GetFromTable<string>("TestSMTPIsSecure");
            }
        }
        public static string AdmissionFilePath
        {
            get
            {
                return GetFromTable<string>("AdmissionFilePath");
            }
        }
        public static string PackageImageUploadPath
        {
            get
            {
                return GetFromTable<string>("PackageImageUploadPath");
            }
        }
        public static string MedicalTourism
        {
            get
            {
                return GetFromTable<string>("MedicalTourism");
            }
        }
        public static string NursingCareImageUploadPath
        {
            get
            {
                return GetFromTable<string>("NursingCareImageUploadPath");
            }
        }
        public static string PatientCareImageUploadPath
        {
            get
            {
                return GetFromTable<string>("PatientCareImageUploadPath");
            }
        }
        public static string PatientCareBrochureImageUploadPath
        {
            get
            {
                return GetFromTable<string>("PatientCareBrochureImageUploadPath");
            }
        }
        public static string PatientCareBrochureFileUploadPath
        {
            get
            {
                return GetFromTable<string>("PatientCareBrochureFileUploadPath");
            }
        }
        public static string PatientCareLeftRightContainImageUploadPath
        {
            get
            {
                return GetFromTable<string>("PatientCareLeftRightContainImageUploadPath");
            }
        }
        public static string AlumniStudentFileUploadPath
        {
            get
            {
                return GetFromTable<string>("AlumniStudentFileUploadPath");
            }
        }
        public static string CovidCareImageUploadPath
        {
            get
            {
                return GetFromTable<string>("CovidCareImageUploadPath");
            }
        }
        public static string CovidCareVideoUploadPath
        {
            get
            {
                return GetFromTable<string>("CovidCareVideoUploadPath");
            }
        }
        public static string CovidCareThumbnailUploadPath
        {
            get
            {
                return GetFromTable<string>("CovidCareThumbnailUploadPath");
            }
        }
        public static string CovidCareFAQImageUploadPath
        {
            get
            {
                return GetFromTable<string>("CovidCareFAQImageUploadPath");
            }
        }
        public static string VideoAlbumThumbnailPath
        {
            get
            {
                return GetFromTable<string>("VideoAlbumThumbnailPath");
            }
        }



        public static string SMSUsername2
        {
            get
            {
                return GetFromTable<string>("SMSUsername2");
            }
        }

        public static string SMSPassword2
        {
            get
            {
                return GetFromTable<string>("SMSPassword2");
            }
        }
        public static string senderid2
        {
            get
            {
                return GetFromTable<string>("senderid2");
            }
        }
        public static string SMSAPI2
        {
            get
            {
                return GetFromTable<string>("SMSAPI2");
            }
        }
        public static string Templateid2
        {
            get
            {
                return GetFromTable<string>("Templateid2");
            }
        }


        public static string SMSTextForAppointment
        {
            get
            {
                return GetFromTable<string>("SMSTextForAppointment");
            }
        }

        public static string SMSForAppointmentTemplateid
        {
            get
            {
                return GetFromTable<string>("SMSForAppointmentTemplateid");
            }
        }

        public static string DoctorRoleId
        {
            get
            {
                return GetFromTable<string>("DoctorRoleId");
            }
        }

        public static string DoctorUnit
        {
            get
            {
                return GetFromTable<string>("UnitRoleId");
            }
        }

    }
}
