namespace DDD.Infrastructure.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken();
}