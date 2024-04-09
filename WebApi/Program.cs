using Data;
using Infrastructure.Interface;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Service;
using Service.DoctorsSevice;
using Service.PateintInfo;
using System.Configuration;
using WebApp.AppCode;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string connectionString = builder.Configuration.GetConnectionString("DBCon");
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var connecString = new ConnectionStrings
{
    DBCon = connectionString,
};
builder.Services.TryAddSingleton(connecString);
builder.Services.AddScoped<IDapper, Data.Dapper>();
builder.Services.AddScoped<IPatientInfo, PatientInfoService>();
builder.Services.AddScoped<IDoctorsSevice, DoctorsSevice>();
builder.Services.AddScoped<IMedicineService, MedicineService>();
builder.Services.AddScoped<ICheckUPService, CheckUPService>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
