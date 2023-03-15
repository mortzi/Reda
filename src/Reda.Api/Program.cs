using FluentValidation;

using MediatR;

using Reda.Api;
using Reda.Api.Behaviors;
using Reda.Api.Validators;
using Reda.Application.Cache;
using Reda.Application.Services;
using Reda.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining(typeof(GetOrderRequestValidator));

builder.Services.Configure<CacheOptions>(builder.Configuration.GetSection(nameof(CacheOptions)));
builder.Services.AddMemoryCache();

builder.Services.AddMediatR(typeof(SubmitOrderHandler).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseExceptionHandler();
    
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
