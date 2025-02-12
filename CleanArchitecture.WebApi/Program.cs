using CleanArchitecture.Application.Behaviors;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.Persistance.Services;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICarService, CarService>();

builder.Services.AddAutoMapper(typeof(CleanArchitecture.Persistance.AssemblyReferance).Assembly);



var connectionString = builder.Configuration.GetConnectionString("PostgreSQLServer");
// aþaðýdaki kod parçasý sayesinde connection string'i appsettings.json dosasýndan alabiliriz.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddControllers().AddApplicationPart(typeof(
    CleanArchitecture.Presentation.AssemblyReferance).Assembly);
// bu kod parçasý sayesinde CleanArchitecture.Presentation
// katmaný içerisindeki tüm class'larý program.cs içinde yapacaðýmýz
// assembly referansý ile çaðýrabiliriz.


builder.Services.AddMediatR(cfr => 
cfr.RegisterServicesFromAssembly(
    typeof(CleanArchitecture.Application.AssemblyReferance).Assembly));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(CleanArchitecture.Application.AssemblyReferance).Assembly);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
