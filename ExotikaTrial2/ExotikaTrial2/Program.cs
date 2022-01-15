using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ExotikaTrial2.Data;
using Microsoft.AspNetCore.Identity;
using ExotikaTrial2.DataAccess.Repository.IRepository;
using ExotikaTrial2.DataAccess.Repository;
using Stripe;
using ExotikaTrial2.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddDbContext<ExotikaTrial2Context>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("ExotikaTrial2ContextSQLdb")
));

builder.Services.Configure<StripeSettings>(
    builder.Configuration.GetSection("Stripe")
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ExotikaTrial2Context>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
	pattern: "{area=Base}/{controller=BasePages}/{action=Index}/{id?}");

app.Run();
