// Services/ApplicationService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using JobApplicationTracker.Models;

namespace JobApplicationTracker.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IJobService _jobService;

        public ApplicationService(IJobService jobService)
        {
            _jobService = jobService;
        }

        public Application Add(Application application)
        {
            // write through to the JobService store
            var saved = _jobService.ApplyToJobAsync(new JobApplication
            {
                JobId         = application.JobId,
                CandidateName = application.ApplicantName,
                Email         = application.ApplicantEmail
            }).GetAwaiter().GetResult();

            // map back to your Application shape
            return new Application
            {
                // if JobApplication has an Id/ApplicationId, map it; otherwise omit
                JobId          = saved.JobId,
                ApplicantName  = saved.CandidateName,
                ApplicantEmail = saved.Email,
                AppliedDate    = saved.AppliedDate
            };
        }

        public IEnumerable<Application> GetByJobId(Guid jobId)
        {
            // read from JobService’s in-memory store so we see seeded data
            var apps = _jobService.GetApplicationsByJobId(jobId);
            return apps.Select(a => new Application
            {
                JobId          = a.JobId,
                ApplicantName  = a.CandidateName,
                ApplicantEmail = a.Email,
                AppliedDate    = a.AppliedDate
            });
        }

        public bool JobExists(Guid jobId)
        {
            // You must await the Task; comparing a Task to null will always be true.
            var job = _jobService.GetJobByIdAsync(jobId).GetAwaiter().GetResult();
            return job is not null;
        }
    }
}