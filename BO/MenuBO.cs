using System;


namespace BO
{
    public class MenuBO
    {
        public bool IsDisabledTranslate { get; set; }

        public string col_menu_name { get; set; }
        public string col_menu_url { get; set; }

        public int col_parent_id { get; set; }
        public int col_menu_rank { get; set; }
        public Boolean enabled { get; set; }
        public string added_by { get; set; }
        public string modified_by { get; set; }
        public string ip_add { get; set; }
        public int user_type_id { get; set; }

        public string HeaderImage { get; set; }
        public string HeaderFullPathImage { get; set; }

        public string templateId { get; set; }
        public string tooltip { get; set; }
        public string Language { get; set; }
        public string MaskingURL { get; set; }

        public string ContentDet { get; set; }
        public int col_menu_level { get; set; }
        public string col_menu_type { get; set; }
        public string col_menu_id { get; set; }
        public int recid { get; set; }


    }
}
