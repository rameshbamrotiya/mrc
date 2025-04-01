using System;


namespace BO
{
    public class AcademicsDescriptionMasterBO
    {
        public int? AcademicsId { get; set; }
        public int? Language_id { get; set; }
        public string Title { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string ParamedicalMainDescription { get; set; }
        public string MedicalMainDescription { get; set; }
        public string ParamedicalInnerDescription { get; set; }
        public string MedicalInnerDescription { get; set; }
        public string AlumniVisible { get; set; }
        public string Is_active { get; set; }
        public string added_by { get; set; }
        public string ip_add { get; set; }
    }
}
