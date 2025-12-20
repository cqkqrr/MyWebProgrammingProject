using System.ComponentModel.DataAnnotations;

namespace MyWebProgrammingProject.Models
{
    public class AppointmentCreateViewModel
    {
        [Required(ErrorMessage = "Eğitmen seçimi zorunludur.")]
        [Display(Name = "Eğitmen")]
        public int? TrainerId { get; set; }

        [Required(ErrorMessage = "Hizmet seçimi zorunludur.")]
        [Display(Name = "Hizmet")]
        public int? ServiceId { get; set; }

        [Required(ErrorMessage = "Başlangıç zamanı zorunludur.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Başlangıç")]
        public DateTime? StartTime { get; set; }

        public List<Trainer> Trainers { get; set; } = new List<Trainer>();
        public List<Service> Services { get; set; } = new List<Service>();
    }
}
