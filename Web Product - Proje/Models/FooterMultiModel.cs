using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProduct.Models
{
    public class FooterMultiModel
    {
        public FirmaBilgi firmabilgi { get; set; }
        public List<SosyalMedya> sosyalmedya { get; set; }
    }
}