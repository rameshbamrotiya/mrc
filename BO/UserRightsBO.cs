using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class UserRightsBO
    {
        public int roleid { get; set; }
        public int parentid { get; set; }

        public bool CanAdd
        {
            get;
            set;
        }

        public bool CanView
        {
            get;
            set;
        }

        public bool CanUpdate
        {
            get;
            set;
        }

        public bool CanDelete
        {
            get;
            set;
        }

        public long MenuId
        {
            get;
            set;
        }
        public string MenuUrl
        {
            get;
            set;
        }
        public string added_by
        {
            get;
            set;
        }
        public string added_date
        {
            get;
            set;
        }
        public string ipaddress
        {
            get;
            set;
        }

    }
}
