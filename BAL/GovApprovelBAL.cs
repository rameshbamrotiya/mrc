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
    public class GovApprovelBAL: GovApprovelDAL
    {
        public DataSet SelectAllCourseType(GovApprovelBO objbo)
        {
            try
            {
                return CourseType(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAllYear()
        {
            try
            {
                return selectyears();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertRecord(GovApprovelBO objBO)
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
        public bool UpdateRecord(GovApprovelBO objBO)
        {
            try
            {
                return Update(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(GovApprovelBO objBO)
        {
            try
            {
                return Delete(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecord(GovApprovelBO objBO)
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
        public DataSet SelectGov_Approvel(int LanguageId)
        {
            try
            {
                return Select_GovApproveldata(LanguageId);
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
        public DataSet SequenceNo()
        {
            try
            {
                return SelectSequenceNo();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
