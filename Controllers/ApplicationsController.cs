using Microsoft.AspNetCore.Mvc;
using JobApplicationTracker.Models;
using JobApplicationTracker.Services;

namespace JobApplicationTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationsController : ControllerBase
    {
        // two endpointsare needed 
		// one that creates an application and ties it to a job
		// one that returns applications for a job
    }
}
