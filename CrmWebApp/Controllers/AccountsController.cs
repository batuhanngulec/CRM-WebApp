using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RmosCrmWebApp.Dapper;
using RmosCrmWebApp.Helpers;
using RmosCrmWebApp.Models.EntityClasses;

namespace CrmWebApp.Controllers
{
    public class AccountsController : Controller
    {
        DapperManager baseDb;
        IDapperTools db;

        public AccountsController()
        {
            baseDb = new DapperManager();
            db = baseDb.DapperBaseConnect();
        }
        // GET: Accounts
        public ActionResult Login()
        {
            //var model = con.GetAll<Crm_Sirket>();
            var model = new List<Crm_Sirket>();
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(string username, string password, int? hotel_code)
        {
            var user = db.GetAll<Crm_Users>().Where(x => x.User_Kod == username && x.User_Sifre == password).FirstOrDefault();
            if (user != null)
            {
                var hotel = db.GetAll<Crm_Sirket>().Where(x => x.Sirket_Kod == hotel_code).FirstOrDefault();

                Session["ch"] = hotel.Sirket_Kod;

                

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.login_error = "Kullanıcı adı ya da şifre hatalı!";

                var model = new List<Crm_Sirket>();

                return View(model);
            }
        }
        public PartialViewResult LoginHotelList(string username)
        {
            username = username.Trim();
            var user = db.Query<Crm_Users>("SELECT * FROM RmosSirket..Crm_Users WHERE User_Kod='" + username + "'").FirstOrDefault();
            if (user != null)
            {
                var details = db.Query<Crm_Users_Detay>(@"SELECT * FROM RmosSirket..Crm_Users WHERE User_Kod='" + username + "'");
                var hotels = db.Query<string>(@"DECLARE @Sirket Nvarchar(MAX) = '' SELECT @Sirket = @Sirket + Sirket + ',' FROM RmosSirket..Crm_Users_Detay WHERE UserKod = '" + username + "' SELECT substring(@Sirket,1,len(@Sirket)-1) RETURN SELECT * FROM RmosSirket..Crm_Users_Detay WHERE UserKod ='" + username + "'").FirstOrDefault();
                var model = db.Query<Crm_Sirket>(@"SELECT * FROM  RmosSirket..Crm_Sirket WHERE Sirket_Kod in (" + hotels + ")").ToList();
                if (model != null)
                    return PartialView("_LoginHotels", model);
                else
                    return PartialView("_LoginHotels", new List<Crm_Sirket>());
            }
            else
            {
                var model = new List<Crm_Sirket>();
                return PartialView("_LoginHotels", model);
            }
        }
    }
}