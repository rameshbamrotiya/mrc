using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class GeneralinstructionBO
    {
        public int Id { get; set; }
        public string Desciption { get; set; }
        public string DocName { get; set; }
        public string DocPath { get; set; }
        public bool IsVisible { get; set; }
        public string CreateBy { get; set; }
        public int? IsExist { get; set; }

    }
}
