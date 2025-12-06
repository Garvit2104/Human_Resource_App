using Human_Resource_App.Data;
using Human_Resource_App.BLL.GradesService;
using Human_Resource_App.BLL.UserServices;
using Human_Resource_App.DAL.GradesRepository;
using Human_Resource_App.DAL.UsersRepository;
using Human_Resource_App.DAL.GradesHistoryRepository;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<HRDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("HR_db")));

builder.Services.AddScoped<IUserRepo, UserRepositoryClass>();
builder.Services.AddScoped<IGradesRepo, GradesRepositoryClass>();
builder.Services.AddScoped<IGradesHistory, GradesHistoryRepositoryClass>();

builder.Services.AddScoped<IUserService, UserServiceClass>();
builder.Services.AddScoped<IGradesService, GradesServiceClass>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // React app URL
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                 .AllowCredentials();
        });
});


var app = builder.Build();

app.UseCors("AllowReactApp");

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
