namespace WebProduct.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Sayfalar")]
    public class Sayfalar
    {
        [Key]
        public int SayfaID { get; set; }

        [StringLength(50)]
        public string SayfaBaslik { get; set; }
        [AllowHtml]
        public string SayfaIcerik { get; set; }

    }
}
