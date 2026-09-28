using Microsoft.EntityFrameworkCore;
using School_System.Mapping;
using School_System.Models;
using School_System.Repo.Implementations;
using School_System.Repo.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

builder.Services.AddScoped<IGenericRepo<Department>, GenericRepo<Department>>();
builder.Services.AddScoped<IGenericRepo<ClassRoom>, GenericRepo<ClassRoom>>();
builder.Services.AddScoped<IGenericRepo<Student>, GenericRepo<Student>>();
builder.Services.AddScoped<IGenericRepo<Enrollment>, GenericRepo<Enrollment>>();
builder.Services.AddScoped<IGenericRepo<Teacher>, GenericRepo<Teacher>>();
builder.Services.AddScoped<DepartmentRepo>();
builder.Services.AddScoped<EnrollmentCustomerRepo>();
builder.Services.AddScoped<StuedentCustomRepo>();

builder.Services.AddDbContext<My_AppContext>(options => 
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("conn")
        ));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

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
