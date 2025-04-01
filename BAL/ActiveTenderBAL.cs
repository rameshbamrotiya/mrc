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
    public class ActiveTenderBAL:ActiveTenderDAL
    {
        public bool UpdateRecordInActiveTenders(ActiveTenderBO objBO)
        {
            try
            {
                return UpdateInActive(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecordActiveTenders(ActiveTenderBO objBO)
        {
            try
            {
                return UpdateActive(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
