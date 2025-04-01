using System;


namespace BO
{
   public class CategoryBO
    {
        public int LanguageId { get; set; }
        public string CategoryName { get; set; }
        public int SchemeID { get; set; }
        public int Recid { get; set; }
        public int CategoryID { get; set; }
        public bool Enabled { get; set; }
        public string added_by { get; set; }
        public string modified_by { get; set; }
        public string ip_add { get; set; }
    }
}
