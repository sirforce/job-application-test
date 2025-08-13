using JobApplicationTracker.Models;
using JobApplicationTracker.Services;

namespace JobApplicationTracker.Data
{
    public class SampleDataSeeder
    {
        private readonly IJobService _jobService;

        public SampleDataSeeder(IJobService jobService)
        {
            _jobService = jobService;
        }

        public void Seed()
        {
            var jobs = new List<Job>
            {
                new Job { Title = "Software Engineer", Department = "IT" },
                new Job { Title = "QA Analyst", Department = "Quality" },
                new Job { Title = "Project Manager", Department = "Management" }
            };

            foreach (var job in jobs)
            {
                _jobService.CreateJobAsync(job).Wait();
            }

            var applications = new List<Application>
            {
                new Application { JobId = jobs[0].JobId, CandidateName = "Alice Smith", Email = "alice@example.com" },
                new Application { JobId = jobs[0].JobId, CandidateName = "Bob Jones", Email = "bob@example.com" },
                new Application { JobId = jobs[1].JobId, CandidateName = "Charlie Brown", Email = "charlie@example.com" }
            };

            foreach (var app in applications)
            {
                _jobService.ApplyToJobAsync(app).Wait();
            }
        }
    }
}
