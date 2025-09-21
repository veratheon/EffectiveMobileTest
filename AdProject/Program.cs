using AdProject.Collections;
using AdProject.Entities;
using AdProject.Repositories;
using AdProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITree<AdPlatform>, Tree<AdPlatform>>();
builder.Services.AddTransient<IAdPlatformRepository, AdPlatformRepository>();
builder.Services.AddTransient<IAdPlatformService, AdPlatformService>();

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

