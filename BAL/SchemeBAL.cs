using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
  public  class SchemeBAL:SchemeDAL
    {
        public bool InsertRecord(SchemeBO objBO)
        {
            try
            {
                return InsertScheme(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectSchemeByID(SchemeBO objBO)
        {
            try
            {
                return GetRecordBySchemeId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(SchemeBO objBO)
        {
            try
            {
                return UpdateSchemeRecord(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(SchemeBO objBO)
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
        public DataSet SelectSchemeByLanguage(SchemeBO objBO)
        {
            try
            {
                return GetSchemeLanguageWise(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long InsertOrUpdateSchemaChartDetail(long lgId, long lgSchemaId, long lgChartId, long lgSequanceNo, string strUsername)
        {
            try
            {
                return InsertOrUpdateSchemaChartDetails(lgId, lgSchemaId, lgChartId, lgSequanceNo, strUsername);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveSchemaChartDetail(long lgSchemaId, string strUsername)
        {
            try
            {
                return RemoveSchemaChartDetails(lgSchemaId, strUsername);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool SchemaChartOrders(string cmd, string col_menu_level, string col_parent_id)
        {
            try
            {
                return SchemaChartOrder(cmd, col_menu_level, col_parent_id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetAllSchemaChartDetails(long lgSchemaId)
        {
            try
            {
                return GetAllSchemaChartDetail(lgSchemaId);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
