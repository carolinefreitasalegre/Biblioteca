using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Models.Enum;

namespace Models.Models;

[Table("Usuarios")]

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public EPerfilUsuario Role { get; set; }
    
    public DateTime? UltimoLogin { get; set; }

    public EStatusUsuario? Status { get; set; }
    public DateTime CriadoEm { get; set; } =   DateTime.UtcNow;

    [JsonIgnore]
    public ICollection<Livro>? Livros { get; set; } = new List<Livro>();
}