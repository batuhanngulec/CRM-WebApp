using RmosCrmWebApp.Dapper;
using RmosCrmWebApp.Models.EntityClasses;
using RmosCrmWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RmosCrmWebApp.Controllers
{
    public class SurveyRecordsController : Controller
    {

        DapperManager baseDb = null;
        IDapperTools db = null;

        public SurveyRecordsController()
        {
            baseDb = new DapperManager();
            var sirketDb = baseDb.DapperBaseConnect();
            var hotel = sirketDb.GetAll<Crm_Sirket>().Where(x => x.Sirket_Kod == Convert.ToInt32(System.Web.HttpContext.Current.Session["ch"].ToString())).FirstOrDefault();
            db = baseDb.DapperConnect(@"Server=" + hotel.Sirket_Server + ";Database=" + hotel.Sirket_Database + ";User id=" + hotel.Sirket_DB_User + ";password=" + hotel.Sirket_DB_Sifre);
        }

        // GET: SurveyRecords
        public ActionResult StartForm()
        {
            Anketler anket = db.Get<Anketler>(1);
            List<AnketForm> form = db.GetAll<AnketForm>().Where(x=> x.AnketID == anket.AnketID).ToList();
            List<Departmanlar> departmanlar = db.GetAll<Departmanlar>().ToList();
            List<Cevaplar> cevaplar = db.GetAll<Cevaplar>().ToList();
            List<Sorular> sorular = db.GetAll<Sorular>().ToList();
            //ANKETS
            //DEPARTMAN 1
            //SORU  1    CEVAP   CEVAP 
            //SORU  2    CEVAP   CEVAP

            //DEPARTMAN 2
            //SORU  1    CEVAP   CEVAP 
            //SORU  4    CEVAP   CEVAP

            //DEPARTMAN 3
            //SORU      CEVAP   CEVAP 
            //SORU      CEVAP   CEVAP

            return View();
        }
        public ActionResult Index()
        {
            // Asıl viewbag listesi
            ViewBag.AnketAd = new SelectList(db.GetAll<Anketler>(), "AnketID", "Ad");
            ViewBag.SoruKod = new SelectList(db.GetAll<Sorular>(),"SoruID","SoruKod");
            ViewBag.CevapKod = new SelectList(db.GetAll<Cevaplar>(),"CevapID","CevapKod");
            
            // Alttaki viewbag example
            ViewBag.Diller          = new SelectList(db.GetAll<Diller>(), "DilID", "Ad");

            var model = db.GetAll<AnketKayit>();
            return View(model);

        }

        public ActionResult ResepsiyonAnketDoldurmaCreate()
        {
            ViewBag.AnketAd = new SelectList(db.GetAll<Anketler>(), "AnketID", "Ad");
            ViewBag.SoruKod = new SelectList(db.GetAll<Sorular>(), "SoruID", "SoruKod");
            ViewBag.CevapKod = new SelectList(db.GetAll<Cevaplar>(), "CevapID", "CevapKod");


            var model = new AnketKayit();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ResepsiyonAnketDoldurmaCreate([Bind(Include = "AnketKayitID, AnketID, SoruID, CevapID")] AnketKayit anketKayit)
        {
            ViewBag.AnketAd = new SelectList(db.GetAll<Anketler>(), "AnketID", "Ad");
            ViewBag.SoruKod = new SelectList(db.GetAll<Sorular>(), "SoruID", "SoruKod");
            ViewBag.CevapKod = new SelectList(db.GetAll<Cevaplar>(), "CevapID", "CevapKod");

            try
            {
                if (ModelState.IsValid && !db.GetAll<AnketKayit>().Any(x => x.AnketKayitID == anketKayit.AnketKayitID))
                {

                    db.Insert<AnketKayit>(anketKayit);

                }
                else
                {
                    ViewBag.result = new string[] { "danger", "Girdiğiniz Anket Form ID zaten mevcut." };
                    return View(anketKayit);
                }
            }
            catch (Exception)
            {
                ViewBag.result = new string[] { "danger", "Hatalı işlem." };
                return View(anketKayit);
            }

            return RedirectToAction("Index", "SurveyRecords");
        }


        public ActionResult ResepsiyonAnketDoldurmaDelete(int id)
        {
            var anket = db.Get<AnketKayit>(id);

            foreach (var item in db.GetAll<AnketKayit>().Where(x => x.AnketKayitID == id))
            {
                db.Delete<AnketKayit>(item);

            }

            return RedirectToAction("Index", "SurveyRecords");
        }

        //public JsonResult AnketGetir(int p)
        //{
        //    var anketler = (from x in ){ }

        //    return Json(anketler, JsonRequestBehavior.AllowGet);

        //}
    }
}