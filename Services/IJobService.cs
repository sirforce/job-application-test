using JobApplicationTracker.Models;

namespace JobApplicationTracker.Services
{
    public interface IJobService
    {
        Task<Job> CreateJobAsync(Job job, CancellationToken cancellationToken = default);
        Task<IEnumerable<Job>> GetJobsAsync(CancellationToken cancellationToken = default);
        Task<Job?> GetJobByIdAsync(Guid jobId, CancellationToken cancellationToken = default);
        Task<JobApplication> ApplyToJobAsync(JobApplication application, CancellationToken cancellationToken = default);
        Task<IEnumerable<JobApplication>> GetApplicationsByJobIdAsync(Guid jobId, CancellationToken cancellationToken = default);

        // Keep synchronous methods for backward compatibility
        IEnumerable<JobApplication> GetApplicationsByJobId(Guid jobId);
    }
}
