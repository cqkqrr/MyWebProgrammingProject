namespace MyWebProgrammingProject.Models
{
    public class Gym
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string WorkingHours { get; set; } // "08:00 - 22:00"

        // Navigation
        public List<Service> Services { get; set; }
        public List<Trainer> Trainers { get; set; }
    }

}
