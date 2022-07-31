using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RmosCrmWebApp.Models.EntityClasses
{
    [Table("CevapSetleri")]
    public class CevapSetleri
    {
        [Key]
        public int CevapSetID { get; set; }

        public string CevapSetKod { get; set; }

        public string Ad { get; set; }

        public bool? Aktiflik { get; set; }
    }
}