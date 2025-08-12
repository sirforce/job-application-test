using JobApplicationTracker.Models;
using JobApplicationTracker.DTOs;

namespace JobApplicationTracker.Services
{
    public interface IApplicationService
    {
        Task<Application> ApplyToJobAsync(Application application);
        Task<IEnumerable<Application>> GetApplicationsByJobAsync(Guid jobId);
        Task<ApplicationSummary> GetApplicationsSummaryAsync();
    }
}
