using System.Reflection;
using FluentValidation;
using MediatR;
using NerdStore.Core.PipelineBehavior;
using NerdStore.Vendas.Api.Middleware;
using NerdStore.Vendas.Domain.CommandHandlers;
using NerdStore.Vendas.Domain.Commands;
using NerdStore.Vendas.Domain.Commands.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssemblyContaining<AddItemOrderCommandValidator>();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddScoped<IRequestHandler<AddItemOrderCommand, bool>, AddItemOrderCommandHandler>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
