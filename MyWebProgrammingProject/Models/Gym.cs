using System.ComponentModel.DataAnnotations;

namespace MyWebProgrammingProject.Models
{
    public class Gym
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Salon adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Salon adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Salon Adı")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres zorunludur.")]
        [StringLength(500, ErrorMessage = "Adres en fazla 500 karakter olabilir.")]
        [Display(Name = "Adres")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Çalışma saatleri zorunludur.")]
        [StringLength(100, ErrorMessage = "Çalışma saatleri en fazla 100 karakter olabilir.")]
        [Display(Name = "Çalışma Saatleri")]
        public string WorkingHours { get; set; } = string.Empty;

        public List<Service> Services { get; set; } = new List<Service>();
        public List<Trainer> Trainers { get; set; } = new List<Trainer>();
    }
}
