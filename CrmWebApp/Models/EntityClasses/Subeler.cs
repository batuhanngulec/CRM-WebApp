using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper.Contrib.Extensions;

namespace RmosCrmWebApp.Models.EntityClasses
{
    [Table("Subeler")]
    public class Subeler
    {
        [Key]
        public int SubeID { get; set; }

        public string SubeKod { get; set; }

        public string SubeAd { get; set; }

        public string SubeFrontLinkServer { get; set; }

        public string SubeFrontServer { get; set; }

        public string SubeFrontDatabase { get; set; }

        public string SubeFrontUser { get; set; }

        public string SubeFrontPassword { get; set; }
        
        //public string Display { get=>SubeKod+"-"+SubeAd;}

        public bool? Aktiflik { get; set; }
    }
}