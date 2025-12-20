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

        [Required(ErrorMessage = "Uzmanlık zorunludur.")]
        [StringLength(100, ErrorMessage = "Uzmanlık en fazla 100 karakter olabilir.")]
        [Display(Name = "Uzmanlık")]
        public string Expertise { get; set; } = string.Empty;

        [Required(ErrorMessage = "Salon seçimi zorunludur.")]
        [Display(Name = "Salon")]
        public int GymId { get; set; }

        public Gym Gym { get; set; } = null!;

        public ICollection<Service> Services { get; set; } = new List<Service>();
        public List<TrainerAvailability> Availabilities { get; set; } = new List<TrainerAvailability>();
    }
}
