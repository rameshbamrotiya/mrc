using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class PrivacyPolicyMasterBAL:PrivacyPolicyMasterDAL
    {
        public bool InsertRecord(PrivacyPolicyMasterBO objBO)
        {
            try
            {
                return InsertPP(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecord(PrivacyPolicyMasterBO objBO)
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
