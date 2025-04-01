using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class StatisticsChartDetailsModel
    {
        public long Id { get; set; }

        public System.Nullable<long> ChartId { get; set; }

        public System.Nullable<long> ColumnId { get; set; }

        public string ColumnName { get; set; }

        public string ColumnValue { get; set; }

        public string AliasName { get; set; }

        public long SequanceNo { get; set; }

        public long SwapFromSequanceNo { get; set; }

        public long SwapToSequanceNo { get; set; }

        public string SwapType { get; set; }
    }
    public class StatisticsChartColumnListModel
    {
        public long Id { get; set; }

        public System.Nullable<long> ChartId { get; set; }

        public System.Nullable<long> ColumnId { get; set; }

        public string TypeColumn { get; set; }

        public string ColName { get; set; }

        public long SequanceNo { get; set; }

        public long SwapFromSequanceNo { get; set; }

        public long SwapToSequanceNo { get; set; }

        public string SwapType { get; set; }
    }
}
