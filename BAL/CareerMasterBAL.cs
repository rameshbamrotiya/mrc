using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BAL
{
    public class CareerMasterBAL:CareerMasterDAL
    {
        public bool InsertRecordOTP(EmailOTPBO objBO)
        {
            try
            {
                return InsertOTP(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public string checkOTP(string Email, string otp)
        {
            try
            {
                return verifyOTP(Email, otp);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet MailCreditials()
        {
            try
            {
                return GetMailCreditials();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetAllCandidateDetail()
        {
            try
            {
                return GetAllCandidateDetails().Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetAllCandidateDetails_ForCallLetter()
        {
            try
            {
                return GetAllCandidateDetailsForCallLetter().Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertRecordScrutiny(ScrutinyMasterBO objBo)
        {
            try
            {
                return InsertScrutiny(objBo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet Candidate_GetJobApplication()
        {
            try
            {
                return CandiGetJobApplication();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet desc_GetJobApplication(int JobId)
        {
            try
            {
                return GetJobApplicationdesc(JobId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet Candidate_CheckFinalSubmitFlag(string EmailId, int JobId)
        {
            try
            {
                return CandidateCheckFinalSubmitFlag(EmailId, JobId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetGradeSheetsDetails()
        {
            try
            {
                return GetAllCandidatesDetailsForGradeSheet();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetTotalApplicationReceived_Details(int IsFinalSubmit)
        {
            try
            {
                return GetTotalApplicationReceivedDetails(IsFinalSubmit);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetAllCandidateDetails_ByPostId(int PostId, int IsFinalSubmit)
        {
            try
            {
                return GetAllCandidateDetailsByPostId(PostId, IsFinalSubmit);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet Candidate_Select(CareerMasterBO objBO)
        {
            try
            {
                return CandidateSelect(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertFinalSubmit(int Id, int IsFinalSubmit)
        {
            try
            {
                return InsertFinal(Id, IsFinalSubmit);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
