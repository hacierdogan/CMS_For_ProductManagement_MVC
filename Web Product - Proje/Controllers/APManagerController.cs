using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebProduct.Models;

namespace WebProduct.Controllers
{
    public class APManagerController : Controller
    {
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            ViewBag.Anakategoriadet = db.Kategorilers.Where(w => w.UstKategori == 0).Count();
            ViewBag.Altkategoriadet = db.Kategorilers.Where(w => w.UstKategori != 0).Count();
            ViewBag.Urunadet = db.Urunlers.Count();
            ViewBag.Onecikanlaradet = db.Urunlers.Where(w => w.OneCikar == true).Count();
            return View(db.Hesaps.Where(w => w.HesapID == 1).SingleOrDefault());
        }
        public ActionResult AdminGiris()
        {
            //beni hatirla onceden isaretlenmisse
            if (Request.Cookies["managerlogincerez"] != null)
            {
                HttpCookie kayitlicerez = Request.Cookies["managerlogincerez"];
                Session["ManagerAdmin"] = kayitlicerez.Values["CerezAdsoyad"];
                Session["ManagerYetki"] = kayitlicerez.Values["CerezYetki"];
                return RedirectToAction("Index", "APManager");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult AdminGiris(Hesap user, string benihatirla)
        {
            var login = db.Hesaps.Where(w => w.Eposta == user.Eposta && w.Parola == user.Parola).SingleOrDefault();
            if (login != null)
            {
                //beni hatirla yeni isaretlenmisse
                if (benihatirla == "on")
                {
                    HttpCookie adminpanelcerez = new HttpCookie("ManagerLoginCerez");
                    adminpanelcerez.Values.Add("CerezAdsoyad", login.AdSoyad);
                    adminpanelcerez.Values.Add("CerezYetki", login.Yetki);

                    adminpanelcerez.Expires = DateTime.Now.AddDays(30);//30gün boyunca hatirla
                    Response.Cookies.Add(adminpanelcerez);
                }
                Session["ManagerAdmin"] = login.AdSoyad.ToString();
                Session["ManagerYetki"] = login.Yetki;

                return RedirectToAction("Index", "APManager");
            }
            else
            {
                ViewBag.LoginHata = "Giriş bilgileriniz hatalı..";
                return View();
            }

        }
        public ActionResult AdminCikis()
        {
            Session["ManagerAdmin"] = null;
            Session["ManagerYetki"] = null;
            Session.Abandon();//tüm sessionları sonlandır

            if (Request.Cookies["ManagerLoginCerez"] != null)
            {
                Response.Cookies["ManagerLoginCerez"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult AdminParolaYenile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminParolaYenile(Hesap user)
        {
            var sorgu = (from i in db.Hesaps where i.Eposta.Equals(user.Eposta) select i).SingleOrDefault(); 

            if (sorgu != null)
            {
                Guid randomkey = Guid.NewGuid();//32 karakterli randomkodu üret

                var apyenisifre = randomkey.ToString().Substring(0, 5);
                sorgu.Parola = apyenisifre;
                db.SaveChanges();

                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.To.Add(sorgu.Eposta);
                mail.From = new MailAddress("site-mail@gmail.com", "WebSite Yönetim Paneli", System.Text.Encoding.UTF8);
                mail.Subject = "Şifre Güncelleme Talebi";
                mail.Body = "Merhaba <b>" + sorgu.AdSoyad + "</b>,<br><br> Şifre Yenileme Talebiniz Alınmıştır.<br> Panel Giriş kodunuz: <b>" + apyenisifre + "</b> <br><br>Aşağıdaki linke tıklayarak yukarıdaki kod ile yönetim panelinize giriş yapabilir ve şifrenizi güncelleyebilirsiniz. </br><br> <a style='text-decoration:none;' href='http://localhost:49684/giris' target='_blank'>Yönetim paneline gitmek için tıklayınız..</a> "; //randomkeyimizi 5 karatere düşdük
                mail.IsBodyHtml = true;
                SmtpClient smp = new SmtpClient();
                smp.Credentials = new NetworkCredential("site-mail@gmail.com", "site-mail-sifre");
                smp.Port = 587;
                smp.Host = "smtp.gmail.com";
                smp.EnableSsl = true;
                smp.Send(mail);

                ViewBag.success = "Şifre yenileme talebiniz eposta adresinize gönderilmiştir.";
                return View();
            }
            else
            {
                ViewBag.uyari = "Eposta adresi bulunamadı!";
                return View(user);
            }

        }
        public ActionResult VeriAnaliz()
        {
            //Panel Dashboard
            GrafikVeri veri = new GrafikVeri();
            var anakategoriler = db.Kategorilers.ToList();
            foreach (var anakatitem in anakategoriler.Where(w => w.UstKategori == 0))
            {
                veri.AnakatListe += anakatitem.KategoriAdi + ",";
                veri.AltKatAdetListe += db.Kategorilers.Where(w => w.UstKategori == anakatitem.KategoriID).Count() + ",";
                int adet = 0;
                foreach (var item in db.Kategorilers.Where(w => w.UstKategori == anakatitem.KategoriID).ToList())
                {
                    adet += item.Urunlers.Count;
                }
                veri.UrunAdetListe += adet.ToString() + ",";
            }
            veri.AnakatListe = veri.AnakatListe.Substring(0, veri.AnakatListe.Length - 1);
            veri.AltKatAdetListe = veri.AltKatAdetListe.Substring(0, veri.AltKatAdetListe.Length - 1);
            veri.UrunAdetListe = veri.UrunAdetListe.Substring(0, veri.UrunAdetListe.Length - 1);
            return Json(veri);
        }
        public ActionResult AdminMenu()
        {
            return View();
        }
        public ActionResult MesajBildirim()
        {
            return View();
        }
        public ActionResult Mesajlar(string id)
        {
            if (id == "favorimesajlar")
            {
                var favmesajlar = db.Mesajlars.Where(w => w.Durum == true && w.Favori == true).OrderByDescending(o=>o.MesajID).ToList();
                ViewBag.mesajpagebaslik = "Favori Mesajlar";
                return View(favmesajlar);
            }
            else if (id == "silinenmesajlar")
            {
                var silmesajlar = db.Mesajlars.Where(w => w.Durum == false).OrderByDescending(o => o.MesajID).ToList();
                ViewBag.mesajpagebaslik = "Silinen Mesajlar";
                return View(silmesajlar);
            }
            else if (id == "gelenmesajlar")
            {
                var gelenmesajlar = db.Mesajlars.Where(w => w.Durum == true && w.Favori == false).OrderByDescending(o => o.MesajID).ToList();
                ViewBag.mesajpagebaslik = "Gelen Mesajlar";
                return View(gelenmesajlar);
            }
            else
            {
                return HttpNotFound();
            }

        }
        public ActionResult MesajOku(int id)
        {
            var mesaj = db.Mesajlars.Where(w => w.MesajID == id).SingleOrDefault();
            mesaj.Bildirim = false;
            db.SaveChanges();
            return View(mesaj);
        }
        public ActionResult MesajSil(int id)
        {
            var mesaj = db.Mesajlars.Where(w => w.MesajID == id).SingleOrDefault();
            if (mesaj != null && mesaj.Durum == true)
            {
                mesaj.Durum = false;
                db.SaveChanges();
                return RedirectToAction("Mesajlar", new { id = "silinenmesajlar" });
            }
            else if (mesaj != null && mesaj.Durum == false)
            {
                mesaj.Durum = true;
                db.SaveChanges();
                return RedirectToAction("Mesajlar", new { id = "silinenmesajlar" });
            }
            else
            {
                return HttpNotFound();
            }

        }
        public ActionResult MesajTemizle(string id)
        {
            if (id=="tumunusil")
            {
                var mesajlar = db.Mesajlars.Where(w => w.Durum == false).ToList();
                foreach (var item in mesajlar)
                {
                    db.Mesajlars.Remove(item);
                    db.SaveChanges();
                }
            }
            else
            {
                int mid = Convert.ToInt32(id);
                db.Mesajlars.Remove(db.Mesajlars.Where(w => w.MesajID == mid).SingleOrDefault());
                db.SaveChanges();
            }
            return RedirectToAction("Mesajlar", new { id = "silinenmesajlar" });
           
        }
        public ActionResult FavoriEkleSil(int id)
        {
            var mesaj = db.Mesajlars.Where(w => w.MesajID == id).SingleOrDefault();
            if (mesaj != null && mesaj.Favori == true)
            {
                mesaj.Favori = false;
                db.SaveChanges();
                return RedirectToAction("Mesajlar", new { id = "favorimesajlar" });
            }
            else if (mesaj != null && mesaj.Favori == false)
            {
                mesaj.Favori = true;
                db.SaveChanges();
                return RedirectToAction("Mesajlar", new { id = "favorimesajlar" });
            }
            else
            {
                return HttpNotFound();
            }

        }
    }
}
