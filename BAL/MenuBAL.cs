using BO;
using DAL;
using System;
using System.Data;


namespace BAL
{
    public class MenuBAL : MenuDAL
    {
        public DataSet SelectRecord(MenuBO objBO)
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
        public DataSet SelectMenutype(MenuBO objBO)
        {
            try
            {
                return selectMenu(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectMenubyResource(MenuBO objBO)
        {
            try
            {
                return SelectMenuByID(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SearchMenuByName(MenuBO objBO)
        {
            try
            {
                return SearchMenu(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectParentMEnu(MenuBO objBO)
        {
            try
            {
                return SelectParentResource(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertRecord(MenuBO objBO)
        {
            try
            {
                return InsertMenu(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(MenuBO objBO)
        {
            try
            {
                return UpdateMenu(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateRecordResource(MenuBO objBO)
        {
            try
            {
                return UpdateMenuResource(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdatePageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            try
            {
                return UpdateOrder(cmd, col_menu_level, col_parent_id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GetTranslateStatus(int languageId, string URl)
        {
            return SelectTranslateStatus(languageId, URl);
        }

       
    }
}
