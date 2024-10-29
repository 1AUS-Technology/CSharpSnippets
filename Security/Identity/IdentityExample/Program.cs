using IdentityExample.CustomSecurity;
using IdentityExample.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseMigrationsEndPoint();
}
else
{
    _ = app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    _ = app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<CustomAuthentication>();
app.UseMiddleware<RoleMembership>();
app.UseRouting();
app.UseAuthorization();
app.UseMiddleware<CustomAuthorization>();
app.UseMiddleware<ClaimsReporter>();

app.UseEndpoints(endpoints => {
    _ = endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello World");
    });
});
app.MapGet("/secret", SecretEndpoint.Endpoint)
.WithDisplayName("secret");

app.MapRazorPages();

app.Run();