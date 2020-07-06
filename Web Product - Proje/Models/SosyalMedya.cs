namespace WebProduct.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SosyalMedya")]
    public partial class SosyalMedya
    {
        [Key]
        public int MedyaID { get; set; }

        [StringLength(50)]
        public string MedyaAdi { get; set; }

        [StringLength(50)]
        public string MedyaClass { get; set; }

        [StringLength(250)]
        public string MedyaUrl { get; set; }

        public int? MedyaSirasi { get; set; }
    }
}
