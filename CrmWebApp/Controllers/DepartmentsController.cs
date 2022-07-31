using RmosCrmWebApp.Dapper;
using RmosCrmWebApp.Models.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RmosCrmWebApp.Controllers
{
    public class DepartmentsController : Controller
    {
        DapperManager baseDb = null;
        IDapperTools db = null;
        public DepartmentsController()
        {
            baseDb = new DapperManager();
            var sirketDb = baseDb.DapperBaseConnect();
            var hotel = sirketDb.GetAll<Crm_Sirket>().Where(x => x.Sirket_Kod == Convert.ToInt32(System.Web.HttpContext.Current.Session["ch"].ToString())).FirstOrDefault();
            db = baseDb.DapperConnect(@"Server=" + hotel.Sirket_Server + ";Database=" + hotel.Sirket_Database + ";User id=" + hotel.Sirket_DB_User + ";password=" + hotel.Sirket_DB_Sifre);

        }


        // GET: Departments
        public ActionResult Index()
        {
            ViewBag.SubeID = new SelectList(db.GetAll<Subeler>().Where(x => x.Aktiflik == true), "SubeID", "SubeAd");
            var model = db.GetAll<Departmanlar>();
            return View(model);
        }



        public ActionResult Create()
        {
            ViewBag.SubeID = new SelectList(db.GetAll<Subeler>().Where(x => x.Aktiflik == true), "SubeID", "SubeAd");

            var model = new Departmanlar();
            return View(model);
        }


        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmanID,DepartmanKod,Ad,SubeID,AnketSirasi,Aktiflik")] Departmanlar dep, bool? isActive)
        {
            ViewBag.SubeID = new SelectList(db.GetAll<Subeler>().Where(x => x.Aktiflik == true), "SubeID", "SubeAd");
            try
            {
                if (ModelState.IsValid && !db.GetAll<Departmanlar>().Any(x => x.DepartmanKod.ToLower() == dep.DepartmanKod.ToLower()))
                {
                    //tüm stringleri trimle
                    var stringProperties = dep.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
                    foreach (var stringProperty in stringProperties)
                    {
                        string currentValue = (string)stringProperty.GetValue(dep, null);
                        stringProperty.SetValue(dep, currentValue.Trim(), null);
                    }
                    dep.Aktiflik = isActive ?? false;
                    db.Insert<Departmanlar>(dep);
                }
                else
                {
                    ViewBag.result = new string[] { "danger", "Girdiğiniz Departman Kodu zaten mevcut." };
                    return View(dep);
                }
            }
            catch (Exception)
            {
                ViewBag.result = new string[] { "danger", "Hatalı işlem." };
                return View(dep);
            }
            return RedirectToAction("Index", "Departments");
        }




        public ActionResult Edit(int id)
        {
            ViewBag.SubeID = new SelectList(db.GetAll<Subeler>().Where(x => x.Aktiflik == true), "SubeID", "SubeAd");
            var model = db.Get<Departmanlar>(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmanID,DepartmanKod,Ad,SubeID,AnketSirasi,Aktiflik")] Departmanlar dep, bool? isActive)
        {
            ViewBag.SubeID = new SelectList(db.GetAll<Subeler>().Where(x => x.Aktiflik == true), "SubeID", "SubeAd");
            try
            {
                if (ModelState.IsValid && !db.GetAll<Departmanlar>().Any(x => x.DepartmanKod.ToLower() == dep.DepartmanKod.ToLower() && x.DepartmanID != dep.DepartmanID))
                {
                    //tüm stringleri trimle
                    var stringProperties = dep.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
                    foreach (var stringProperty in stringProperties)
                    {
                        string currentValue = (string)stringProperty.GetValue(dep, null);
                        stringProperty.SetValue(dep, currentValue.Trim(), null);
                    }
                    dep.Aktiflik = isActive ?? false;
                    db.Update<Departmanlar>(dep);
                    ViewBag.result = new string[] { "success", "Kaydedildi." };
                }
                else
                {
                    ViewBag.result = new string[] { "danger", "Girdiğiniz Departman Kodu zaten mevcut." };
                    return View(dep);
                }

            }
            catch (Exception)
            {
                ViewBag.result = new string[] { "danger", "Hatalı işlem." };
                return View(dep);
            }
            return View(dep);
        }



        public ActionResult Delete(int id)
        {
            var model = db.Get<Departmanlar>(id);

            if (model != null)
            {
                if (db.GetAll<AltDepartmanlar>().Any(x=> x.DepartmanID == model.DepartmanID))
                {
                    ViewBag.deleteresult = "Silinemez! Bu departmana bağlı alt departmanlar var.";
                    ViewBag.SubeID = new SelectList(db.GetAll<Subeler>().Where(x => x.Aktiflik == true), "SubeID", "SubeAd");
                    return View("Index",db.GetAll<Departmanlar>());        
                }
                else
                {
                db.Delete<Departmanlar>(model);
                }
            }

            return RedirectToAction("Index", "Departments");
        }
    }
}