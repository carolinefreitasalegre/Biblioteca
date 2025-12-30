using System.Text.Json.Serialization;
using Models.Enum;

namespace Domain.DTO.Response;

public class UsuarioResponse
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public EPerfilUsuario Role { get; set; }
    public DateTime? UltimoLogin { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public EStatusUsuario Status { get; set; }
    public DateTime CriadoEm { get; set; }
}