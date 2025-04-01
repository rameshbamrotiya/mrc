using System;
using System.Data;
using DAL;

namespace BAL
{
   public class WriteToUNMICRCBAL: WriteToUNMICRCDAL
    {
        public DataTable GetUNMICRCdata()
        {
            try
            {
                return Getdata();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
