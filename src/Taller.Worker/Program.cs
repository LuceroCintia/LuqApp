using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Taller.Infrastructure.DependencyInjection;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHostedService<EmailApprovalWorker>();

var host = builder.Build();
await host.RunAsync();
