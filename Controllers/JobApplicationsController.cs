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
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
        }

        // DTO kept local until proper DTOs structure is set up
        public class CreateJobApplicationRequest
        {
            public Guid JobId { get; init; }
            public required string ApplicantName { get; init; } = string.Empty;
            public required string ApplicantEmail { get; init; }
            public string? CoverLetter { get; init; }
        }

        /// <summary>
        /// Returns all job applications for a given job.
        /// GET /jobapplications/job/{jobId}
        /// </summary>
        /// <param name="jobId">The ID of the job to get applications for</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpGet("job/{jobId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplicationsForJob(
            Guid jobId, 
            CancellationToken cancellationToken = default)
        {
            var job = await _jobService.GetJobByIdAsync(jobId, cancellationToken);
            if (job == null)
                return NotFound($"Job with ID {jobId} not found.");

            var applications = await _applicationService.GetByJobIdAsync(jobId, cancellationToken);
            return Ok(applications);
        }

        /// <summary>
        /// Creates a job application and ties it to a job.
        /// POST /jobapplications
        /// </summary>
        /// <param name="request">The application details</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Application>> CreateApplication(
            [FromBody] CreateJobApplicationRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request is null)
                return BadRequest("Request body is required.");

            if (request.JobId == Guid.Empty)
                return BadRequest("JobId is required.");

            if (string.IsNullOrWhiteSpace(request.ApplicantName))
                return BadRequest("ApplicantName is required.");

            if (string.IsNullOrWhiteSpace(request.ApplicantEmail))
                return BadRequest("ApplicantEmail is required.");

            // Verify the job exists
            var job = await _jobService.GetJobByIdAsync(request.JobId, cancellationToken);
            if (job == null)
                return NotFound($"Job with ID {request.JobId} not found.");

            var application = new Application
            {
                JobId = request.JobId,
                ApplicantName = request.ApplicantName.Trim(),
                ApplicantEmail = request.ApplicantEmail.Trim()
            };

            var created = _applicationService.Add(application);

            return CreatedAtAction(
                nameof(GetApplicationsForJob),
                new { jobId = created.JobId },
                created
            );
        }
    }
}