namespace MyWebProgrammingProject.Models
{
    public class TrainerAvailabilityFormViewModel
    {
        public TrainerAvailability Availability { get; set; } = new TrainerAvailability();
        public List<Trainer> Trainers { get; set; } = new List<Trainer>();
    }
}
