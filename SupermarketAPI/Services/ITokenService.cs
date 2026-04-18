using SupermarketAPI.Models;

namespace SupermarketAPI.Services;

public interface ITokenService
{
    string CreateToken(User user);
}
