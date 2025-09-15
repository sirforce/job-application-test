using System.ComponentModel.DataAnnotations;

namespace JobApplicationTracker.DTO;  
// DTO/ApplicationDto.cs
public class ApplicationDto
{
    [Required]
    public Guid JobId { get; init; }
    [Required]
    public required string ApplicantName { get; init; }
    [Required]
    public required string ApplicantEmail { get; init; }
    [Required]
    public DateTime AppliedDate { get; init; }
}