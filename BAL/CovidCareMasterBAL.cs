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
    public class CovidCareMasterBAL : CovidCareMasterDAL
    {
        public bool InsertRecord(CovidCareMasterBO objBO)
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
        public bool UpdateRecord(CovidCareMasterBO objBO)
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
        public bool DeleteRecord(CovidCareMasterBO objBO)
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
        public DataSet SelectRecord(CovidCareMasterBO objBO)
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
        public DataSet GetCovidCareDetailsByLanguage(CovidCareMasterBO objBO)
        {
            try
            {
                return GetCovidCareDetailsByLanguageId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertAccredationRecord(CovidCareAccredationDetailsBO objBO)
        {
            try
            {
                return InsertAccredation(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateAccredationRecord(CovidCareAccredationDetailsBO objBO)
        {
            try
            {
                return UpdateAccredation(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteAccredationRecord(CovidCareAccredationDetailsBO objBO)
        {
            try
            {
                return DeleteAccredation(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAccredationRecord(CovidCareAccredationDetailsBO objBO)
        {
            try
            {
                return SelectAccredation(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetAccredationDetailsById(CovidCareAccredationDetailsBO objBO)
        {
            try
            {
                return GetAccredationDetailsByAccredationId(objBO);
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
