using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RmosCrmWebApp.Models.EntityClasses
{
    [Table("Diller")]
    public class Diller
    {
        [Key]
        public int DilID { get; set; }

        public string DilKod { get; set; }

        public string Ad { get; set; }

        public bool? Aktiflik { get; set; }
    }
}