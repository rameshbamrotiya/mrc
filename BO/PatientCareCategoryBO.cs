using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
  public  class PatientCareCategoryBO
    {
        public int LanguageId { get; set; }
        public string CategoryName { get; set; }
        public int SequenceNo { get; set; }
        public int Recid { get; set; }
        public int CategoryID { get; set; }
        public bool Enabled { get; set; }
        public string added_by { get; set; }
        public string modified_by { get; set; }
        public string ip_add { get; set; }
        public string FormType { get; set; }
    }
}
