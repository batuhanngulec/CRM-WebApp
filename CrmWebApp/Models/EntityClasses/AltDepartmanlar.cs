using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper.Contrib.Extensions;

namespace RmosCrmWebApp.Models.EntityClasses
{
    [Table("AltDepartmanlar")]
    public class AltDepartmanlar
    {
        [Key]
        public int AltDepartmanID { get; set; }

        public int? DepartmanID { get; set; }

        public string DepartmanKod { get; set; }

        public string AltDepartmanKod { get; set; }

        public string Ad { get; set; }

        public int? AnketSirasi { get; set; }

        public bool? Aktiflik { get; set; }
    }
}