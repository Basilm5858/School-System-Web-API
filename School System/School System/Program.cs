using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using School_System.Mapping;
using School_System.Models;
using School_System.Repo.Implementations;
using School_System.Repo.Interfaces;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

builder.Services.AddScoped<SubjectCustomRepo>();
builder.Services.AddScoped<AuthCustomRepo>();
builder.Services.AddScoped<TeacherCustomRepo>();
builder.Services.AddScoped<DepartmentRepo>();
builder.Services.AddScoped<EnrollmentCustomRepo>();
builder.Services.AddScoped<StuedentCustomRepo>();
builder.Services.AddScoped<ClassRoomCustomRepo>();


builder.Services.AddScoped<ISubject ,SubjectCustomRepo>();
builder.Services.AddScoped<IAuth , AuthCustomRepo>();
builder.Services.AddScoped<ITeacher, TeacherCustomRepo>();
builder.Services.AddScoped<IDepartment, DepartmentRepo>();
builder.Services.AddScoped<IEnrollment , EnrollmentCustomRepo>();
builder.Services.AddScoped<IStuedent, StuedentCustomRepo>();
builder.Services.AddScoped<IClassRoom, ClassRoomCustomRepo>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]));
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(opt =>
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
