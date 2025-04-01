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
    public class ConfigUnitDoctorDetailMasterBAL : ConfigUnitDoctorDetailDAL
    {
        public bool InsertRecord(ConfigUnitDoctorDetailMasterBO objBO)
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
        public bool UpdateRecord(ConfigUnitDoctorDetailMasterBO objBO)
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
        public bool DeleteRecord(long? Id)
        {
            try
            {
                return Delete(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecord(ConfigUnitDoctorDetailMasterBO objBO)
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
