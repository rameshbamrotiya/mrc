using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class ClinicalMaterialMasterBAL : ClinicalMaterialMasterDAL
    {
        public bool InsertRecord(ClinicalMaterialMasterBO objBO)
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
        public DataSet SelectRecord(ClinicalMaterialMasterBO objBO)
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
        public DataSet GetClinicalMaterialMasterDetails(ClinicalMaterialMasterBO objBO)
        {
            try
            {
                return GetClinicalMaterialMasterDetailsData(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
