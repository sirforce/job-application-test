using Microsoft.AspNetCore.Mvc;
using JobApplicationTracker.Models;
using JobApplicationTracker.Services;

namespace JobApplicationTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
        }

        /// <summary>
        /// Creates a new job posting.
        /// POST /jobs
        /// </summary>
        /// <param name="job">The job details</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created job</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Job>> CreateJob([FromBody] Job job, CancellationToken cancellationToken = default)
        {
            if (job is null)
                return BadRequest("Job details are required.");

            var created = await _jobService.CreateJobAsync(job, cancellationToken);
            return CreatedAtAction(nameof(GetJobById), new { jobId = created.JobId }, created);
        }

        /// <summary>
        /// Retrieves all jobs.
        /// GET /jobs
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs(CancellationToken cancellationToken = default)
        {
            var jobs = await _jobService.GetJobsAsync(cancellationToken);
            return Ok(jobs);
        }

        /// <summary>
        /// Retrieves a specific job by ID.
        /// GET /jobs/{jobId}
        /// </summary>
        /// <param name="jobId">The ID of the job to retrieve</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpGet("{jobId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Job>> GetJobById(Guid jobId, CancellationToken cancellationToken = default)
        {
            var job = await _jobService.GetJobByIdAsync(jobId, cancellationToken);
            if (job == null) 
                return NotFound($"Job with ID {jobId} not found.");
            
            return Ok(job);
        }
    }
}