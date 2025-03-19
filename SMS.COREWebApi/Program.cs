using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SMS.COREWebApi.Controllers;
using SMS.COREWebApi.Repo;
using SMS.COREWebApi.Repository;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
//Dapper

builder.Services.AddSingleton<DapperDbContext>();


//Inteface and Repository

builder.Services.AddScoped<IStudentRepository, SMS.COREWebApi.Repo.StudentRepository>();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<StudentController>();
TypeAdapterConfig.GlobalSettings.Default
    .IgnoreNullValues(true);

// Register MediatR
IServiceCollection serviceCollection = builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));


// Register FluentValidation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Register AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Register Mapster
builder.Services.AddMapster();
// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<SMSEntity>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure session
builder.Services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout
    options.Cookie.HttpOnly = true; // Make the cookie accessible only by the server
    options.Cookie.IsEssential = true; // Make the session cookie essential for application functionality
});

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shaik Abdul Azeez", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shaik Abdul Azeez V1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

// Configure routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();