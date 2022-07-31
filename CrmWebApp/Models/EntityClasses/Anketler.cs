using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RmosCrmWebApp.Models.EntityClasses
{
    [Table("Anketler")]
    public class Anketler
    {
        [Key]
        public int AnketID { get; set; }

        public int AnketTipID { get; set; }

        public string AnketKod { get; set; }

        public string Ad { get; set; }
    }
}