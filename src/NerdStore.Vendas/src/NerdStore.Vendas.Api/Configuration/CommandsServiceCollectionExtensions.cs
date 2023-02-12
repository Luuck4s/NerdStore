using System.Reflection;
using FluentValidation;
using MediatR;
using NerdStore.Core.EventHandler;
using NerdStore.Core.PipelineBehavior;
using NerdStore.Vendas.Domain.CommandHandlers;
using NerdStore.Vendas.Domain.Commands;
using NerdStore.Vendas.Domain.Commands.Validators;

namespace NerdStore.Vendas.Api.Configuration;

public static class CommandsServiceCollectionExtensions
{
    public static void AddCommandsService(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<IMediatRHandler, MediatRHandler>();

        services.AddScoped<IRequestHandler<AddItemOrderCommand, bool>, AddItemOrderCommandHandler>();
        services.AddScoped<IRequestHandler<CreateOrderCommand, bool>, CreateOrderCommandHandler>();

        AddValidators(services);
    }

    private static void AddValidators(IServiceCollection services)
    {
        services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssemblyContaining<AddItemOrderCommandValidator>();
    }
}