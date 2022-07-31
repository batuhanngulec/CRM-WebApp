using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RmosCrmWebApp.Models.EntityClasses
{
    [Table("AnketTipleri")]
    public class AnketTipleri
    {
        [Key]
        public int AnketTipID { get; set; }

        public string AnketTipKod { get; set; }

        public string Ad { get; set; }

        public string RIH { get; set; }

        public bool? Aktiflik { get; set; }
    }
}