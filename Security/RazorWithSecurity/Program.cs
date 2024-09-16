using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RazorWithSecurity.Data;

namespace RazorWithSecurity;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();


        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddRazorPages();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("EditResourcePolicy",
                policy => policy.Requirements.Add(new SameAuthorRequirement()));
        });
        // Add imperative authorization handler
        builder.Services.AddSingleton<IAuthorizationHandler, AuthorAuthorizationHandler>();
        var app = builder.Build();



        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}

public class SameAuthorRequirement : IAuthorizationRequirement
{
}

public class AuthorAuthorizationHandler : AuthorizationHandler<SameAuthorRequirement, Document>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameAuthorRequirement requirement, Document resource)
    {
        if (context.User.Identity.Name == resource.Author)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public class CrudOnDocumentAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Document>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Document resource)
    {
        if (context.User.Identity?.Name == resource.Author &&
            requirement.Name == Operations.Read.Name)
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}

public class Document
{
    public string? Author { get; set; }
}

public static class Operations
{
    public static OperationAuthorizationRequirement Create = new() { Name = nameof(Create) };

    public static OperationAuthorizationRequirement Read = new() { Name = nameof(Read) };

    public static OperationAuthorizationRequirement Update = new() { Name = nameof(Update) };

    public static OperationAuthorizationRequirement Delete = new() { Name = nameof(Delete) };
}