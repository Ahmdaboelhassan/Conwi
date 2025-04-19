using System.Globalization;
using API.Hubs;
using API.Localization;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Logging.ClearProviders();
builder.Host.UseSerilog((context, confg) => confg.ReadFrom.Configuration(context.Configuration));

builder.Services.AddSignalR();

// Configure The Layers
var appConfig = builder.Configuration;
builder.Services
    .AddInfrastructureLayer(appConfig)
    .AddApplicaionLayer(appConfig);


// add locaiztion
builder.Services.AddLocalization();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonLocalizerFactory>();

builder.Services.AddMvc().AddDataAnnotationsLocalization( 
    opt => opt.DataAnnotationLocalizerProvider = (type , factory) => factory.Create(typeof(JsonLocalizerFactory))
);

builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
    var supportCutures = new[]
    {
        new CultureInfo("en-US"), // english united state 
        new CultureInfo("ar-EG"), // arabic egypt
        new CultureInfo("fr"),

    };

    opt.DefaultRequestCulture = new RequestCulture(culture: supportCutures[0], uiCulture: supportCutures[0]);
    opt.SupportedCultures = supportCutures;
    opt.SupportedUICultures = supportCutures;
});


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
var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRequestLocalization(locOptions.Value);

app.UseSerilogRequestLogging();  

// app.UseCors(options => options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseCors("AllowedAudience");

app.MapHub<MessageHub>("/MessageHub");

app.UseAuthentication();
    
app.UseAuthorization();

app.MapControllers();

app.Run();
