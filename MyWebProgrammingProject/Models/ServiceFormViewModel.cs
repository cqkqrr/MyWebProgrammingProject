namespace MyWebProgrammingProject.Models
{
    public class ServiceFormViewModel
    {
        public Service Service { get; set; } = new Service();
        public List<Gym> Gyms { get; set; } = new List<Gym>();
    }
}
