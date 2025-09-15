using System.ComponentModel.DataAnnotations;

namespace JobApplicationTracker.Models
{
    public class JobApplication
    {
        [Required]
        public Guid ApplicationId { get; set; } = Guid.NewGuid();
        [Required]
        public Guid JobId { get; set; }
        [Required]
        public string CandidateName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
    }
}
