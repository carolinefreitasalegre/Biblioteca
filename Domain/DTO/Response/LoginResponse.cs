namespace Domain.DTO.Response;

public class LoginResponse
{
    public string Token { get; set; }
    public string Nome { get; set; }
    public string Role { get; set; }
    
    public DateTime? UltimoLogin { get; set; }

}