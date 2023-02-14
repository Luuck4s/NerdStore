using MediatR;
using NerdStore.Core.EventHandler;
using NerdStore.Core.Notification;
using NerdStore.Vendas.Domain.Commands;
using NerdStore.Vendas.Domain.Exceptions;
using NerdStore.Vendas.Domain.Repository;

namespace NerdStore.Vendas.Domain.CommandHandlers;

public class AddVoucherCommandHandler: IRequestHandler<AddVoucherCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IVoucherRepository _voucherRepository;
    private readonly IMediatRHandler _mediatRHandler;

    public AddVoucherCommandHandler(IOrderRepository orderRepository, IMediatRHandler mediatRHandler, IVoucherRepository voucherRepository)
    {
        _orderRepository = orderRepository;
        _mediatRHandler = mediatRHandler;
        _voucherRepository = voucherRepository;
    }

    public async Task<bool> Handle(AddVoucherCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrder(request.OrderId);

        if (order is null)
        {
            await _mediatRHandler.PublishNotification(new DomainNotification(request.MessageType, "Order not found", request.AggregateId));
            throw new OrderNotFoundException("Not found Order");
        }

        var voucher = await _voucherRepository.GetVoucherByCode(request.VoucherCode);

        if (voucher is null)
        {
            await _mediatRHandler.PublishNotification(new DomainNotification(request.MessageType, "Voucher not found", request.AggregateId));
            return false;
        }

        if (voucher.IsAppliable() is false)
        {
            await _mediatRHandler.PublishNotification(new DomainNotification(request.MessageType, "Voucher not valid", request.AggregateId));
            return false;
        }
        
        order.ApplyDiscount(voucher);
        
        _orderRepository.Update(order);

        return await _orderRepository.UnitOfWork.Commit();
    }
}