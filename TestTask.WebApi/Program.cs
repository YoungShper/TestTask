using Microsoft.Extensions.Options;
using Serilog;
using TestTask.Core;
using TestTask.Core.Interfaces;
using TestTask.DataAccess;
using TestTask.DataAccess.Interfaces;
using TestTask.DataAccess.Repositories;
using TestTask.Logic.Interfaces;
using TestTask.Logic.Services;
using TestTask.WebApi.Middlewares;

namespace TestTask.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
            .WriteTo.File("logs.txt").CreateLogger();
        Log.Information("Starting up");
        
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSerilog();

        // Add services to the container.
        builder.Configuration.AddJsonFile("appsettings.json");
        builder.Services.AddTransient<IDataReader<CenterPoint>, CenterPointReader>();
        builder.Services.AddTransient<IDataReader<CustomPolygon>, FieldReader>();
        builder.Services.AddTransient<ICenterRepository, CenterRepository>();
        builder.Services.AddTransient<IFieldRepository, FieldRepository>();
        builder.Services.Configure<FullDataServiceOptions>(builder.Configuration.GetSection(IFullDataServiceOptions.FullDataService));
        builder.Services.AddSingleton<IFullDataServiceOptions>(x => x.GetRequiredService<IOptions<FullDataServiceOptions>>().Value);
        builder.Services.AddTransient<IFullDataService, FullDataService>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseHttpsRedirection();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}