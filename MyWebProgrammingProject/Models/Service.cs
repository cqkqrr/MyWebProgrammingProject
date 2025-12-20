using System.ComponentModel.DataAnnotations;

namespace MyWebProgrammingProject.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hizmet adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Hizmet adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Hizmet Adı")]
        public string Name { get; set; } = string.Empty;

        [Range(15, 480, ErrorMessage = "Süre 15-480 dakika arasında olmalıdır.")]
        [Display(Name = "Süre (dk)")]
        public int Duration { get; set; }

        [Range(0, 1000000, ErrorMessage = "Fiyat 0 ile 1.000.000 arasında olmalıdır.")]
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }

        // ✅ GÜNCELLEME: Nullable (?) yaptık, yoksa validasyon hatası verir.
        public ICollection<Trainer>? Trainers { get; set; }

        [Required(ErrorMessage = "Salon seçimi zorunludur.")]
        [Display(Name = "Salon")]
        public int GymId { get; set; }

        // ✅ GÜNCELLEME: Nullable (?) yaptık. Formdan sadece ID gelir, nesne gelmez.
        public Gym? Gym { get; set; }
    }
}