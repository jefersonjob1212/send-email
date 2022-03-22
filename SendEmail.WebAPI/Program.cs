using SendEmail.Domain.Configurations;
using SendEmail.Services;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationModule();
builder.Services.AddControllers();
var emailConfiguration = new EmailConfiguration();
builder.Configuration.GetSection("EmailConfiguration").Bind(emailConfiguration);
builder.Services.AddSingleton(emailConfiguration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
