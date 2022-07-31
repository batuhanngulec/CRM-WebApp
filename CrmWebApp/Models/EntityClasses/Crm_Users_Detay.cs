using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper.Contrib.Extensions;

namespace RmosCrmWebApp.Models.EntityClasses
{
    [Table("Crm_Users_Detay")]
    public class Crm_Users_Detay
    {
        [Key]
        public int       /**/ Id                   /**/ { get; set; }
        public string    /**/ UserKod              /**/ { get; set; }
        public string    /**/ Sirket               /**/ { get; set; }
        public string    /**/ Sube                 /**/ { get; set; }
        public string    /**/ Sube_Secili          /**/ { get; set; }
        public string    /**/ Sube_Rapor           /**/ { get; set; }
        public string    /**/ Departman            /**/ { get; set; }
        public string    /**/ Departman_Rapor      /**/ { get; set; }
    }
}