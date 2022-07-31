using RmosCrmWebApp.Dapper;
using RmosCrmWebApp.Helpers;
using RmosCrmWebApp.Models.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RmosCrmWebApp.Controllers
{
    public class CountriesController : Controller
    {
        DapperManager baseDb = null;
        IDapperTools db = null;

        public CountriesController()
        {
            baseDb = new DapperManager();
            var sirketDb = baseDb.DapperBaseConnect();
            var hotel = sirketDb.GetAll<Crm_Sirket>().Where(x => x.Sirket_Kod == Convert.ToInt32(System.Web.HttpContext.Current.Session["ch"].ToString())).FirstOrDefault();
            db = baseDb.DapperConnect(@"Server=" + hotel.Sirket_Server + ";Database=" + hotel.Sirket_Database + ";User id=" + hotel.Sirket_DB_User + ";password=" + hotel.Sirket_DB_Sifre);
        }
       
        // GET: Countries
        public ActionResult Index()
        {
            var model = db.GetAll<Ulkeler>();
            return View(model);
        }



        public ActionResult Create()
        {
            return View(new Ulkeler());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UlkeID,UlkeKod,Ad,Aktiflik")] Ulkeler ulke, bool? isActive)
        {
            try
            {
                if (ModelState.IsValid && !db.GetAll<Ulkeler>().Any(x => x.UlkeKod.ToLower().Trim() == ulke.UlkeKod.ToLower().Trim()))
                {
                    //tüm stringleri trimle
                    var stringProperties = ulke.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
                    foreach (var stringProperty in stringProperties)
                    {
                        string currentValue = (string)stringProperty.GetValue(ulke, null);
                        stringProperty.SetValue(ulke, currentValue.Trim(), null);
                    }
                 
                    ulke.Aktiflik = isActive ?? false;
                    db.Insert<Ulkeler>(ulke);
                }
                else
                {
                    ViewBag.result = new string[] { "danger", "Girdiğiniz Ülke Kodu zaten mevcut." };
                    return View(ulke);
                }
            }
            catch (Exception)
            {
                ViewBag.result = new string[] { "danger", "Hatalı işlem." };
                return View(ulke);
            }

            return RedirectToAction("Index", "Countries");
        }



        public ActionResult Edit(int id)
        {
            var model = db.Get<Ulkeler>(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UlkeID,UlkeKod,Ad,Aktiflik")] Ulkeler ulke, bool? isActive)
        {
            try
            {
                if (ModelState.IsValid && !db.GetAll<Ulkeler>().Any(x => x.UlkeKod.ToLower().Trim() == ulke.UlkeKod.ToLower().Trim() && x.UlkeID != ulke.UlkeID))
                {
                    //tüm stringleri trimle
                    var stringProperties = ulke.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
                    foreach (var stringProperty in stringProperties)
                    {
                        string currentValue = (string)stringProperty.GetValue(ulke, null);
                        stringProperty.SetValue(ulke, currentValue.Trim(), null);
                    }

                    ulke.Aktiflik = isActive ?? false;
                    db.Update<Ulkeler>(ulke);
                    ViewBag.result = new string[] { "success", "Kaydedildi." };
                }
                else
                {
                    ViewBag.result = new string[] { "danger", "Girdiğiniz Ülke Kodu zaten mevcut." };
                    return View(ulke);
                }
            }
            catch (Exception)
            {
                ViewBag.result = new string[] { "danger", "Hatalı işlem." };
                return View(ulke);
            }
            return View(ulke);
        }
        public ActionResult Delete(int id)
        {
            var model = db.Get<Ulkeler>(id);
            if (model != null)
            {
                db.Delete<Ulkeler>(model);
            }
            return RedirectToAction("Index", "Countries");
        }
    }
}