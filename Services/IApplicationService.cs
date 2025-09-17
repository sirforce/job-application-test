using System;
using System.Collections.Generic;
using JobApplicationTracker.Models;

namespace JobApplicationTracker.Services
{
    public interface IApplicationService
    {
        // Async methods with CancellationToken support
        Task<Application> AddAsync(Application application, CancellationToken cancellationToken = default);
        Task<IEnumerable<Application>> GetByJobIdAsync(Guid jobId, CancellationToken cancellationToken = default);
        Task<Application?> GetByIdAsync(Guid applicationId, CancellationToken cancellationToken = default);

        // Synchronous methods for backward compatibility
        Application Add(Application application);
        IEnumerable<Application> GetByJobId(Guid jobId);
        bool JobExists(Guid jobId);
    }
}