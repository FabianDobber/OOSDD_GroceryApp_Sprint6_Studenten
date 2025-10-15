
using Grocery.Core.Models;

namespace Grocery.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Client? CurrentUser { get; }
        Client? Login(string email, string password);
    }
}
