using System;


namespace BO
{
    public class EducationDetailsBO
    {
        public int? Id { get; set; }
        public int? FacultyDetailsId { get; set; }
        public int? LanguageId { get; set; }
        public string EducationName { get; set; }
        public string FromYear { get; set; }
        public string ToYear { get; set; }
        public string DegreeName { get; set; }
        public string added_by { get; set; }
        public string ip_add { get; set; }
    }
    public class AreaExperienceBO
    {
        public int? Id { get; set; }
        public int? FacultyDetailsId { get; set; }
        public int? LanguageId { get; set; }
        public string EmployerName { get; set; }
        public string FromYear { get; set; }
        public string ToYear { get; set; }
        public bool? IsPresent { get; set; }
        public string added_by { get; set; }
        public string ip_add { get; set; }
    }
    public class PublicationResearchDetailsBO
    {
        public int? Id { get; set; }
        public int? FacultyDetailsId { get; set; }
        public int? LanguageId { get; set; }
        public string FromYear { get; set; }
        public string ToYear { get; set; }
        public string Description { get; set; }
        public string added_by { get; set; }
        public string ip_add { get; set; }
    }
    public class FacultyAwardsDetailsBO
    {
        public int? Id { get; set; }
        public int? FacultyDetailsId { get; set; }
        public int? LanguageId { get; set; }
        public string Title { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string AwardsDescription { get; set; }
        public string added_by { get; set; }
        public string ip_add { get; set; }
    }
    public class FacultyServiceDetailsBO
    {
        public int? Id { get; set; }
        public int? FacultyDetailsId { get; set; }
        public int? LanguageId { get; set; }
        public string ServiceName { get; set; }
        public string added_by { get; set; }
        public string ip_add { get; set; }
    }
}
