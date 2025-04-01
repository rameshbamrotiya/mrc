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
    public class RighttoInformationMasterBAL:RighttoInformationMasterDAL
    {
        public bool InsertRecord(RighttoInformationMasterBO objBO,DataTable dt)
        {
            try
            {
                return Insert(objBO,dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
     
        public bool DeleteRecord(RighttoInformationMasterBO objBO)
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
        public DataSet SelectRecord(RighttoInformationMasterBO objBO)
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

        public DataSet SelectContaint(RighttoInformationMasterBO objBO)
        {
            try
            {
                return getContaint(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
