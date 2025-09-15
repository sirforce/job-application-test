using System.ComponentModel.DataAnnotations;

// Models/Application.cs
public class Application
{
    [Required]
    public Guid JobId { get; set; }
    [Required]
    public required string ApplicantName { get; set; }
    [Required]
    public required string ApplicantEmail { get; set; }
    [Required]
    public DateTime AppliedDate { get; set; }
}