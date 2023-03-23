global using Smakoowa_Api.Models.Enums;
global using Smakoowa_Api.Models.Identity;
global using Smakoowa_Api.Models.Interfaces;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.AspNetCore.Identity;
global using Smakoowa_Api.Models.DatabaseModels;
global using System.Linq.Expressions;
global using Microsoft.EntityFrameworkCore;
global using Smakoowa_Api.Data;
global using ModernPantryBackend.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SmakoowaApiDBConnection")));

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
