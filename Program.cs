using JobApplicationTracker.Services;
using JobApplicationTracker.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IJobService, JobService>();
builder.Services.AddSingleton<IApplicationService, ApplicationService>();
builder.Services.AddSingleton<SampleDataSeeder>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<SampleDataSeeder>();
    seeder.Seed();
}

app.UseRouting();
app.MapControllers();
app.Run();
