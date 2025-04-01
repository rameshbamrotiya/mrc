using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ExtraInfoforAdmissionDAL: DBConnection
    {
        protected bool Insert(ExtraInfoforAdmissionBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateExtraInfoAdmissionDetails";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@CandidateId", objbo.CandidateId);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
                command.Parameters.AddWithValue("@UNMICRCContact", objbo.UNMICRCContact);
                command.Parameters.AddWithValue("@ChronicalIllness", objbo.ChronicalIllness);
                command.Parameters.AddWithValue("@BloodGroup", objbo.BloodGroup);
                command.Parameters.AddWithValue("@Allergic", objbo.Allergic);
                command.Parameters.AddWithValue("@MonthYear", objbo.MonthYear);
                command.Parameters.AddWithValue("@EmergencyContactNo", objbo.EmergencyContactNo);
                command.Parameters.AddWithValue("@EmergencyContactPersonName", objbo.EmergencyContactPersonName);
                command.Parameters.AddWithValue("@EmergencyContactPersonRelation", objbo.EmergencyContactPersonRelation);
                command.Parameters.AddWithValue("@EmergencyContactPersonAdd", objbo.EmergencyContactPersonAdd);
                command.Parameters.AddWithValue("@CourtOfLaw", objbo.CourtOfLaw);
                command.Parameters.AddWithValue("@Accommodation", objbo.Accommodation);
                command.Parameters.AddWithValue("@ExtraActivities", objbo.ExtraActivities);
                command.Parameters.AddWithValue("@ExtraActivitiesSocial", objbo.ExtraActivitiesSocial);
                command.Parameters.AddWithValue("@SurgeryInfo", objbo.SurgeryInfo);
                command.Parameters.AddWithValue("@AboutCourse", objbo.AboutCourse);
                command.Parameters.AddWithValue("@AboutCourseOther", objbo.AboutCourseOther);
                command.Parameters.AddWithValue("@UserName", objbo.modified_by);
                command.Parameters.AddWithValue("@courseheard", objbo.courseheard);
                command.Parameters.AddWithValue("@UNMICRCContactYN", objbo.UNMICRCContactYN);
                command.Parameters.AddWithValue("@ChronicalIllnessYN", objbo.ChronicalIllnessYN);
                command.Parameters.AddWithValue("@AllergicYN", objbo.AllergicYN);
                command.Parameters.AddWithValue("@SurgeryInfoYN", objbo.SurgeryInfoYN);
                command.Parameters.AddWithValue("@CourtOfLawYN", objbo.CourtOfLawYN);
                
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertReferralDetails(ExtraInfoforAdmissionBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertOrUpdateExtraInfoAdmissionReferralDetails";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@CandidateId", objbo.CandidateId);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
                command.Parameters.AddWithValue("@Name", objbo.Name);
                command.Parameters.AddWithValue("@Position", objbo.Position);
                command.Parameters.AddWithValue("@MobileNo", objbo.MobileNo);
                command.Parameters.AddWithValue("@RelationShip", objbo.RelationShip);
                command.Parameters.AddWithValue("@YearsKnown", objbo.YearsKnown);
                command.Parameters.AddWithValue("@Address", objbo.Address);
                command.Parameters.AddWithValue("@UserName", objbo.modified_by);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(ExtraInfoforAdmissionBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ExtraInfoAdmissionReferralDetails_Delete";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(ExtraInfoforAdmissionBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ExtraInfoAdmissionDetailsSelect";
                command.Parameters.AddWithValue("@CandidateId", objbo.CandidateId);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectReferralDetails(ExtraInfoforAdmissionBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ExtraInfoAdmissionDetailsSelectReferralDetails";
                command.Parameters.AddWithValue("@CandidateId", objbo.CandidateId);
                command.Parameters.AddWithValue("@CourseId", objbo.CourseId);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectAdvertisementSource()
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "AdvertisementSourceMasterDetails_SelectAll";
                //command.Parameters.AddWithValue("@col_menu_id", objbo.parentid);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet CandidateSelect(int Id,long CourseId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetStudentDetails";
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@CourseId", CourseId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetStudentDetails(int Id,long CourseId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetStudentDetails";
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@CourseId", CourseId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertFinal(int Id, long CourseId, int IsFinalSubmit)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "FinalSubmit_StudentRegistration";
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@CourseId", CourseId);
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
