using JobApplicationTracker.Models;

namespace JobApplicationTracker.Services
{
    public interface IJobService
    {
        Task<Job> CreateJobAsync(Job job);
        Task<IEnumerable<Job>> GetJobsAsync();
        Task<Job?> GetJobByIdAsync(Guid jobId);
        Task<JobApplication> ApplyToJobAsync(JobApplication application);
        IEnumerable<JobApplication> GetApplicationsByJobId(Guid jobId);
    }
}
