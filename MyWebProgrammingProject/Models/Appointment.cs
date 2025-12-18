using MyWebProgrammingProject.Models;

public class Appointment
{
    public int Id { get; set; }

    public DateTime AppointmentDate { get; set; }

    public int TrainerId { get; set; }
    public Trainer Trainer { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int ServiceId { get; set; }
    public Service Service { get; set; }

    public bool IsApproved { get; set; }
}
