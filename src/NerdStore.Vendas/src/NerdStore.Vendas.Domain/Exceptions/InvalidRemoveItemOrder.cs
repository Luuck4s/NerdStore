namespace NerdStore.Vendas.Domain.Exceptions;

public class InvalidRemoveItemOrder : Exception
{
    public InvalidRemoveItemOrder(string message): base(message)
    { }
}