using System;
using BO;
using DAL;
using System.Data;

namespace BAL
{
    public class StudentGenerateMeritNumberBAL : StudentGenerateMeritNumberDAL, IDisposable
    {
        public DataTable GetAllStudentForGenerateMeritNumbers(long CourseId,string strSorting="")
        {
            try
            {
                return GetAllStudentForGenerateMeritNumbersList(CourseId,strSorting);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetAllMeritLstByCourse()
        {
            try
            {
                return GetAllMeritListByCourse();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public bool UpdateStudentRegMeritNumber(long Id, long MeritNo, string MeritNoRefNo, string Username)
        {
            bool ret = false;
            try
            {
                return UpdateStudentRegMeritNo(Id, MeritNo, MeritNoRefNo, Username);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        public bool UpdateStudentRegGrpName(long Id, string GroupName, string GroupRefNo, string Username)
        {
            bool ret = false;
            try
            {
                return UpdateStudentRegGroupName(Id, GroupName, GroupRefNo, Username);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        public void InsertLogg(long RefId, bool IsFilterOrSort, string ColumnName, string ColumnValue)
        {
            bool ret = false;
            try
            {
                 InsertLog(RefId, IsFilterOrSort, ColumnName, ColumnValue);
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
