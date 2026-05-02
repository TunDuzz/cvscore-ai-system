using CVScore.API.Extensions;
using Microsoft.EntityFrameworkCore;
using CVScore.Application;
using CVScore.Application.Abstractions.Persistence;
using CVScore.Application.Common.Behaviors;
using CVScore.Infrastructure.Data;
using CVScore.Infrastructure.Extensions;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssembly(typeof(CVScore.Application.AssemblyReference).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Configure MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
builder.Services.AddAiServices(builder.Configuration);

// Configure MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CVScore.Application.AssemblyReference).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
    await ApplicationDbSeeder.SeedAsync(dbContext);
}

app.UseGlobalExceptionHandling();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
