using System;
using System.Collections.Generic;
using JobApplicationTracker.Models;
using JobApplicationTracker.Services;

namespace JobApplicationTracker.Data
{
    public class SampleDataSeeder
    {
        private readonly IJobService _jobService;

        public SampleDataSeeder(IJobService jobService)
        {
            _jobService = jobService;
        }

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            // 1) Create jobs
            var jobsToCreate = new List<Job>
            {
                new Job { Title = "Software Engineer",        Department = "IT" },
                new Job { Title = "QA Analyst",               Department = "Quality" },
                new Job { Title = "Engineer Analyst",         Department = "Engineering" },
                new Job { Title = "Developer Developer",      Department = "Engineering" },
                new Job { Title = "Product Owner of PO",      Department = "Product" },
                new Job { Title = "Manager Persons Op",       Department = "Product" },
                new Job { Title = "Human Resources",          Department = "HR" },
                new Job { Title = "Legal Analyst",            Department = "Org" },
                new Job { Title = "SpecOps Hardware Engineer",Department = "Engineering" },
                new Job { Title = "Secret Agent",             Department = "Engineering" },
                new Job { Title = "Mentor teacher",           Department = "Engineering" }
            };

            var createdJobs = new List<Job>();
            foreach (var job in jobsToCreate)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var created = await _jobService.CreateJobAsync(job, cancellationToken);
                createdJobs.Add(created);
            }

            // 2) Randomized applicants
            var random   = new Random();
            string[] firstNames = { "Alice", "Bob", "Charlie", "Diana", "Ethan", "Fiona", "George", "Hannah", "Ian", "Julia" };
            string[] lastNames  = { "Smith", "Jones", "Brown", "Taylor", "Johnson", "Lee", "Walker", "Clark", "Hall", "Wright" };
            string[] domains    = { "example.com", "mail.com", "test.org", "demo.net" };

            (string name, string email) GenerateCandidate()
            {
                var first  = firstNames[random.Next(firstNames.Length)];
                var last   = lastNames[random.Next(lastNames.Length)];
                var domain = domains[random.Next(domains.Length)];

                var name  = $"{first} {last}";
                // first initial + last + random 3-digit number to reduce duplicates
                var email = $"{first.ToLower()[0]}{last.ToLower()}{random.Next(100, 999)}@{domain}";
                return (name, email);
            }

            // 3) Seed a random number of applications per job (0..99)
            foreach (var job in createdJobs)
            {
                cancellationToken.ThrowIfCancellationRequested();

                int applicationsPerJob = random.Next(100); // 0 to 99 inclusive
                for (int i = 0; i < applicationsPerJob; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var (name, email) = GenerateCandidate();

                    var app = new JobApplication
                    {
                        JobId         = job.JobId,
                        CandidateName = name,
                        Email         = email
                    };

                    await _jobService.ApplyToJobAsync(app, cancellationToken);
                }
            }
        }

        // Keep synchronous version for backward compatibility
        public void Seed()
        {
            SeedAsync(CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}