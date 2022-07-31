using RmosCrmWebApp.Models.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RmosCrmWebApp.Models.ViewModels
{
    public class SurveyRecordsViewModel
    {
        public List<AnketForm> AnketForm { get; set; }
        public Anketler Anket { get; set; }
        //public List<Departmanlar> Departmanlar { get; set; }
        public Departmanlar Departmanlar { get; set; }
        public List<Cevaplar> Cevaplar { get; set; }
        public List<Sorular> Sorular { get; set; }
        public List<CevapSetleri> CevapSetleri { get; set; }

        public List<Diller> Diller { get; set; }
    }
}