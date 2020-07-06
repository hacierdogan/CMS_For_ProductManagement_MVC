using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebProduct.Models;
using System.Data.Entity;
using System.Net;

namespace WebProduct.Controllers
{
    public class APUrunlerController : Controller
    {
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var urunler = db.Urunlers.OrderByDescending(o=>o.UrunID).ToList();
            return View(urunler);
        }
        public ActionResult UrunEkle()
        {
            ViewBag.KategoriNo = new SelectList(db.Kategorilers.Where(w => w.UstKategori != 0), "KategoriID", "KategoriAdi");
           
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Urunler urun, HttpPostedFileBase Resim1, HttpPostedFileBase Resim2, HttpPostedFileBase Resim3, HttpPostedFileBase Resim4, string YeniUrun, string OneCikar,string Durum)
        {
            try
            {
                if (Resim1 != null)
                {
                    WebImage img1 = new WebImage(Resim1.InputStream);
                    FileInfo fotoinfo1 = new FileInfo(Resim1.FileName);

                    string newfoto1 = Guid.NewGuid().ToString() + fotoinfo1.Extension;
                    img1.Resize(600,800);
                    img1.Save("~/upload/urunler/" + newfoto1);
                    urun.Resim1 = "/upload/urunler/" + newfoto1;
                }
                else
                {
                    ModelState.AddModelError("", "Resim yok.");
                }

                if (Resim2 != null)
                {
                    WebImage img2 = new WebImage(Resim2.InputStream);
                    FileInfo fotoinfo2 = new FileInfo(Resim2.FileName);

                    string newfoto2 = Guid.NewGuid().ToString() + fotoinfo2.Extension;
                    img2.Resize(600, 800);
                    img2.Save("~/upload/urunler/" + newfoto2);
                    urun.Resim2 = "/upload/urunler/" + newfoto2;
                }
                else
                {
                    ModelState.AddModelError("", "Resim yok.");
                }
                if (Resim3 != null)
                {
                    WebImage img3 = new WebImage(Resim3.InputStream);
                    FileInfo fotoinfo3 = new FileInfo(Resim3.FileName);

                    string newfoto3 = Guid.NewGuid().ToString() + fotoinfo3.Extension;
                    img3.Resize(600, 800);
                    img3.Save("~/upload/urunler/" + newfoto3);
                    urun.Resim3 = "/upload/urunler/" + newfoto3;
                }
                else
                {
                    ModelState.AddModelError("", "Resim yok.");
                }
                if (Resim4 != null)
                {
                    WebImage img4 = new WebImage(Resim4.InputStream);
                    FileInfo fotoinfo4 = new FileInfo(Resim4.FileName);

                    string newfoto4 = Guid.NewGuid().ToString() + fotoinfo4.Extension;
                    img4.Resize(600, 800);
                    img4.Save("~/upload/urunler/" + newfoto4);
                    urun.Resim4 = "/upload/urunler/" + newfoto4;
                }
                else
                {
                    ModelState.AddModelError("", "Resim yok.");
                }
                if (YeniUrun=="on")
                {
                    urun.YeniUrun = true;
                }
                else
                {
                    urun.YeniUrun = false;
                }
                if (OneCikar == "on")
                {
                    urun.OneCikar = true;
                }
                else
                {
                    urun.OneCikar = false;
                }
                if (Durum == "on")
                {
                    urun.Durum = true;
                }
                else
                {
                    urun.Durum = false;
                }
                db.Urunlers.Add(urun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(urun);
            }
        }
        public ActionResult Duzenle(string id)
        {
            var urun = db.Urunlers.Where(w => w.UrunURL == id.ToString()).SingleOrDefault();
            ViewBag.KategoriNo = new SelectList(db.Kategorilers.Where(w => w.UstKategori != 0), "KategoriID", "KategoriAdi",urun.KategoriNo);
            
            if (urun != null)
            {
                return View(urun);
            }
            else
            {
                return HttpNotFound();
            }
            
        }
        [HttpPost]
        public ActionResult Duzenle(Urunler gelenurun, HttpPostedFileBase Resim1, HttpPostedFileBase Resim2, HttpPostedFileBase Resim3, HttpPostedFileBase Resim4, string YeniUrun, string OneCikar, string Durum)
        {
            try 
            {
                var urun = db.Urunlers.Where(w => w.UrunID == gelenurun.UrunID).SingleOrDefault();

                if (Resim1 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(urun.Resim1)))
                    {
                        System.IO.File.Delete(Server.MapPath(urun.Resim1));
                    }
                    WebImage img1 = new WebImage(Resim1.InputStream);
                    FileInfo fotoinfo1 = new FileInfo(Resim1.FileName);

                    string newfoto1 = Guid.NewGuid().ToString() + fotoinfo1.Extension;
                    img1.Resize(600, 800);
                    img1.Save("~/upload/urunler/" + newfoto1);
                    urun.Resim1 = "/upload/urunler/" + newfoto1;
                }
                else
                {
                    ModelState.AddModelError("", "Resim yok.");
                }
                if (Resim2 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(urun.Resim2)))
                    {
                        System.IO.File.Delete(Server.MapPath(urun.Resim2));
                    }
                    WebImage img2 = new WebImage(Resim2.InputStream);
                    FileInfo fotoinfo2 = new FileInfo(Resim2.FileName);

