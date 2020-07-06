namespace WebProduct.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hesap")]
    public partial class Hesap
    {
        public int HesapID { get; set; }

        [StringLength(50)]
        public string AdSoyad { get; set; }

        [StringLength(50)]
        public string Eposta { get; set; }

        [StringLength(100)]
        public string Parola { get; set; }

        [StringLength(50)]
        public string Yetki { get; set; }
    }
}
