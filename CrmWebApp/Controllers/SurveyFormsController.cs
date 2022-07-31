using RmosCrmWebApp.Dapper;
using RmosCrmWebApp.Models.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RmosCrmWebApp.Controllers
{
    public class SurveyFormsController : Controller
    {
        DapperManager baseDb = null;
        IDapperTools db = null;
        // GET: SurveyForms

        public SurveyFormsController()
        {
            baseDb = new DapperManager();
            var sirketDb = baseDb.DapperBaseConnect();
            var hotel = sirketDb.GetAll<Crm_Sirket>().Where(x => x.Sirket_Kod == Convert.ToInt32(System.Web.HttpContext.Current.Session["ch"].ToString())).FirstOrDefault();
            db = baseDb.DapperConnect(@"Server=" + hotel.Sirket_Server + ";Database=" + hotel.Sirket_Database + ";User id=" + hotel.Sirket_DB_User + ";password=" + hotel.Sirket_DB_Sifre);
        }
        public ActionResult Index()
        {
            ViewBag.AnketTip = new SelectList(db.GetAll<AnketTipleri>().Where(x => x.Aktiflik == true), "AnketTipID", "AnketTipKod");
            var model = db.GetAll<Anketler>();
            return View(model);
        }

        public ActionResult Delete (int id)
        {
            var anket = db.Get<Anketler>(id);

            foreach (var item in db.GetAll<Anketler>().Where(x => x.AnketID == id))
            {
                db.Delete<Anketler>(item);

            }

            return RedirectToAction("Index", "SurveyForms");
        }

        public ActionResult Create()
        {
            ViewBag.AnketTip = new SelectList(db.GetAll<AnketTipleri>().Where(x => x.Aktiflik == true), "AnketTipID", "AnketTipKod");
            var model = new Anketler();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "AnketID, AnketTipID, AnketKod, Ad")] Anketler anket)
        {
            ViewBag.AnketTip = new SelectList(db.GetAll<AnketTipleri>().Where(x => x.Aktiflik == true), "AnketTipID", "AnketTipKod");

            try
            {
                if (ModelState.IsValid && !db.GetAll<Anketler>().Any(x => x.AnketID == anket.AnketID))
                {

                    db.Insert<Anketler>(anket);

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

            return RedirectToAction("Index", "SurveyForms");
        }
    }
}