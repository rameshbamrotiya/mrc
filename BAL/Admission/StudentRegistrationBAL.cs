using BO.Admission;
using DAL.Admission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Admission
{
    public class StudentRegistrationBAL: StudentRegistrationDAL,IDisposable
    {

        public DataTable GetAllStudentRegistrationMaster()
        {
            try
            {
                return SelectAllStudentRegistrationMaster();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetAllStudentAppliedList(long id)
        {
            try
            {
                return AppliedList(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable LoginStudent(string userName,string passWord)
        {
            try
            {
                return Login(userName,passWord);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool SignUpStudent(StudentRegistrationBO objbo)
        {
            bool ret = false;
            try
            {
                return SignUp(objbo);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        public bool UpdateUserMaster(StudentRegistrationBO objbo)
        {
            bool ret = false;
            try
            {
                return UpdateUser(objbo);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        
        public bool ChangeStudent(string strUsername, string strPassword)
        {
            bool ret = false;
            try
            {
                return ChangePassword(strUsername, strPassword);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
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
        public DataTable GetPasswordByUser(string mobile)
        {
            try
            {
                return GetPasswordByUserName(mobile);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string checkOTP(string MobileNo, string otp)
        {
            try
            {
                return verifyOTP(MobileNo, otp);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Dispose()
        {

        }
    }
}
