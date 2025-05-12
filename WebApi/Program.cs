using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Presentation.ActionFilters;
using Repositories.EFCore;
using Services.Contract;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config")); eski
LogManager.Setup().LoadConfigurationFromFile("nlog.config");


builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true; //artık içerik pazarlığına açığız..default false
    config.ReturnHttpNotAcceptable = true; //pazarlığa açık istenilen format verilemiyor //406 not acceptable
})
    .AddCustomCsvFormatter()
    .AddXmlDataContractSerializerFormatters()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly) //Presentation Layerdan sonra ekledik.
    .AddNewtonsoftJson();

//builder.Services.AddScoped<ValidationFilterAttribute>(); //IoC Kaydı //ServiceExtensiona taşıdık.

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

////IOC dahilinde Servis kaydı (register) oldu.
//builder.Services.AddDbContext<RepositoryContext>(options => 
//options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureActionFilters();
builder.Services.ConfigureCors();
builder.Services.ConfigureDataShaper();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseHsts();

}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
