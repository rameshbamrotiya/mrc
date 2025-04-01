using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace DAL
{
    public class CareerMasterDAL : DBConnection
    {
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        protected bool InsertOTP(EmailOTPBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "InsertOrUpdateRequirtmentEmailOtpManage");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        


        protected DataSet GetAllCandidateDetails()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetAllCandidateDetails";
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected string verifyOTP(string EmailId, string OTP)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "VerifyOTP";
                command.Parameters.AddWithValue("@EmailId", EmailId);
                command.Parameters.AddWithValue("@OTP", OTP);
                string result = ExecuteScalar(command).ToString();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected DataSet GetAllCandidatesDetailsForGradeSheet()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllCandidatesDetailsForGradeSheet";
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetMailCreditials()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_Get_tbl_SendMailCredentials";
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet CandiGetJobApplication()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllRecruitmentAdvertisementMaster";
                //command.Parameters.AddWithValue("@Id", JobId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetJobApplicationdesc(int JobId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllJobMasterById";
                command.Parameters.AddWithValue("@Id", JobId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataSet CandidateCheckFinalSubmitFlag(string EmailId, int JobId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "CheckFinalSubmitFlag";
                command.Parameters.AddWithValue("@EmailId", EmailId);
                command.Parameters.AddWithValue("@JobId", JobId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetAllCandidateDetailsForCallLetter()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetAllCandidateDetailsForCallLetter";
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected string UnlockProfile(string CandId, string JobId, string UserName, out string strError)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateLock";
                command.Parameters.AddWithValue("@CandId", CandId);
                command.Parameters.AddWithValue("@JobId", JobId);
                command.Parameters.AddWithValue("@UserName", UserName);
                string result = ExecuteScalar(command).ToString();
                strError = "";

                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                throw ex;
            }
        }
        protected DataTable GetFileListByJobIdAndCandId(long CandId, long JobId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetFileListByJobIdAndCandId";
                command.Parameters.AddWithValue("@CandId", CandId);
                command.Parameters.AddWithValue("@JobId", JobId);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertScrutiny(ScrutinyMasterBO objBo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objBo, ref command, "Insert_ScrutinyMaster");
                //command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                //ExecuteNonQuery(command);
                string IsExits = ExecuteScalar(command).ToString();
                if (IsExits == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetTotalApplicationReceivedDetails(int IsFinalSubmit)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_ApplicationReceived";
                command.Parameters.AddWithValue("@IsFinalSubmit", IsFinalSubmit);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetAllCandidateDetailsByPostId(int PostId, int IsFinalSubmit)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetAllCandidateDetailsByPostId";
                command.Parameters.AddWithValue("@JobId", PostId);
                command.Parameters.AddWithValue("@IsFinalSubmit", IsFinalSubmit);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet CandidateSelect(CareerMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetCandidateDetails";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertFinal(int Id, int IsFinalSubmit)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "FinalSubmit_CandidateRegistration";
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@IsFinalSubmit", IsFinalSubmit);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
