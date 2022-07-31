using RmosCrmWebApp.Dapper;
using RmosCrmWebApp.Models.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RmosCrmWebApp.Controllers
{
    public class QuestionsController : Controller
    {
        DapperManager baseDb = null;
        IDapperTools db = null;

        public QuestionsController()
        {
            baseDb = new DapperManager();
            var sirketDb = baseDb.DapperBaseConnect();
            var hotel = sirketDb.GetAll<Crm_Sirket>().Where(x => x.Sirket_Kod == Convert.ToInt32(System.Web.HttpContext.Current.Session["ch"].ToString())).FirstOrDefault();
            db = baseDb.DapperConnect(@"Server=" + hotel.Sirket_Server + ";Database=" + hotel.Sirket_Database + ";User id=" + hotel.Sirket_DB_User + ";password=" + hotel.Sirket_DB_Sifre);
        }
        // GET: Questions
        public ActionResult Index()
        {
            ViewBag.CevapSet = new SelectList(db.GetAll<CevapSetleri>().Where(x => x.Aktiflik == true), "CevapSetID", "Ad");
            var model = db.GetAll<Sorular>();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new Sorular();
            ViewBag.CevapSet = new SelectList(db.GetAll<CevapSetleri>().Where(x => x.Aktiflik == true), "CevapSetID", "Ad");
            ViewBag.Translates = new SelectList(db.GetAll<Diller>().Where(x => x.Aktiflik == true), "DilID", "Ad");
            return View(model);
        }


        public ActionResult Edit(int id)
        {

            var model = db.Get<Sorular>(id);
            ViewBag.CevapSet = new SelectList(db.GetAll<CevapSetleri>().Where(x => x.Aktiflik == true), "CevapSetID", "Ad");
            //
            ViewBag.Translates          = new SelectList(db.GetAll<Diller>().Where(x => x.Aktiflik == true), "DilID", "Ad");
            ViewBag.CeviriID            = db.GetAll<SoruCevirileri>().Where(x => x.OrijinalID == id);

            ViewBag.Dil1                = db.GetAll<Diller>().Where(x => x.DilID == id);
            ViewBag.Dil                 = new SelectList(db.GetAll<Diller>().Where(x => x.DilID == id),"DilID","Ad");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoruID,SoruKod ,SiraID,Aktiflik,CevapSetID")] Sorular soru, bool? isActive)
        {

            try
            {
                if (ModelState.IsValid && !db.GetAll<Sorular>().Any(x => x.SoruKod.ToLower().Trim() == soru.SoruKod.ToLower().Trim() && x.SiraID != soru.SiraID))
                {
                    //tüm stringleri trimle
                    var stringProperties = soru.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
                    foreach (var stringProperty in stringProperties)
                    {
                        string currentValue = (string)stringProperty.GetValue(soru, null);
                        stringProperty.SetValue(soru, currentValue.Trim(), null);
                    }

                    soru.Aktiflik = isActive ?? false;
                    db.Update<Sorular>(soru);
                    ViewBag.result = new string[] { "success", "Kaydedildi." };
                }
                else
                {
                    ViewBag.result = new string[] { "danger", "Girdiğiniz Soru Kodu zaten mevcut." };
                    return View(soru);
                }
            }
            catch (Exception)
            {
                ViewBag.result = new string[] { "danger", "Hatalı işlem." };
                return View(soru);
            }
            return View(soru);
        }


        public ActionResult Delete(int id)
        {
            var soru = db.Get<Sorular>(id);

            foreach (var item in db.GetAll<SoruCevirileri>().Where(x => x.OrijinalID == id))
            {
                db.Delete<SoruCevirileri>(item);
            }
            db.Delete<Sorular>(soru);
            return RedirectToAction("Index", "Questions");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoruID,SoruKod ,SiraID,Aktiflik")] Sorular soru, bool? isActive)
        {
            ViewBag.CevapSet = new SelectList(db.GetAll<CevapSetleri>().Where(x => x.Aktiflik == true), "CevapSetID", "Ad");
            ViewBag.Translates = new SelectList(db.GetAll<Diller>().Where(x => x.Aktiflik == true), "DilID", "Ad");
            //soru.Aktiflik = isActive ?? false;
            //db.Insert<Sorular>(soru);

            try
            {
                if (ModelState.IsValid && !db.GetAll<Sorular>().Any(x => x.SoruKod.ToLower().Trim() == soru.SoruKod.ToLower().Trim()))
                {
                    //tüm stringleri trimle
                    var stringProperties = soru.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
                    foreach (var stringProperty in stringProperties)
                    {
                        string currentValue = (string)stringProperty.GetValue(soru, null);
                        stringProperty.SetValue(soru, currentValue.Trim(), null);
                    }

                    soru.Aktiflik = isActive ?? false;
                    db.Insert<Sorular>(soru);
                }
                else
                {
                    ViewBag.result = new string[] { "danger", "Girdiğiniz cevap seti zaten mevcut." };
                    return View(soru);
                }
            }
            catch (Exception)
            {
                ViewBag.result = new string[] { "danger", "Hatalı işlem yapıldı." };
                return View(soru);
            }

            return RedirectToAction("Index", "Questions");


            
            //foreach (var c in ceviri.Zip(dil, Tuple.Create))
            //{
            //    SoruCevirileri translate = new SoruCevirileri();
            //    translate.OrijinalID = db.GetAll<Sorular>().Where(x => x.SoruKod == soru.SoruKod).FirstOrDefault().SoruID;
            //    translate.DilID = c.Item2;
            //    translate.Ad = c.Item1;
            //    db.Insert<SoruCevirileri>(translate);

            //}


           
        }
        


    }
}