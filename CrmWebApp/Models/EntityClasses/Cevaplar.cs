using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RmosCrmWebApp.Models.EntityClasses
{
    [Table("Cevaplar")]
    public class Cevaplar
    {
        [Key]
        public int CevapID { get; set; }

        public string CevapKod { get; set; }

        //public string Text { get; set; }

        public int? SetSirasi { get; set; }

        public bool? Aktiflik { get; set; }


    }
}