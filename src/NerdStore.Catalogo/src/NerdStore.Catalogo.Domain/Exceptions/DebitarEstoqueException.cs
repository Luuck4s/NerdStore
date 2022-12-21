namespace NerdStore.Catalogo.Domain.Exceptions;

public class DebitarEstoqueException: Exception
{
    public DebitarEstoqueException()
    { }

    public DebitarEstoqueException(string message) : base(message)
    { }

    public DebitarEstoqueException(string message, Exception innerException) : base(message, innerException)
    { }
}