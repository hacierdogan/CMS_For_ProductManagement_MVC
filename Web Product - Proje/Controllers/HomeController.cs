using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebProduct.Models;

namespace WebProduct.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            HomeMultiModel homemodel = new HomeMultiModel
            {
                sabitler = db.Sabitlers.Find(1),
                popup=db.Popups.Find(1),
                homeslider = db.HomeSliders.ToList(),
            };
            return View(homemodel);
        }
        public ActionResult OneCikanlar(int? page)
        {
            ViewBag.HomeVitrinBaslik = db.Sabitlers.Find(1).HomeVitrinBaslik;
            
            Thread.Sleep(500);//işlemi 0.5saniye beklet
            int pagesize = 4;

            IEnumerable<Urunler> onecikanlar = null;
            if (!page.HasValue)//ilk sayfa için null ise
            {
                onecikanlar = db.Urunlers.Where(w => w.Durum == true && w.OneCikar == true).OrderByDescending(o=>o.UrunID).Take(pagesize);
            }
            else
            {
                int pageIndex = pagesize * page.Value;
                onecikanlar = db.Urunlers.Where(w => w.Durum == true && w.OneCikar == true).OrderByDescending(o => o.UrunID).Skip(pageIndex).Take(pagesize);//skip(10).take(5) 10.indexten başla 5 tane oku
            }
            if (Request.IsAjaxRequest())//gelen istek ajax'tan ise
            {
                return PartialView("_OneCikanlarList", onecikanlar);
            }
            else
            {
                return View(onecikanlar);
            }   
        }
        public ActionResult Topbar()
        {
            var sabit = db.Sabitlers.Find(1);
            ViewBag.aramaplaceholder = sabit.AramaPlaceHolder;
            ViewBag.aramabutton = sabit.AramaButton;
            return View(db.FirmaBilgis.Find(1));
        }

        public ActionResult Menu()
        {
            ViewBag.menulogo = db.FirmaBilgis.Find(1).FirmaLogo;
            var kategoriler = db.Kategorilers.Where(w => w.Durum == true).OrderBy(o=>o.Sira).ToList();
            return View(kategoriler);
        }
        public ActionResult UrunAra(string id)
        {
            var aranan = db.Urunlers.Where(u => u.UrunAdi.Contains(id)).Where(w=>w.Durum==true).ToList();
            ViewBag.aramabaslik = db.Sabitlers.Find(1).AramaSonucBaslik;
            ViewBag.urunadi = id;
            return View(aranan);
        }
        public ActionResult Footer()
        {
            FooterMultiModel footermodel = new FooterMultiModel
            {
                firmabilgi = db.FirmaBilgis.Find(1),
                sosyalmedya = db.SosyalMedyas.OrderBy(o => o.MedyaSirasi).ToList()
            };
            return View(footermodel);
        }

        public ActionResult Hata()
        {
            return View();
        }
        public ActionResult _GoogleAnalytics()
        {
            return View(db.Ceos.Find(1));
        }
    }
}
