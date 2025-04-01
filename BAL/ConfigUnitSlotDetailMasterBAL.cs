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
    public class ConfigUnitSlotDetailMasterBAL : ConfigUnitSlotDetailDAL
    {
        public bool InsertRecord(ConfigUnitSlotDetailMasterBO objBO)
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
        public bool UpdateRecord(ConfigUnitSlotDetailMasterBO objBO)
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
        public DataSet SelectRecord(ConfigUnitSlotDetailMasterBO objBO)
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
