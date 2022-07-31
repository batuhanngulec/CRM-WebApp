using RmosCrmWebApp.Dapper;
using RmosCrmWebApp.Helpers;
using RmosCrmWebApp.Models.EntityClasses;
using RmosCrmWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RmosCrmWebApp.Controllers
{
    public class SurveysController : Controller
    {
        DapperManager baseDb = null;
        IDapperTools db = null;

        public SurveysController()
        {
            baseDb = new DapperManager();
            var sirketDb = baseDb.DapperBaseConnect();
            var hotel = sirketDb.GetAll<Crm_Sirket>().Where(x => x.Sirket_Kod == Convert.ToInt32(System.Web.HttpContext.Current.Session["ch"].ToString())).FirstOrDefault();
            db = baseDb.DapperConnect(@"Server=" + hotel.Sirket_Server + ";Database=" + hotel.Sirket_Database + ";User id=" + hotel.Sirket_DB_User + ";password=" + hotel.Sirket_DB_Sifre);
        }
        // GET: Surveys
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Departments = db.GetAll<Departmanlar>();
            ViewBag.Branches = new SelectList(db.GetAll<Subeler>(), "SubeID", "SubeAd");
            ViewBag.SurveyTypes = new SelectList(db.GetAll<AnketTipleri>(), "AnketTipID", "Ad");
            ViewBag.Questions = new SelectList(db.GetAll<Sorular>(), "SoruID", "SoruKod");

            ViewBag.AnswerSets = new SelectList(db.GetAll<CevapSetleri>(), "CevapSetID", "Ad"); // batu yazdı
            return View();
        }

      
        public ActionResult Build()
        {
            ViewBag.Departments = db.GetAll<Departmanlar>();
            ViewBag.Branches = new SelectList(db.GetAll<Subeler>(), "SubeID", "SubeAd");
            ViewBag.SurveyTypes = new SelectList(db.GetAll<AnketTipleri>(), "AnketTipID", "Ad");
            ViewBag.Questions = new SelectList(db.GetAll<Sorular>(), "SoruID", "SoruKod");
            ViewBag.Sets = new SelectList(db.GetAll<CevapSetleri>(), "CevapSetId", "Ad");
            return View();
        }


        public ActionResult BatuhanDeneme()
        {
            //var model = db.Get<AnketForm>(id);
            ViewBag.Anketler      = new SelectList(db.GetAll<Anketler>(), "AnketID", "Ad");
            ViewBag.DepartmanAdi  = new SelectList(db.GetAll<Departmanlar>(), "DepartmanID", "Ad");
            ViewBag.CevapSetKod   = new SelectList(db.GetAll<Sorular>(), "SoruID", "SoruKod");
            ViewBag.SoruKod       = new SelectList(db.GetAll<CevapSetleri>(), "CevapSetID", "Ad");

            var model = db.GetAll<AnketForm>();
            return View(model);
        }

        public ActionResult BatuhanDenemeCreate()
        {
            ViewBag.Anketler        = new SelectList(db.GetAll<Anketler>(), "AnketID", "Ad");
            ViewBag.DepartmanAdi    = new SelectList(db.GetAll<Departmanlar>(), "DepartmanID", "Ad");
            ViewBag.CevapSetKod     = new SelectList(db.GetAll<Sorular>(), "SoruID", "SoruKod");
            ViewBag.SoruKod         = new SelectList(db.GetAll<CevapSetleri>(), "CevapSetID", "Ad");


            var model = new AnketForm();
            return View(model);
        }

        [HttpPost]        
        [ValidateAntiForgeryToken]
        public ActionResult BatuhanDenemeCreate([Bind(Include = "AnketFormID, AnketID, DepartmanID, CevapSetID,SoruID")] AnketForm anket)
        {
            ViewBag.Anketler        = new SelectList(db.GetAll<Anketler>(), "AnketID", "Ad");
            ViewBag.DepartmanAdi    = new SelectList(db.GetAll<Departmanlar>(), "DepartmanID", "Ad");
            ViewBag.CevapSetKod     = new SelectList(db.GetAll<Sorular>(), "SoruID", "SoruKod");
            ViewBag.SoruKod         = new SelectList(db.GetAll<CevapSetleri>(), "CevapSetID", "Ad");

            try
            {
                if (ModelState.IsValid && !db.GetAll<AnketForm>().Any(x => x.AnketFormID == anket.AnketFormID))
                {
                   
                    db.Insert<AnketForm>(anket);
                   
                }
                else
                {
                    ViewBag.result = new string[] { "danger", "Girdiğiniz Anket Form ID zaten mevcut." };
                    return View(anket);
                }
            }
            catch (Exception)
            {
                ViewBag.result = new string[] { "danger", "Hatalı işlem." };
                return View(anket);
            }

            return RedirectToAction("BatuhanDeneme","Surveys");
        }



        
        public ActionResult BatuhanDenemeEdit(int id)
        {
            var model = db.Get<AnketForm>(id);
            ViewBag.Anketler        = new SelectList(db.GetAll<Anketler>(), "AnketID", "Ad");
            ViewBag.DepartmanAdi    = new SelectList(db.GetAll<Departmanlar>(), "DepartmanID", "Ad");
            ViewBag.CevapSetKod     = new SelectList(db.GetAll<Sorular>(), "SoruID", "SoruKod");
            ViewBag.SoruKod         = new SelectList(db.GetAll<CevapSetleri>(), "CevapSetID", "Ad");
            return View(model);

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult BatuhanDenemeEdit([Bind(Include ="AnketFormID, AnketID, DepartmanID, CevapSetID, SoruID")] AnketForm form)
        {
            ViewBag.Anketler        = new SelectList(db.GetAll<Anketler>(), "AnketID", "Ad");
            ViewBag.DepartmanAdi    = new SelectList(db.GetAll<Departmanlar>(), "DepartmanID", "Ad");
            ViewBag.CevapSetKod     = new SelectList(db.GetAll<Sorular>(), "SoruID", "SoruKod");
            ViewBag.SoruKod         = new SelectList(db.GetAll<CevapSetleri>(), "CevapSetID", "Ad");

            try
            {
                if (ModelState.IsValid && !db.GetAll<AnketForm>().Any(x => x.AnketFormID != form.AnketFormID))
                {
                    db.Update<AnketForm>(form);
                    ViewBag.result = new string[] { "success", "Anket Formunuz Kayıt Edilidi." };

                }
                else
                {
                    ViewBag.result = new string[] { "danger", "Hatalı işlem." };
                    return View(form);
                }

            }
            catch (Exception)
            {
                ViewBag.result = new string[] { "danger", "Hatalı İşlem Yaptınız Tekrar Deneyiniz" };
                return View(form);
                                
            }
            return View(form);

        }


        public ActionResult BatuhanDenemeDelete(int id)
        {
            var anket = db.Get<AnketForm>(id);
            foreach (var item in db.GetAll<AnketForm>().Where(x => x.AnketFormID == id))
            {
                db.Delete<AnketForm>(item);   
            }
            return RedirectToAction("BatuhanDeneme", "Surveys");
        }
    }
}