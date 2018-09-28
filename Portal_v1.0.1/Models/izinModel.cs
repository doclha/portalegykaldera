using Portal_v1._0._1.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal_v1._0._1.Models
{
    public class IzinModel
    {
        public int Id { get; set; }


        [Required]
        [Display(Name ="İzin Türü")]
        public string IzinTuru { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "* A valid first name is required.")]
        [Display(Name = "İzin Açıklama")]
        public string IzinAciklama { get; set; }
        public string IzinGun { get; set; }

        [Required]
        public DateTime BaslangicTarihi { get; set; }

        [Required]
        public DateTime BitisTarihi { get; set; }
        public bool Onaylandi { get; set; }

        public string PortalUserId { get; set; }
        public PortalUser User { get; set; }


    }
    public class IzinGelen
    {
        public int Id { get; set; }
        public string IzinTuru { get; set; }
        public string IzinAciklama { get; set; }
        public string IzinGun { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public bool Onaylandi { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }


    }

    public class ResmiTatiller
    {
        [Key]
        public int Id { get; set; }
        public double tatil_sayisi { get; set; }
        public DateTime tarih { get; set; }
    }
    public class RaporModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Rapor Açıklama")]
        public string RaporAciklama { get; set; }

        [Required]
        [Display(Name = "Başlangıç Tarihi")]
        public DateTime BaslangicTarihi { get; set; }

        [Required]
        [Display(Name = "Bitiş Tarihi")]
        public DateTime BitisTarihi { get; set; }

        public string RaporGun { get; set; }
        public string Resim { get; set; }

        public bool Onaylandi { get; set; }

        public string PortalUserId { get; set; }
        public PortalUser User { get; set; }

    }

    public class RaporGelen
    {
        public int Id { get; set; }
        public string RaporAciklama { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public string RaporGun { get; set; }
        public bool Onaylandi { get; set; }
        public string Resim { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
    public static class IzinTurRepository
    {
        public static List<SelectListItem> IzinTurListesi()
        {

            List<SelectListItem> IzinListe = new List<SelectListItem>()
            {
                new SelectListItem{ Value="Yıllık izin", Text="Yıllık izin"},
                new SelectListItem{ Value="Doğum izni", Text="Doğum izni"},
                new SelectListItem{ Value="Ölüm izni", Text="Ölüm izni"},
                new SelectListItem{ Value="Ücretli izin", Text="Ücretli izin"},
                new SelectListItem{ Value="Evlilik izni", Text="Evlilik izni"},
            };
            return IzinListe;
        }
    }
}