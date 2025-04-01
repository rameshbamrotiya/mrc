using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using BO;
using DAL;

namespace BL
{
    public class HomePageContentBAL : HomePageContentDAL
    {

        #region Constructor

        public HomePageContentBAL()
        {
        }

        #endregion Constructors

        #region Insert method  
        public bool InsertRecord(HomePageContent_DetailsBO objHomePageContent)
        {
            try
            {
                return Insert(objHomePageContent);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Update method  
        public bool UpdateRecord(HomePageContentBO objHomePageContent)
        {
            try
            {
                return Update(objHomePageContent);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Delete method  
        public bool DeleteRecord(HomePageContentBO objHomePageContent)
        {
            try
            {
                return Delete(objHomePageContent);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Select method  
        public DataTable SelectRecord(HomePageContent_DetailsBO objHomePageContent)
        {
            try
            {
                return SelectById(objHomePageContent);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable SelectReadmoreLeftRightSide()
        {
            try
            {
                return SelectReadmore();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Select All method  
        public DataTable SelectAllRecord(int languageId)
        {
            try
            {
                return SelectAll(languageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

    }
}
