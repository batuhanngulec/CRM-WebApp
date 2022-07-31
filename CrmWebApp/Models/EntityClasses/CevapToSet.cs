using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RmosCrmWebApp.Models.EntityClasses
{
    [Table("CevapToSet")]
    public class CevapToSet
    {
        [Key]
        public int CevapToSetID { get; set; }

        public int CevapSetID { get; set; }

        public int CevapID { get; set; }
    }
}