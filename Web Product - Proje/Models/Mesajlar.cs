namespace WebProduct.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Mesajlar")]
    public partial class Mesajlar
    {
        [Key]
        public int MesajID { get; set; }

        [StringLength(50)]
        public string GonderenAdSoyad { get; set; }

        [StringLength(50)]
        public string Eposta { get; set; }

        public string Mesaj { get; set; }

        public DateTime? Tarih { get; set; }

        public bool? Bildirim { get; set; }

        public bool? Favori { get; set; }

        public bool? Durum { get; set; }
    }
}
