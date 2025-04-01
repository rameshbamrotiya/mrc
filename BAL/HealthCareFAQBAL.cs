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
    public class HealthCareFAQBAL:HealthCareFAQDAL
    {
        public bool InsertRecord(HealthCareFAQBO objBO)
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
        public bool UpdateRecord(HealthCareFAQBO objBO)
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
        public bool DeleteRecord(HealthCareFAQBO objBO)
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
        public DataSet SelectRecord(HealthCareFAQBO objBO)
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
    }
}
