using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RmosCrmWebApp.Models.EntityClasses
{
    [Table("AnketForm")]
    public class AnketForm
    {
        [Key]
        public int AnketFormID { get; set; }

        public int AnketID { get; set; }

        public int DepartmanID { get; set; }

        public int CevapSetID { get; set; }

        public int SoruID { get; set; }
    }
}