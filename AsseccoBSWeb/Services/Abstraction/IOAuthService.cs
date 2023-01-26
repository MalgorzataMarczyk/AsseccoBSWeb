using AsseccoBSWeb.Models;

namespace AsseccoBSWeb.Services.Abstraction
{
    public interface IOAuthService
    {
        Token GetAccessToken(string username, string password);
    }
}
