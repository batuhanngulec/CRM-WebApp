using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RmosCrmWebApp.Models.EntityClasses
{
    [Table("CevapCevirileri")]
    public class CevapCevirileri
    {
        [Key]
        public int CeviriID { get; set; }

        public int? OrijinalID { get; set; }

        public int? DilID { get; set; }

        public string Ad { get; set; }
    }
}