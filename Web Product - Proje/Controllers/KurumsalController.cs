using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProduct.Models;

namespace WebProduct.Controllers
{
    public class KurumsalController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Hakkimizda()
        {
            var sayfa = db.Sayfalars.Where(w => w.SayfaBaslik == "Hakkımızda").SingleOrDefault();
            return View(sayfa);
        }

        public ActionResult Gizlilik_Politikasi()
        {
            var sayfa = db.Sayfalars.Where(w => w.SayfaBaslik == "Gizlilik Politikası").SingleOrDefault();
            return View(sayfa);
        }

        public ActionResult Kullanim_Kosullari()
        {
            var sayfa = db.Sayfalars.Where(w => w.SayfaBaslik == "Kullanım Koşulları").SingleOrDefault();
            return View(sayfa);
        }
    }
}
