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
    public class OnlineEventRegistrtionBAL : OnlineEventRegistrtionDAL
    {
        public bool InsertRecord(OnlineEventRegistrtionBO objBO)
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
        public DataTable SelectRecord()
        {
            try
            {
                return Select();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
