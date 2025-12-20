namespace MyWebProgrammingProject.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;    // Fitness, Yoga, Pilates
        public int Duration { get; set; } // dakika
        public decimal Price { get; set; }
        public ICollection<Trainer> Trainers { get; set; } = new List<Trainer>();

        // Foreign Key
        public int GymId { get; set; }
        public Gym Gym { get; set; } = null!;
    }

}
