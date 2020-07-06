namespace WebProduct.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sabitler")]
    public partial class Sabitler
    {
        [Key]
        public int SabitID { get; set; }

        [StringLength(50)]
        public string AramaPlaceHolder { get; set; }

        [StringLength(50)]
        public string AramaButton { get; set; }

        [StringLength(250)]
        public string HomeRes { get; set; }

        [StringLength(10)]
        public string HomeResBaslik { get; set; }

        [StringLength(250)]
        public string HomeResURL { get; set; }

        [StringLength(50)]
        public string HomeVitrinBaslik { get; set; }

        [StringLength(50)]
        public string SolMenuBaslik { get; set; }

        [StringLength(50)]
        public string UrunOwlBaslik { get; set; }

        [StringLength(50)]
        public string AramaSonucBaslik { get; set; }

        [StringLength(50)]
        public string IletisimBaslik { get; set; }
    }
}
