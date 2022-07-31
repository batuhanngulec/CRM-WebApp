using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RmosCrmWebApp.Models.EntityClasses
{
    [Table("Sorular")]
    public class Sorular
    {
        [Key]
        public int SoruID { get; set; }

        public string SoruKod { get; set; }

        //public string Ad { get; set; }

        public int? SiraID { get; set; }

        public bool? Aktiflik { get; set; }

        public int CevapSetID { get; set; }
    }
}