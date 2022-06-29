using Microsoft.EntityFrameworkCore;
using Rise.PhoneDirectory.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PhoneDirectoryDbContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("NpgSql"));
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
