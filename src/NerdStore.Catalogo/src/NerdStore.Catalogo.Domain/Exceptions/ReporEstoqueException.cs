namespace NerdStore.Catalogo.Domain.Exceptions;

public class ReporEstoqueException: Exception
{
    public ReporEstoqueException()
    { }

    public ReporEstoqueException(string message) : base(message)
    { }

    public ReporEstoqueException(string message, Exception innerException) : base(message, innerException)
    { }
}