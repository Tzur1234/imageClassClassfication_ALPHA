using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Company.Function;

var builder = FunctionsApplication.CreateBuilder(args);



builder.Services.AddSingleton<IImageDescriptionService, ImageDescriptionService>();// Register services for DI container
builder.Services.AddSingleton<ICosmosDbService, CosmosDbService>(); // Register CosmosDB Service

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
