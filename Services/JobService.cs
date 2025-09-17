using JobApplicationTracker.Models;

namespace JobApplicationTracker.Services
{
    public class JobService : IJobService
    {
        private readonly List<Job> _jobs = new();
		private readonly List<JobApplication> _applications = new();

        public Task<Job> CreateJobAsync(Job job, CancellationToken cancellationToken = default)
        {
            if (job.JobId == Guid.Empty) job.JobId = Guid.NewGuid();
            _jobs.Add(job);
            return Task.FromResult(job);
        }

        public Task<IEnumerable<Job>> GetJobsAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IEnumerable<Job>>(_jobs);
        }

        public Task<Job?> GetJobByIdAsync(Guid jobId, CancellationToken cancellationToken = default)
        {
            var job = _jobs.FirstOrDefault(j => j.JobId == jobId);
            return Task.FromResult(job);
        }

        public async Task<JobApplication> ApplyToJobAsync(JobApplication application, CancellationToken cancellationToken = default)
        {
            var job = await GetJobByIdAsync(application.JobId, cancellationToken);
            if (job == null)
                throw new ArgumentException("Job not found.");

            _applications.Add(application);
            return application;
        }

        public Task<IEnumerable<JobApplication>> GetApplicationsByJobIdAsync(Guid jobId, CancellationToken cancellationToken = default)
        {
            var applications = _applications.Where(a => a.JobId == jobId).ToList();
            return Task.FromResult<IEnumerable<JobApplication>>(applications);
        }

        // Synchronous methods for backward compatibility
        public IEnumerable<JobApplication> GetApplicationsByJobId(Guid jobId)
        {
            return _applications.Where(a => a.JobId == jobId).ToList();
        }

    }
}
