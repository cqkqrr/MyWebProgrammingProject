namespace MyWebProgrammingProject.Models;

public class Appointment
{
    public int Id { get; set; }

    public DateTime AppointmentDate { get; set; } 

    public int TrainerId { get; set; }
    public Trainer Trainer { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;

    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;

    public bool IsApproved { get; set; }
}
