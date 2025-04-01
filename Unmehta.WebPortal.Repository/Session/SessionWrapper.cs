using BO;
using BO.Admission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unmehta.WebPortal.Model.Common;

/// <summary>
/// Description :   Manage session variables by using this class
/// This wrapper provides a safe typed access from all over the application, 
/// one place to define all objects that stored in the session
/// </summary>

public class SessionWrapper
{
    private static T GetFromSession<T>(string key)
    {
        object obj = HttpContext.Current.Session[key];
        if (obj == null)
        {
            return default(T);
        }
        return (T)obj;
    }
    private static void SetInSession<T>(string key, T value)
    {
        if (value == null)
        {
            HttpContext.Current.Session.Remove(key);
        }
        else
        {
            HttpContext.Current.Session[key] = value;
        }
    }

    public static StudentRegistrationBO StudentRegistration
    {
        get
        {
            return GetFromSession<StudentRegistrationBO>("StudentRegistration");
        }
        set
        {
            SetInSession<StudentRegistrationBO>("StudentRegistration", value);
        }
    }

    public static SessionUserModel UserDetails
    {
        get
        {
            return GetFromSession<SessionUserModel>("UserDetails");
        }
        set
        {
            SetInSession<SessionUserModel>("UserDetails", value);
        }
    }

    public static string PatientMobileNo
    {
        get
        {
            return GetFromSession<string>("PatientMobileNo");
        }
        set
        {
            SetInSession<string>("PatientMobileNo", value);
        }
    }


    //public static List<UserRightsBO> UserPageDetails
    //{
    //    get
    //    {
    //        return GetFromSession<List<UserRightsBO>>("UserPageDetails");
    //    }
    //    set
    //    {
    //        SetInSession<List<UserRightsBO>>("UserPageDetails", value);
    //    }
    //}

    public static int BasicDetailsFlag
    {
        get
        {
            return GetFromSession<int>("BasicDetailsFlag");
        }
        set
        {
            SetInSession<int>("BasicDetailsFlag", value);
        }
    }
    public static int EducationDetailsFlag
    {
        get
        {
            return GetFromSession<int>("EducationDetailsFlag");
        }
        set
        {
            SetInSession<int>("EducationDetailsFlag", value);
        }
    }

    public static int ProfessionalDetailsFlag
    {
        get
        {
            return GetFromSession<int>("ProfessionalDetailsFlag");
        }
        set
        {
            SetInSession<int>("ProfessionalDetailsFlag", value);
        }
    }
    public static int JobId
    {
        get
        {
            return GetFromSession<int>("JobId");
        }
        set
        {
            SetInSession<int>("JobId", value);
        }
    }

    public static int LanguageId
    {
        get
        {
            return GetFromSession<int>("LanguageId");
        }
        set
        {
            SetInSession<int>("LanguageId", value);
        }
    }
    public static string PostName
    {
        get
        {
            return GetFromSession<string>("PostName");
        }
        set
        {
            SetInSession<string>("PostName", value);
        }
    }
    public static string EmailId
    {
        get
        {
            return GetFromSession<string>("EmailId");
        }
        set
        {
            SetInSession<string>("EmailId", value);
        }
    }
    public static string PaymentReturnUrl
    {
        get
        {
            return GetFromSession<string>("PaymentReturnUrl");
        }
        set
        {
            SetInSession<string>("PaymentReturnUrl", value);
        }
    }
    public static int CandidateId
    {
        get
        {
            return GetFromSession<int>("CandidateId");
        }
        set
        {
            SetInSession<int>("CandidateId", value);
        }
    }
    public static int FinalDetailsFlag
    {
        get
        {
            return GetFromSession<int>("FinalDetailsFlag");
        }
        set
        {
            SetInSession<int>("FinalDetailsFlag", value);
        }
    }
    public static UserRightsBO UserPageDetails
    {
        get
        {
            return GetFromSession<UserRightsBO>("UserPageDetails");
        }
        set
        {
            SetInSession<UserRightsBO>("UserPageDetails", value);
        }
    }

    public static int sendConfirmationMail
    {
        get
        {
            return GetFromSession<int>("sendConfirmationMail");
        }
        set
        {
            SetInSession<int>("sendConfirmationMail", value);
        }
    }
    public static int sendConfirmationMailFlag
    {
        get
        {
            return GetFromSession<int>("sendConfirmationMailFlag");
        }
        set
        {
            SetInSession<int>("sendConfirmationMailFlag", value);
        }
    }
    public static SessionFileUploadModel FileUploadDetails
    {
        get
        {
            return GetFromSession<SessionFileUploadModel>("FileUploadDetails");
        }
        set
        {
            SetInSession<SessionFileUploadModel>("FileUploadDetails", value);
        }
    }
    public static string RegistrationId
    {
        get
        {
            return GetFromSession<string>("RegistrationId");
        }
        set
        {
            SetInSession<string>("RegistrationId", value);
        }
    }
    public static int OTP
    {
        get
        {
            return GetFromSession<int>("OTP");
        }
        set
        {
            SetInSession<int>("OTP", value);
        }
    }
    public static int ForgetStudentOTP
    {
        get
        {
            return GetFromSession<int>("ForgetStudentOTP");
        }
        set
        {
            SetInSession<int>("ForgetStudentOTP", value);
        }
    }
}
