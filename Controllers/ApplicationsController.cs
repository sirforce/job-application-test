using Microsoft.AspNetCore.Mvc;
using JobApplicationTracker.Models;
using JobApplicationTracker.Services;

namespace JobApplicationTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        public async Task<IActionResult> ApplyToJob(Application application)
        {
            try
            {
                var applied = await _applicationService.ApplyToJobAsync(application);
                return Ok(applied);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("job/{jobId}")]
        public async Task<IActionResult> GetApplicationsByJob(Guid jobId)
        {
            var apps = await _applicationService.GetApplicationsByJobAsync(jobId);
            return Ok(apps);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetApplicationsSummary()
        {
            var summary = await _applicationService.GetApplicationsSummaryAsync();
            return Ok(summary);
        }
    }
}
