using System.ComponentModel.DataAnnotations;

namespace MyWebProgrammingProject.Models
{
    public class Appointment : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlangıç zamanı zorunludur.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Başlangıç")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Bitiş zamanı zorunludur.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Bitiş")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Onaylandı mı?")]
        public bool IsApproved { get; set; }

        [Required(ErrorMessage = "Eğitmen seçimi zorunludur.")]
        [Display(Name = "Eğitmen")]
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } = null!;

        [Required(ErrorMessage = "Hizmet seçimi zorunludur.")]
        [Display(Name = "Hizmet")]
        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        [Display(Name = "Admin Mesajı")]
        public string? AdminMessage { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartTime != default && EndTime != default && EndTime <= StartTime)
                yield return new ValidationResult("Bitiş zamanı başlangıçtan sonra olmalıdır.", new[] { nameof(EndTime) });
        }
    }
}
