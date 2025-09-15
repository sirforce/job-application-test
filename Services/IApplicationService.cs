using System;
using System.Collections.Generic;
using JobApplicationTracker.Models;

namespace JobApplicationTracker.Services
{
    public interface IApplicationService
    {
        Application Add(Application application);
        IEnumerable<Application> GetByJobId(Guid jobId);   // Guid
        bool JobExists(Guid jobId);                        // Guid
    }
}