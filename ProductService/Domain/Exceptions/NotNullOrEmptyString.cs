namespace Domain.Exceptions;
public class NotNullOrEmptyStringException : Exception
{
    public NotNullOrEmptyStringException(string message) : base(message) { }
}