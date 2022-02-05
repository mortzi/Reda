using System.Reflection;

using FluentValidation.AspNetCore;

using MediatR;

using Reda.Api;
using Reda.Api.Extensions;
using Reda.Application.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddControllers(options => options.Filters.Add<ExceptionFilter>())
    .AddFluentValidation(f => f.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddMediatR(typeof(SubmitOrderHandler).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();

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
