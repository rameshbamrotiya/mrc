using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class TCBAL:TCDAL
    {
        public bool InsertRecord(TCBO objBO)
        {
            try
            {
                return InsertTC(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecord(TCBO objBO)
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
