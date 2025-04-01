using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class FeedbackBAL : FeedbackDAL
    {
        public bool InsertRecord(FeedbackBO objBO)
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
        public DataSet SelectAllRecord()
        {
            try
            {
                return SelectAll();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecord(FeedbackBO objBO)
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
    }
}
