using Domain.Abstractions.Interfaces;
using Infrastructure.DataAccess;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

/* 
 Todo:
    At the end, check performance and sql queries
    Create exceptinos
    create exception handling middleware (created, needs to be a bit better)
    After group creation, the department cannot be changed (trigger and group request for editing) done but I could also create a trigger
    Defend against department updates as departments are read only maybe add trigger to deny database editing (as endpoint editing won't be possible)
    HTTP Status codes (check once more)
    Create unit tests (check what makes sense to test)
    Defend against nulls and such on all repositories
    Embed sql trigger script in migration script
 */
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("SQLServer")!;
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("StudentTestPontis"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.TryAddScoped<IStudentRepository, StudentRepository>();
builder.Services.TryAddScoped<IGroupRepository, GroupRepository>();
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

app.UseExceptionHandler("/Error");
app.MapControllers();

app.Run();
