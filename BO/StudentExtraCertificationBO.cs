using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StudentExtraCertificationBO
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public long? CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string Duration { get; set; }
        public string InstituteName { get; set; }
        public string CITY { get; set; }
        public bool IsVisible { get; set; }
        public string UserName { get; set; }

    }
}
