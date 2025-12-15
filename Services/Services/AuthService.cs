    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Domain.DTO;
    using Domain.DTO.Response;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Models.Models;
    using Repositories.Repositories.Contracts;
    using Services.Contracts;

    namespace Services.Services;

    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IConfiguration _config;
        
        public AuthService(IUsuarioRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config =  config;
        }
        
        public async Task<LoginResponse?> Login(LoginDto login)
        {
            try
            {
                var user = await _repository.ObterPorEmail(login.Email);
            
                if(user == null)
                    return null;
            
                if(login.Senha != user.Senha)
                    return null;
                var token = GerarToken(user);

                return new LoginResponse
                {
                    
                    Token = token, Role = user.Role.ToString()
                    
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
            

        }

        public Task RegisterAsync(Usuario usuario)
        {
            return null;
        }

        public string GerarToken(Usuario user)
        {
            
                var key = Encoding.UTF8.GetBytes(_config["Jwt:Secret"]);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Name, user.Nome),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
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