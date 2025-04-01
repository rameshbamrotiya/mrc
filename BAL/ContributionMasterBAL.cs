using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class ContributionMasterBAL : ContributionMasterDAL
    {
        public bool InsertRecord(ContributionMasterBO objBO)
        {
            try
            {
                return InsertContribution(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(ContributionMasterBO objBO)
        {
            try
            {
                return UpdateContribution(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectContributionDetails()
        {
            try
            {
                return GetAllContributionMasterDetails();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectContributionDetailsByID(ContributionMasterBO objbo)
        {
            try
            {
                return GetContributionDetailsByID(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(ContributionMasterBO objBO)
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
    }
}
