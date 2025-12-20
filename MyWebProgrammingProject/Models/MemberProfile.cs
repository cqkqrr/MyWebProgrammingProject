using MyWebProgrammingProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MemberProfile
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; } = null!;

    [Range(100, 250)]
    public int Height { get; set; }

    [Range(30, 300)]
    public int Weight { get; set; }

    [Required]
    public string Goal { get; set; } = string.Empty;
}
