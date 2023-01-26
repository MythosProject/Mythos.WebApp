using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Mythos.Common.Users;
using Mythos.WebApp.Server.Data;
using Mythos.WebApp.Server.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<MythosDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

if (false)
{
    builder.Services.AddDefaultIdentity<MythosUser>(options => options.SignIn.RequireConfirmedAccount = true);
//    .AddEntityFrameworkStores<MythosDbContext>();
}

builder.Services
    .AddAuthentication(o =>
    {
        o.DefaultScheme = IdentityConstants.ApplicationScheme;
        o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    //.AddIdentityServerJwt()
    .AddIdentityCookies(o => { });
    ;

builder.Services.AddIdentityCore<MythosUser>(options =>
{
    options.Stores.MaxLengthForKeys = 128;
    options.SignIn.RequireConfirmedAccount = true;
})
.AddDefaultUI()
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<MythosDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<MythosUser, MythosDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
