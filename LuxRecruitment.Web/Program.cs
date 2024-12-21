using LuxRecruitment.DependencyInjection;
using LuxRecruitment.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Serilog
LoggerConfigurator.ConfigureLogger(builder.Configuration);
builder.Host.UseSerilog();

builder.Services.AddLuxRecruitmentServices();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
