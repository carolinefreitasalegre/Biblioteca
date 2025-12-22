using Models.Enum;

namespace Domain.DTO.Response;

public class UsuarioResponse
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }

    public EPerfilUsuario Role { get; set; }
    public DateTime? UltimoLogin { get; set; }

    public EStatusUsuario Status { get; set; }
    public DateTime CriadoEm { get; set; }
}