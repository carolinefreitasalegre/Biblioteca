using Models.Enum;

namespace Models.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public EPerfilUsuario Role { get; set; }

    public EStatusUsuario Status { get; set; }
    public DateTime CriadoEm { get; set; }
}