using IdentityExample.CustomSecurity;
using IdentityExample.CustomSecurity.AuthorizationHandlers;
using IdentityExample.CustomSecurity.AuthorizationPolicies;
using IdentityExample.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
builder.Services.AddControllersWithViews();

// Register the authorization handler
builder.Services.AddTransient<IAuthorizationHandler, CustomRequirementHandler>();

// Add the custom authentication handler
builder.Services.AddAuthentication(options =>
{
    
    //options.AddScheme<CutiUsesAuthenticationHandler>(CutiUsesAuthenticationHandler.AuthenticationSchemeName, "Query String Authentication Scheme");
    //options.DefaultAuthenticateScheme = CutiUsesAuthenticationHandler.AuthenticationSchemeName;

    // use the built in cookies authentication scheme
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(cookiesOptions =>
{
    cookiesOptions.LoginPath = "/signin";
    // defines the URL where clients will be redirected for Forbid responses
    cookiesOptions.AccessDeniedPath = "/signin/403";
    cookiesOptions.ExpireTimeSpan = TimeSpan.FromHours(2);
    cookiesOptions.SlidingExpiration = true;
});

builder.Services.AddAuthorization(options => AuthorizationPolicies.AddPolicies(options));
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

//app.UseMiddleware<CustomAuthentication>();
// use the default authentication middleware
//app.UseAuthentication();
app.UseMiddleware<RoleMembership>();

app.UseRouting();

//app.UseAuthorization();
//app.UseMiddleware<CustomAuthorization>();
app.UseMiddleware<AuthorizationReporter>();
app.UseMiddleware<ClaimsReporter>();

app.UseEndpoints(endpoints => { _ = endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World"); }); });
app.MapGet("/secret", SecretEndpoint.Endpoint).WithDisplayName("secret");

//app.Map("/signin", CustomSignInAndSignOut.SignIn);
//app.Map("/signout", CustomSignInAndSignOut.SignOut);

app.MapRazorPages();
app.MapControllers();

app.Run();