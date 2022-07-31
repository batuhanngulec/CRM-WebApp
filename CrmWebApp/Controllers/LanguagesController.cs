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
    public class LanguagesController : Controller
    {
        DapperManager baseDb = null;
        IDapperTools db = null;

        public LanguagesController()
        {
            baseDb = new DapperManager();
            var sirketDb = baseDb.DapperBaseConnect();
            var hotel = sirketDb.GetAll<Crm_Sirket>().Where(x => x.Sirket_Kod == Convert.ToInt32(System.Web.HttpContext.Current.Session["ch"].ToString())).FirstOrDefault();
            db = baseDb.DapperConnect(@"Server=" + hotel.Sirket_Server + ";Database=" + hotel.Sirket_Database + ";User id=" + hotel.Sirket_DB_User + ";password=" + hotel.Sirket_DB_Sifre);
        }
        // GET: Languages
        public ActionResult Index()
        {
            var model = db.GetAll<Diller>();
            return View(model);
        }


        public ActionResult Create(string code, string name, bool? isActive)
        {
            code = code.Trim();
            name = name.Trim();//Sonlardaki ve başlardaki fazlalık boşluklar temizlendi
            if (!db.GetAll<Diller>().Any(x => x.DilKod.ToLower() == code.ToLower() || x.Ad.ToLower() == name.ToLower()) && code != "" && name != "")
            {
                Diller dil = new Diller()
                {
                    DilKod = code,
                    Ad = name,
                    Aktiflik = isActive ?? false
                };
                db.Insert<Diller>(dil);
                Response.Write("<script>alert('İşlem başarılı.');</script>");
            }
            else
            {
                Response.Write("<script>alert('İşlem başarısız.Var olan dil kodu ya da isim kullanılamaz. Boş giriş yapılamaz.');</script>");
                //Not yet.
            }
            return View("Index", db.GetAll<Diller>());
        }

        public JsonResult Delete(int? id)
        {
            db.Delete<Diller>(db.Get<Diller>(id));
            return Json("tm");
        }

        public ActionResult Edit(int id, string code, string name, bool? isActive)
        {
            var edit = db.Get<Diller>(id);
            edit.DilKod = code;
            edit.Ad = name;
            edit.Aktiflik = isActive;
            db.Update<Diller>(edit);
            return RedirectToAction("Index", "Languages");
        }

        public JsonResult Read(string code)
        {
            var data = db.GetAll<Diller>().Where(x => x.DilKod == code).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}