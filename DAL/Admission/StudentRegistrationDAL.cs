using BO.Admission;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL.Admission
{
    public class StudentRegistrationDAL : DBConnection
    {

        protected bool UpdateUser(StudentRegistrationBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "StudentRegistrationUpdate";
                command.Parameters.AddWithValue("@Id", objbo.Id);
                command.Parameters.AddWithValue("@FirstName", objbo.FirstName);
                command.Parameters.AddWithValue("@MiddleName", objbo.MiddleName);
                command.Parameters.AddWithValue("@LastName", objbo.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", objbo.DateOfBirth);
                command.Parameters.AddWithValue("@Email", objbo.Email);
                command.Parameters.AddWithValue("@Mobile", objbo.Mobile);
                command.Parameters.AddWithValue("@Gender", objbo.Gender);
                command.Parameters.AddWithValue("@MaritalStatus", objbo.MaritalStatus);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool SignUp(StudentRegistrationBO objbo)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "StudentSignUp";
                //command.Parameters.AddWithValue("@NamePrefix", objbo.NamePrefix);
                command.Parameters.AddWithValue("@AadharCard", objbo.AadharCard);
                command.Parameters.AddWithValue("@FirstName", objbo.FirstName);
                command.Parameters.AddWithValue("@MiddleName", objbo.MiddleName);
                command.Parameters.AddWithValue("@LastName", objbo.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", objbo.DateOfBirth);
                command.Parameters.AddWithValue("@Email", objbo.Email);
                command.Parameters.AddWithValue("@Mobile", objbo.Mobile);
                command.Parameters.AddWithValue("@Username", objbo.Username);
                command.Parameters.AddWithValue("@Password", objbo.Password);
                command.Parameters.AddWithValue("@Gender", objbo.Gender);
                command.Parameters.AddWithValue("@MaritalStatus", objbo.MaritalStatus);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return ret;
        }

        protected bool ChangePassword(string strUsername, string strPassword)
        {
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "StudentChangePassword";
                command.Parameters.AddWithValue("@Username", strUsername);
                command.Parameters.AddWithValue("@Password", strPassword);
                ExecuteNonQuery(command);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        protected bool InsertOTP(EmailOTPBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "InsertOrUpdateAdmissionOtpManage");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected DataTable Login(string Username, string Password)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "LoginOfStudent";
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", Password);
                //command.Parameters.AddWithValue("@col_menu_id", objbo.parentid);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataTable AppliedList(long id)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetStudentAppliedList";
                command.Parameters.AddWithValue("@Id", id);
                //command.Parameters.AddWithValue("@col_menu_id", objbo.parentid);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataTable SelectAllStudentRegistrationMaster()
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllStudentRegistrationMaster";
                //command.Parameters.AddWithValue("@col_menu_id", objbo.parentid);
                ds = ExecuteQuery(command);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected string verifyOTP(string MobileNo, string OTP)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "VerifyOTPAdmission";
                command.Parameters.AddWithValue("@MobileNo", MobileNo);
                command.Parameters.AddWithValue("@OTP", OTP);
                string result = ExecuteScalar(command).ToString();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
       

        protected DataTable GetPasswordByUserName(string MobileNo)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetUserNamePassword";
                command.Parameters.AddWithValue("@Username", MobileNo);
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
