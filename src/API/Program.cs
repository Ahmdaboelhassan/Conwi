using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();

var appConfig = builder.Configuration;

builder.Services
    .AddApplicaionLayer(appConfig)
    .AddInfrastructureLayer(appConfig);



builder.Services.AddCors(
    options => options.AddPolicy("AllowedAudience",
        policy =>
        {
            policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:4200");
        }
    )
);



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

// app.UseCors(options => options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseCors("AllowedAudience");

app.UseAuthorization();

app.MapControllers();

app.Run();
