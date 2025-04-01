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
    public class ChairManMessageBAL:ChairManMessageDAL
    {
        public bool Insert_Update_ChairMessageMaster(ChairManMessageBO objBO)
        {
            try
            {
                return InsertOrUpdateChairmanMessMaster(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete_ChairmanMessage(int DMID)
        {
            try
            {
                return DeleteChairmanMessMaster(DMID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<GetChairmanMessMasterByIdAndLangIdResult> GetCMessMasterByIdLangId(int CMId, int LangId)
        {
            try
            {
                return GetChairmanMesMasterByIdAndLangId(CMId, LangId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
