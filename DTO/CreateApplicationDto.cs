namespace JobApplicationTracker.Models
{
    // DTO/CreateApplicationDto.cs
using System.ComponentModel.DataAnnotations;

public class CreateApplicationDto
{
    [Required]
    public required Guid JobId { get; init; }

    [Required]
    public required string ApplicantName { get; init; }

    [EmailAddress]
    public required string ApplicantEmail { get; init; }  // make optional if it’s not required
}
}