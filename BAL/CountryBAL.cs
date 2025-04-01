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
    public class CountryBAL : CountryDAL
    {
        public bool InsertRecord(CountryBO objBO)
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
        public bool UpdateRecord(CountryBO objBO)
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
        public bool DeleteRecord(CountryBO objBO)
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
        public DataSet SelectRecord(CountryBO objBO)
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
        public DataSet SelectCountryCode()
        {
            try
            {
                return SelectCountry_Code();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectTalukaCode(int District_id, int LangId)
        {
            try
            {
                return SelectTaluka_Code(District_id, LangId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
