namespace NerdStore.Core.Contracts.Results;

public class GenericCommandResult
{
    public object Data { get; set; }

    public GenericCommandResult()
    { }
    
    public GenericCommandResult(object data)
    {
        Data = data;
    }
}