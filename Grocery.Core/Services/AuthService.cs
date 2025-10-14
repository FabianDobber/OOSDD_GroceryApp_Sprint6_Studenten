using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

public class AuthService : IAuthService
{
    private readonly IClientService _clientService;
    public Client? CurrentUser { get; private set; } 

    public AuthService(IClientService clientService)
    {
        _clientService = clientService;
    }

    public Client? Login(string email, string password)
    {
        Client? client = _clientService.Get(email);
        if (client == null)
        {
            CurrentUser = null; 
            return null;
        }

        if (PasswordHelper.VerifyPassword(password, client.Password))
        {
            CurrentUser = client; 
            return client;
        }

        CurrentUser = null;
        return null;
    }
}