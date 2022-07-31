using RmosCrmWebApp.Dapper;
using RmosCrmWebApp.Helpers;
using RmosCrmWebApp.Models.EntityClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace RmosCrmWebApp.Controllers
{
    public class HomeController : Controller
    {
        DapperManager baseDb = null;
        IDapperTools db = null;

        public HomeController()
        {

            baseDb = new DapperManager();
            var sirketDb    = baseDb.DapperBaseConnect();
            var hotel       = sirketDb.GetAll<Crm_Sirket>().Where(x => x.Sirket_Kod == Convert.ToInt32(System.Web.HttpContext.Current.Session["ch"].ToString())).FirstOrDefault();
            db = baseDb.DapperConnect(@"Server=" + hotel.Sirket_Server + ";Database=" + hotel.Sirket_Database + ";User id=" + hotel.Sirket_DB_User + ";password=" + hotel.Sirket_DB_Sifre);


        }

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.dep                 = db.GetAll<Departmanlar>();                                   
            ViewBag.cvp                 = db.GetAll<Anketler>();            
            ViewBag.anketKayit          = db.GetAll<AnketKayit>();   
            ViewBag.girilmisAnket       = db.GetAll<AnketForm>(); 
            ViewBag.dil                 = db.GetAll<Diller>();              
            return View();
        }


        public ActionResult ChangeLanguage (string selectedlanguage)
        {
            if (selectedlanguage != null)
            {
                Thread.CurrentThread.CurrentCulture     = CultureInfo.CreateSpecificCulture(selectedlanguage);
                Thread.CurrentThread.CurrentUICulture   = new CultureInfo(selectedlanguage);
                var cookie                              = new HttpCookie("Language");
                cookie.Value                            = selectedlanguage;
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}