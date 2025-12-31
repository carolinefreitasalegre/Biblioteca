using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Services.Contracts;

namespace Services.Services;

public class LoggedinUser : ILoggedinUser
{
    private readonly IHttpContextAccessor _accessor;

    public LoggedinUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public bool IsAuthenticated => _accessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public int UserId
    {
        get
        {
            var id = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return int.Parse(id!);
        }
    }

    public string? Email => _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
}