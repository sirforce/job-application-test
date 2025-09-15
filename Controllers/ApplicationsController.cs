using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using JobApplicationTracker.Services;
using JobApplicationTracker.Models;
using JobApplicationTracker.DTO;

namespace JobApplicationTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        public IActionResult CreateApplication([FromBody] CreateApplicationDto dto)
        {
            if (dto is null) return BadRequest("Application data is required.");
            if (string.IsNullOrWhiteSpace(dto.ApplicantName))
                return BadRequest("ApplicantName is required.");

            if (!_applicationService.JobExists(dto.JobId))
                return NotFound($"Job with JobId {dto.JobId} not found.");

            var created = _applicationService.Add(new Application
            {
                JobId = dto.JobId,
                ApplicantName = dto.ApplicantName,
                ApplicantEmail = dto.ApplicantEmail
            });

            var resultDto = new ApplicationDto
            {
                JobId = created.JobId,                // Guid -> Guid
                ApplicantName = created.ApplicantName,
                ApplicantEmail = created.ApplicantEmail,
                AppliedDate = created.AppliedDate
            };

            return CreatedAtAction(nameof(GetApplicationsForJob),
                new { jobId = created.JobId },       // Guid route value
                resultDto);
        }

        [HttpGet("job/{jobId:guid}")]
        public IActionResult GetApplicationsForJob(Guid jobId)
        {
            if (!_applicationService.JobExists(jobId))
                return NotFound($"Job with id {jobId} not found.");

            var dtos = _applicationService.GetByJobId(jobId)
                .Select(a => new ApplicationDto
                {
                    JobId = a.JobId,                  // Guid
                    ApplicantName = a.ApplicantName,
                    ApplicantEmail = a.ApplicantEmail,
                    AppliedDate = a.AppliedDate
                });

            return Ok(dtos);
        }
    }
}