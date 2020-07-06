using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProduct.Models
{
    public class HomeMultiModel
    {
        public Sabitler sabitler { get; set; }
        public Popup popup { get; set; }
        public List<HomeSlider> homeslider { get; set; }
    }
}