namespace MyWebProgrammingProject.Models
{
    public class AdminDashboardViewModel
    {
        // Kartlar için Sayılar
        public int TotalMembers { get; set; }
        public int TotalTrainers { get; set; }
        public int TotalAppointments { get; set; }
        public int PendingAppointmentsCount { get; set; }

        // Bekleyen Randevular Listesi (Tablo için)
        public List<Appointment> PendingAppointments { get; set; } = new List<Appointment>();

        // Grafik Verileri (Chart.js için)
        public List<string> ServiceNames { get; set; } = new List<string>();
        public List<int> ServicePopularity { get; set; } = new List<int>();
    }
}