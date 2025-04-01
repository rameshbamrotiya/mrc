using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class MedicalTourismBO
    {
        public int? MT_Id { get; set; }
        public int? LanguageId { get; set; }
        public string MTDescription { get; set; }
        public string MTInnerImgpath { get; set; }
        public string MTInnerVideolink { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Is_active { get; set; }
        public string added_by { get; set; }
        public string ip_add { get; set; }
        public int? IsExist { get; set; }
    }
    public class MedicalTourismDocumentBO
    {
        public int? MTDOC_Id { get; set; }
        public int? MTDetails_Id { get; set; }
        public int? Language_id { get; set; }
        public string DocumentTitle { get; set; }
        public string DocPath { get; set; }
        public string Is_active { get; set; }
        public string added_by { get; set; }
        public string ip_add { get; set; }
    }
    public class MedicalTourismFacilitiesBO
    {
        public int? MTF_id { get; set; }
        public string Name { get; set; }
        public string Doc_path { get; set; }
        public string Description { get; set; }
        public int? LanguageId { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
