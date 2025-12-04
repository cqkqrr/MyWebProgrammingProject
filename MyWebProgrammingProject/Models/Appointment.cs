namespace MyWebProgrammingProject.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        // Üye (Identity User)
        public string MemberId { get; set; }
        public ApplicationUser Member { get; set; }

        // Antrenör
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        // Hizmet
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        // Randevu Zamanı
        public DateTime AppointmentDate { get; set; }

        public bool IsApproved { get; set; }
    }

}
