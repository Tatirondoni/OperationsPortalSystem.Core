using Microsoft.EntityFrameworkCore;
using OperationsPortal.Api.ExceptionHandlers;
using OperationsPortal.Application.Interfaces.Repositories;
using OperationsPortal.Application.Services.Categories;
using OperationsPortal.Infrastructure.Data;
using OperationsPortal.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddProblemDetails();

builder.Services.AddExceptionHandler<
    CategoryNameAlreadyExistsExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString(
    "DefaultConnection"
);

builder.Services.AddDbContext<OperationsPortalDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();