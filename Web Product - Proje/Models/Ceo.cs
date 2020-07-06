namespace WebProduct.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Ceo")]
    public partial class Ceo
    {
        public int CeoID { get; set; }

        [StringLength(50)]
        public string SiteBaslik { get; set; }

        [StringLength(115)]
        public string SiteAciklama { get; set; }

        [StringLength(50)]
        public string SiteSahip { get; set; }

        [StringLength(50)]
        public string SiteYonetici { get; set; }

        [StringLength(4)]
        public string SiteKurulus { get; set; }

        [StringLength(150)]
        public string SiteKeys { get; set; }

        [AllowHtml]
        public string GoogleAnalytics { get; set; }
    }
}
