using RmosCrmWebApp.Dapper;
using RmosCrmWebApp.Helpers;
using RmosCrmWebApp.Models.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RmosCrmWebApp.Controllers
{
    
    public class AnswersController : Controller
    {
        DapperManager baseDb = null;
        IDapperTools db = null;

        public AnswersController()
        {
            baseDb = new DapperManager();
            var sirketDb = baseDb.DapperBaseConnect();
            var hotel = sirketDb.GetAll<Crm_Sirket>().Where(x => x.Sirket_Kod == Convert.ToInt32(System.Web.HttpContext.Current.Session["ch"].ToString())).FirstOrDefault();
            db = baseDb.DapperConnect(@"Server=" + hotel.Sirket_Server + ";Database=" + hotel.Sirket_Database + ";User id=" + hotel.Sirket_DB_User + ";password=" + hotel.Sirket_DB_Sifre);
        }
        // GET: Answers
        public ActionResult Index()
        {
            
            ViewBag.DilAdlar = new SelectList(db.GetAll<Diller>().Where(x => x.Aktiflik == true), "DilID", "Ad");

            
            ViewBag.Ceviri = new SelectList(db.GetAll<CevapCevirileri>(),"OrjinalID", "Ad");
            var model = db.GetAll<Cevaplar>();
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.DilAdlar = new SelectList(db.GetAll<Diller>().Where(x => x.Aktiflik == true), "DilID", "Ad");
            var model = new Cevaplar();
            return View(model);
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CevapID,CevapKod,SetSirasi,Aktiflik")] Cevaplar cevaplar, bool? isActive)
        {
            ViewBag.DilAdlar = new SelectList(db.GetAll<Diller>().Where(x => x.Aktiflik == true), "DilID", "Ad");
            try
            {
                if (ModelState.IsValid && !db.GetAll<Cevaplar>().Any(x => x.CevapKod.ToLower().Trim() == cevaplar.CevapKod.ToLower().Trim()))
                {
                    //tüm stringleri trimle
                    var stringProperties = cevaplar.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
                    foreach (var stringProperty in stringProperties)
                    {
                        string currentValue = (string)stringProperty.GetValue(cevaplar, null);
                        stringProperty.SetValue(cevaplar, currentValue.Trim(), null);
                    }

                    cevaplar.Aktiflik = isActive ?? false;
                    db.Insert<Cevaplar>(cevaplar);
                }
                else
                {
                    ViewBag.result = new string[] { "danger", "Girdiğiniz cevap seti zaten mevcut." };
                    return View(cevaplar);
                }
            }
            catch (Exception)
            {
                ViewBag.result = new string[] { "danger", "Hatalı işlem yapıldı." };
                return View(cevaplar);
            }

            return RedirectToAction("Index", "Answers");
        }


        public ActionResult Delete(int id)
        {
            var cevaplar = db.Get<Cevaplar>(id);

            foreach (var item in db.GetAll<Cevaplar>().Where(x => x.CevapID == id))
            {
                db.Delete<Cevaplar>(item);
            }
            
            return RedirectToAction("Index", "Questions");
        }


        public ActionResult Edit(int id)
        {
            var model = db.Get<Cevaplar>(id);    
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "CevapID, CevapKod, SetSirasi, Aktiflik")] Cevaplar cevaplar, bool? isActive)
        {
            try
            {
                if (ModelState.IsValid && !db.GetAll<Cevaplar>().Any(x => /*x.SetSirasi != cevaplar.SetSirasi &&*/ x.CevapID != cevaplar.CevapID))
                {
                    cevaplar.Aktiflik = isActive ?? false;
                    db.Update<Cevaplar>(cevaplar);

                    ViewBag.Result = new string[] { "succes", "Kaydedildi. " };

                }
                else
                {
                    ViewBag.Result = new string[] { "danger", "Girdiğiniz cevap set sırası zaten mevcut.." };
                    return View(cevaplar);

                } 


            }
            catch (Exception)
            {
                ViewBag.Result = new string[] { "danger ", "Hatalı İşlem Yaptınız." };
                return View(cevaplar);
            }
                        

            return View(cevaplar);
        }






    }
}