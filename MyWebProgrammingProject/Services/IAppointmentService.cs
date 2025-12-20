namespace MyWebProgrammingProject.Services
{
    public interface IAppointmentService
    {
        Task<bool> IsTrainerAvailableAsync(int trainerId, DateTime start, DateTime end);
        Task<bool> CheckConflictAsync(int trainerId, DateTime start, DateTime end);
    }
}