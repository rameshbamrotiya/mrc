using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
  public  class TenderMasterBO
    {
        public string DocName;
        public int? DocType;

        public int IsNewIcon { get; set; }
        public int Status { get; set; }
        public string Details { get; set; }
        public string ip_add { get; set; }
        public int? IsExist { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? LastDate { get; set; }
        public DateTime? OpeningDate { get; set; }
        public DateTime? PBMeetingDate { get; set; }
        public int? TenderID { get; set; }
        public string TenderNo { get; set; }
        public string Title { get; set; }
        public int? DocID { get; set; }
        public string DocPath { get; set; }
        public decimal? ProjectEstimateValue { get; set; }
        public decimal? ProjectFinalValue { get; set; }
        public string NameOfBidder { get; set; }
        public DateTime? IssueOfWorkOrderDate { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string UpdatedBy { get; set; }
        public int? Tender_level_id { get; set; }
    }
}
