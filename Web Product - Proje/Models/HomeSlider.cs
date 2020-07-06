namespace WebProduct.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HomeSlider")]
    public partial class HomeSlider
    {
        [Key]
        public int SliderID { get; set; }

        [StringLength(250)]
        public string ResimURL { get; set; }

    }
}
