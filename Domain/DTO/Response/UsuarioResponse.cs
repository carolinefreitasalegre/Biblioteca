using Models.Enum;

namespace Domain.DTO.Response;

public class UsuarioResponse
{
    public string Nome { get; set; }
    public string Email { get; set; }

    public EStatusUsuario Status { get; set; }
    public DateTime CriadoEm { get; set; }
}