using Microsoft.AspNetCore.Mvc;
using JobApplicationTracker.Models;
using JobApplicationTracker.Services;

namespace JobApplicationTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(Job job)
        {
            var created = await _jobService.CreateJobAsync(job);
            return CreatedAtAction(nameof(GetJobById), new { jobId = created.JobId }, created);
        }

        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {
            var jobs = await _jobService.GetJobsAsync();
            return Ok(jobs);
        }

        [HttpGet("{jobId}")]
        public async Task<IActionResult> GetJobById(Guid jobId)
        {
            var job = await _jobService.GetJobByIdAsync(jobId);
            if (job == null) return NotFound();
            return Ok(job);
        }
    }
}
