namespace MyWebProgrammingProject.Models.DTOs
{
    public class TrainerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Expertise { get; set; } = string.Empty;
        public string GymName { get; set; } = string.Empty;
    }
}