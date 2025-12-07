namespace DDD.Application.Common.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException()
        : base("شما دسترسی به این منبع را ندارید")
    {
    }
}