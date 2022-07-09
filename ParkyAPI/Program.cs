using Microsoft.EntityFrameworkCore;
using ParkyAPI;
using ParkyAPI.ParkyMapper;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<INationalParkRepository, NationalParkRepository>();
builder.Services.AddScoped<ITrailsRepository, TrailsRepository>();
builder.Services.AddAutoMapper(typeof(ParkyMappings));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.IncludeXmlComments("ParkyAPI.xml");
        options.SwaggerDoc("NationalPark",
            new Microsoft.OpenApi.Models.OpenApiInfo()
            {
                Title = "NationalPark Api"
            });
        options.SwaggerDoc("Trails",
          new Microsoft.OpenApi.Models.OpenApiInfo()
          {
              Title = "Trails Api"
          });

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/NationalPark/swagger.json", "NationalPark Api");
        options.SwaggerEndpoint("/swagger/Trails/swagger.json", "Trails Api");
        //options.RoutePrefix = ""; 
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
