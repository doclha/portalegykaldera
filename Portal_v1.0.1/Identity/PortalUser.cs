using Microsoft.AspNet.Identity.EntityFramework;
using Portal_v1._0._1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portal_v1._0._1.Identity
{
    public class PortalUser:IdentityUser
    {
        [Required]
        [Display(Name="İsim")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Soyisim")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Unvan")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Required]
        [Display(Name="İşe Giriş Tarihi")]
        public DateTime IseGiris { get; set; }

        [Display(Name="İşten Çıkış Tarihi")]
        public string IsCikis { get; set; }

        public string Resim { get; set; }

        public bool CiktiMi { get; set; }

        public List<IzinModel> Izinler { get; set; }

        public List<RaporModel> Raporlar { get; set; }

        public List<MasrafSepet> Masraflar { get; set; }

    }
}