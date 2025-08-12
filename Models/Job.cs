namespace JobApplicationTracker.Models
{
    public class Job
    {
        public Guid JobId { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public DateTime PostedDate { get; set; } = DateTime.UtcNow;
    }
}
