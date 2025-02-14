using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPortfolio.Models
{
    public class Experience
    {
        public int Id { get; set; }

        // Şirket Adı

        [Required(ErrorMessage = "Şirket Adı Boş Geçilemez")]
        [StringLength(200)]

        public string CompanyName { get; set; }

        // Pozisyon

        [Required(ErrorMessage = "Pozisyon Boş Geçilemez")]
        [StringLength(200)]

        public string JobTitle { get; set; }

        // İş Tanımı
        [Required(ErrorMessage = "İş Tanımı Boş Geçilemez")]
        [StringLength(500)]

        public string JobDescription { get; set; }

        // İş Görseli
        [Required(ErrorMessage = "İş Görseli Boş Geçilemez")]
        [StringLength(500)]

        public string JobImageUrl { get; set; }

        // İş Başlangıç Tarihi
        [Required(ErrorMessage = "İş Başlangıç Tarihi Boş Geçilemez")]
        [Display(Name = "İş Başlangıç Tarihi")]
        [DataType(DataType.Date)]

        public DateTime JobStartDate { get; set; }

        // İş Bitiş Tarihi

        [Display(Name = "İş Bitiş Tarihi")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "İş Bitiş Tarihi Boş Geçilemez")]

        public DateTime JobEndDate { get; set; }

        public bool IsActive { get; set; } = true;

        
    }
}