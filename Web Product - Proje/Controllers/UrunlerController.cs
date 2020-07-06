using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProduct.Models;

namespace WebProduct.Controllers
{
    public class UrunlerController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SolMenu()
        {
            ViewBag.solmenubaslik = db.Sabitlers.Find(1).SolMenuBaslik;
            var kategoriler = db.Kategorilers.Where(w => w.Durum == true).OrderBy(o => o.Sira).ToList();
            return View(kategoriler);
        }
        public ActionResult UrunListesi(string id, int page = 1)
        {
            // PagedList Eklenecek
            var urunler = db.Urunlers.Where(w => w.Kategoriler.KategoriURL == id && w.Durum == true).ToList();
            ViewBag.kategoriadi = db.Kategorilers.Where(w=>w.KategoriURL==id).FirstOrDefault().KategoriAdi;
            return View(urunler);
        }
        public ActionResult UrunDetay(string id)
        {
            string[] url = id.Split('/');
            if (url.Length>2)
            {
                return RedirectToAction("Hata", "Home");
            }
            else
            {
                var katurl = url[0].ToString();
                var urunurl = url[1].ToString();
                var urun = db.Urunlers.Where(w => w.Kategoriler.KategoriURL == katurl && w.UrunURL == urunurl&&w.Durum==true).SingleOrDefault();
                ViewBag.kategoriadi = urun.Kategoriler.KategoriAdi;
                ViewBag.BenzerBaslik = db.Sabitlers.Find(1).UrunOwlBaslik;
                return View(urun);
            }
        }
        public ActionResult BenzerUrunler(string id,int urunno)
        {
            var urunler = db.Urunlers.Where(w => w.Kategoriler.KategoriURL == id && w.Durum == true&&w.UrunID!=urunno).Take(8);
            return View(urunler);
        }
    }
}