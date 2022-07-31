using RmosCrmWebApp.Models.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper.Contrib.Extensions;

namespace RmosCrmWebApp.Models.ViewModels
{
    
    public class batuhan_anket_liste
    {
        
        public int AnketFormID { get; set; }
        public string AnketAdi { get; set; }
        public string DepartmanAdi { get; set; }
        public string CevapSetKod { get; set; }
        public string SoruKod { get; set; }
    }
}