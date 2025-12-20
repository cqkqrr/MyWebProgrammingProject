using MyWebProgrammingProject.Models;

namespace MyWebProgrammingProject.Models.ViewModels
{
    public class MemberDashboardViewModel
    {
        public string FullName { get; set; } = string.Empty;
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public string Goal { get; set; } = string.Empty;

        // Sıradaki Randevu
        public Appointment? NextAppointment { get; set; }

        // Toplam yapılan antrenman sayısı (Geçmiş randevular)
        public int TotalWorkouts { get; set; }
    }
}