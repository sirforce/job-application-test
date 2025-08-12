namespace JobApplicationTracker.Models
{
    public class Application
    {
        public Guid ApplicationId { get; set; } = Guid.NewGuid();
        public Guid JobId { get; set; }
        public string CandidateName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
    }
}
