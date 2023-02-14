using System.Text.Json;
using MediatR;
using NerdStore.Core.EventHandler;
using NerdStore.Vendas.Api.Configuration;
using NerdStore.Vendas.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddScoped<IMediatRHandler, MediatRHandler>();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddCommandsService();
builder.Services.AddNotificationsService();
builder.Services.AddQueries();
builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddJsonConverter();

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