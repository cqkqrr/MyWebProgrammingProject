using System.ComponentModel.DataAnnotations;

namespace MyWebProgrammingProject.Models
{
    public class AiCoachViewModel
    {
        [Required(ErrorMessage = "Boy zorunludur.")]
        [Range(100, 250, ErrorMessage = "Boy 100 - 250 cm aralığında olmalı.")]
        [Display(Name = "Boy (cm)")]
        public int HeightCm { get; set; }

        [Required(ErrorMessage = "Kilo zorunludur.")]
        [Range(30, 300, ErrorMessage = "Kilo 30 - 300 kg aralığında olmalı.")]
        [Display(Name = "Kilo (kg)")]
        public int WeightKg { get; set; }

        [Required(ErrorMessage = "Vücut tipi zorunludur.")]
        [StringLength(50, ErrorMessage = "Vücut tipi en fazla 50 karakter olmalı.")]
        [Display(Name = "Vücut Tipi")]
        public string BodyType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hedef zorunludur.")]
        [StringLength(100, ErrorMessage = "Hedef en fazla 100 karakter olmalı.")]
        [Display(Name = "Hedef (örn: yağ yakma, kas kazanma, kondisyon)")]
        public string Goal { get; set; } = string.Empty;

        public string? AiResult { get; set; }
    }
}
