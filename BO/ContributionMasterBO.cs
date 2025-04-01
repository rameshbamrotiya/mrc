using System;


namespace BO
{
    public class ContributionMasterBO
    {
        public int? Language_id { get; set; }
        public int? Contribution_Id { get; set; }
        public string Is_active { get; set; }
        public string PageDescription { get; set; }
        public string OfflineDonationDescription { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string added_by { get; set; }
        public string ip_add { get; set; }
    }
}
