namespace MyWebProgrammingProject.Models
{
    public class TrainerAvailability
    {
        public int Id { get; set; }

        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } = null!;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }

    }
}
