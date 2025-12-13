namespace Biblioteca.Client.ServiceClient.InterfacesClient;

public interface ITokenService
{
    Task<string> GetTokenAsync();
}