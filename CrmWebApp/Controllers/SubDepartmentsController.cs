using RmosCrmWebApp.Dapper;
using RmosCrmWebApp.Models.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RmosCrmWebApp.Controllers
{
    public class SubDepartmentsController : Controller
    {

        DapperManager baseDb = null;
        IDapperTools db = null;

        public SubDepartmentsController()
        {
            baseDb = new DapperManager();
            var sirketDb = baseDb.DapperBaseConnect();
            var hotel = sirketDb.GetAll<Crm_Sirket>().Where(x => x.Sirket_Kod == Convert.ToInt32(System.Web.HttpContext.Current.Session["ch"].ToString())).FirstOrDefault();
            db = baseDb.DapperConnect(@"Server=" + hotel.Sirket_Server + ";Database=" + hotel.Sirket_Database + ";User id=" + hotel.Sirket_DB_User + ";password=" + hotel.Sirket_DB_Sifre);
        }

        // GET: SubDepartmnets
        public ActionResult Index()
        {
            var model = db.GetAll<AltDepartmanlar>();
            ViewBag.Departments       = new SelectList(db.GetAll<Departmanlar>().Where(x => x.Aktiflik == true), "DepartmanID", "Ad");
            ViewBag.DepartmentsKod    = new SelectList(db.GetAll<Departmanlar>().Where(x => x.Aktiflik == true), "DepartmanID", "DepartmanKod");
            ViewBag.AnketSirasi       = new SelectList(db.GetAll<Departmanlar>().Where(x => x.Aktiflik == true), "DepartmanID", "AnketSirasi");
            return View(model);
        }


        public ActionResult Create()
        {
            var model = new AltDepartmanlar();
            ViewBag.DepartmentID     = new SelectList(db.GetAll<Departmanlar>(), "DepartmanID", "Ad");
            ViewBag.DepartmentsKod   = new SelectList(db.GetAll<Departmanlar>(), "DepartmanID", "DepartmanKod");
            ViewBag.AnketSirasi      = new SelectList(db.GetAll<Departmanlar>(), "DepartmanID", "AnketSirasi");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AltDepartmanID,AltDepartmanKod,DepartmanID,DepartmanKod,Ad,Aktiflik")] AltDepartmanlar dep, bool? isActive)
        {
            if (dep != null && ModelState.IsValid && !db.GetAll<AltDepartmanlar>().Any(x => x.AltDepartmanKod.ToLower().Trim() == dep.AltDepartmanKod.ToLower().Trim()))
            {
                //tüm stringleri trimle
                var stringProperties = dep.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
                foreach (var stringProperty in stringProperties)
                {
                    string currentValue = (string)stringProperty.GetValue(dep, null);
                    if (currentValue != null)
                    {
                    stringProperty.SetValue(dep, currentValue.Trim(), null);
                    }
                }

                dep.Aktiflik = isActive ?? false;
                db.Insert<AltDepartmanlar>(dep);
            }
            else
            {
                ViewBag.result = new string[] { "danger", "Girdiğiniz Alt Departman Kodu zaten mevcut." };
                return View(dep);
            }
            return RedirectToAction("Index", "SubDepartments");
        }


        public ActionResult Edit(int id)
        {
            var model = db.Get<AltDepartmanlar>(id);
            ViewBag.DepartmentID = new SelectList(db.GetAll<Departmanlar>().Where(x => x.Aktiflik == true), "DepartmanID", "Ad");
            ViewBag.DepartmentsKod = new SelectList(db.GetAll<Departmanlar>().Where(x => x.Aktiflik == true), "DepartmanID", "DepartmanKod");
            ViewBag.AnketSirasi = new SelectList(db.GetAll<Departmanlar>(), "DepartmanID", "AnketSirasi");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AltDepartmanID,AltDepartmanKod,DepartmanID,DepartmanKod,Ad,Aktiflik")] AltDepartmanlar dep, bool? isActive)
        {
            if (dep != null && ModelState.IsValid && !db.GetAll<AltDepartmanlar>().Any(x => x.AltDepartmanKod.ToLower().Trim() == dep.AltDepartmanKod.ToLower().Trim() && x.AltDepartmanID != dep.AltDepartmanID))
            {
                //tüm stringleri trimle
                var stringProperties = dep.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
                foreach (var stringProperty in stringProperties)
                {
                    string currentValue = (string)stringProperty.GetValue(dep, null);
                    if (currentValue!= null)
                    {
                    stringProperty.SetValue(dep, currentValue.Trim(), null);
                    }
                }

                dep.Aktiflik = isActive ?? false;
                db.Update<AltDepartmanlar>(dep);
                ViewBag.result = new string[] { "success", "Kaydedildi." };
            }
            else
            {
                ViewBag.result = new string[] { "danger", "Girdiğiniz Alt Departman Kodu zaten mevcut." };
                ViewBag.DepartmentID = new SelectList(db.GetAll<Departmanlar>(), "DepartmanID", "Ad");
                return View(dep);
            }

            ViewBag.DepartmentID = new SelectList(db.GetAll<Departmanlar>().Where(x => x.Aktiflik == true), "DepartmanID", "Ad");
            return View(dep);
        }


        public ActionResult Delete(int id)
        {
            var model = db.Get<AltDepartmanlar>(id);
            if (model != null)
            {
                db.Delete<AltDepartmanlar>(model);
            }
            return RedirectToAction("Index", "SubDepartments");
        }
    }
}