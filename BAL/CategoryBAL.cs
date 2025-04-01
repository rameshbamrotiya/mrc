using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;


namespace BAL
{
    public class CategoryBAL:CategoryDAL
    {
        public IEnumerable<CategoryBO> GetAllCategoryList(long lgLangId = 1)
        {
            try
            {
                return GetAllCategory(lgLangId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertRecord(CategoryBO objBO)
        {
            try
            {
                return InsertCategory(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(CategoryBO objBO)
        {
            try
            {
                return UpdateCategory(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectCategiryById(CategoryBO objBO)
        {
            try
            {
                return GetCategoryByID(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
