using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BO
{
    public class PackageMasterBO
    {
        public int? LanguageId { get; set; }
        public int? Package_id { get; set; }
        public int? Package_level_id { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Is_active { get; set; }        
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
        public int? PackageDetails_Id { get; set; }
    }
    public class PackageSubMasterBO
    {
        public int? Id { get; set; }
        public int? PackageDetails_Id { get; set; }
        public string SubTitle { get; set; }
        public decimal Price { get; set; }
        public int? PackageId { get; set; }
        public string PackageType { get; set; }
        public string Description { get; set; }
        public string Img_path { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
        public int? Package_SubMaster_level_id { get; set; }
    }
}
