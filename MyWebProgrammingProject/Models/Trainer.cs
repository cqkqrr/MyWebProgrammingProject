namespace MyWebProgrammingProject.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Expertise { get; set; } = string.Empty;// Kas geliştirme, cardio...
        public ICollection<Service> Services { get; set; } = new List<Service>();

        // Hangi gym’de çalışıyor?
        public int GymId { get; set; }
        public Gym Gym { get; set; } = null!;

        // Hangi hizmetleri verebiliyor?


        // Müsaitlik saatleri
        public List<TrainerAvailability> Availabilities { get; set; } = new List<TrainerAvailability>();
    }

}
