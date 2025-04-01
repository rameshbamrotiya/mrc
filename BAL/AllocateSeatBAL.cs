using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class AllocateSeatBAL: AllocateSeatDAL,IDisposable
    {

        public bool AllocateSeatAsPerStudentChoiceFilling(string RoundNo, string UserName, out DataTable dt)
        {
            try
            {
                return AllocateSEatAsPerStudentChoiceFilling(RoundNo, UserName,out dt);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetAllAssignSeatLst()
        {
            try
            {
                return GetAllAssignSeatList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {

        }
    }
}
