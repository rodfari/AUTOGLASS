namespace Application.Base;
public abstract class Response
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public Domain.Enums.ErrorsCode ErrorCode { get; set; }
}