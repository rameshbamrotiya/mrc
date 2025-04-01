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
    public class AwardsBAL:AwardsDAL
    {
        public bool InsertRecord(AwardsBO objBO, DataTable dt)
        {
            try
            {
                return Insert(objBO, dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(AwardsBO objBO, DataTable dt)
        {
            try
            {
                return Update(objBO, dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(AwardsBO objBO)
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
        public DataSet SelectRecord(AwardsBO objBO)
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
        public DataSet SelectRecordIMG(AwardsBO objBO)
        {
            try
            {
                return SelectIMG(objBO);
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
