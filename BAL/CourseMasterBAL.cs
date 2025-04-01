using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class CourseMasterBAL:CourseMasterDAL
    {
        public bool InsertRecord(CourseMasterBO objBO)
        {
            try
            {
                return Insert(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(CourseMasterBO objBO)
        {
            try
            {
                return Update(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(CourseMasterBO objBO)
        {
            try
            {
                return Delete(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecord(CourseMasterBO objBO)
        {
            try
            {
                return Select(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectVideo(CourseMasterBO objBO)
        {
            try
            {
                return SelectVideoDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
