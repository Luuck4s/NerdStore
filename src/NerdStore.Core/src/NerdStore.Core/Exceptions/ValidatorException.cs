namespace NerdStore.Core.Exceptions;

public class ValidatorException: Exception
{
    public Dictionary<string, string[]> Errors { get; set; }

    public ValidatorException(Dictionary<string, string[]> errors): base(errors.ToString())
    {
        Errors = errors;
    }
}