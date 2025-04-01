using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StudentFamilyDetailsBO
    {
        public long? Id { get; set; }
        public long? StudentId { get; set; }
        public long? CourseId { get; set; }
        public string MemberName { get; set; }
        public string FamilyContactNumber { get; set; }
        public long? Age { get; set; }
        public string RelationId { get; set; }
        public string Relation { get; set; }
        public string RelationName { get; set; }
        public string Occupation { get; set; }
        public decimal MonthlyIncome { get; set; }
        public bool? IsVisible { get; set; }
    }
}
