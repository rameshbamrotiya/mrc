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
    public class CityBAL:CityDAL
    {
        public bool InsertRecord(CityBO objBO)
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
        public bool UpdateRecord(CityBO objBO)
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
        public bool DeleteRecord(CityBO objBO)
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
        public DataSet SelectRecord(CityBO objBO)
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
        public DataSet SelectStateByLanguage(CityBO objBO)
        {
            try
            {
                return GetStateLanguageWise(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectCountryByLanguage(CityBO objBO)
        {
            try
            {
                return GetCountryLanguageWise(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
