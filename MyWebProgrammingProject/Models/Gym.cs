namespace MyWebProgrammingProject.Models
{
    public class Gym
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string WorkingHours { get; set; } = string.Empty; // "08:00 - 22:00"

        // Navigation
        public List<Service> Services { get; set; } = new List<Service>();
        public List<Trainer> Trainers { get; set; } = new List<Trainer>();
    }

}
