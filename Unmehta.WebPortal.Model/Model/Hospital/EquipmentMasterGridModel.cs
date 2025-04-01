using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model.Hospital
{
    public class EquipmentMasterGridModel
    {
        public int? Id { get; set; }
        public long? LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentFileName { get; set; }
        public string EquipmentFilePath { get; set; }
        public bool? IsVisible { get; set; }
    }
}
