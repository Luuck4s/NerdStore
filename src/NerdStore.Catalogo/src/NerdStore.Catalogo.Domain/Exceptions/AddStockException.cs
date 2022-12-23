namespace NerdStore.Catalogo.Domain.Exceptions;

public class AddStockException: Exception
{
    public AddStockException()
    { }

    public AddStockException(string message) : base(message)
    { }

    public AddStockException(string message, Exception innerException) : base(message, innerException)
    { }
}