using API.Extinctions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var appConfig = builder.Configuration;

builder.Services.AddApplicaionServices(appConfig);

builder.Services.AddDataBase(appConfig);

builder.Services.AddIdentityService(appConfig);


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

// app.UseCors(options => options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseCors("AllowedAudience");

app.UseAuthorization();

app.MapControllers();

app.Run();
