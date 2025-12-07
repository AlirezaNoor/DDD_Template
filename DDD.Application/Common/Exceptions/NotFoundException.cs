namespace DDD.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"موجودیت \"{name}\" با شناسه ({key}) یافت نشد")
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }
}