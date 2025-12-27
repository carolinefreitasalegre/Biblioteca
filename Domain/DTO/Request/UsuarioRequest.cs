using Models.Enum;

namespace Domain.DTO;

public class UsuarioRequest
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string? Senha { get; set; }
    public EPerfilUsuario Role { get; set; } = EPerfilUsuario.Usuario;

    public EStatusUsuario Status { get; set; } =  EStatusUsuario.Ativo;
    public DateTime CriadoEm { get; set; } =  DateTime.UtcNow;
}