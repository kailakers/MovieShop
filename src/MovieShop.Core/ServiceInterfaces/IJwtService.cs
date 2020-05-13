using MovieShop.Core.Entities;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}