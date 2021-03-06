using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Rise.PhoneDirectory.Core.Tools;
using Rise.PhoneDirectory.Repository;
using Rise.PhoneDirectory.Service.Mappings;
using Rise.PhoneDirectory.Service.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddDbContext<PhoneDirectoryDbContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("NpgSql"));
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
});
builder.Services.AddSingleton(sp => new ConnectionFactory()
{
    Uri = new Uri(builder.Configuration.GetConnectionString("ReportService")),
    DispatchConsumersAsync = true
});


builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacBusinessModule());
});

var app = builder.Build();
ServiceTool.Set(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
