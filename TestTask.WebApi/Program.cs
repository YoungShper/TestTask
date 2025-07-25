using TestTask.Core;
using TestTask.Core.Interfaces;
using TestTask.DataAccess;
using TestTask.DataAccess.Interfaces;
using TestTask.DataAccess.Repositories;
using TestTask.Logic.Interfaces;
using TestTask.Logic.Services;

namespace TestTask.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddTransient<IDataReader<CenterPoint>, CenterPointReader>();
        builder.Services.AddTransient<IDataReader<CustomPolygon>, FieldReader>();
        builder.Services.AddTransient<ICenterRepository, CenterRepository>();
        builder.Services.AddTransient<IFieldRepository, FieldRepository>();
        builder.Services.AddTransient<IFullDataService, FullDataService>(provider =>
        {
            var fieldRepo = provider.GetRequiredService<IFieldRepository>();
            var centerRepo = provider.GetRequiredService<ICenterRepository>();

            var fieldsPath = "../TestTask.DataAccess/Data/fields.kml";  
            var centersPath = "../TestTask.DataAccess/Data/centroids.kml";

            return new FullDataService(centerRepo, fieldRepo, fieldsPath, centersPath);
        });

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

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}