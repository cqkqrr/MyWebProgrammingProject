using System.ComponentModel.DataAnnotations;

namespace MyWebProgrammingProject.Models
{
    public class TrainerFormViewModel
    {
        public Trainer Trainer { get; set; } = new Trainer();

        [Display(Name = "Hizmetler")]
        public List<int> SelectedServiceIds { get; set; } = new List<int>();

        public List<Gym> Gyms { get; set; } = new List<Gym>();
        public List<Service> Services { get; set; } = new List<Service>();
    }
}
