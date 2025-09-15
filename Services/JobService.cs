using JobApplicationTracker.Models;

namespace JobApplicationTracker.Services
{
    public class JobService : IJobService
    {
        private readonly List<Job> _jobs = new();
		private readonly List<JobApplication> _applications = new();
        public IEnumerable<Job> GetJobs() => _jobs;

        public bool Exists(Guid jobId) => _jobs.Any(j => j.JobId == jobId);

        public Job Add(Job job)
        {
            if (job.JobId == Guid.Empty) job.JobId = Guid.NewGuid();
            _jobs.Add(job);
            return job;
        }

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
        
        public async Task<JobApplication> ApplyToJobAsync(JobApplication application)
        {
            var job = await GetJobByIdAsync(application.JobId);
            if (job == null)
                throw new ArgumentException("Job not found.");

            _applications.Add(application);
            return application;
        }
        public IEnumerable<JobApplication> GetApplicationsByJobId(Guid jobId)
        {
            return _applications.Where(a => a.JobId == jobId).ToList();
        }


    }
}
