namespace WebProduct.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Urunler")]
    public partial class Urunler
    {
        DataContext db = new DataContext();

        [Key]
        public int UrunID { get; set; }

        [StringLength(150)]
        public string UrunAdi { get; set; }

        [StringLength(150)]
        public string UrunURL { get; set; }

        public int? KategoriNo { get; set; }

        public string UrunAciklama { get; set; }

        public string UrunOzellik { get; set; }

        [StringLength(250)]
        public string Resim1 { get; set; }

        [StringLength(250)]
        public string Resim2 { get; set; }

        [StringLength(250)]
        public string Resim3 { get; set; }

        [StringLength(250)]
        public string Resim4 { get; set; }

        public bool? YeniUrun { get; set; }

        public bool? OneCikar { get; set; }

        public bool? Durum { get; set; }

        public virtual Kategoriler Kategoriler { get; set; }
    }
   
}
