using Veeho.Domain.Entities;

namespace Veeho.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
