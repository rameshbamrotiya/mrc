using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class PackageTypeGridModels
    {
        public long? Id { get; set; }
        public long? LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string PackageType { get; set; }
        public bool? IsVisible { get; set; }
    }
}
