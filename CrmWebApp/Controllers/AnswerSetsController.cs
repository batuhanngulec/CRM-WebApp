using RmosCrmWebApp.Dapper;
using RmosCrmWebApp.Models.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RmosCrmWebApp.Controllers
{
    public class AnswerSetsController : Controller
    {
        DapperManager baseDb = null;
        IDapperTools db = null;

        public AnswerSetsController()
        {
            baseDb = new DapperManager();
            var sirketDb = baseDb.DapperBaseConnect();
            var hotel = sirketDb.GetAll<Crm_Sirket>().Where(x => x.Sirket_Kod == Convert.ToInt32(System.Web.HttpContext.Current.Session["ch"].ToString())).FirstOrDefault();
            db = baseDb.DapperConnect(@"Server=" + hotel.Sirket_Server + ";Database=" + hotel.Sirket_Database + ";User id=" + hotel.Sirket_DB_User + ";password=" + hotel.Sirket_DB_Sifre);
        }
        // GET: AnswerSets
        public ActionResult Index()
        {
            var model = db.GetAll<CevapSetleri>();
            return View(model);
        }


        public ActionResult Create()
        {
            return View(new CevapSetleri());
        }


        public ActionResult Delete(int id)
        {
            var cevap = db.Get<CevapSetleri>(id);

            foreach (var item in db.GetAll<CevapSetleri>().Where(x => x.CevapSetID == id))
            {
                db.Delete<CevapSetleri>(item);
            }

            return RedirectToAction("Index", "AnswerSets");
        }

        public ActionResult Edit(int id)
        {
            var model = db.Get<CevapSetleri>(id);
            ViewBag.CevapID = db.GetAll<CevapSetleri>().Where(x => x.CevapSetID == id);
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CevapSetID,CevapSetKod,Ad,Aktiflik")] CevapSetleri cevap, bool? isActive)
        {
            try
            {
                if (ModelState.IsValid && !db.GetAll<CevapSetleri>().Any(x => x.CevapSetKod.ToLower().Trim() == cevap.CevapSetKod.ToLower().Trim()))
                {
                    //tüm stringleri trimle
                    var stringProperties = cevap.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
                    foreach (var stringProperty in stringProperties)
                    {
                        string currentValue = (string)stringProperty.GetValue(cevap, null);
                        stringProperty.SetValue(cevap, currentValue.Trim(), null);
                    }

                    cevap.Aktiflik = isActive ?? false;
                    db.Insert<CevapSetleri>(cevap);
                }
                else
                {
                    ViewBag.result = new string[] { "danger", "Girdiğiniz cevap seti zaten mevcut." };
                    return View(cevap);
                }
            }
            catch (Exception)
            {
                ViewBag.result = new string[] { "danger", "Hatalı işlem yapıldı." };
                return View(cevap);
            }

            return RedirectToAction("Index", "AnswerSets");
        }
    }
}