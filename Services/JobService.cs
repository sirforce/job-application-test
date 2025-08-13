using JobApplicationTracker.Models;

namespace JobApplicationTracker.Services
{
    public class JobService : IJobService
    {
        private readonly List<Job> _jobs = new();
		private readonly List<Application> _applications = new();

        public Task<Job> CreateJobAsync(Job job)
        {
            _jobs.Add(job);
            return Task.FromResult(job);
        }

        public Task<IEnumerable<Job>> GetJobsAsync()
        {
            return Task.FromResult<IEnumerable<Job>>(_jobs);
        }

        public Task<Job?> GetJobByIdAsync(Guid jobId)
        {
            var job = _jobs.FirstOrDefault(j => j.JobId == jobId);
            return Task.FromResult(job);
        }
        
        public async Task<Application> ApplyToJobAsync(Application application)
        {
            var job = await GetJobByIdAsync(application.JobId);
            if (job == null)
                throw new ArgumentException("Job not found.");

            _applications.Add(application);
            return application;
        }
    }
}