                    string newfoto2 = Guid.NewGuid().ToString() + fotoinfo2.Extension;
                    img2.Resize(600, 800);
                    img2.Save("~/upload/urunler/" + newfoto2);
                    urun.Resim2 = "/upload/urunler/" + newfoto2;
                }
                else
                {
                    ModelState.AddModelError("", "Resim yok.");
                }
                if (Resim3 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(urun.Resim3)))
                    {
                        System.IO.File.Delete(Server.MapPath(urun.Resim3));
                    }
                    WebImage img3 = new WebImage(Resim3.InputStream);
                    FileInfo fotoinfo3 = new FileInfo(Resim3.FileName);

                    string newfoto3 = Guid.NewGuid().ToString() + fotoinfo3.Extension;
                    img3.Resize(600, 800);
                    img3.Save("~/upload/urunler/" + newfoto3);
                    urun.Resim3 = "/upload/urunler/" + newfoto3;
                }
                else
                {
                    ModelState.AddModelError("", "Resim yok.");
                }
                if (Resim4 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(urun.Resim4)))
                    {
                        System.IO.File.Delete(Server.MapPath(urun.Resim4));
                    }
                    WebImage img4 = new WebImage(Resim4.InputStream);
                    FileInfo fotoinfo4 = new FileInfo(Resim4.FileName);

                    string newfoto4 = Guid.NewGuid().ToString() + fotoinfo4.Extension;
                    img4.Resize(600, 800);
                    img4.Save("~/upload/urunler/" + newfoto4);
                    urun.Resim4 = "/upload/urunler/" + newfoto4;
                }
                else
                {
                    ModelState.AddModelError("", "Resim yok.");
                }
                if (YeniUrun == "on")
                {
                    urun.YeniUrun = true;
                }
                else
                {
                    urun.YeniUrun = false;
                }
                if (OneCikar == "on")
                {
                    urun.OneCikar = true;
                }
                else
                {
                    urun.OneCikar = false;
                }
                if (Durum == "on")
                {
                    urun.Durum = true;
                }
                else
                {
                    urun.Durum = false;
                }
                urun.UrunAdi = gelenurun.UrunAdi;
                urun.UrunURL = gelenurun.UrunURL;
                urun.KategoriNo = gelenurun.KategoriNo;
                urun.UrunAciklama = gelenurun.UrunAciklama;
                urun.UrunOzellik = gelenurun.UrunOzellik;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return HttpNotFound();
            }
           
        }
        public ActionResult Sil(string id)
        {
            var urun = db.Urunlers.Where(w => w.UrunURL == id.ToString()).SingleOrDefault();
            if (urun != null)
            {
                if (System.IO.File.Exists(Server.MapPath(urun.Resim1)))
                {
                    System.IO.File.Delete(Server.MapPath(urun.Resim1));
                }
                if (System.IO.File.Exists(Server.MapPath(urun.Resim2)))
                {
                    System.IO.File.Delete(Server.MapPath(urun.Resim2));
                }
                if (System.IO.File.Exists(Server.MapPath(urun.Resim3)))
                {
                    System.IO.File.Delete(Server.MapPath(urun.Resim3));
                }
                if (System.IO.File.Exists(Server.MapPath(urun.Resim4)))
                {
                    System.IO.File.Delete(Server.MapPath(urun.Resim4));
                }
                db.Urunlers.Remove(urun);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }

        }
        public ActionResult UrlKontrol(string url)
        {
            var sorgu = db.Urunlers.Where(w => w.UrunURL == url).SingleOrDefault();
            if (sorgu == null) { return Json(true); }
            else { return Json(false); }
        }
    }
}
