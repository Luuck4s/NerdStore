namespace NerdStore.Catalogo.Domain.Exceptions;

public class DebitStockException: Exception
{
    public DebitStockException()
    { }

    public DebitStockException(string message) : base(message)
    { }

    public DebitStockException(string message, Exception innerException) : base(message, innerException)
    { }
}