using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IStatisticsChartRepository :IDisposable
    {
         List<GetAllStatisticsChartMasterResult> GetAllStatisticsChart();

        GetAllStatisticsChartMasterResult GetStatisticsChartById(long lgId);

        List<GetAllStatisticsChartMasterDetailsResult> GetAllStatisticsChartDetails();
        
        List<GetAllStatisticsChartMasterDetailsByChartIdResult> GetAllStatisticsChartDetailsByChartId(long lgChartId);

        GetAllStatisticsChartMasterDetailsResult GetStatisticsChartDetailsById(long lgId);

        bool InsertOrUpdateStatisticsChart(GetAllStatisticsChartMasterResult objData, out string strError);

        bool InsertOrUpdateStatisticsChartDetails(StatisticsChartDetailsModel objData, out string strError);

        bool RemoveStatisticsChart(long lgId, out string strError);

        bool RemoveStatisticsChartDetails(long lgId, out string strError);

        bool StatisticsChartMasterSwap(string cmd, decimal? col_menu_level, int col_parent_id, out string strError);

        List<GetAllStatisticsChartMasterColumnListByChartIdResult> GetAllStatisticsChartColumnListByChartId(long lgChartId);

        GetAllStatisticsChartMasterColumnListByChartIdResult GetStatisticsChartColumnListById(long lgChartId, long lgId);

        bool InsertOrUpdateStatisticsChartColumnList(StatisticsChartColumnListModel objData, out string strError);

        bool RemoveStatisticsChartColumnList(long lgId, out string strError);

        bool RemoveStatisticsChartColumnListByChartId(long lgId, out string strError);
        bool RemoveStatisticsChartDetailByChartId(long lgId, out string strError);
    }
}
