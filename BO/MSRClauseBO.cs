using System;


namespace BO
{
    public class MSRClauseBO
    {
        public int msrid { get; set; }
        public int? Msr_level_id { get; set; }
        public string Particulars { get; set; }
        public string imagepath { get; set; }
        public DateTime? LatstupdateDate { get; set; }
        public int languageid { get; set; }
        public Boolean enabled { get; set; }
        public string AddedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ipadd { get; set; }
    }
}
