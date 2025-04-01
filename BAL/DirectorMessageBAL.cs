using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BO;
using BAL;
using System.Data;

namespace BAL
{
    public class DirectorMessageBAL:DirectorMessageDAL
    {


        public bool Insert_Update_DirectorMessageMaster(DirectorMessageBO objBO)
        {
            try
            {
                return InsertOrUpdateDirectorMessMaster(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete_DirectorMessage(int DMID)
        {
            try
            {
                return DeleteDirectorMessMaster(DMID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<GetDirectorMessMasterByIdAndLangIdResult> GetDMMasterByIdLangId(int DMID, int LangId)
        {
            try
            {
                return GetDireMesMasterByIdAndLangId(DMID, LangId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
