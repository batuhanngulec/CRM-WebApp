using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RmosCrmWebApp.Models.EntityClasses
{
    [Table("AnketKayit")]
    public class AnketKayit
    {
        [Key]
        public int AnketKayitID { get; set; }

        public int AnketID { get; set; }

        public int SoruID { get; set; }

        public int CevapID { get; set; }
    }
}