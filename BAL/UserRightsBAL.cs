using System;
using BO;
using DAL;
using System.Data;

namespace BAL
{
   public class UserRightsBAL:UserRightsDAL
    {
        public DataSet selectMenu(UserRightsBO objBO)
        {
            try
            {
                return SelectMenuRecord(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(UserRightsBO objBO)
        {
            try
            {
                return DeleteMenuByRole(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertMenu(UserRightsBO objBO)
        {
            try
            {
                return InsertMenuByRole(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectResource(MenuBO objbo)
        {
            try
            {
                return SelectResourceRights(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectMenuResourceWise()
        {
            try
            {
                return SelectResource();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
