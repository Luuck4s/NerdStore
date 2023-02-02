using System.Reflection;
using FluentValidation;
using MediatR;
using NerdStore.Core.PipelineBehavior;
using NerdStore.Vendas.Domain.CommandHandlers;
using NerdStore.Vendas.Domain.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(ValidationBehaviour<,>), typeof(ValidationBehaviour<,>));

builder.Services.AddScoped<IRequestHandler<AddItemOrderCommand, bool>, AddItemOrderCommandHandler>();

var app = builder.Build();

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
