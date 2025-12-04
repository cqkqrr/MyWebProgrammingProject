namespace MyWebProgrammingProject.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Expertise { get; set; } // Kas geliştirme, cardio...

        // Hangi gym’de çalışıyor?
        public int GymId { get; set; }
        public Gym Gym { get; set; }

        // Hangi hizmetleri verebiliyor?
        public List<Service> Services { get; set; }

        // Müsaitlik saatleri
        public List<TrainerAvailability> Availabilities { get; set; }
    }

}
