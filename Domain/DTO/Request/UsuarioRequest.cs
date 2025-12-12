using Models.Enum;

namespace Domain.DTO;

public class UsuarioRequest
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public EPerfilUsuario Role { get; set; }

    public EStatusUsuario Status { get; set; }
    public DateTime CriadoEm { get; set; }
}