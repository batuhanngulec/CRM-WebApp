using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RmosCrmWebApp.Models.EntityClasses
{
    [Table("Ulkeler")]
    public class Ulkeler
    {
        [Key]
        public int UlkeID { get; set; }

        public string UlkeKod { get; set; }

        public string Ad { get; set; }

        public bool? Aktiflik { get; set; }
    }
}