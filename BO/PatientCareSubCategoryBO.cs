using System;


namespace BO
{
   public class PatientCareSubCategoryBO
    {
        public int LanguageId { get; set; }
        public string CategoryName { get; set; }
        public int SequenceNo { get; set; }
        public int Recid { get; set; }
        public int CategoryID { get; set; }
        public string Description { get; set; }
        public int SubCategoryID { get; set; }
        public int Img_id { get; set; }
        public bool Enabled { get; set; }
        public string added_by { get; set; }
        public string modified_by { get; set; }
        public string ip_add { get; set; }
    }
}
