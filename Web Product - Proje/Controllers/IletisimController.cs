using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProduct.Models;

namespace WebProduct.Controllers
{
    public class IletisimController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            ViewBag.iletisimbaslik = db.Sabitlers.Find(1).IletisimBaslik;
            var firmabilgi = db.FirmaBilgis.Find(1);
            return View(firmabilgi);
        }
        public JsonResult MesajGonder(Mesajlar _mesaj)
        {
            _mesaj.Bildirim = true;
            _mesaj.Durum = true;
            _mesaj.Favori = false;
            _mesaj.Tarih = DateTime.Now;
            db.Mesajlars.Add(_mesaj);
            db.SaveChanges();
            return Json(true);
        }
    }
}
