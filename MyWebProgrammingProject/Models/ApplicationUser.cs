using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MyWebProgrammingProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Ad Soyad")]
        public string FullName { get; set; }

        [Display(Name = "Boy (cm)")]
        public double? Height { get; set; }

        [Display(Name = "Kilo (kg)")]
        public double? Weight { get; set; }

        [Display(Name = "Vücut Tipi")]
        public string? BodyType { get; set; }

        [Display(Name = "Spor Hedefi")]
        public string? Goal { get; set; }

        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}