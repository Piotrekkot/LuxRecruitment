using LuxRecruitment.Core.Helpers;
using LuxRecruitment.DependencyInjection;
using LuxRecruitment.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

LoggerConfigurator.ConfigureLogger(builder.Configuration);
builder.Host.UseSerilog();

builder.Services.AddLuxRecruitmentServices();
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Ustawienie JSON, aby zwraca³ wartoœci `enum` jako tekst
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
})
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
}); ;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LuxRecruitment API",
        Version = "v1",
        Description = "API do pobierania kursów walut z NBP.",
    });
    //helper do lepszego wyœwietlania enum w swaggerze
    c.SchemaFilter<EnumSchemaFilter>();
    c.IncludeXmlComments(xmlPath);
});

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
