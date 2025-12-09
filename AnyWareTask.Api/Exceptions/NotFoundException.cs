namespace AnyWareTask.Api.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string resourceType, string identifier) : base($"{resourceType} with identifier {identifier} not found") { }
    public NotFoundException() { }
}
