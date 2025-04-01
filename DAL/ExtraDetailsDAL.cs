using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class ExtraDetailsDAL : DBConnection
    {
        protected bool InsertEducationDetails(EducationDetailsBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyEducationDetails_Insert";
                command.Parameters.AddWithValue("@FacultyDetailsId", objbo.FacultyDetailsId);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@EducationName", objbo.EducationName);
                command.Parameters.AddWithValue("@FromYear", objbo.FromYear);
                command.Parameters.AddWithValue("@ToYear", objbo.ToYear);
                command.Parameters.AddWithValue("@DegreeName", objbo.DegreeName);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool UpdateEducationDetails(EducationDetailsBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyEducationDetails_Update";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@EducationName", objbo.EducationName);
                command.Parameters.AddWithValue("@FromYear", objbo.FromYear);
                command.Parameters.AddWithValue("@ToYear", objbo.ToYear);
                command.Parameters.AddWithValue("@DegreeName", objbo.DegreeName);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet GetAllEducationDetailsByFacultyId(EducationDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyEducationDetailsByFacultyId";
                command.Parameters.AddWithValue("@FacultyDetailsId", objbo.FacultyDetailsId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetEducationDetailsByID(EducationDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyEducationDetailsById";
                command.Parameters.AddWithValue("@Id", objbo.Id);             
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(EducationDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyEducationDetails_Delete";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertAreaExperienceDetails(AreaExperienceBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyAreaExperienceDetails_Insert";
                command.Parameters.AddWithValue("@FacultyDetailsId", objbo.FacultyDetailsId);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@EmployerName", objbo.EmployerName);
                command.Parameters.AddWithValue("@FromYear", objbo.FromYear);
                command.Parameters.AddWithValue("@ToYear", objbo.ToYear);
                command.Parameters.AddWithValue("@IsPresent", objbo.IsPresent);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool UpdateAreaExperienceDetails(AreaExperienceBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyAreaExperienceDetails_Update";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@EmployerName", objbo.EmployerName);
                command.Parameters.AddWithValue("@FromYear", objbo.FromYear);
                command.Parameters.AddWithValue("@ToYear", objbo.ToYear);
                command.Parameters.AddWithValue("@IsPresent", objbo.IsPresent);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet GetAllAreaExperienceDetailsByFacultyId(AreaExperienceBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyAreaExperienceDetailsByFacultyId";
                command.Parameters.AddWithValue("@FacultyDetailsId", objbo.FacultyDetailsId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetAreaExperienceDetailsByID(AreaExperienceBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyAreaExperienceDetailsById";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteAreaExperience(AreaExperienceBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyAreaExperienceDetails_Delete";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertPublicationResearchDetails(PublicationResearchDetailsBO objbo)
        {
            bool ret = false;
            try
            {

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyPublicationResearchDetails_Insert";
                command.Parameters.AddWithValue("@FacultyDetailsId", objbo.FacultyDetailsId);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@FromYear", objbo.FromYear);
                command.Parameters.AddWithValue("@ToYear", objbo.ToYear);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool UpdatePublicationResearchDetails(PublicationResearchDetailsBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyPublicationResearchDetails_Update";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@FromYear", objbo.FromYear);
                command.Parameters.AddWithValue("@ToYear", objbo.ToYear);
                command.Parameters.AddWithValue("@Description", objbo.Description);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet GetAllPublicationResearchByFacultyId(PublicationResearchDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyPublicationResearchDetailsByFacultyId";
                command.Parameters.AddWithValue("@FacultyDetailsId", objbo.FacultyDetailsId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetPublicationResearchByID(PublicationResearchDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyPublicationResearchDetailsById";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeletePublicationResearch(PublicationResearchDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyPublicationResearchDetails_Delete";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertAwardsDetails(FacultyAwardsDetailsBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyAwardsDetails_Insert";
                command.Parameters.AddWithValue("@FacultyDetailsId", objbo.FacultyDetailsId);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Title", objbo.Title);
                command.Parameters.AddWithValue("@Month", objbo.Month);
                command.Parameters.AddWithValue("@Year", objbo.Year);
                command.Parameters.AddWithValue("@AwardsDescription", objbo.AwardsDescription);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool UpdateAwardsDetails(FacultyAwardsDetailsBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyAwardsDetails_Update";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@Title", objbo.Title);
                command.Parameters.AddWithValue("@Month", objbo.Month);
                command.Parameters.AddWithValue("@Year", objbo.Year);
                command.Parameters.AddWithValue("@AwardsDescription", objbo.AwardsDescription);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet GetAllAwardsDetailsByFacultyId(FacultyAwardsDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyAwardsDetailsDetailsByFacultyId";
                command.Parameters.AddWithValue("@FacultyDetailsId", objbo.FacultyDetailsId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetAwardsDetailsByID(FacultyAwardsDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyAwardsDetailsById";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteAwards(FacultyAwardsDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyAwardsDetails_Delete";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertServiceDetails(FacultyServiceDetailsBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyServiceDetails_Insert";
                command.Parameters.AddWithValue("@FacultyDetailsId", objbo.FacultyDetailsId);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@ServiceName", objbo.ServiceName);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool UpdateServiceDetails(FacultyServiceDetailsBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyServiceDetails_Update";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@ServiceName", objbo.ServiceName);
                command.Parameters.AddWithValue("@user_id", objbo.added_by);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected DataSet GetAllServiceDetailsByFacultyId(FacultyServiceDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyServiceDetailsByFacultyId";
                command.Parameters.AddWithValue("@FacultyDetailsId", objbo.FacultyDetailsId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetServiceDetailsByID(FacultyServiceDetailsBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyServiceDetailsById";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteService(FacultyServiceDetailsBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_FacultyServiceDetails_Delete";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetFacultyExtraDetailsByFacultyId(int FacultyId,int LanguageId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_GetFacultyExtraDetailsByFacultyId";
                command.Parameters.AddWithValue("@FacultyDetailsId", FacultyId);
                command.Parameters.AddWithValue("@LanguageId", LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectSequenceNo(int DepartmentId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Get_SequenceNo_FacultyMasterDetails";
                command.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
