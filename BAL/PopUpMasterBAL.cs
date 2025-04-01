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
    public class PopUpMasterBAL:PopUpMasterDAL
    {
        public DataSet PopUpMaster_SelectBypagenameID(PopUpMasterBO objBO)
        {
            try
            {
                return PopUpMasterSelectBypagename(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update_PopUpMaster(PopUpMasterBO objBO)
        {
            try
            {
                return UpdatePopUpMaster(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectHomepagePopupDetail()
        {
            try
            {
                return HomepagePopupDetail();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
