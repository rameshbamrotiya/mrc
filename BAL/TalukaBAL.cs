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
   public class TalukaBAL: TalukaDAL
    {
        public bool InsertRecord(TalukaBO objBO)
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
        public bool UpdateRecord(TalukaBO objBO)
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
        public bool DeleteRecord(TalukaBO objBO)
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
        public DataSet SelectRecord(TalukaBO objBO)
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
        public DataSet SelectStateByLanguage(TalukaBO objBO)
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
        public DataSet SelectCountryByLanguage(TalukaBO objBO)
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
        public DataSet SelectDistrictByLanguage(TalukaBO objBO)
        {
            try
            {
                return GetDistrictLanguageWise(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
