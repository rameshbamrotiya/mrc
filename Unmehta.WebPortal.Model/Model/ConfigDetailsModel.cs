using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class ConfigDetailsModel
    {
        public long Id { get; set; }

        public string ParameterName { get; set; }

        public string ParameterValue { get; set; }

        public string Description { get; set; }
    }
}
