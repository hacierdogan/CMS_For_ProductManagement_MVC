namespace WebProduct.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Popup")]
    public partial class Popup
    {
        public int PopupID { get; set; }

        [StringLength(50)]
        public string Baslik { get; set; }

        [StringLength(250)]
        public string Resim { get; set; }

        public bool? ResimDurum { get; set; }

        [AllowHtml]
        public string Icerik { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Baslangic { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Bitis { get; set; }

        public bool? Durum { get; set; }
    }
}
