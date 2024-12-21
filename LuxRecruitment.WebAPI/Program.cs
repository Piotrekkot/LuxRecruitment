using LuxRecruitment.DependencyInjection;
using LuxRecruitment.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

LoggerConfigurator.ConfigureLogger(builder.Configuration);
builder.Host.UseSerilog();

builder.Services.AddLuxRecruitmentServices();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
