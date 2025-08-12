using JobApplicationTracker.Models;
using JobApplicationTracker.DTOs;

namespace JobApplicationTracker.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly List<Application> _applications = new();
        private readonly IJobService _jobService;

        public ApplicationService(IJobService jobService)
        {
            _jobService = jobService;
        }

        public async Task<Application> ApplyToJobAsync(Application application)
        {
            var job = await _jobService.GetJobByIdAsync(application.JobId);
            if (job == null)
                throw new ArgumentException("Job not found.");

            _applications.Add(application);
            return application;
        }

        public Task<IEnumerable<Application>> GetApplicationsByJobAsync(Guid jobId)
        {
            var apps = _applications.Where(a => a.JobId == jobId);
            return Task.FromResult<IEnumerable<Application>>(apps);
        }

        public async Task<List<ApplicationSummary>> GetApplicationsSummaryAsync()
        {
            var jobs = await _jobService.GetJobsAsync();
            var summary = jobs.Select(j => new ApplicationSummary
            {
                JobTitle = j.Title,
                TotalApplications = _applications.Count(a => a.JobId == j.JobId)
            }).ToList();
            return summary;
        }
    }
}
