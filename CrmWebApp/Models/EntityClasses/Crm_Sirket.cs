using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper.Contrib.Extensions;

namespace RmosCrmWebApp.Models.EntityClasses
{
    [Table("Crm_Sirket")]
    public class Crm_Sirket
    {
        [Key]
        public int       /**/ Sirket_Id               /**/ { get; set; }
        public int       /**/ Sirket_Kod              /**/ { get; set; }
        public string    /**/ Sirket_Ad               /**/ { get; set; }
        public string    /**/ Sirket_Database         /**/ { get; set; }
        public string    /**/ Sirket_Onburo           /**/ { get; set; }
        public bool      /**/ Sirket_G                /**/ { get; set; }
        public string    /**/ Sirket_Sifre            /**/ { get; set; }
        public string    /**/ Sirket_Demirbas         /**/ { get; set; }
        public string    /**/ Sirket_Gecenyil         /**/ { get; set; }
        public string    /**/ Sirket_LinkServer       /**/ { get; set; }
        public string    /**/ Sirket_Server           /**/ { get; set; }
        public string    /**/ Sirket_DB_User          /**/ { get; set; }
        public string    /**/ Sirket_DB_Sifre         /**/ { get; set; }
        public string    /**/ Sirket_Devremulk        /**/ { get; set; }
        public DateTime? /**/ Sirket_UpdateTarih      /**/ { get; set; }
        public string    /**/ Sirket_Grup             /**/ { get; set; }
        public string    /**/ Sirket_MuhLink          /**/ { get; set; }
        public string    /**/ Sirket_MuhServer        /**/ { get; set; }
        public string    /**/ Sirket_MuhUser          /**/ { get; set; }
        public string    /**/ Sirket_MuhPass          /**/ { get; set; }
    }
}