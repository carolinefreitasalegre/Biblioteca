using Domain.DTO;
using Domain.DTO.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using Models.Models;
using Repositories.Repositories.Contracts;
using Services.Contracts;

namespace Services.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _repository;
    
    public AuthService(IUsuarioRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<LoginResponse> Login(LoginDto login)
    {   
  
        var email =  login.Email;
        var senha = login.Senha;
        
        var usuario = await _repository.ObterPorEmail(email);
        if (usuario == null)
            return null;
        if (usuario.Email != login.Email && usuario.Senha != login.Senha)
            return null;
        //gerar o token

        return GerarToken(usuario);
    }

    public Task RegisterAsync(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public string GerarToken(Usuario user)
    {
        
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Secret"]);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("name", user.Nome),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(6),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}