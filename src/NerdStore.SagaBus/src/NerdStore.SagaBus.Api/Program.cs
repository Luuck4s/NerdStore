using NerdStore.SagaBus.Api.Configuration;
using NerdStore.SagaBus.Core.Messages.IntegrationEvents;
using Rebus.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureServices();

var app = builder.Build();

((IApplicationBuilder)app).ApplicationServices.UseRebus(c =>
{
    c.Subscribe<OrderStarted>().Wait();
    c.Subscribe<PaymentSuccessful>().Wait();
    c.Subscribe<OrderEnded>().Wait();
    c.Subscribe<PaymentRefused>().Wait();
    c.Subscribe<OrderCanceled>().Wait();
});

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
