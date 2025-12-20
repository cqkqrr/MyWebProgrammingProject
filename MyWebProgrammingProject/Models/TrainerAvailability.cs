using System.ComponentModel.DataAnnotations;

namespace MyWebProgrammingProject.Models
{
    public class TrainerAvailability : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Eğitmen seçimi zorunludur.")]
        [Display(Name = "Eğitmen")]
        public int TrainerId { get; set; }

        public Trainer Trainer { get; set; } = null!;

        [Required(ErrorMessage = "Başlangıç zamanı zorunludur.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Başlangıç")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Bitiş zamanı zorunludur.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Bitiş")]
        public DateTime EndTime { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartTime != default && EndTime != default)
            {
                if (EndTime <= StartTime)
                    yield return new ValidationResult("Bitiş zamanı başlangıçtan sonra olmalıdır.", new[] { nameof(EndTime) });

                if (StartTime.Date != EndTime.Date)
                    yield return new ValidationResult("Başlangıç ve bitiş aynı gün içinde olmalıdır.", new[] { nameof(StartTime), nameof(EndTime) });
            }
        }
    }
}
