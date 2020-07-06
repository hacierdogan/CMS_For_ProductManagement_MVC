using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebProduct.Models;

namespace WebProduct.Controllers
{
    public class APAyarlarController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CeoAyar()
        {
            return View(db.Ceos.Where(w => w.CeoID == 1).SingleOrDefault());
        }
        [HttpPost]
        public ActionResult CeoAyar(Ceo yeniceo)
        {
            var ceo = db.Ceos.Where(w => w.CeoID == 1).SingleOrDefault();
            ceo.SiteBaslik = yeniceo.SiteBaslik;
            ceo.SiteAciklama = yeniceo.SiteAciklama;
            ceo.SiteSahip = yeniceo.SiteSahip;
            ceo.SiteYonetici = yeniceo.SiteYonetici;
            ceo.SiteKurulus = yeniceo.SiteKurulus;
            ceo.SiteKeys = yeniceo.SiteKeys;
            ceo.GoogleAnalytics = yeniceo.GoogleAnalytics;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FirmaAyar()
        {
            return View(db.FirmaBilgis.Where(w => w.FirmaID == 1).SingleOrDefault());
        }
        [HttpPost]
        public ActionResult FirmaAyar(FirmaBilgi yenifirma, HttpPostedFileBase ResimLogo)
        {
            var firma = db.FirmaBilgis.Where(w => w.FirmaID == 1).SingleOrDefault();
            if (ResimLogo != null)
            {
                if (System.IO.File.Exists(Server.MapPath(firma.FirmaLogo)))
                {
                    System.IO.File.Delete(Server.MapPath(firma.FirmaLogo));
                }
                WebImage img1 = new WebImage(ResimLogo.InputStream);
                FileInfo fotoinfo1 = new FileInfo(ResimLogo.FileName);

                string newfoto1 = Guid.NewGuid().ToString() + fotoinfo1.Extension;
                img1.Save("~/upload/" + newfoto1);
                firma.FirmaLogo = "/upload/" + newfoto1;
            }
            firma.FirmaAdi = yenifirma.FirmaAdi;
            firma.FirmaMail = yenifirma.FirmaMail;
            firma.FirmaTel = yenifirma.FirmaTel;
            firma.FirmaAdres = yenifirma.FirmaAdres;
            firma.FirmaKonum = yenifirma.FirmaKonum;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PopupAyar()
        {
            return View(db.Popups.Where(w => w.PopupID == 1).SingleOrDefault());
        }
        [HttpPost]
        public ActionResult PopupAyar(Popup gelenpopup, DateTime start, DateTime end, HttpPostedFileBase PopupResim, string RDurum, string PDurum)
        {
            var popup = db.Popups.Where(w => w.PopupID == 1).SingleOrDefault();
            if (popup != null)
            {
                if (PopupResim != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(popup.Resim)))
                    {
                        System.IO.File.Delete(Server.MapPath(popup.Resim));
                    }
                    WebImage img1 = new WebImage(PopupResim.InputStream);
                    FileInfo fotoinfo1 = new FileInfo(PopupResim.FileName);

                    string newfoto1 = Guid.NewGuid().ToString() + fotoinfo1.Extension;
                    img1.Save("~/upload/" + newfoto1);
                    popup.Resim = "/upload/" + newfoto1;
                }
                if (RDurum == "on")
                {
                    popup.ResimDurum = true;
                }
                else
                {
                    popup.ResimDurum = false;
                }
                popup.Baslik = gelenpopup.Baslik;
                popup.Icerik = gelenpopup.Icerik;
                popup.Baslangic = start;
                popup.Bitis = end;
               
                if (PDurum == "on")
                {
                    popup.Durum = true;
                }
                else
                {
                    popup.Durum = false;
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult SabitlerAyar()
        {
            return View(db.Sabitlers.Where(w => w.SabitID == 1).SingleOrDefault());
        }
        [HttpPost]
        public ActionResult SabitlerAyar(Sabitler gelensabit, HttpPostedFileBase HomeResim)
        {
            var sabitler = db.Sabitlers.Where(w => w.SabitID == 1).SingleOrDefault();

            if (HomeResim != null)
            {
                if (System.IO.File.Exists(Server.MapPath(sabitler.HomeRes)))
                {
                    System.IO.File.Delete(Server.MapPath(sabitler.HomeRes));
                }
                WebImage img1 = new WebImage(HomeResim.InputStream);
                FileInfo fotoinfo1 = new FileInfo(HomeResim.FileName);

                string newfoto1 = Guid.NewGuid().ToString() + fotoinfo1.Extension;
                img1.Save("~/upload/" + newfoto1);
                sabitler.HomeRes = "/upload/" + newfoto1;
            }

            sabitler.AramaPlaceHolder = gelensabit.AramaPlaceHolder;
            sabitler.AramaButton = gelensabit.AramaButton;
            sabitler.HomeResBaslik = gelensabit.HomeResBaslik;
            sabitler.HomeResURL = gelensabit.HomeResURL;
            sabitler.HomeVitrinBaslik = gelensabit.HomeVitrinBaslik;
            sabitler.SolMenuBaslik = gelensabit.SolMenuBaslik;
            sabitler.UrunOwlBaslik = gelensabit.UrunOwlBaslik;
            sabitler.AramaSonucBaslik = gelensabit.AramaSonucBaslik;
            sabitler.IletisimBaslik = gelensabit.IletisimBaslik;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult HesapAyar()
        {
            return View(db.Hesaps.Where(w => w.HesapID == 1).SingleOrDefault());
        }
        [HttpPost]
        public ActionResult HesapAyar(Hesap gelenhesap, string parola1, string parola2)
        {
            var hesap = db.Hesaps.Where(w => w.HesapID == 1).SingleOrDefault();
            if (hesap != null)
            {
                hesap.AdSoyad = gelenhesap.AdSoyad;
                hesap.Eposta = gelenhesap.Eposta;
                if (gelenhesap.Parola != null && gelenhesap.Parola == hesap.Parola)
                {
                    if (parola1 == parola2)
                    {
                        hesap.Parola = parola1;
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Hatalı parola girdiniz. Parolanızı hatırlamıyorsanız hesabınızdan çıkış yaparak şifrenizi yenileyebilirsiniz.");
                    return View(hesap);
                }

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult SosyalMedya()
        {
            return View(db.SosyalMedyas.OrderBy(o => o.MedyaSirasi).ToList());
        }
        public ActionResult SosyalMedyaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SosyalMedyaEkle(int MedyaID, string MedyaAdi, string MedyaClass, string MedyaUrl, int MedyaSirasi)
        {
            if (MedyaID == 0)
            {
                if (MedyaAdi != "" || MedyaClass != "" || MedyaUrl != "")
                {
                    SosyalMedya sm = new SosyalMedya();
                    sm.MedyaAdi = MedyaAdi;
                    sm.MedyaClass = MedyaClass;
                    sm.MedyaUrl = MedyaUrl;
                    sm.MedyaSirasi = MedyaSirasi;
                    db.SosyalMedyas.Add(sm);
                    db.SaveChanges();
                    return RedirectToAction("SosyalMedya");
                }
                else
                {
                    return RedirectToAction("SosyalMedya");
                }
            }
            else
            {
                var medya = db.SosyalMedyas.Where(w => w.MedyaID == MedyaID).SingleOrDefault();
                if (medya != null)
                {
                    medya.MedyaAdi = MedyaAdi;
                    medya.MedyaClass = MedyaClass;
                    medya.MedyaUrl = MedyaUrl;
                    medya.MedyaSirasi = MedyaSirasi;
                    db.SaveChanges();
                }
                return RedirectToAction("SosyalMedya");
            }

        }
        public ActionResult SosyalMedyaSil(int id)
        {
            var sm = db.SosyalMedyas.Where(w => w.MedyaID == id).SingleOrDefault();
            if (sm != null)
            {
                db.SosyalMedyas.Remove(sm);
                db.SaveChanges();
            }
            return RedirectToAction("SosyalMedya");
        }
        public ActionResult SMGuncelle(int id)
        {
            var sm = db.SosyalMedyas.Where(w => w.MedyaID == id).SingleOrDefault();
            return Json(sm);
        }
        public ActionResult SliderAyar()
        {
            return View(db.HomeSliders.ToList());
        }
        public ActionResult SliderEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SliderEkle(HttpPostedFileBase SliderResim)
        {
            if (SliderResim != null)
            {
                HomeSlider sr = new HomeSlider();
                WebImage img1 = new WebImage(SliderResim.InputStream);
                FileInfo fotoinfo1 = new FileInfo(SliderResim.FileName);

                string newfoto1 = Guid.NewGuid().ToString() + fotoinfo1.Extension;
                img1.Save("~/upload/slider/" + newfoto1);
                sr.ResimURL = "/upload/slider/" + newfoto1;
                db.HomeSliders.Add(sr);
                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Resim ekleyin.");
            }
            return RedirectToAction("SliderAyar");
        }
        public ActionResult SliderSil(int id)
        {
            var sr = db.HomeSliders.Where(w => w.SliderID == id).SingleOrDefault();
            if (sr != null)
            {
                if (System.IO.File.Exists(Server.MapPath(sr.ResimURL)))
                {
                    System.IO.File.Delete(Server.MapPath(sr.ResimURL));
                }
                db.HomeSliders.Remove(sr);
                db.SaveChanges();
            }
            return RedirectToAction("SliderAyar");
        }
        public ActionResult SayfaAyar()
        {
            return View(db.Sayfalars.ToList());
        }
        public ActionResult SayfaDuzenle(string id)
        {
            return View(db.Sayfalars.Where(w=>w.SayfaBaslik==id).SingleOrDefault());
        }
        [HttpPost]
        public ActionResult SayfaDuzenle(Sayfalar gelensayfa)
        {
            var sayfa = db.Sayfalars.Where(w => w.SayfaID == gelensayfa.SayfaID).SingleOrDefault();
            if (sayfa!=null)
            {
                sayfa.SayfaIcerik = gelensayfa.SayfaIcerik;
                db.SaveChanges();
            }
            return RedirectToAction("SayfaAyar");
        }
    }
}
