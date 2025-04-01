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
    public class StudentEducationDocumentBAL : StudentEducationDocumentDAL , IDisposable
    {

        public DataTable GetAll()
        {
            try
            {
                return SelectAll();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetAllEducationType()
        {
            try
            {
                return SelectAllEducationType();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveData(long Id, string Username)
        {
            bool ret = false;
            try
            {
                return Remove(Id, Username);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        public bool InsertOrUpdateData(StudentEducationDocumentBO objbo)
        {
            bool ret = false;
            try
            {
                return InsertOrUpdate(objbo);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        public void Dispose()
        {

        }
    }
}
