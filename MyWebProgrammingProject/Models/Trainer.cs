using System.ComponentModel.DataAnnotations;

namespace MyWebProgrammingProject.Models
{
    public class Trainer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad Soyad zorunludur.")]
        [StringLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter olabilir.")]
        [Display(Name = "Ad Soyad")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Uzmanlık alanı zorunludur.")]
        [Display(Name = "Uzmanlık")]
        public string Expertise { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        // İlişkiler
        public ICollection<Service>? Services { get; set; } = new List<Service>();

        [Required(ErrorMessage = "Salon seçimi zorunludur.")]
        [Display(Name = "Salon")]
        public int GymId { get; set; }
        public Gym? Gym { get; set; }

        // ✅ BU SATIR EKSİK OLDUĞU İÇİN HATA VERİYOR:
        public ICollection<TrainerAvailability> Availabilities { get; set; } = new List<TrainerAvailability>();
    }
}