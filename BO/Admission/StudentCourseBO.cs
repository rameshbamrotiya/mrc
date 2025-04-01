using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Admission
{

    [Serializable()]
    public class StudentSubCourseBO
    {
        public long Id { get; set; }

        public string TempId { get; set; }

        public long? StudentCourseId { get; set; }

        public string CourseName { get; set; }

        public string Information { get; set; }

        public string FeesDescription { get; set; }

        public string CourseNote { get; set; }

        public string ImagePath { get; set; }

        public string CourseCode { get; set; }

        public long? TotalSeat { get; set; }

        public string CourseDuration { get; set; }

        public bool IsDelete { get; set; }

        public string CreateBy { get; set; }

        public System.Nullable<System.DateTime> CreateDate { get; set; }

        public string UpdateBy { get; set; }

        public System.Nullable<System.DateTime> UpdateDate { get; set; }

        public string DeleteBy { get; set; }

        public System.Nullable<System.DateTime> DeleteDate { get; set; }
    }


    [Serializable()]
    public class StudentCourseBO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Type { get; set; }

        public bool IsVisible { get; set; }

        public bool IsDelete { get; set; }

        public string CreateBy { get; set; }

        public System.Nullable<System.DateTime> CreateDate { get; set; }

        public string UpdateBy { get; set; }

        public System.Nullable<System.DateTime> UpdateDate { get; set; }

        public string DeleteBy { get; set; }

        public System.Nullable<System.DateTime> DeleteDate { get; set; }
    }

    [Serializable()]
    public class StudentCourseConfigurationBO
    {
        public long Id { get; set; }
        public long CourseId { get; set; }
        public string CourseName { get; set; }
        public long StudentAdvertisementId { get; set; }
        public string AdvertisementName { get; set; }
        public long? EntryFees { get; set; }
        public long? MinAge { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Desciption { get; set; }
        public bool IsVisible { get; set; }
        public bool IsDelete { get; set; }
        public string CreateBy { get; set; }
        public System.Nullable<System.DateTime> CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public System.Nullable<System.DateTime> UpdateDate { get; set; }
        public string DeleteBy { get; set; }
        public System.Nullable<System.DateTime> DeleteDate { get; set; }
    }

    [Serializable()]
    public class StudentEducationQualificationDetailBO
    {
        public long Id { get; set; }

        public long CourseId { get; set; }

        public string CourseName { get; set; }

        public long QualificationId { get; set; }

        public string QualificationName { get; set; }
    }

    [Serializable()]
    public class StudentEducationTypeDetailBO
    {
        public long Id { get; set; }

        public long CourseId { get; set; }

        public string CourseName { get; set; }

        public long QualificationTypeId { get; set; }

        public string TypeName { get; set; }

        public int IsNonMandatory { get; set; }
    }
}
