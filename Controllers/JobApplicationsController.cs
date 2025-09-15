using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using JobApplicationTracker.Models;
using JobApplicationTracker.Services;

namespace JobApplicationTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IJobService _jobService;

        public JobApplicationsController(IApplicationService applicationService, IJobService jobService)
        {
            _applicationService = applicationService;
            _jobService = jobService;
        }

        // DTO kept local to avoid namespace mismatches
        public class CreateJobApplicationRequest
        {
            public Guid JobId { get; init; }
            public required string ApplicantName { get; init; } = string.Empty;
            public required string ApplicantEmail { get; init; }
        }

        /// <summary>
        /// Returns all job applications for a given job.
        /// GET /jobapplications/job/{jobId}
        /// </summary>
        [HttpGet("job/{jobId:guid}")]
        public IActionResult GetApplicationsForJob(Guid jobId)
        {
            var apps = _applicationService.GetByJobId(jobId);
            return Ok(apps);
        }

        /// <summary>
        /// Creates a job application and ties it to a job.
        /// POST /jobapplications
        /// </summary>
        [HttpPost]
        public IActionResult CreateApplication([FromBody] CreateJobApplicationRequest request)
        {
            if (request is null)
                return BadRequest("Request body is required.");

            if (request.JobId == Guid.Empty)
                return BadRequest("JobId is required.");

            if (string.IsNullOrWhiteSpace(request.ApplicantName))
                return BadRequest("ApplicantName is required.");

            var created = _applicationService.Add(new Application
            {
                JobId = request.JobId,
                ApplicantName = request.ApplicantName,
                ApplicantEmail = request.ApplicantEmail
            });

            // Return 201 with a Location header pointing to the job's applications list
            return CreatedAtAction(
                nameof(GetApplicationsForJob),
                new { jobId = created.JobId },
                created
            );
        }
    }
}