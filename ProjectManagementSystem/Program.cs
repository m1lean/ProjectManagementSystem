using Microsoft.EntityFrameworkCore; // For DbContextOptionsBuilder and UseInMemoryDatabase
using ProjectManagementSystem.Data; // For ApplicationDbContext
using ProjectManagementSystem.Models; // For User, Project, ProjectUser
using ProjectManagementSystem.Services; // For IProjectService, ProjectService, IProjectTaskService, ProjectTaskService, IUserService, UserService

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("ProjectManagementDb"));

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectTaskService, ProjectTaskService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();

    // Seed data
    if (!context.Users.Any())
    {
        context.Users.AddRange(
            new User { Name = "Alice", Email = "alice@example.com" },
            new User { Name = "Bob", Email = "bob@example.com" }
        );
        await context.SaveChangesAsync();
    }

    if (!context.Projects.Any())
    {
        var project = new Project { Name = "Sample Project", Description = "Test", Status = "Open" };
        context.Projects.Add(project);
        await context.SaveChangesAsync();

        // Add participants
        var alice = await context.Users.FirstAsync(u => u.Name == "Alice");
        context.ProjectUsers.Add(new ProjectUser { ProjectId = project.Id, UserId = alice.Id });
        await context.SaveChangesAsync();
    }
}

app.Run();