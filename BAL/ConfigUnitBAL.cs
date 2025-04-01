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
    public class ConfigUnitBAL : ConfigUnitDAL
    {
        public bool InsertRecord(ConfigUnitMasterBO objBO)
        {
            try
            {
                return Insert(ref objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(ConfigUnitMasterBO objBO)
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
        public bool DeleteRecord(ConfigUnitMasterBO objBO)
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
        public DataSet SelectRecord(ConfigUnitMasterBO objBO)
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
        public DataSet SelectUnitSloteRecord(ConfigSloteDetailsByidforEdit objBO)
        {
            try
            {
                return SelectSloteRecord(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectAppoinmentRecord(AppoinmentDetaiBO objBO)
        {
            try
            {
                return SelectAppoinment(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateAppoinmentRecord(int patientId, string strUserName)
        {
            try
            {
                return UpdateAppoinment(patientId, strUserName);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
