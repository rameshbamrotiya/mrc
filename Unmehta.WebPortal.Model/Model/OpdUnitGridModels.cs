using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class OpdUnitGridModels
    {
        public long? Id { get; set; }
        public long? LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string UnitName { get; set; }
        public bool? IsVisible { get; set; }
    }
}
