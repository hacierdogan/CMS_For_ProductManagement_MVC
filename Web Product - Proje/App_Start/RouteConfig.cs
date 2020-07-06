using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebProduct
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            
            routes.MapRoute(
            "Giris",
            "giris",
            new { controller = "APManager", action = "AdminGiris" }
            );
            routes.MapRoute(
           "UrunAra",
           "urun-ara/{Id}",
           new { controller = "Home", action = "UrunAra" }
           );
            routes.MapRoute(
            "ParolaYenile",
            "parolayenile",
            new { controller = "APManager", action = "AdminParolaYenile" }
            );

            routes.MapRoute(
            "UrunListesi",
            "Urunler/{Id}",
            new { controller = "Urunler", action = "UrunListesi" }
            );

            routes.MapRoute(
            "UrunDetay",
            "Urunler/{*Id}",
            new { controller = "Urunler", action = "UrunDetay" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}