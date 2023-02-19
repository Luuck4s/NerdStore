using FluentValidation;
using MediatR;
using NerdStore.Core.PipelineBehavior;
using NerdStore.Vendas.Domain.CommandHandlers;
using NerdStore.Vendas.Domain.Commands;
using NerdStore.Vendas.Domain.Commands.Validators;

namespace NerdStore.Api.Configuration;

public static class CommandsServiceCollectionExtensions
{
    public static void AddCommandsService(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CreateOrderCommand, bool>, CreateOrderCommandHandler>();
        services.AddScoped<IRequestHandler<StartOrderCommand, bool>, StartOrderCommandHandler>();
        services.AddScoped<IRequestHandler<CancelOrderCommand, bool>, CancelOrderCommandHandler>();

        services.AddScoped<IRequestHandler<AddItemOrderCommand, bool>, AddItemOrderCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateItemOrderCommand, bool>, UpdateItemOrderCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteItemOrderCommand, bool>, DeleteItemOrderCommandHandler>();

        services.AddScoped<IRequestHandler<AddVoucherCommand, bool>, AddVoucherCommandHandler>();
        
        services.AddScoped<IRequestHandler<EndOrderCommand, bool>, EndOrderCommandCommandHandler>();
        services.AddScoped<IRequestHandler<CancelOrderReverseStockCommand, bool>, CancelOrderReverseStockCommandHandler>();

        AddValidators(services);
    }

    private static void AddValidators(IServiceCollection services)
    {
        services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssemblyContaining<AddItemOrderCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateItemOrderCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<DeleteItemOrderCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<AddVoucherCommandValidator>();
    }
}