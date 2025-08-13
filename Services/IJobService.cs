using JobApplicationTracker.Models;

namespace JobApplicationTracker.Services
{
    public interface IJobService
    {
        Task<Job> CreateJobAsync(Job job);
        Task<IEnumerable<Job>> GetJobsAsync();
        Task<Job?> GetJobByIdAsync(Guid jobId);
        Task<Application> ApplyToJobAsync(Application application);
    }
}
