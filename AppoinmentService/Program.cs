using AppoinmentService.Event;
using CommonProject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AppoinmentService.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppoinmentServiceContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AppoinmentServiceContext") ?? throw new InvalidOperationException("Connection string 'AppoinmentServiceContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<Producer>(serviceProvider =>
{
    var options = builder.Configuration.GetSection(Settings.RabbitMQ).Get<Settings>();
    return new Producer(options.ConnectionAddress, options.Username, options.Password);
});

builder.Services.Configure<Settings>(
    builder.Configuration.GetSection(Settings.RabbitMQ));

builder.Services.AddHostedService<ConsumerService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppoinmentServiceContext>();
    db.Database.Migrate();
}

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
