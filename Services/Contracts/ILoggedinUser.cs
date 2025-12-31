namespace Services.Contracts;

public interface ILoggedinUser
{
    int UserId { get;  }
    string? Email { get;  }
    bool IsAuthenticated { get;  }
}