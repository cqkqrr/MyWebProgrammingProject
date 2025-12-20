namespace MyWebProgrammingProject.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        // ----------------------------
        // ZAMAN BİLGİLERİ
        // ----------------------------
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool IsApproved { get; set; }

        // ----------------------------
        // EĞİTMEN
        // ----------------------------
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } = null!;

        // ----------------------------
        // HİZMET
        // ----------------------------
        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        // ----------------------------
        // KULLANICI (ÜYE)
        // ----------------------------
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public string? AdminMessage { get; set; }  // Onay / Red mesajı


    }
}
