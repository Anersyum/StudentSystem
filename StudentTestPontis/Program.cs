using Application.Abstractions.Services.Group;
using Application.Abstractions.Services.Repositories;
using Application.Services.Group;
using Infrastructure.DataAccess;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("SQLServer")!;
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("StudentTestPontis"));
});

builder.Services.AddControllers()
    .AddApplicationPart(PresentationAssembly.GetAssembly());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.TryAddScoped<IStudentRepository, StudentRepository>();
builder.Services.TryAddScoped<IGroupRepository, GroupRepository>();
builder.Services.TryAddScoped<IGroupService, GroupService>();
builder.Services.TryAddScoped<IDepartmentRepository, DepartmentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

string errorUrl = "/Error" ;
app.UseExceptionHandler(errorUrl);

app.MapControllers();

app.Run();

public partial class Program { }