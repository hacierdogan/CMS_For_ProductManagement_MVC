using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProduct.Models;

namespace WebProduct.Controllers
{
    public class APKategorilerController : Controller
    {
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var kategoriler = db.Kategorilers.OrderBy(o => o.UstKategori).ToList();
            return View(kategoriler);
        }

        public ActionResult KategoriEkle()
        {
            List<SelectListItem> kategoriler = new List<SelectListItem>();
            kategoriler.Add(new SelectListItem { Text = "Ana Kategori", Value = "0" });
            foreach (var item in db.Kategorilers.Where(w => w.UstKategori == 0).ToList())
            {
                kategoriler.Add(new SelectListItem { Text = item.KategoriAdi, Value = item.KategoriID.ToString() });
            }
            ViewBag.UstKategori = kategoriler;
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategoriler yenikategori, string KDurum)
        {
            if (Session["ManagerYetki"].ToString() == "Admin")
            {
                var kategorisorgu = db.Kategorilers.Where(w => w.KategoriAdi == yenikategori.KategoriAdi).SingleOrDefault();
                if (kategorisorgu == null)
                {

                    if (yenikategori != null)
                    {
                        if (KDurum == "on")
                        {
                            yenikategori.Durum = true;
                        }
                        else
                        {
                            yenikategori.Durum = false;
                        }
                        db.Kategorilers.Add(yenikategori);
                        db.SaveChanges();
                    }

                    return RedirectToAction("index", "apkategoriler");
                }
                else
                {
                    ModelState.AddModelError("", "Bu kategori adı zaten mevcut.");
                    return View(yenikategori);
                }
            }
            else
            {
                return HttpNotFound();
            }

        }

        public ActionResult KategoriSil(string id)
        {
            if (Session["ManagerYetki"].ToString() == "Admin")
            {
                var kategori = db.Kategorilers.Where(w => w.KategoriURL == id).SingleOrDefault();
                if (kategori.Urunlers.Count() >= 0)
                {
                    db.Kategorilers.Remove(kategori);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("index", "apkategoriler");
        }

        public ActionResult KategoriDuzenle(string id)
        {
            var kategorisorgu = db.Kategorilers.Where(w => w.KategoriURL == id).SingleOrDefault();
            if (kategorisorgu != null)
            {

                List<SelectListItem> kategoriler = new List<SelectListItem>();
                if (kategorisorgu.Urunlers.Count()==0)
                {
                    kategoriler.Add(new SelectListItem { Text = "Ana Kategori", Value = "0" });
                }
                foreach (var item in db.Kategorilers.Where(w => w.UstKategori == 0).ToList())
                {
                    kategoriler.Add(new SelectListItem { Text = item.KategoriAdi, Value = item.KategoriID.ToString(), Selected = (item.KategoriID == kategorisorgu.UstKategori ? true : false) });
                }
                ViewBag.UstKategori = kategoriler;
                return View(kategorisorgu);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult KategoriDuzenle(Kategoriler duzenlenenkategori, string KDurum)
        {
            if (Session["ManagerYetki"].ToString() == "Admin")
            {
                var kategori = db.Kategorilers.Find(duzenlenenkategori.KategoriID);
                if (kategori != null)
                {
                    if (KDurum == "on")
                    {
                        kategori.Durum = true;
                    }
                    else
                    {
                        kategori.Durum = false;
                    }
                    kategori.KategoriAdi = duzenlenenkategori.KategoriAdi;
                    kategori.KategoriURL = duzenlenenkategori.KategoriURL;
                    kategori.Sira = duzenlenenkategori.Sira;
                    kategori.UstKategori = duzenlenenkategori.UstKategori;
                    db.SaveChanges();
                    return RedirectToAction("index", "apkategoriler");
                }
                else
                {
                    ModelState.AddModelError("", "Kategori bulunamadı");
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
           
        }

        public ActionResult UrlKontrol(string url)
        {
            var sorgu = db.Kategorilers.Where(w => w.KategoriURL == url).SingleOrDefault();
            if (sorgu == null) { return Json(true); }
            else { return Json(false); }
        }

    }
}
