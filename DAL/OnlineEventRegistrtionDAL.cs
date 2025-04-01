using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class OnlineEventRegistrtionDAL : DBConnection
    {
        protected bool Insert(OnlineEventRegistrtionBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_OnlineEventRegistrtion_Insert";
                command.Parameters.AddWithValue("@EventId", objbo.EventId);
                command.Parameters.AddWithValue("@FirstName", objbo.FirstName);
                command.Parameters.AddWithValue("@LastName", objbo.LastName);
                command.Parameters.AddWithValue("@SurName", objbo.SurName);
                command.Parameters.AddWithValue("@EmailId", objbo.EmailId);
                command.Parameters.AddWithValue("@PhoneNumber", objbo.PhoneNumber);
                command.Parameters.AddWithValue("@MobileNumber", objbo.MobileNumber);
                command.Parameters.AddWithValue("@Gender", objbo.Gender);
                command.Parameters.AddWithValue("@BirthDate", objbo.BirthDate);
                command.Parameters.AddWithValue("@PhysicalDisability", objbo.PhysicalDisability);
                command.Parameters.AddWithValue("@ExplainTypeofDisability", objbo.ExplainTypeofDisability);
                command.Parameters.AddWithValue("@PhysicalActivity", objbo.PhysicalActivity);
                command.Parameters.AddWithValue("@TypeOfIdentity", objbo.TypeOfIdentity);
                command.Parameters.AddWithValue("@IdentityNumber", objbo.IdentityNumber);
                command.Parameters.AddWithValue("@Residential", objbo.Residential);
                command.Parameters.AddWithValue("@City", objbo.City);
                command.Parameters.AddWithValue("@State", objbo.State);
                command.Parameters.AddWithValue("@Country", objbo.Country);
                command.Parameters.AddWithValue("@PostalCode", objbo.PostalCode);
                command.Parameters.AddWithValue("@EducationQualification", objbo.EducationQualification);
                command.Parameters.AddWithValue("@OrganizationName", objbo.OrganizationName);
                command.Parameters.AddWithValue("@Designation", objbo.Designation);
                command.Parameters.AddWithValue("@EmployeeId", objbo.EmployeeId);
                command.Parameters.AddWithValue("@JoiningDate", objbo.JoiningDate);
                command.Parameters.AddWithValue("@NoOfOrganizationYouWorkWith", objbo.NoOfOrganizationYouWorkWith);
                command.Parameters.AddWithValue("@NoOfCNEYouAttend", objbo.NoOfCNEYouAttend);
                command.Parameters.AddWithValue("@NoOfCMEYouAttend", objbo.NoOfCMEYouAttend);
                command.Parameters.AddWithValue("@WorkExperience", objbo.WorkExperience);
                command.Parameters.AddWithValue("@GNCNo", objbo.GNCNo);
                command.Parameters.AddWithValue("@RegistrtionNo", objbo.RegistrtionNo);
                command.Parameters.AddWithValue("@WorkProfession", objbo.WorkProfession);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataTable Select()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllOnlineEventRegistrtion";
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
