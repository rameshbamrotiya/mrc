using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OfflineDonationBO
    {
        public int Language { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MoNo { get; set; }
        public int? OD_level_id { get; set; }
        public string ReciptPath { get; set; }
        public Boolean Status { get; set; }
        public string added_by { get; set; }
        public string modified_by { get; set; }
        public string ip_add { get; set; }
        public string PanNo { get; set; }
        public string Amount { get; set; }
        public string Address { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public int ODId { get; set; }
        public int recid { get; set; }
    }
}
