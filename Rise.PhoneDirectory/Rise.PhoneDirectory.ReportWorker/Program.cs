using RabbitMQ.Client;
using Rise.PhoneDirectory.Core.Services;
using Rise.PhoneDirectory.ReportWorker;
using Rise.PhoneDirectory.ReportWorker.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton(sp => new ConnectionFactory()
        {
            Uri = new Uri(hostContext.Configuration.GetConnectionString("ReportService")),
            DispatchConsumersAsync = true
        });
        services.AddSingleton<IReporterClientService, ReporterClientService>();

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
