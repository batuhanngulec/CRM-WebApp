using RmosCrmWebApp.Dapper;
using RmosCrmWebApp.Models.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace RmosCrmWebApp.Controllers
{
    public class SurveyTypesController : Controller
    {
        DapperManager baseDb = null;
        IDapperTools db = null;
        public SurveyTypesController()
        {
            baseDb = new DapperManager();
            var sirketDb = baseDb.DapperBaseConnect();
            var hotel = sirketDb.GetAll<Crm_Sirket>().Where(x => x.Sirket_Kod == Convert.ToInt32(System.Web.HttpContext.Current.Session["ch"].ToString())).FirstOrDefault();
            db = baseDb.DapperConnect(@"Server=" + hotel.Sirket_Server + ";Database=" + hotel.Sirket_Database + ";User id=" + hotel.Sirket_DB_User + ";password=" + hotel.Sirket_DB_Sifre);
        }
        // GET: SurveyTypes
        public ActionResult Index()
        {
            ViewBag.AnketTipID = new SelectList(db.GetAll<AnketTipleri>().Where(x => x.Aktiflik == true), "AnketTipID", "RIH");
            ViewBag.AnketTipAd = new SelectList(db.GetAll<Anketler>(), "AnketID", "Ad");
            var model = db.GetAll<AnketTipleri>();
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.AnketTipID = new SelectList(db.GetAll<AnketTipleri>().Where(x => x.Aktiflik == true), "AnketTipID", "RIH");
            ViewBag.AnketTipAd = new SelectList(db.GetAll<Anketler>(), "AnketID", "Ad");
            return View(new AnketTipleri());
        }

        public ActionResult Delete(int id)
        {
            
            var cevap = db.Get<AnketTipleri>(id);

            foreach (var item in db.GetAll<AnketTipleri>().Where(x => x.AnketTipID == id))
            {
                db.Delete<AnketTipleri>(item);
            }

            return RedirectToAction("Index", "SurveyTypes");
        }

        public ActionResult Edit(int id)
        {
            
            ViewBag.AnketTipID = new SelectList(db.GetAll<AnketTipleri>().Where(x => x.Aktiflik == true), "AnketTipID", "RIH");
            ViewBag.AnketTipAd = new SelectList(db.GetAll<Anketler>(), "AnketID", "Ad");
            var model = db.Get<AnketTipleri>(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AnketTipID,AnketTipKod,Ad,RIH,AnketSirasi,Aktiflik")] AnketTipleri tip, bool? isActive)
        {
            ViewBag.AnketTipID = new SelectList(db.GetAll<AnketTipleri>().Where(x => x.Aktiflik == true), "AnketTipID", "RIH");
            ViewBag.AnketTipAd = new SelectList(db.GetAll<Anketler>(), "AnketID", "Ad");
            try
            {
                if (ModelState.IsValid && !db.GetAll<AnketTipleri>().Any(x => x.AnketTipKod.ToLower() == tip.AnketTipKod.ToLower() && x.AnketTipID != tip.AnketTipID))
                {
                    //tüm stringleri trimle
                    var stringProperties = tip.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
                    foreach (var stringProperty in stringProperties)
                    {
                        string currentValue = (string)stringProperty.GetValue(tip, null);
                        stringProperty.SetValue(tip, currentValue.Trim(), null);
                    }
                    tip.Aktiflik = isActive ?? false;
                    db.Update<AnketTipleri>(tip);
                    ViewBag.result = new string[] { "success", "Kaydedildi." };
                }
                else
                {
                    ViewBag.result = new string[] { "danger", "Girdiğiniz Departman Kodu zaten mevcut." };
                    return View(tip);
                }

            }
            catch (Exception)
            {
                ViewBag.result = new string[] { "danger", "Hatalı işlem." };
                return View(tip);
            }
            return View(tip);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnketTipID,AnketTipID,AnketTipKod,Ad,RIH,Aktiflik")] AnketTipleri tip, bool? isActive)
        {
            ViewBag.AnketTipID = new SelectList(db.GetAll<AnketTipleri>().Where(x => x.Aktiflik == true), "AnketTipID", "RIH");
            ViewBag.AnketTipAd = new SelectList(db.GetAll<Anketler>(), "AnketID", "Ad");
            try
            {
                if (ModelState.IsValid && !db.GetAll<AnketTipleri>().Any(x => x.AnketTipKod == tip.AnketTipKod))
                {
                    ////tüm stringleri trimle
                    //var stringProperties = tip.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
                    //foreach (var stringProperty in stringProperties)
                    //{
                    //    string currentValue = (string)stringProperty.GetValue(tip, null);
                    //    stringProperty.SetValue(tip, currentValue.Trim(), null);
                    //}

                    //tip.Aktiflik = isActive ?? false;
                    db.Insert<AnketTipleri>(tip);
                }
                else
                {
                    ViewBag.result = new string[] { "danger", "Girdiğiniz cevap seti zaten mevcut." };
                    return View(tip);
                }
            }
            catch (Exception)
            {
                ViewBag.result = new string[] { "danger", "Hatalı işlem yapıldı." };
                return View(tip);
            }

            return RedirectToAction("Index", "SurveyTypes");
        }
    }
}