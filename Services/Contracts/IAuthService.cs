using Domain.DTO;
using Domain.DTO.Response;
using Microsoft.AspNetCore.Identity.Data;
using Models.Models;

namespace Services.Contracts;

public interface IAuthService
{
    Task<LoginResponse> Login(LoginDto login);
    Task RegisterAsync(Usuario usuario);
    string GerarToken(Usuario user);
}