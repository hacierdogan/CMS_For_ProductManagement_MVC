namespace WebProduct.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FirmaBilgi")]
    public partial class FirmaBilgi
    {
        [Key]
        public int FirmaID { get; set; }

        [StringLength(100)]
        public string FirmaAdi { get; set; }

        [StringLength(250)]
        public string FirmaLogo { get; set; }

        [StringLength(100)]
        public string FirmaMail { get; set; }

        [StringLength(50)]
        public string FirmaTel { get; set; }

        [StringLength(100)]
        public string FirmaAdres { get; set; }

        [StringLength(100)]
        public string FirmaKonum { get; set; }
    }
}
